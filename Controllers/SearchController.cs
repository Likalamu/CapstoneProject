using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CapstoneTranslator.Models;
using CapstoneTranslator.Data;
using Microsoft.EntityFrameworkCore;
using CapstoneTranslator.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CapstoneTranslator.Controllers
{
    public class SearchController : Controller
    {
        private LanguageDbContext context;

        public SearchController(LanguageDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.columns = ListController.ColumnChoices;
            return View();
        }

        public IActionResult Results(string searchType, string searchTerm)
        {
            List<Job> jobs;
            List<JobDetailViewModel> displayJobs = new List<JobDetailViewModel>();

            if (string.IsNullOrEmpty(searchTerm))
            {
                jobs = context.Jobs
                   .Include(j => j.Employer)
                   .ToList();

                foreach (var job in jobs)
                {
                    List<JobLanguage> jobLanguages = context.JobLanguages
                        .Where(js => js.JobId == job.Id)
                        .Include(js => js.Language)
                        .ToList();

                    JobDetailViewModel newDisplayJob = new JobDetailViewModel(job, jobLanguages);
                    displayJobs.Add(newDisplayJob);
                }
            }
            else
            {
                if (searchType == "employer")
                {
                    jobs = context.Jobs
                        .Include(j => j.Employer)
                        .Where(j => j.Employer.Name == searchTerm)
                        .ToList();

                    foreach (Job job in jobs)
                    {
                        List<JobLanguage> jobLanguages = context.JobLanguages
                        .Where(js => js.JobId == job.Id)
                        .Include(js => js.Language)
                        .ToList();

                        JobDetailViewModel newDisplayJob = new JobDetailViewModel(job, jobLanguages);
                        displayJobs.Add(newDisplayJob);
                    }

                }
                else if (searchType == "Language")
                {
                    List<JobLanguage> jobLanguages = context.JobLanguages
                        .Where(j => j.Language.Name == searchTerm)
                        .Include(j => j.Job)
                        .ToList();

                    foreach (var job in jobLanguages)
                    {
                        Job foundJob = context.Jobs
                            .Include(j => j.Employer)
                            .Single(j => j.Id == job.JobId);

                        List<JobLanguage> displayLanguages = context.JobLanguages
                            .Where(js => js.JobId == foundJob.Id)
                            .Include(js => js.Language)
                            .ToList();

                        JobDetailViewModel newDisplayJob = new JobDetailViewModel(foundJob, displayLanguages);
                        displayJobs.Add(newDisplayJob);
                    }
                }
            }

            ViewBag.columns = ListController.ColumnChoices;
            ViewBag.title = "Jobs with " + ListController.ColumnChoices[searchType] + ": " + searchTerm;
            ViewBag.jobs = displayJobs;

            return View("Index");
        }
    }
}
