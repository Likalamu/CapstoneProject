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
    public class LanguageController : Controller
    {
        private LanguageDbContext context;

        public LanguageController(LanguageDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Language> language = context.Languages.ToList();
            return View(language);
        }

        public IActionResult Add()
        {
            Language language = new Language();
            return View(language);
        }

        [HttpPost]
        public IActionResult Add(Language language)
        {
            if (ModelState.IsValid)
            {
                context.Languages.Add(language);
                context.SaveChanges();
                return Redirect("/language/");
            }

            return View("Add", language);
        }

        public IActionResult AddJob(int id)
        {
            Job theJob = context.Jobs.Find(id);
            List<Language> possibleLanguages = context.Languages.ToList();
            AddJobLanguageViewModel viewModel = new AddJobLanguageViewModel(theJob, possibleLanguages);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddJob(AddJobLanguageViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

                int jobId = viewModel.JobId;
                int languageId = viewModel.LanguageId;

                List<JobLanguage> existingItems = context.JobLanguages
                    .Where(js => js.JobId == jobId)
                    .Where(js => js.LanguageId == languageId)
                    .ToList();

                if (existingItems.Count == 0)
                {
                    JobLanguage jobLanguage = new JobLanguage
                    {
                        JobId = jobId,
                        LanguageId = languageId
                    };
                    context.JobLanguages.Add(jobLanguage);
                    context.SaveChanges();
                }

                return Redirect("/Home/Detail/" + jobId);
            }

            return View(viewModel);
        }

        public IActionResult About(int id)
        {
            List<JobLanguage> jobLanguages = context.JobLanguages
                .Where(js => js.LanguageId == id)
                .Include(js => js.Job)
                .Include(js => js.Language)
                .ToList();

            return View(jobLanguages);
        }

        public IActionResult Detail(int id)
        {
            List<Job> jobs = context.Jobs.Include(j => j.Employer).ToList();

            return View(jobs);
        }

    }
}
