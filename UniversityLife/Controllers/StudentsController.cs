using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniversityLife.Models;
using UniversityLife.Services;

namespace UniversityLife.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ICosomosDbService _cosmosDbService;
        public StudentsController(ICosomosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
           
        }

        [ActionName("Index")]
        public async Task<IActionResult> Index()
        {
            return View(await _cosmosDbService.GetStudentsAsync("SELECT * FROM c"));
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
