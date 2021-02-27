using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JOB_ADS.Models;

namespace JOB_ADS.Controllers
{
    public class PostJobController : Controller
    {
        private Entities DbFile = new Entities();
        // GET: PostJob
        public ActionResult Post_JOB()
        {
            if (Session["User"] == null)
            {
                // check if a new session id was generated
                
                  return RedirectToAction("Index", "Home");

             //   return View();
            }
            else {
                ViewBag.Depart = DbFile.ADS_Master_Department.ToList(); return View();
            }

        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        [HttpPost]
        public string SavePostjob(string JOBTITLE, string JOBLOCATION,
            string JOBREGION, string JOBTYPE, string JOBDES,string JOBRQ,
            string OPTIONc, string OPTIONa, string OPTIONo, string COMPANY, string EXPERIENCE, string SALARY, string GENDER,string ImgUplode)
        {
            try
            {
                var Ran = RandomString(5);
                Session["IMG"] = Ran + JOBTITLE; 
                var TR_PJ = new ADS_PostJob();
                TR_PJ.JOB_Title = JOBTITLE;
                TR_PJ.JOB_Location = JOBLOCATION;
                TR_PJ.JOB_Region = JOBREGION;
                TR_PJ.JOB_Type = JOBTYPE;
                TR_PJ.JOB_Description = JOBDES;
                TR_PJ.JOB_Requirement = JOBRQ;
                TR_PJ.Option_Chevron = OPTIONc;
                TR_PJ.Option_Adisorn = OPTIONa;
                TR_PJ.OPtion_Others = OPTIONo;
                TR_PJ.Company_Name = COMPANY;
                TR_PJ.Experience = EXPERIENCE;
                TR_PJ.Salary = SALARY;
                TR_PJ.Gender = GENDER;
                if (ImgUplode != "")
                {
                    TR_PJ.Logo_Image = Session["IMG"] + ".JPG";
                }
                //User
                TR_PJ.Status = "T";
                TR_PJ.JOB_CreateDate = DateTime.Now;
                TR_PJ.JOB_UpdateDate = DateTime.Now;
                TR_PJ.User = Session["User"].ToString();
                DbFile.ADS_PostJob.Add(TR_PJ);
                DbFile.SaveChanges();
                return "S";

            }
            catch { return "N"; }
        }

       
        public ActionResult UploadFiles(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
                try
                { 
                    string path = Path.Combine(Server.MapPath("~/Content/images/LOGO"),
                    Path.GetFileName(Session["IMG"].ToString() + ".jpg"));
                    file.SaveAs(path);

                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }

            return null;

        }
    }
}