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

        public class image
        {

            public int ID { get; set; }
            public string UploadedBy { get; set; }
            public byte[] Image { get; set; }
            public DateTime CreatedAt { get; set; }

          

        }

       

        [WebMethod()]

        public System.Data.DataSet getTenRecentImages()
        {

            System.Data.DataSet workDS = new System.Data.DataSet();

            myConn.Open();

            //select latest 10 image here
           // da = new SqlDataAdapter("select * from Image order by ID desc limit 10", myConn);

            string sqlText = "select * from Image where ID=@inID";
            cmd.CommandText = sqlText;

            myConn.Open();
            da.Fill(workDS, "IMGTable");
            myConn.Open();

            return workDS;

        }



        [WebMethod()]

        public System.Data.DataSet getImagesByID(int userid)
        {

            System.Data.DataSet workDS = new System.Data.DataSet();

            string sqlText = "select * from Image where ID=@inID";
            cmd.CommandText = sqlText;



            //select latest 10 image here
            //da = new SqlDataAdapter("select * from Image where ID=@inID", myConn);
            
            cmd.Parameters.Add("@inID", SqlDbType.Int, 100);
            cmd.Parameters["@inID"].Value = userid;
            myConn.Open();
            da.Fill(workDS, "IMGTable");
            myConn.Open();
            

            return workDS;

        }
        [WebMethod()]
        public int addImage(string inUploadedBy, byte[] inImageData)
        {
            int numOfRecordsAffected = 0;

            

            //Prepare a INSERT SQL template
            string sqlText = "INSERT INTO Image(inUploadedBy,inImageData)";
            sqlText += " VALUES (@inUploadedBy,@inImageData);";
            //setup the SQL in the cmd object
            cmd.CommandText = sqlText;
            cmd.Parameters.Add("@inUploadedBy", SqlDbType.VarChar, 100);
            cmd.Parameters["@inUploadedBy"].Value = inUploadedBy;
            cmd.Parameters.Add("@inImageData", SqlDbType.Image);
            cmd.Parameters["@inImageData"].Value = inImageData;
            myConn.Open();
            numOfRecordsAffected = cmd.ExecuteNonQuery();
            myConn.Close();

            return numOfRecordsAffected;
        }//addImage() method


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
