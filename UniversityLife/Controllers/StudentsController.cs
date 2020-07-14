using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using MySql.Data.MySqlClient.Memcached;
using UniversityLife.Models;
using UniversityLife.Services;
using System.Net.Mail;
using System.Text;

namespace UniversityLife.Controllers
{
   
    [Authorize]
    public class StudentsController : Controller
    {
        private readonly ICosomosDbService _cosmosDbService;
        public StudentsController(ICosomosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
           
        }
      
        public IActionResult Excel()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Students");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Student N0.";
                worksheet.Cell(currentRow, 2).Value = "Name";
                worksheet.Cell(currentRow, 3).Value = "Surname";
                worksheet.Cell(currentRow, 4).Value = "Email";
                worksheet.Cell(currentRow, 5).Value = "Home Address";
                worksheet.Cell(currentRow, 6).Value = "Mobile";
                worksheet.Cell(currentRow, 7).Value = "IsActive";
                worksheet.Cell(currentRow, 8).Value = "ImageUrl";

            
                List<Student> asList = _cosmosDbService.StudentList("SELECT * FROM c");
               

                foreach (var item in asList)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = item.StudentNo;
                    worksheet.Cell(currentRow, 2).Value = item.FirstName;
                    worksheet.Cell(currentRow, 3).Value = item.LastName;
                    worksheet.Cell(currentRow, 4).Value = item.Email;
                    worksheet.Cell(currentRow, 5).Value = item.HomeAddress;
                    worksheet.Cell(currentRow, 6).Value = item.MobileNo;
                    worksheet.Cell(currentRow, 7).Value = item.IsActive;
                    worksheet.Cell(currentRow, 8).Value = item.ImageUrl;

                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, 
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "StudentInfo.xlsx");
                }

            }
        }
        public IActionResult SendEmail()
        {
            try
            {
                StringBuilder message = new StringBuilder();
                MailAddress from = new MailAddress("andilebshange@gmail.com");
                message.Append("Good Day" + "\n" + "\n");
                message.Append("Please click on the following link to download all students' information" + "\n");
                message.Append("href='https://localhost:44325/Students/Excel' ");
                message.Append("\n" + "\n" + "Regards, " + "\n");
                message.Append("System Admin");

                MailMessage msg = new MailMessage();
                msg.From = from;
                msg.To.Add("andilebshange@gmail.com");
                msg.Subject = "Student Information";
                msg.Body = message.ToString();
                
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("andilebshange@gmail.com", "141296#Maria");
                smtp.Send(msg);


                ViewBag.Message = "Email has been sent";
            }
            

             catch (Exception ex)
            {
                ModelState.Clear();
                ViewBag.Message = $" Sorry we are facing Problem here {ex.Message}";
            }
            return RedirectToAction("Index");
        }
        [ActionName("Index")]
        public async Task<IActionResult> Index()
        {
            return View(await _cosmosDbService.GetStudentsAsync("SELECT * FROM c"));
        }
        [ActionName("IndexActive")]
        public async Task<IActionResult> IndexActive()
        {
            return View(await _cosmosDbService.GetStudentsAsync("SELECT * FROM c Where c.isactive = true"));
        }
        [ActionName("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Student student)
        {
            if (ModelState.IsValid)
            {
                student.Id = Guid.NewGuid().ToString();
                await _cosmosDbService.AddStudentAsync(student);
                return RedirectToAction("Index");
            }
            return View(student);
        }

        [ActionName("Edit")]
        public async Task<ActionResult> EditAsync(string id)
        {
            if (id == null) return BadRequest();
            Student student = await _cosmosDbService.GetStudentAsync(id);
            if (student == null) return NotFound();
            return View(student);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(Student student)
        {
            if (ModelState.IsValid)
            {
                await _cosmosDbService.UpdateStudentAsync(student.Id, student);
                return RedirectToAction("Index");
            }
            return View(student);
        }

        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            if (id == null) return BadRequest();
            Student student = await _cosmosDbService.GetStudentAsync(id);
            if (student == null) return NotFound();
            return View(student);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedAsync(string id)
        {
            await _cosmosDbService.DeleteStudentAsync(id);
            return RedirectToAction("Index");
        }

        [ActionName("Details")]
        public async Task<ActionResult> DetailsAsync(string id)
        {
            return View(await _cosmosDbService.GetStudentAsync(id));
        }
    }
}
