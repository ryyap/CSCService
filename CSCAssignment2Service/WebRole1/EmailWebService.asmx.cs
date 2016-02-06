using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Services;

namespace WebRole1
{
    /// <summary>
    /// Summary description for EmailWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class EmailWebService : System.Web.Services.WebService
    {
        //SqlCommand custCMD = new SqlCommand();
        SqlCommand cmd = new SqlCommand();

        protected SqlConnection myConn = new SqlConnection("Data Source=DIT-NB1333932;Initial Catalog=CSC_Assignment;Integrated Security=True");

        protected SqlDataAdapter da;

        protected SqlCommandBuilder cb;
        [WebMethod()]
        public void SendActivationEmail(int userID,string txtEmail,string txtUsername)
        {
            
            string activationCode = Guid.NewGuid().ToString();

           
            string sqlText = "INSERT INTO UserActivation(UserId,ActivationCode)";
            sqlText += " VALUES(@UserId, @ActivationCode);";
            //setup the SQL in the cmd object
            cmd.CommandText = sqlText;
            cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 10);
            cmd.Parameters["@UserId"].Value = userID;
            cmd.Parameters.Add("@ActivationCode",SqlDbType.VarChar, 1000);
            cmd.Parameters["@ActivationCode"].Value = activationCode;
                cmd.Connection = myConn;
                myConn.Open();
                cmd.ExecuteNonQuery();
             

                using (MailMessage mm = new MailMessage("sender@gmail.com", txtEmail))
                {
                    mm.Subject = "Account Activation";
                    string body = "Hello " + txtUsername.Trim() + ",";
                    body += "<br /><br />Please click the following link to activate your account";
                    body += "<br /><a href = "+"http://localhost:29732/Activation.aspx?ActivationCode=" + activationCode + ">Click here to activate your account.</a>";
                    body += "<br /><br />Thanks";
                    mm.Body = body;
                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential("hariscsctest@gmail.com", "135790KLMN");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);
                }
                myConn.Close();
            }
        
         
        [WebMethod()]
        public bool ValidateActivation(string validationCode)
        {
            cmd.CommandText = "DELETE FROM UserActivation WHERE ActivationCode = @ActivationCode";
            cmd.Parameters.AddWithValue("@ActivationCode", validationCode);
            cmd.Connection = myConn;
            myConn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            myConn.Close();
            if (rowsAffected == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
    }
}
