using DynamicTimeTableGenerator.Models;
using Microsoft.AspNetCore.Mvc;

namespace DynamicTimeTableGenerator.Controllers
{
    public class TimeTableController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(TimeTableInputModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["WorkingDays"] = model.WorkingDays;
                TempData["SubjectsPerDay"] = model.SubjectsPerDay;
                TempData["TotalSubjects"] = model.TotalSubjects;
                TempData["TotalHours"] = model.WorkingDays * model.SubjectsPerDay;
                return RedirectToAction("EnterSubjects");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult EnterSubjects()
        {
            int totalSubjects = Convert.ToInt32(TempData.Peek("TotalSubjects"));
            var model = new List<SubjectHourInput>();
            for (int i = 0; i < totalSubjects; i++)
                model.Add(new SubjectHourInput());
            return View(model);
        }

        [HttpPost]
        public IActionResult EnterSubjects(List<SubjectHourInput> subjectHours)
        {
            int totalHours = Convert.ToInt32(TempData["TotalHours"]);
            int workingDays = Convert.ToInt32(TempData["WorkingDays"]);
            int subjectsPerDay = Convert.ToInt32(TempData["SubjectsPerDay"]);

            if (subjectHours.Sum(x => x.Hours) != totalHours)
            {
                ModelState.AddModelError("", $"Total hours must equal {totalHours}");
                return View(subjectHours);
            }

            var allSubjects = new List<string>();
            foreach (var subject in subjectHours)
                allSubjects.AddRange(Enumerable.Repeat(subject.SubjectName, subject.Hours));

            var random = new Random();
            allSubjects = allSubjects.OrderBy(_ => random.Next()).ToList();

            var timetable = new List<List<string>>();
            for (int row = 0; row < subjectsPerDay; row++)
            {
                var rowSubjects = new List<string>();
                for (int col = 0; col < workingDays; col++)
                {
                    rowSubjects.Add(allSubjects.First());
                    allSubjects.RemoveAt(0);
                }
                timetable.Add(rowSubjects);
            }

            ViewBag.WorkingDays = workingDays;
            return View("DisplayTimeTable", timetable);
        }
    }
}
