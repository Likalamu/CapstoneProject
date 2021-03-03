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
    public class ListController : Controller
    {
        internal static Dictionary<string, string> ColumnChoices = new Dictionary<string, string>()
        {
            {"all", "All"},
            {"employer", "Employer"},
            {"language", "Language"}
        };

        internal static List<string> TableChoices = new List<string>()
        {
            "employer",
            "language"
        };

        private LanguageDbContext context;

        public ListController(LanguageDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.columns = ColumnChoices;
            ViewBag.tablechoices = TableChoices;
            ViewBag.employers = context.Employers.ToList();
            ViewBag.languages = context.Languages.ToList();
            return View();
        }

        // list jobs by column and value
        public IActionResult Jobs(string column, string value)
        {
            List<Job> jobs = new List<Job>();
            List<JobDetailViewModel> displayJobs = new List<JobDetailViewModel>();

            if (column.ToLower().Equals("all"))
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

                ViewBag.title = "All Jobs";
            }
            else
            {
                if (column == "employer")
                {
                    jobs = context.Jobs
                        .Include(j => j.Employer)
                        .Where(j => j.Employer.Name == value)
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
                else if (column == "language")
                {
                    List<JobLanguage> jobLanguage = context.JobLanguages
                        .Where(j => j.Language.Name == value)
                        .Include(j => j.Job)
                        .ToList();

                    foreach (var job in jobLanguage)
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
                ViewBag.title = "Jobs with " + ColumnChoices[column] + ": " + value;
            }
            ViewBag.jobs = displayJobs;

            return View();
        }
    }
}
