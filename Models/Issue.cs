using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFModernVerticalMenu.Models
{
    public class Issue
    {
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string ImgPath { get; set; }

        public Issue(string location, string category, string description, string imgPath)
        {
            Location = location;
            Category = category;
            Description = description;
            ImgPath = imgPath;
            
        }



    }
}
