using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Softka.Utils;

using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Http.Extensions;

namespace Softka.App.Controllers
{
    /* [AuthorizationRequired] */
    public class HomeController : Controller
    {
        private readonly IConverter _converter;
        public HomeController(IConverter converter)
        {
            _converter = converter;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ViewToPdf()
        {
            return View();
        }

        public IActionResult ShowPdfOnPage()
        {
            string ActualPage = HttpContext.Request.Path;
            string UrlPage = HttpContext.Request.GetEncodedUrl();
            UrlPage = UrlPage.Replace(ActualPage, "");
            UrlPage = $"{UrlPage}/Home/ViewToPdf";


            var Pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings()
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait
                },
                Objects = {
                    new ObjectSettings()
                    {
                        Page = UrlPage
                    }
                }
            };

            var ArchivePdf = _converter.Convert(Pdf);


            return File(ArchivePdf, "application/pdf");
        }

        public IActionResult DownloadPdf()
        {
            string ActualPage = HttpContext.Request.Path;
            string UrlPage = HttpContext.Request.GetEncodedUrl();
            UrlPage = UrlPage.Replace(ActualPage, "");
            UrlPage = $"{UrlPage}/Home/ViewToPdf";


            var Pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings()
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait
                },
                Objects = {
                    new ObjectSettings()
                    {
                        Page = UrlPage
                    }
                }
            };

            var ArchivePdf = _converter.Convert(Pdf);
            string PdfName = "curriculum_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".pdf";


            return File(ArchivePdf, "application/pdf", PdfName);
        }
    }
}