using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
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
            public int ID { get; set; }
            public string Logo_Image { get; set; }
            public string JOB_Title { get; set; }
            public string JOB_UpdateDate { get; set; }
            public string Depart { get; set; }
            public string company { get; set; }
            public string experience { get; set; }
            public string salary { get; set; }
            public int JOB_ID { get; set; }


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
                ViewBag.Titles = DbFile.ADS_Register.Where(a => a.Status_Confirm != "F").DistinctBy(x => x.Re_Position).ToList();
                ViewBag.Departs = DbFile.ADS_Master_Department.OrderBy(a => a.Department_Name).ToList();
                return View();
            }

        }

        public ActionResult Admin_Dep(string Depart)
        {
            if (Session["User"] == null)
            {
                // check if a new session id was generated
                return RedirectToAction("Index", "Home");
                //return View();
            }
            else
            {
                DateTime minusThirty = DateTime.Now.AddDays(-90);
                ViewBag.JobDepart = Depart;
                // ViewBag.PostList = DbFile.ADS_PostJob.Where(a => a.JOB_Region.Contains(Depart))
                //     .Where(a => a.Status.Equals("T")).Where(c => c.JOB_UpdateDate > minusThirty).Where(d => d.OPtion_Others.Equals("T")).OrderByDescending(b => b.JOB_UpdateDate).ToList();


                var data = (from x in DbFile.ADS_Register
                            where (x.Re_Department.Contains(Depart) || x.Re_Position.Contains(Depart)) && x.Status_Confirm != "F"
                            group x by x.Job_ID into g
                            join postjob in DbFile.ADS_PostJob on g.FirstOrDefault().Job_ID equals postjob.ID
                            select new regis
                            {
                                // position = g.Key,
                                position = postjob.JOB_Title,
                                number = g.Count(),
                                ID = postjob.ID,
                                Logo_Image = postjob.Logo_Image,
                                JOB_Title = postjob.JOB_Title,
                                JOB_UpdateDate = postjob.JOB_UpdateDate.ToString(),
                                company = postjob.Company_Name,
                                experience = postjob.Experience,
                                salary = postjob.Salary,
                                Depart = postjob.JOB_Region,//department 
                                JOB_ID = postjob.ID

                            }).ToList();
                ViewBag.JobRegis = data;

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
                            join postjob in DbFile.ADS_PostJob on g.FirstOrDefault().Re_Current_Position equals postjob.JOB_Title
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
            else
            {
                ViewBag.RegisName = DbFile.ADS_Register.Where(a => a.Re_Position.Contains(Position)).Where(a => a.Status.Equals("Regis")).FirstOrDefault();
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

        public string ChecCan(int ID)
        {

            var data = DbFile.ADS_Candidate.Where(a => a.Re_ID == ID).OrderByDescending(a => a.ID).FirstOrDefault();
            if (data == null) { return "-"; }
            else
            {
                return data.Date.ToString().Substring(0, 10) + " " + data.Time.ToString().Substring(0, 5);
            }

        }

        public string LoadRegistJoblistByPosi(int JOB_ID)
        {
            var data = (from TR_Regis in DbFile.ADS_Register
                        where TR_Regis.Job_ID == JOB_ID && TR_Regis.Status_Confirm != "F"
                        // join TR_Candi in DbFile.ADS_Candidate on TR_Regis.ID equals TR_Candi.Re_ID into TR_Candis
                        // from TR_Candi1 in TR_Candis.DefaultIfEmpty()
                        select new
                        {
                            TR_Regis.ID,
                            TR_Regis.Re_Position,
                            TR_Regis.Pic_File,
                            TR_Regis.Re_Title_EN,
                            TR_Regis.Re_Name_EN,
                            TR_Regis.Re_Surname_EN,
                            TR_Regis.Re_Title_TH,
                            TR_Regis.Re_Name_TH,
                            TR_Regis.Re_Surname_TH,
                            TR_Regis.Exp,
                            Create_Date = TR_Regis.Create_Date.ToString().Substring(0, 16),
                            TR_Regis.VC_File,
                            TR_Regis.Cer_File,
                            TR_Regis.CerWork_File,
                            TR_Regis.Status,
                            TR_Regis.Status_Appointment,
                            TR_Regis.Status_Confirm
                            // Date= ChecCan(TR_Regis.ID)
                            // Date= TR_Candi1.Date.ToString().Substring(0, 16)+" "+TR_Candi1.Time.ToString().Substring(0, 5)
                        }).AsEnumerable().Select(x => new
                        {
                            ID = x.ID,
                            Re_Position = x.Re_Position,
                            Pic_File = x.Pic_File,
                            Re_Title_EN = x.Re_Title_EN,
                            Re_Name_EN = x.Re_Name_EN,
                            Re_Surname_EN = x.Re_Surname_EN,
                            Re_Title_TH = x.Re_Title_TH,
                            Re_Name_TH = x.Re_Name_TH,
                            Re_Surname_TH = x.Re_Surname_TH,
                            Exp = x.Exp,
                            Create_Date = x.Create_Date.ToString().Substring(0, 16),
                            VC_File = x.VC_File,
                            Cer_File = x.Cer_File,
                            CerWork_File = x.CerWork_File,
                            Status = x.Status,
                            Status_Appointment = x.Status_Appointment,
                            Status_Confirm = x.Status_Confirm,
                            Date = ChecCan(x.ID)
                        }).ToList();
            string jsonlog = new JavaScriptSerializer().Serialize(data);
            return jsonlog;

        }

        public string ChangApp(int ID, DateTime DATE, string TIME, string USERAPP, string DETAIL)
        {
            if (DATE.Year < DateTime.Now.Year)
            {
                DATE = DATE.AddYears(+543);
            }
            var TR_Candidat = new ADS_Candidate();
            var TR_Regis = DbFile.ADS_Register.Where(a => a.ID.Equals(ID)).FirstOrDefault();
            TR_Candidat.Re_ID = ID;
            TR_Candidat.Date = DATE;
            TR_Candidat.Time = TimeSpan.Parse(TIME);
            TR_Candidat.UserApp = USERAPP;
            TR_Candidat.Detail = DETAIL;

            TR_Regis.Status = "App";
            TR_Regis.Status_Appointment = "App";
            TR_Regis.Appointment_Date = DATE;
            DbFile.ADS_Candidate.Add(TR_Candidat);
            DbFile.SaveChanges();
            email(DETAIL, TR_Regis.Re_Email, (TR_Regis.Re_Name_TH + " " + TR_Regis.Re_Surname_TH), DATE.ToString(), TIME);
            NotiReceiveNewItem(TR_Regis.Re_Position, TR_Regis.Re_Title_TH + TR_Regis.Re_Name_TH + " " + TR_Regis.Re_Surname_TH, DATE.ToString().Substring(0, 10) + " " + TIME);

            return "S";
        }

        public string ChangConf(int ID, string STATUS)
        {
            var TR_Regis = DbFile.ADS_Register.Where(a => a.ID.Equals(ID)).FirstOrDefault();

            TR_Regis.Status_Confirm = STATUS;
            TR_Regis.Confirm_Date = DateTime.Now;
            TR_Regis.Status = "Conf";
            DbFile.SaveChanges();
            if (STATUS == "T")
            {
                return "";
            }
            else
            {
                return TR_Regis.Job_ID.ToString();
            }
        }
        public string email(string DETAIL, string Re_Email, string Name, string Date, string Time)
        {
            var senderEmail = new MailAddress("kritsada.th@pdcable.com", "Adisorn");
            var receiverEmail = new MailAddress(Re_Email, "Receiver");
            var password = "Password1";
            var sub = "ยืนยันนัดสัมภาษณ์งาน";
            string body = DETAIL;
            body += DETAIL;
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address, password)
            };
            using (var mess = new MailMessage(senderEmail, receiverEmail)
            {
                Subject = sub,
                Body = body
            })
            {
                smtp.Send(mess);
            }
            return "";
        }
        private string NotiReceiveNewItem(string Position, string Name, string date)
        {
            string token = "UYk16LxAx1NBUZOZBSptjzwJedAjFPGsDZDFeYF2Ofc";// ADS Recruitment

            try
            {
                var request = (HttpWebRequest)WebRequest.Create("https://notify-api.line.me/api/notify");
                var postData = string.Format("message={0}", "นัดสัมภาษณ์งาน" + "\n");
                //   postData += string.Format("Barcode : " + EditP.Barcode + "\n");
                postData += string.Format("ชื่อ : " + Name + "\n");
                postData += string.Format("ตำแหน่ง : " + Position + "\n");
                postData += string.Format("วันที/เวลา นัด: " + date + "\n");
                postData += string.Format("วันที/เวลา: " + DateTime.Now + "\n");

                var data = Encoding.UTF8.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                request.Headers.Add("Authorization", "Bearer " + token);

                using (var stream = request.GetRequestStream()) stream.Write(data, 0, data.Length);
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }

        }
    }
}