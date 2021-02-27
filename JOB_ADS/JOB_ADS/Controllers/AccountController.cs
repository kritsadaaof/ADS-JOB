using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using JOB_ADS.Models;

namespace JOB_ADS.Controllers
{
    public class AccountController : Controller
    {
        private Entities DbFile = new Entities();
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult EditJOB(string JOB)
        {
            if (Session["User"] == null)
            {
                // check if a new session id was generated
                return RedirectToAction("Index", "Home");
            }
            else
            {
                DateTime minusThirty = DateTime.Now.AddDays(-90);
                ViewBag.PostListEdit = DbFile.ADS_PostJob.OrderByDescending(a => a.Status).ThenByDescending(b => b.JOB_UpdateDate).Where(c => c.JOB_UpdateDate > minusThirty).ToList();
                ViewBag.Depart = DbFile.ADS_Master_Department.ToList();
                return View();
            } 
        }


        public ActionResult LogOut()
        {
            Session["User"] = null;
            return RedirectToAction("Index", "Home");
        }

        public string CheckJOB(int ID)
        {
            try
            {
                var data = (from TR_PJ in DbFile.ADS_PostJob
                            where TR_PJ.ID.Equals(ID)
                            select new
                            {
                                TR_PJ.ID,
                                TR_PJ.JOB_Title,
                                TR_PJ.JOB_Location,
                                TR_PJ.JOB_Region,
                                TR_PJ.JOB_Type,
                                TR_PJ.JOB_Description,
                                TR_PJ.JOB_Requirement,
                                TR_PJ.Option_Chevron,
                                TR_PJ.Option_Adisorn,
                                TR_PJ.OPtion_Others,
                                TR_PJ.Company_Name,
                                TR_PJ.Logo_Image,
                                TR_PJ.User,
                                TR_PJ.JOB_CreateDate,
                                TR_PJ.JOB_UpdateDate,
                                TR_PJ.Status,
                                TR_PJ.Experience,
                                TR_PJ.Salary,
                                TR_PJ.Gender
                            }).ToList();
                string jsonlog = new JavaScriptSerializer().Serialize(data);
                return jsonlog;

            }
            catch { return null; }
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        [HttpPost]
        public string SavePostEdit(string ID, string JOBTITLE, string JOBLOCATION,
            string JOBREGION, string JOBTYPE, string JOBDES, string JOBRQ,
            string OPTIONc, string OPTIONa, string OPTIONo, string STATUS, string COMPANY, string EXPERIENCE, string SALARY, string GENDER,string ImgUplode)
        {
            try
            {
                var Ran = RandomString(5);
                int IDs = int.Parse(ID);
                Session["IMG"] = Ran + JOBTITLE;
                var TR_PJ = DbFile.ADS_PostJob.Where(a => a.ID.Equals(IDs)).FirstOrDefault();
                TR_PJ.JOB_Title = JOBTITLE;
                TR_PJ.JOB_Location = JOBLOCATION;
                if (JOBREGION != "")
                {
                    TR_PJ.JOB_Region = JOBREGION;
                }
                if (JOBTYPE != "")
                {
                    TR_PJ.JOB_Type = JOBTYPE;
                }
                TR_PJ.JOB_Description = JOBDES;
                TR_PJ.JOB_Requirement = JOBRQ;
                TR_PJ.Option_Chevron = OPTIONc;
                TR_PJ.Option_Adisorn = OPTIONa;
                TR_PJ.OPtion_Others = OPTIONo;
                TR_PJ.Company_Name = COMPANY;
                TR_PJ.Experience = EXPERIENCE;
                TR_PJ.Salary = SALARY;
                if (GENDER != "")
                {
                    TR_PJ.Gender = GENDER;
                }
                if (ImgUplode != "")
                {
                    TR_PJ.Logo_Image = Session["IMG"] + ".JPG";
                }
                TR_PJ.Status = STATUS;
                TR_PJ.JOB_UpdateDate = DateTime.Now;
                TR_PJ.User = Session["User"].ToString();
                DbFile.SaveChanges();
                return "S";

            }
            catch { return "N"; }
        }
        public string CheckUser(string USER, string PASS)
        {
            try
            {
                var User = DbFile.ADS_Master_User.Where(a => a.Mem_User.Equals(USER)).Where(b => b.Mem_Pass.Equals(PASS)).SingleOrDefault();

                if (User != null)
                {
                    Session["User"] = User.Code_Mem;
                    return User.Code_Mem;
                }
                else { return null; }
            }
            catch { return null; }
        }

    }
}