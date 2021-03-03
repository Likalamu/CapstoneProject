using System;
using CapstoneTranslator.Models;
using System.Collections.Generic;

namespace CapstoneTranslator.ViewModels
{
    public class JobDetailViewModel
    {
        public int JobId { get; set; }
        public string Name { get; set; }
        public string EmployerName { get; set; }
        public string LanguageText { get; set; }

        public JobDetailViewModel(Job theJob, List<JobLanguage> jobLanguages)
        {
            JobId = theJob.Id;
            Name = theJob.Name;
            EmployerName = theJob.Employer.Name;

            LanguageText = "";
            for (int i = 0; i < jobLanguages.Count; i++)
            {
                LanguageText += jobLanguages[i].Language.Name;
                if (i < jobLanguages.Count - 1)
                {
                    LanguageText += ", ";
                }
            }
        }
    }
}
