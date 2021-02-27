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
        public ActionResult RegisterForm()
        {

            return View();
        }
        public ActionResult JobList(string JOB)
        {
            DateTime minusThirty = DateTime.Now.AddDays(-90);
            ViewBag.PostList = DbFile.ADS_PostJob.Where(a => a.JOB_Title.Contains(JOB))
                .Where(a => a.Status.Equals("T")).Where(c => c.JOB_UpdateDate > minusThirty).Where(d => d.OPtion_Others.Equals("T")).OrderByDescending(b => b.JOB_UpdateDate).ToList();
            return View();
        }
        public ActionResult JobListChevron(string JOB)
        {
            DateTime minusThirty = DateTime.Now.AddDays(-90);
            ViewBag.PostList = DbFile.ADS_PostJob.Where(a => a.JOB_Title.Contains(JOB))
                .Where(a => a.Status.Equals("T")).Where(c => c.JOB_UpdateDate > minusThirty).OrderByDescending(b => b.JOB_UpdateDate).ToList();
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
        public string LoadRegistJoblist(int IDJob)
        {

            var data = (from TR_JobP in DbFile.ADS_PostJob
                        where TR_JobP.ID.Equals(IDJob)
                        select new
                        {
                            TR_JobP.ID,
                            TR_JobP.JOB_Title,
                            TR_JobP.JOB_Location,
                            TR_JobP.Salary,
                            TR_JobP.JOB_Region
                        }).ToList();
            string jsonlog = new JavaScriptSerializer().Serialize(data);
            return jsonlog;

        } 
        public string SaveRegist(string TitleEN, string NameEN,
            string SurnameEN, string TitleTH, string NameTH, string SurnameTH,
            string BDate,string Sex, string Phone, string Email, string PositionOld, string Address,string datacv,
            string datapic,string datacer,string Agree,string Experience, int JobID)
        {
            try
            {
                //Session["IMG"] = JOBTITLE;
              //  UploadFiles(formDataCV,CV);
                var Register = new ADS_Register();
                Register.Re_Title_EN = TitleEN;
                Register.Re_Name_EN = NameEN;
                Register.Re_Surname_EN = SurnameEN;
                Register.Re_Title_TH = TitleTH;
                Register.Re_Name_TH = NameTH;
                Register.Re_Surname_TH = SurnameTH;
                Register.BirthDays = BDate;
                Register.Sex = Sex;
                Register.Re_Phone = Phone;
                Register.Re_Email = Email;
                Register.Re_Current_Position=PositionOld;
                Register.Address = Address;
                Register.Status = "Regis";
                Register.Status_Appointment = null;
                Register.Status_Confirm = null;
                Register.Create_Date = DateTime.Now;
                Register.VC_File = "http://mmbkk.dyndns.org:8084/ads-JOB/Content/images/DOC/" + datacv;
                Register.Pic_File = datapic;
                Register.Cer_File = "http://mmbkk.dyndns.org:8084/ads-JOB/Content/images/DOC/" + datacer;
                Register.Check = Agree;
                Register.Exp = Experience;
                Register.Job_ID = JobID;
                DbFile.ADS_Register.Add(Register);
                DbFile.SaveChanges(); 
                return "S";

            }
            catch { return "N"; }
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public string UploadFiles(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
                try
                {
                    var Ran = RandomString(10);
                    string path = Path.Combine(Server.MapPath("~/Content/images/DOC"),
                    Path.GetFileName(Ran + file.FileName));
                    file.SaveAs(path);


                    return Ran + file.FileName;
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();

                    return ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";

                return "You have not specified a file.";

            }


        }


    }
}