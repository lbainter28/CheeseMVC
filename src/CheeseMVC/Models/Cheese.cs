using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CheeseMVC.Models
{
    public class Cheese
    {
        public string Name { get; set; }
        public string Description { get; set; }

        private static readonly Regex validCheeseNamePattern = new Regex(@"[a-zA-Z\s]+");

        public Cheese(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public static bool IsValidName(string name)
        {
            return name != null && validCheeseNamePattern.IsMatch(name);
        }
    }
}
