using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapstoneTranslator.Data;
using CapstoneTranslator.Models;
using CapstoneTranslator.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CapstoneTranslator.Controllers
{
    public class AdminController : Controller
    {
        private LanguageDbContext context;

        public AdminController(LanguageDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Job> jobs = context.Jobs.Include(j => j.Employer).ToList();

            return View(jobs);
        }

        public IActionResult AddJob()
        {
            List<Employer> newEmployers = context.Employers.ToList();
            List<Language> newLanguages = context.Languages.ToList();
            AddJobViewModel addJobViewModel = new AddJobViewModel(newEmployers, newLanguages);
            return View(addJobViewModel);
        }

        [HttpPost]
        public IActionResult ProcessAddJobForm(AddJobViewModel addJobViewModel, string[] selectedLanguages)
        {
            if (ModelState.IsValid)
            {
                Employer newEmployers = context.Employers.Find(addJobViewModel.EmployerId);
                Job job = new Job
                {
                    Name = addJobViewModel.Name,
                    EmployerId = addJobViewModel.EmployerId,
                    Employer = newEmployers
                };

                foreach (string language in selectedLanguages)
                {
                    JobLanguage jobLanguage = new JobLanguage
                    {
                        LanguageId = int.Parse(language),
                        Job = job
                    };
                    context.JobLanguages.Add(jobLanguage);
                }

                context.Jobs.Add(job);
                context.SaveChanges();
                return Redirect("/Admin/Index");
            }
            return View(addJobViewModel);
        }

        public IActionResult Detail(int id)
        {
            Job theJob = context.Jobs
                .Include(j => j.Employer)
                .Single(j => j.Id == id);

            List<JobLanguage> jobLanguages = context.JobLanguages
                .Where(js => js.JobId == id)
                .Include(js => js.Language)
                .ToList();

            JobDetailViewModel viewModel = new JobDetailViewModel(theJob, jobLanguages);
            return View(viewModel);
        }
    }
}
