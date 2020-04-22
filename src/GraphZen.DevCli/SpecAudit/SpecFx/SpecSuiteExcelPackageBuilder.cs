﻿// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace GraphZen.SpecAudit.SpecFx
{
    public static class SpecSuiteExcelPackageBuilder
    {
        private static readonly bool Fail = true;

        private static IReadOnlyDictionary<(SpecCoverageStatus status, SpecPriority? prioroity), (Color background,
            Color text)> Colors { get; } =
            new Dictionary<(SpecCoverageStatus status, SpecPriority? prioroity), (Color background, Color text)>
            {
                {(SpecCoverageStatus.Missing, SpecPriority.High), (Color.Red, Color.White)},
                {(SpecCoverageStatus.Missing, SpecPriority.Medium), (Color.IndianRed, Color.White)},
                {(SpecCoverageStatus.Missing, SpecPriority.Low), (Color.PaleVioletRed, Color.White)},
                {(SpecCoverageStatus.Missing, null), (Color.PaleVioletRed, Color.White)},

                {(SpecCoverageStatus.Implemented, SpecPriority.High), (Color.Green, Color.White)},
                {(SpecCoverageStatus.Implemented, SpecPriority.Medium), (Color.MediumSeaGreen, Color.White)},
                {(SpecCoverageStatus.Implemented, SpecPriority.Low), (Color.LightSeaGreen, Color.White)},
                {(SpecCoverageStatus.Implemented, null), (Color.LightSeaGreen, Color.White)},

                {(SpecCoverageStatus.Skipped, SpecPriority.High), (Color.Yellow, Color.Black)},
                {(SpecCoverageStatus.Skipped, SpecPriority.Medium), (Color.LightGoldenrodYellow, Color.Black)},
                {(SpecCoverageStatus.Skipped, SpecPriority.Low), (Color.LightYellow, Color.Black)},
                {(SpecCoverageStatus.Skipped, null), (Color.LightYellow, Color.Black)},

                {(SpecCoverageStatus.NotNeeded, null), (Color.White, Color.Black)}
            };

        private static void AddDiagnosticSheets(ExcelPackage p, SpecSuite suite)
        {
            var modelWs = p.Workbook.Worksheets.Add("Model");
            var pathHeader = modelWs.Cells[1, 1];
            pathHeader.Value = "Path";
            for (var i = 0; i < suite.Subjects.Count; i++)
            {
                var subj = suite.Subjects[i];
                var rowNumber = i + 2;
                var pathCell = modelWs.Cells[rowNumber, 1];
                pathCell.Value = subj.Path;
            }

            modelWs.Cells.AutoFitColumns();

            var testWs = p.Workbook.Worksheets.Add("Tests");
            var classCol = 1;
            var classHeader = testWs.Cells[1, classCol];
            classHeader.Value = "Class";
            var methodCol = 2;
            var methodHeader = testWs.Cells[1, methodCol];
            methodHeader.Value = "Method";
            var testPathCol = 3;
            var testPathHeader = testWs.Cells[1, testPathCol];
            testPathHeader.Value = "Path";
            var specCol = 4;
            var specHeader = testWs.Cells[1, specCol];
            specHeader.Value = "Spec";
            var subjectInModelCol = 5;
            var subjectInModelHeader = testWs.Cells[1, subjectInModelCol];
            subjectInModelHeader.Value = "Model Subject";
            var specInModelCol = 6;
            var specModelHeader = testWs.Cells[1, specInModelCol];
            specModelHeader.Value = "Model Spec";

            for (var i = 0; i < suite.Tests.Count; i++)
            {
                var testInfo = suite.Tests[i];
                var row = i + 2;
                var classCell = testWs.Cells[row, classCol];
                classCell.Value = testInfo.TestMethod.DeclaringType?.Name;
                var methodCell = testWs.Cells[row, methodCol];
                methodCell.Value = testInfo.TestMethod.Name;
                var testPathCell = testWs.Cells[row, testPathCol];
                testPathCell.Value = testInfo.SubjectPath;
                var specCell = testWs.Cells[row, specCol];
                specCell.Value = testInfo.SpecId;
                var modelSubject = suite.SubjectsByPath.TryGetValue(testInfo.SubjectPath, out var subj) ? subj : null;
                var subjectInModelCell = testWs.Cells[row, subjectInModelCol];
                var subjectInModel = modelSubject != null;
                subjectInModelCell.Value = subjectInModel ? "✔" : "❌";
                if (!subjectInModel) subjectInModelCell.Style.Font.Color.SetColor(Color.Crimson);
                var specInModelCell = testWs.Cells[row, specInModelCol];
                var specInModel = modelSubject != null && modelSubject.Specs.ContainsKey(testInfo.SpecId);
                specInModelCell.Value = specInModel ? "✔" : "❌";
                if (!specInModel) specInModelCell.Style.Font.Color.SetColor(Color.Crimson);

                if (!specInModel || !subjectInModel)
                    testWs.Row(row).Style.Border.BorderAround(ExcelBorderStyle.MediumDashDot);
            }

            testWs.Cells.AutoFitColumns();
        }

        private static void AddCoverageSheet(ExcelPackage p, SpecSuite suite)
        {
            var worksheet = p.Workbook.Worksheets.Add("Coverage");
            var specRows = new Dictionary<string, int>();
            var currentRow = 3;
            var specRowStart = currentRow;
            foreach (var spec in suite.Specs)
            {
                worksheet.Cells[specRowStart, 1].Value = spec.Name;
                for (var i = 0; i < spec.Children.Count; i++)
                {
                    var child = spec.Children[i];
                    currentRow = specRowStart + i;
                    var childRow = worksheet.Cells[currentRow, 2];
                    childRow.Value = child.Name;
                    specRows[child.Id] = currentRow;
                }

                var specHeader = worksheet.Cells[specRowStart, 1, currentRow, 1];
                specHeader.Merge = true;
                specRowStart = currentRow + 1;
            }

            var currentColumn = 3;
            var columnStart = currentColumn;
            foreach (var subj in GetPrimarySubjects(suite))
            {
                worksheet.Cells[1, currentColumn].Value = subj.Name;
                for (var i = 0; i < subj.Children.Count; i++)
                {
                    var child = subj.Children[i];
                    currentColumn = columnStart + i;
                    var childHeader = worksheet.Cells[2, currentColumn];
                    childHeader.Value = child.Name;
                    childHeader.Style.TextRotation = 45;
                    worksheet.Column(currentColumn).Width = 6;
                    foreach (var (specId, row) in specRows)
                    {
                        var statusCell = worksheet.Cells[row, currentColumn];
                        if (child.Specs.TryGetValue(specId, out var spec))
                        {
                            var status = child.GetCoverageStatus(specId, suite);
                            var values = new Dictionary<SpecCoverageStatus, string>
                            {
                                {SpecCoverageStatus.Missing, "❌"},
                                {SpecCoverageStatus.Implemented, "✔"},
                                {SpecCoverageStatus.NotNeeded, "--"},
                                {SpecCoverageStatus.Skipped, "❓"}
                            };
                            statusCell.Value = values[status];


                            if (Colors.TryGetValue((status, spec.Priority), out var c) ||
                                Colors.TryGetValue((status, null), out c)
                            )
                            {
                                var (background, text) = c;
                                statusCell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                statusCell.Style.Fill.BackgroundColor.SetColor(background);
                                statusCell.Style.Font.Color.SetColor(text);
                            }
                        }
                        else
                        {
                            statusCell.Value = "-";
                        }

                        statusCell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }
                }

                var subjectHeader = worksheet.Cells[1, columnStart, 1, currentColumn];
                subjectHeader.Value = subj.Name;
                subjectHeader.Merge = true;
                subjectHeader.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                columnStart = currentColumn + 1;
            }
        }

        public static ExcelPackage Create(SpecSuite suite)
        {
            var p = new ExcelPackage();
            AddSummarySheet(p, suite);
            AddCoverageSheet(p, suite);
            AddDiagnosticSheets(p, suite);
            return p;
        }

        private static void AddSummarySheet(ExcelPackage p, SpecSuite suite)
        {
            var worksheet = p.Workbook.Worksheets.Add("Summary");
            var tests = suite.Subjects.SelectMany(_ => _.GetCoverage(suite)).ToImmutableList();
            tests.Select(_ => { return _; })
                .Dump("test", true);
            if (Fail)
            {
                //throw new Exception("");
            }

            var priorities = new[] {SpecPriority.High, SpecPriority.Medium, SpecPriority.Low};
            var statuses = new[]
                {SpecCoverageStatus.Implemented, SpecCoverageStatus.Skipped, SpecCoverageStatus.Missing};
            var total = tests.Count;
            for (var i = 0; i < priorities.Length; i++)
            {
                var priority = priorities[i];
                var priorityRow = i + 2;
                var priorityLabelCell = worksheet.Cells[priorityRow, 1];
                priorityLabelCell.Value = priority.ToString();
                var startingColumn = 2;
                var priorityTests = tests.Where(_ => _.priority == priority).ToImmutableList();
                for (var j = 0; j < statuses.Length; j++)
                {
                    var status = statuses[j];
                    var statusCountCol = startingColumn;
                    var statusPercentCol = statusCountCol + 1;
                    if (i == 0)
                    {
                        // Column headers
                        var headerCell = worksheet.Cells[1, statusCountCol, 1, statusPercentCol];
                        headerCell.Merge = true;
                        headerCell.Value = status.ToString();
                        var statusTotal = tests.Count(_ => _.status == status);
                        var statusTotalCell = worksheet.Cells[2 + priorities.Length, statusCountCol];
                        statusTotalCell.Value = statusTotal;
                    }

                    var count = priorityTests.Count(_ => _.status == status);
                    var percent = (double) count / total;
                    var countCell = worksheet.Cells[priorityRow, statusCountCol];
                    countCell.Value = count;
                    var percentCell = worksheet.Cells[priorityRow, statusPercentCol];
                    percentCell.Value = percent;
                    percentCell.Style.Numberformat.Format = "0%";
                    startingColumn = statusPercentCol + 1;
                }


                var priorityTotalCell = worksheet.Cells[priorityRow, startingColumn];
                priorityTotalCell.Value = priorityTests.Count;
                if (i == 0)
                {
                    var totalHeaderCell = worksheet.Cells[1, startingColumn];
                    totalHeaderCell.Value = "Total";
                    var totalCell = worksheet.Cells[priorities.Length + 2, startingColumn];
                    totalCell.Value = total;
                }
            }

            var totalRowLabel = worksheet.Cells[priorities.Length + 2, 1];
            totalRowLabel.Value = "Total";
        }


        private static IReadOnlyList<Subject> GetPrimarySubjects(SpecSuite suite)
        {
            void Add(Subject subj, List<Subject> list)
            {
                list.Add(subj);
                foreach (var grandChild in subj.Children.SelectMany(c => c.Children))
                {
                    Add(grandChild, list);
                }
            }

            var subjects = new List<Subject>();
            Add(suite.RootSubject, subjects);
            return subjects.AsReadOnly();
        }
    }
}