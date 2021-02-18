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
        public ActionResult CandidatesDetail()
        {
        
            

                return View();
            

        }
        public ActionResult FindByDepart()
        {
            if (Session["User"] == null && Session["DEPARTMENT"] == null)
            {
                // check if a new session id was generated
                return RedirectToAction("Index", "Home");
                //   return View();
            }
            else 
            {
                string departnew = Session["DEPARTMENT"].ToString();
                ViewBag.JobDepart = DbFile.ADS_Master_Department.Where(a => a.Department_Name.Contains(departnew)).Where(a => a.Status.Equals("T")).FirstOrDefault();
                
                return View();
            }
           
        }
      
        [HttpPost]
        public string DataFindByDepart(string Depart)
        {
            Session["DEPARTMENT"] = null;
            var data = (from x in DbFile.ADS_Register
                        where x.Re_Department.Contains(Depart)
                        group x by x.Re_Position into g
                        select new
                        {
                            position = g.Key,
                            number = g.Count()
                        }).ToList();
            if (data.Count != 0)
            {
                Session["DEPARTMENT"] = Depart;
                string jsonlog = new JavaScriptSerializer().Serialize(data);
                return jsonlog;
            }
            else
            {
                Session["DEPARTMENT"] = Depart;
                return null;
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
                ViewBag.RegisName = DbFile.ADS_Register.Where(a => a.Re_Position.Contains(Position)).Where(a => a.Status.Equals("T")).FirstOrDefault();
                ViewBag.RegisList = DbFile.ADS_Register.Where(a => a.Re_Position.Contains(Position))
                .Where(a => a.Status.Equals("T")).ToList();

                return View();
            }

        }
        public string LoadDataDepart(string DEPART)
        {
            
            var data = (from x in DbFile.ADS_Register
                        where x.Re_Department.Equals(DEPART)
                        group x by x.Re_Position into g
                        select new
                        {
                            position = g.Key.ToString(),
                            number = g.Count().ToString()
                        }).AsEnumerable().Select(x => new
                        {
                            Position =x.position.ToString(),
                            Number =x.number
                        }
                        ).ToList();
            string jsonlog = new JavaScriptSerializer().Serialize(data);
            return jsonlog;

        }
    }
}