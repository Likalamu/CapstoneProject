using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CapstoneTranslator.Data;
using CapstoneTranslator.Models;
using CapstoneTranslator.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CapstoneTranslator.Controllers
{
    public class EmployerController : Controller
    {
        // GET: /<controller>/
        private LanguageDbContext context;

        public EmployerController(LanguageDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Employer> employer = context.Employers.ToList();
            return View(employer);
        }

        public IActionResult Add()
        {
            AddEmployerViewModel addEmployerViewModel = new AddEmployerViewModel();
            return View(addEmployerViewModel);
        }

        [HttpPost]
        public IActionResult ProcessAddEmployerForm(AddEmployerViewModel addEmployerViewModel)
        {
            if (ModelState.IsValid)
            {
                Employer newEmployer = new Employer
                {
                    Name = addEmployerViewModel.Name,
                    Location = addEmployerViewModel.Location,
                    Pay = addEmployerViewModel.Pay,
                    Schedule = addEmployerViewModel.Schedule
                };

                context.Employers.Add(newEmployer);
                context.SaveChanges();
                return Redirect("/Employer");
            }
            return View(addEmployerViewModel);
        }

        public IActionResult About(int id)
        {
            Job theJob = context.Jobs
               .Include(e => e.Employer)
               .Single(e => e.Id == id);

            List<JobLanguage> jobLanguages = context.JobLanguages
                .Where(et => et.JobId == id)
                .Include(et => et.Language)
                .ToList();

            JobDetailViewModel viewModel = new JobDetailViewModel(theJob, jobLanguages);
            return View(viewModel);
        }
    }
}
