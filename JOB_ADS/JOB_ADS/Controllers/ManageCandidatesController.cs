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
    
        }
        public ActionResult Manage_Candidates()
        {
            if (Session["User"] == null)
            {
                // check if a new session id was generated
                  return RedirectToAction("Index", "Home");
                //return View();
            }
            else { 
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
                      select new regis
                      {
                      position = g.Key,
                      number = g.Count()
                       }).ToList();


                ViewBag.JobRegis = data;
                return View();
            }
         }
       // public string DataFindByDepart(string Depart)
        //{
            
            //ViewBag.JobDepart = DbFile.ADS_Master_Department.Where(a => a.Department_Name.Contains(Depart)).Where(a => a.Status.Equals("T")).FirstOrDefault();
           // var data = (from x in DbFile.ADS_Register
                      //  where x.Re_Department.Contains(Depart)
                        //group x by x.Re_Position into g
                        //select new
                       // {
                            //position = g.Key,
                           // number = g.Count()
                      //  }).ToList();
                      
            //string jsonlog = new JavaScriptSerializer().Serialize(data);
            //return jsonlog;
       // }
        public ActionResult FindByCandidates(string Position)
        {
              if (Session["User"] == null)
              {
                  // check if a new session id was generated
                    return RedirectToAction("Index", "Home");
               //   return View();
              }
              else {
                ViewBag.RegisName = DbFile.ADS_Register.Where(a => a.Re_Position.Contains(Position)).Where(a => a.Status.Equals("T")).FirstOrDefault();
                ViewBag.RegisList = DbFile.ADS_Register.Where(a => a.Re_Position.Contains(Position))
                .Where(a => a.Status.Equals("T")).ToList();

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
    }
}