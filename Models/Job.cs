﻿using System;
using System.Collections.Generic;

namespace CapstoneTranslator.Models
{
    public class Job
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Employer Employer { get; set; }

        public int EmployerId { get; set; }

        public List<JobLanguage> JobLanguages { get; set; }

        public Job(string name, Employer employer)
        {
            Name = name;
            Employer = employer;
        }

        public Job()
        {
        }


        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            return obj is Job @job &&
                   Id == @job.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

    }
}
