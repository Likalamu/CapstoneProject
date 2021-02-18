using System;
namespace CapstoneTranslator.Models
{
    public class JobLanguage
    {
        public int JobId { get; set; }
        public Job Job { get; set; }

        public int LanguageId { get; set; }
        public Language Language { get; set; }

        public JobLanguage()
        {
        }
    }
}