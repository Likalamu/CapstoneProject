using System;
namespace CapstoneTranslator.Models
{
    public class Employer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Pay { get; set; }
        public string Schedule { get; set; }

        public Employer(string name, string location, string pay, string schedule)
        {
            Name = name;
            Location = location;
            Pay = pay;
            Schedule = schedule;
        }


        public Employer()
        {
        }

    }
}
