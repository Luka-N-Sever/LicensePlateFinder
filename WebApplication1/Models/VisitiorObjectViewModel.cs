using System;

namespace WebApplication1.Models
{
    public class VisitiorObjectViewModel
    {

        public VisitiorObjectViewModel(string lp, DateTime dt, string city, string country)
        {
            LicensePlate = lp;
            VisitDate = dt;
            City = city;
            Country = country;
        }                  

        public int id { get; set; }

        public string LicensePlate { get; set; }

        public DateTime VisitDate { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

    }
}