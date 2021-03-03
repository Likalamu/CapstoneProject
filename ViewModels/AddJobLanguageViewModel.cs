using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using CapstoneTranslator.Models;

namespace CapstoneTranslator.ViewModels
{
    public class AddJobLanguageViewModel
    {
        [Required(ErrorMessage = "Job is required")]
        public int JobId { get; set; }

        [Required(ErrorMessage = "Language is required")]
        public int LanguageId { get; set; }

        public Job Job { get; set; }

        public List<SelectListItem> Languages { get; set; }

        public AddJobLanguageViewModel(Job theJob, List<Language> possibleLanguages)
        {
            Languages = new List<SelectListItem>();

            foreach (var language in possibleLanguages)
            {
                Languages.Add(new SelectListItem
                {
                    Value = language.Id.ToString(),
                    Text = language.Name
                });
            }

            Job = theJob;
        }

        public AddJobLanguageViewModel()
        {
        }
    }
}
