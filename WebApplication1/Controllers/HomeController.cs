using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public static SqlConnection con = new SqlConnection("Data Source=SIR;Initial Catalog=model;Integrated Security=True");
        public SqlCommand cmd = new SqlCommand("SELECT * From Visitores", con);

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public List<VisitiorObjectViewModel> GetAllVisitorsToday()
        {
            List<VisitiorObjectViewModel> Visitores = new List<VisitiorObjectViewModel>();

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                if (dr.GetDateTime(4).DayOfYear == DateTime.Now.DayOfYear)
                {
                    Visitores.Add(new VisitiorObjectViewModel(dr.GetString(1), dr.GetDateTime(4), dr.GetString(2), dr.GetString(3)));
                }
            }

            con.Close();

            return Visitores;
        }

        public List<VisitiorObjectViewModel> GetAllVisitorsLastWeek()
        {
            List<VisitiorObjectViewModel> Visitores = new List<VisitiorObjectViewModel>();

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                if ((dr.GetDateTime(4) >= DateTime.Now.AddDays(-7)) && (dr.GetDateTime(4) <= DateTime.Now.AddDays(7)))
                {
                    Visitores.Add(new VisitiorObjectViewModel(dr.GetString(1), dr.GetDateTime(4), dr.GetString(2), dr.GetString(3)));
                }
            }

            con.Close();

            return Visitores;
        }

        public List<VisitiorObjectViewModel> GetAllVisitorsThisYear()
        {
            List<VisitiorObjectViewModel> Visitores = new List<VisitiorObjectViewModel>();

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                if (dr.GetDateTime(4).Year == DateTime.Now.Year)
                {
                    Visitores.Add(new VisitiorObjectViewModel(dr.GetString(1), dr.GetDateTime(4), dr.GetString(2), dr.GetString(3)));
                }
            }

            con.Close();

            return Visitores;
        }

        public List<VisitiorObjectViewModel> GetAllVisitorsLastHour()
        {
            List<VisitiorObjectViewModel> Visitores = new List<VisitiorObjectViewModel>();

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                if (dr.GetDateTime(4).Hour == DateTime.Now.Hour)
                {
                    Visitores.Add(new VisitiorObjectViewModel(dr.GetString(1), dr.GetDateTime(4), dr.GetString(2), dr.GetString(3)));
                }
            }

            con.Close();

            return Visitores;
        }
    }
}
