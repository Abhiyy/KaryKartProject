using DA.EmailEngine;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace AppBanwao.KaryKart.Web.Helpers
{
    public class EmailHelper
    {
        string _senderEmail = ConfigurationManager.AppSettings["SenderEmail"].ToString();
        string _adminEmail = ConfigurationManager.AppSettings["AdminEmail"].ToString();
        string _companyLogo = ConfigurationManager.AppSettings["Logo"].ToString();
        string _supportPhone = ConfigurationManager.AppSettings["ContactNumber"].ToString();

        public bool SendRegisterEmail(string toEmail)
        {
            string body;
            //Read template file from the App_Data folder
            try
            {
                using (var sr = new StreamReader(HttpContext.Current.Server.MapPath(@"\\App_Data\\EmailTemplates\RegisterConfirmation.txt")))
                {
                    body = sr.ReadToEnd();
                }
                string otp = toEmail.Substring(0,4)+CommonHelper.GenerateOTP();
                CommonHelper.SaveOTP(otp,toEmail);
                string messagebody = string.Format(body, _companyLogo, "", toEmail,otp ,  _supportPhone);
                return SendEmail("Welcome to Karrykart.com", messagebody, toEmail);
                
            }
            catch (Exception ex) {
                return false;
            }
            return false;
        }

        public bool SendVerificationEmail(string toEmail)
        {
            string body;
            //Read template file from the App_Data folder
            try
            {
                using (var sr = new StreamReader(HttpContext.Current.Server.MapPath(@"\\App_Data\\EmailTemplates\OtpVerificationEmail.txt")))
                {
                    body = sr.ReadToEnd();
                }
               
                string messagebody = string.Format(body, _companyLogo, "", toEmail, _supportPhone);
                
                return SendEmail("Karrykart.com - Email Verification Complete ", messagebody, toEmail);
                
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }

        public bool SendOtpEmail(string toEmail,string otp)
        {

            string body;
            //Read template file from the App_Data folder
            try
            {
                using (var sr = new StreamReader(HttpContext.Current.Server.MapPath(@"\\App_Data\\EmailTemplates\OtpEmail.txt")))
                {
                    body = sr.ReadToEnd();
                }

                string messagebody = string.Format(body, _companyLogo,  toEmail,otp, _supportPhone);

                return SendEmail("Karrykart.com - OTP", messagebody, toEmail);

            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }

        public bool SendPasswordChangeEmail(string toEmail)
        {

            string body;
            //Read template file from the App_Data folder
            try
            {
                using (var sr = new StreamReader(HttpContext.Current.Server.MapPath(@"\\App_Data\\EmailTemplates\ChangePasswordEmail.txt")))
                {
                    body = sr.ReadToEnd();
                }

                string messagebody = string.Format(body, _companyLogo, toEmail, _supportPhone);

                return SendEmail("Karrykart.com - Password changed successfully", messagebody, toEmail);

            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }

        bool SendEmail(string Subject, string Body, string EmailAddress)
        {
            //StringBuilder
            Mailer objEmail = new Mailer();
            objEmail.Recipient = EmailAddress;
            objEmail.Body = Body;
            objEmail.Sender = _senderEmail;
            objEmail.Subject = Subject;
            return objEmail.Send();
           
        }
    }
}