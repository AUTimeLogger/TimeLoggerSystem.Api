using AUTimeManagement.Api.Business.Logic.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace AUTimeManagement.Api.Management.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IBusinessService _service;

        public ReportController(IBusinessService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        /// <summary>
        /// Endpoint GET /api/report/
        /// 
        /// <br/>
        /// Return all users, all projects
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        [Produces("text/csv")]
        public async Task<IActionResult> OnGet(
            [FromQuery] int year)
        {
            var report = await _service.Report.CreateReportAsync(year);

            Document document = new Document { FileName = $"report_{DateTime.UtcNow}.csv", ContentType = "text/csv", Data = Encoding.UTF8.GetBytes(report) };

            var cd = new ContentDispositionHeaderValue("attachment")
            {
                FileNameStar = document.FileName
            };
            Response.Headers.Add(HeaderNames.ContentDisposition, cd.ToString());

            return File(document.Data, document.ContentType);
        }

        public class Document
        {
            public string FileName { get; set; }
            public string ContentType { get; set; }
            public byte[] Data { get; set; }
        }
    }
}
