﻿using System;
namespace CapstoneTranslator.Models
{
    public class Employer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public Employer(string name, string location)
        {
            Name = name;
            Location = location;
        }


        public Employer()
        {
        }

    }
}
