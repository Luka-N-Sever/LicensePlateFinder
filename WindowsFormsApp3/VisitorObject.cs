using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;

namespace WindowsFormsApp3
{
    
    public class VisitorObject 
    {
        SqlCommand cmd;
        SqlConnection con;
        SqlDataAdapter da;

        public static LicensePlateCodeLibraryEntities1 Ents = new LicensePlateCodeLibraryEntities1();
        
        public VisitorObject(string LicensePlateCam, string CountryCam/*, string DateCam*/)
        {
            LicensePlate = LicensePlateCam;
            //VisitDate = Convert.ToDateTime(DateCam);
            VisitDate = DateTime.Now;
            CodeString = Parser(LicensePlate);
            Country = CountryCam;
            City = FindCity(CodeString, Country);
            //Country = FindCountry(CodeString);
            id = FindIdInTable(CodeString, Country);
        }

        public void Cache()
        {
            con = new SqlConnection("Data Source=SIR;Initial Catalog=model;Integrated Security=True");
            con.Open();
            cmd = new SqlCommand("INSERT INTO Visitores(LicensePlate, City, Country, VisitDate) VALUES (@w,@x,@y,@z)", con);
            cmd.Parameters.Add("@w", this.LicensePlate);
            cmd.Parameters.Add("@x", this.City);
            cmd.Parameters.Add("@y", this.Country);
            cmd.Parameters.Add("@z", this.VisitDate);
            cmd.ExecuteNonQuery();
        }
        
        private string FindCity(string codestring, string country)
        { 
            var CSity = from PlateCodes in Ents.PlateCodes
                        where PlateCodes.Code == codestring && PlateCodes.Country == country
                        select new { PlateCodes.City };
            return CSity.First().City.ToString();
        }

        private int FindIdInTable(string codestring, string country)
        {
            var ID = from PlateCodes in Ents.PlateCodes
                     where PlateCodes.Code == codestring && PlateCodes.Country == country
                     select new { PlateCodes.id };
            return ID.First().id;
        }

        private string Parser(string licenseplate)
        {
            char[] licenseplatechars = licenseplate.ToCharArray();
            return (Char.ToString(licenseplatechars[0]) + Char.ToString(licenseplatechars[1])).ToUpper();
        }

        private int id { get; set; }
        private string LicensePlate { get; set; }
        private string CodeString { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        //public string Date;
        public DateTime VisitDate { get; set; }
    }
}
