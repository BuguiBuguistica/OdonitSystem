using iTextSharp.text;
using iTextSharp.text.pdf;
using log4net;
using Odonit.Business;
using Odonit.DAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Odonit.WebApi.Controllers
{
    [RoutePrefix("api/PatientsReport")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    /// <summary>
    /// Patients Report controller
    /// </summary>
    public class PatientReportController : ApiController
    {
        private readonly ILog _logger;
        private PatientsReportService _service;

        public PatientReportController(ILog logger, PatientsReportService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// Get Patients by parameters
        /// </summary>
        /// <param name="serviceId">serviceId</param>
        /// <param name="treatmentId">treatmentId</param>
        /// <param name="medicalSecurityId">medicalSecurityId</param>
        /// <param name="FromDate">FromDate</param>
        /// <param name="EndDate">EndDate</param>
        /// <returns>List of Patient</returns>
        [Route("v1")]
        [HttpGet]
        public IEnumerable<PatientsReport> GetPatients( int? serviceId = null, int? treatmentId = null, int? medicalSecurityId = null, DateTime? fromDate = null, DateTime? endDate = null)
        {
            List<PatientsReport> items = null;
            var filter = new ReportFilter() { 
                ServiceId = serviceId,
                TreatmentId = treatmentId,
                MedicalSecurityId = medicalSecurityId,
                FromDate = fromDate,
                EndDate = endDate
            };
            try
            {
                items = _service.get(filter);
            }
            catch (Exception)
            {
                throw;
            }

            return items;

        }
      
       /// <summary>
       /// Get a report of Patients
       /// </summary>
       /// <param name="filter">ReportFilter</param>
       /// <returns>Pdf</returns>
        [Route("v1")]
        [HttpPost]
        public HttpResponseMessage GetReport(ReportFilter filter)
        {
            List<PatientsReport> items = null;
            HttpResponseMessage Response = new HttpResponseMessage(HttpStatusCode.OK);
            try
            {
                items = _service.get(filter);
                if (items.Any<PatientsReport>()) {
                    byte[] strS = CreatePDF2(filter, items);
                    Response.Content = new ByteArrayContent(strS);
                    Response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                    Response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                }
            }
            catch (Exception)
            {                
                throw;
            }
            
            return Response;
            
        }
        private byte[] CreatePDF2(ReportFilter filter, List<PatientsReport> patients)
        {
            Document doc = new Document(PageSize.LETTER, 40, 20, 30, 20);

            using (MemoryStream output = new MemoryStream())
            {
                PdfWriter wri = PdfWriter.GetInstance(doc, output);
                doc.Open();
                var headerFont = FontFactory.GetFont("Arial", 14, new BaseColor(0,129,198));
                Image logo = Image.GetInstance("http://localhost/Odonit.WebApi/Images/logo-2.png");
                logo.ScalePercent(70f);
                logo.Alignment = Element.ALIGN_RIGHT;
                Paragraph header = new Paragraph("Reporte de Pacientes", headerFont)
                { 
                    Alignment = Element.ALIGN_LEFT,
                    PaddingTop = 10
                };
                doc.Add(logo);
                doc.Add(header);

                if (filter.ServiceId.HasValue) {
                    Paragraph service = new Paragraph(string.Format("Servicio: {0}", filter.ServiceName));
                    doc.Add(service); 
                }
                if (filter.TreatmentId.HasValue)
                {
                    Paragraph tratment = new Paragraph(string.Format("Tratamiento: {0}", filter.TreatmentName));
                    doc.Add(tratment);
                }
                if (filter.MedicalSecurityId.HasValue)
                {
                    Paragraph medicalSecurity = new Paragraph(string.Format("Obra Social: {0}", filter.MedicalSecurityName));
                    doc.Add(medicalSecurity);
                }
                if (filter.FromDate.HasValue && filter.EndDate.HasValue)
                {
                    string dFrom = string.Format("{0} {1} {2} {3} {4}", filter.FromDate.Value.Day.ToString() , "/" , filter.FromDate.Value.Month.ToString(), "/", filter.FromDate.Value.Year.ToString());
                    string dEnd = string.Format("{0} {1} {2} {3} {4}", filter.EndDate.Value.Day.ToString(), "/", filter.EndDate.Value.Month.ToString(), "/", filter.EndDate.Value.Year.ToString());
                    Paragraph date = new Paragraph(string.Format("Pacientes tratados entre: {0} {1} {2}", dFrom, "-", dEnd));
                    doc.Add(date);
                }

                PdfPTable table = new PdfPTable(6);
               
                table.WidthPercentage = 100;
                float[] widths = new float[] { 40f, 40f, 40f, 60f, 40f, 50f};
                table.SetWidths(widths);
                table.DefaultCell.Padding = 5;
                table.SpacingBefore = 30;
                table.SpacingAfter = 10;
                table.DefaultCell.Border = 0;
                table.HorizontalAlignment = 0;
                
                PdfPCell firtsNameCell = createHeaderCell("Nombre");                
                PdfPCell lastNameCell = createHeaderCell("Apellido");
                PdfPCell codeCell = createHeaderCell("DNI");
                PdfPCell addressCell = createHeaderCell("Dirección");
                PdfPCell phoneCell = createHeaderCell("Celular");
                PdfPCell emailCell = createHeaderCell("Email"); 
               
                table.AddCell(firtsNameCell);
                table.AddCell(lastNameCell);
                table.AddCell(codeCell);
                table.AddCell(addressCell);
                table.AddCell(phoneCell);
                table.AddCell(emailCell);
                
                foreach (var item in patients)
                {
                    PdfPCell cell1 = createCell(item.FirstName);
                    table.AddCell(cell1);
                    PdfPCell cell2 = createCell(item.LastName);
                    table.AddCell(cell2);
                    PdfPCell cell3 = createCell(item.PersonCode.ToString());
                    table.AddCell(cell3);
                    PdfPCell cell4 = createCell(item.Address);
                    table.AddCell(cell4);
                    PdfPCell cell5 = createCell(item.CellPhone);
                    table.AddCell(cell5);
                    PdfPCell cell6 = createCell(item.Email);
                    table.AddCell(cell6);
                   
                }
                
                doc.Add(table);
                doc.Close();
                return output.ToArray();
            }

        }
        private PdfPCell createCell(string item)
        {            
            var tableFont = FontFactory.GetFont("Arial", 10, new BaseColor(51, 51, 51));

           var cell =  new PdfPCell(new Phrase(item, tableFont)){
                    Border = 0,
                    Padding = 5,
                    BorderWidthBottom = 1,
                    BorderColorBottom = new BaseColor(179, 179, 179),
                    VerticalAlignment = Element.ALIGN_TOP,
                    HorizontalAlignment = Element.ALIGN_LEFT
                };

            return cell; 
        }
        private PdfPCell createHeaderCell(string item)
        {           

            var cell = new PdfPCell(new Phrase(item))
            {
                Border = 0,
                Padding = 5,
                PaddingBottom = 10,
                BorderWidthBottom = 3,
                BorderColorBottom = new BaseColor(0, 129, 198),
                VerticalAlignment = Element.ALIGN_MIDDLE,
                HorizontalAlignment = Element.ALIGN_LEFT
            };

            return cell;
        }
	}
}