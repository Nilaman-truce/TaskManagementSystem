using System.Data;
using Dapper;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.Data.SqlClient;
using TaskManagementSystem.Models;
using TaskManagementSystem.Repositories;
using System.Data.Common;
using System.Threading.Tasks;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Fluent;
using ClosedXML.Excel;
using System.IO;

namespace TaskManagementSystem.Controllers
{
    public class ReportController : Controller
    {

        private readonly ITaskRepository _repository;

        public ReportController(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var report = await _repository.GetReportAsync();
                return View(report);
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }

        public async Task<IActionResult> DownloadPdf()
        {
            try
            {
                var report = (await _repository.GetReportAsync()).FirstOrDefault();

                var document = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(2, Unit.Centimetre);
                        page.PageColor(Colors.White);
                        page.DefaultTextStyle(x => x.FontSize(14));

                        page.Header()
                            .Text("Report")
                            .SemiBold().FontSize(24).FontColor(Colors.Blue.Medium);

                        page.Content()
                            .PaddingVertical(1, Unit.Centimetre)
                            .Column(x =>
                            {
                                x.Item().Text($"Total Tasks: {report.TotalTasks}");
                                x.Item().Text($"Pending Tasks: {report.PendingTasks}");
                                x.Item().Text($"In Progress Tasks: {report.InProgressTasks}");
                                x.Item().Text($"Completed Tasks: {report.CompletedTasks}");
                                x.Item().Text($"Completion Percentage: {report.CompletedPercentage}%");
                            });
                    });
                });

                var stream = new MemoryStream();
                document.GeneratePdf(stream);
                stream.Position = 0;

                return File(stream, "application/pdf", "Report.pdf");
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }

        public async Task<IActionResult> DownloadExcel()
        {
            try
            {
                var report = (await _repository.GetReportAsync()).FirstOrDefault();
                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Report");

                worksheet.Cell(1, 1).Value = "Task";
                worksheet.Cell(1, 2).Value = "Count";

                worksheet.Cell(2, 1).Value = "Total Tasks";
                worksheet.Cell(2, 2).Value = report.TotalTasks;

                worksheet.Cell(3, 1).Value = "Pending Tasks";
                worksheet.Cell(3, 2).Value = report.PendingTasks;

                worksheet.Cell(4, 1).Value = "In Progress Tasks";
                worksheet.Cell(4, 2).Value = report.InProgressTasks;

                worksheet.Cell(5, 1).Value = "Completed Tasks";
                worksheet.Cell(5, 2).Value = report.CompletedTasks;

                worksheet.Cell(6, 1).Value = "Completion Percentage";
                worksheet.Cell(6, 2).Value = report.CompletedPercentage;

                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                stream.Position = 0;

                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReportExcel.xlsx");

            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }

    }
}
