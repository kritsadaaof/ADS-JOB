using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using JOB_ADS.Models;
using Microsoft.Ajax.Utilities;

namespace JOB_ADS.Controllers
{
    public class ManageCandidatesController : Controller
    {
        private Entities DbFile = new Entities();
        // GET: Manage Candidates
        public class regis
        {
            public string position { get; set; }
            public int number { get; set; }
            public string company { get; set; }
            public string experience { get; set; }
            public string salary { get; set; }

        }

        public class candidate
        {
            public string title { get; set; }
            public string name { get; set; }
            public string surname { get; set; }
            public string phone { get; set; }
            public string email { get; set; }
            public string position { get; set; }
            public DateTime interviewdate { get; set; }

        }
        public ActionResult Manage_Candidates()
        {
            if (Session["User"] == null)
            {
                // check if a new session id was generated
                return RedirectToAction("Index", "Home");
                //return View();
            }
            else
            {
                ViewBag.Titles = DbFile.ADS_Register.DistinctBy(x => x.Re_Position).ToList();
                ViewBag.Departs = DbFile.ADS_Master_Department.OrderBy(a => a.Department_Name).ToList();
                return View();
            }

        }

        public ActionResult FindByDepart(string Depart)
        {
            if (Session["User"] == null)
            {
                // check if a new session id was generated
                return RedirectToAction("Index", "Home");
                //   return View();
            }
            else
            {
                ViewBag.JobDepart = DbFile.ADS_Master_Department.Where(a => a.Department_Name.Contains(Depart)).Where(a => a.Status.Equals("T")).FirstOrDefault();
                //ViewBag.JobRegis = DbFile.ADS_Register.DistinctBy(x => x.Re_Position).Where(a => a.Re_Department.Contains(Depart)).ToList();
                var data = (from x in DbFile.ADS_Register
                            where x.Re_Department.Contains(Depart)
                            group x by x.Re_Position into g
                            join postjob in DbFile.ADS_PostJob on g.FirstOrDefault().Re_Position equals postjob.JOB_Title
                            select new regis
                            {
                                position = g.Key,
                                number = g.Count(),
                                company = postjob.Company_Name,
                                experience = postjob.Experience,
                                salary = postjob.Salary

                            }).ToList();
                ViewBag.JobRegis = data;


                return View();
            }
        }
      
        public ActionResult FindByCandidates(string Position)
        {
            if (Session["User"] == null)
            {
                // check if a new session id was generated
                return RedirectToAction("Index", "Home");
                //   return View();
            }
            else
            {
                ViewBag.RegisName = DbFile.ADS_Register.Where(a => a.Re_Position.Contains(Position)).FirstOrDefault();
                ViewBag.RegisList = DbFile.ADS_Register.Where(a => a.Re_Position.Contains(Position)).ToList();

                return View();
            }

        }
        public string LoadRegistCandidates(int IDCandidates)
        {

            var data = (from TR_Regis in DbFile.ADS_Register
                        where TR_Regis.ID.Equals(IDCandidates)
                        select new
                        {
                            TR_Regis.ID,
                            TR_Regis.Re_Title_TH,
                            TR_Regis.Re_Name_TH,
                            TR_Regis.Re_Surname_TH
                        }).ToList();
            string jsonlog = new JavaScriptSerializer().Serialize(data);
            return jsonlog;

        }
        public ActionResult Interview(int ID)
        {
            if (Session["User"] == null)
            {
                // check if a new session id was generated
                return RedirectToAction("Index", "Home");
                //   return View();
            }
            else
            {
          
                ViewBag.interviewCandidates = DbFile.ADS_Register.Where(a => a.ID.Equals(ID)).ToList();
                return View();
                
            }

        }
    }
}