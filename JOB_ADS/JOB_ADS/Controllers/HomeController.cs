using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JOB_ADS.Models;

namespace JOB_ADS.Controllers
{
    public class HomeController : Controller
    {
        private Entities DbFile = new Entities();
        public ActionResult Index()
        {
            DateTime minusThirty = DateTime.Now.AddDays(-90);
            ViewBag.PostJob = DbFile.ADS_PostJob.Where(a => a.Status.Equals("T")).Where(c => c.JOB_UpdateDate > minusThirty).Where(d => d.OPtion_Others.Equals("T")).OrderByDescending(b => b.JOB_UpdateDate).ToList();
            return View();
        }
        public ActionResult JobSingle(int JOB)
        {
            ViewBag.PostSing = DbFile.ADS_PostJob.Where(a => a.ID.Equals(JOB))
               .Where(a => a.Status.Equals("T")).OrderByDescending(b => b.JOB_UpdateDate).ToList();

            return View();
        }
        public ActionResult JobList(string JOB)
        {
            DateTime minusThirty = DateTime.Now.AddDays(-90);
            ViewBag.PostList = DbFile.ADS_PostJob.Where(a => a.JOB_Title.Contains(JOB))
                .Where(a => a.Status.Equals("T")).Where(c => c.JOB_UpdateDate > minusThirty).Where(d => d.OPtion_Others.Equals("T")).OrderByDescending(b => b.JOB_UpdateDate).ToList();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}