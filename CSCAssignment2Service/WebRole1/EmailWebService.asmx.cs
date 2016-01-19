using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
        SqlCommand cmd;

        protected SqlConnection myConn = new SqlConnection("Data Source=DIT-NB1334607\\SQLEXPRESS;Initial Catalog=CSC_Assignment;Integrated Security=True");

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
            da = new SqlDataAdapter("select * from Image order by ID desc limit 10", myConn);

            da.Fill(workDS, "IMGTable");

            return workDS;

        }



        [WebMethod()]

        public System.Data.DataSet getImagesByID(int userid)
        {

            System.Data.DataSet workDS = new System.Data.DataSet();

            myConn.Open();

            //select latest 10 image here
            da = new SqlDataAdapter("select * from Image where ID=userid", myConn);

            da.Fill(workDS, "IMGTable");

            return workDS;

        }
        [WebMethod()]
        public int addImage(String inUploadedBy, byte[] inImageData)
        {
            int numOfRecordsAffected = 0;

            

            //Prepare a INSERT SQL template
            string sqlText = "INSERT INTO Image(UploadedBy,inImageData)";
            sqlText += " VALUES (@inUploadedBy,@inImageData)";
            //setup the SQL in the cmd object
            cmd.CommandText = sqlText;
            cmd.Parameters.Add("@inFullName", SqlDbType.VarChar, 100);
            cmd.Parameters["@inFullName"].Value = inUploadedBy;
            cmd.Parameters.Add("@inImageData", SqlDbType.Image);
            cmd.Parameters["@inImageData"].Value = inImageData;
            myConn.Open();
            numOfRecordsAffected = cmd.ExecuteNonQuery();
            myConn.Close();

            return numOfRecordsAffected;
        }//addImage() method


       



        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
    }
}
