using System;
namespace CapstoneTranslator.Models
{
    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Language(string name, string description)
        {
            Name = name;
            Description = description;
        }


        public Language()
        {
        }
    }
}
