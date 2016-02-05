using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebRole1
{
    /// <summary>
    /// Summary description for ImgWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ImgWebService : System.Web.Services.WebService
    {
        //Data Source=DIT-NB1334607\\SQLEXPRESS;Initial Catalog=CSC_Assignment;Integrated Security=True
        //Data Source=DIT-NB1333932;Initial Catalog=CSC_Assignment;Integrated Security=True
        //SqlCommand custCMD = new SqlCommand();
        SqlCommand cmd = new SqlCommand();

        protected SqlConnection myConn = new SqlConnection("Data Source=DIT-NB1333932;Initial Catalog=CSC_Assignment;Integrated Security=True");

        protected SqlDataAdapter da = new SqlDataAdapter();

        protected SqlCommandBuilder cb;

        public class image
        {

            public int ID { get; set; }
            public string UploadedBy { get; set; }
            public byte[] Image { get; set; }
            public DateTime CreatedAt { get; set; }



        }

        [WebMethod()]
        public int deleteOneImageRecord(int userid)
        {
            int numOfRecordsAffected = 0;

            string sqlText = "DELETE Image ";
            sqlText += " WHERE ID=@inID";
            //Prepare a valid DELETE SQL statement
            cmd.CommandText = sqlText;//setup the SQL in the cmd object
            cmd.Parameters.Add("@inID", SqlDbType.Int);
            cmd.Parameters["@inID"].Value = userid;
            cmd.Connection = myConn;
            myConn.Open();
            numOfRecordsAffected = cmd.ExecuteNonQuery();
            myConn.Close();
            return numOfRecordsAffected;
        }


        [WebMethod()]

        public byte[] getImagesByID(int userid)
        {
            byte[] btImage = null;
            System.Data.DataSet workDS = new System.Data.DataSet();
            string sqlText = "Select * from Image where ID=@inID";
            cmd.Connection = myConn;
            da.SelectCommand = cmd;
            cmd.CommandText = sqlText;
            //select latest 10 image here
            //da = new SqlDataAdapter("select * from Image where ID=@inID", myConn);
            cmd.Parameters.Add("@inID", SqlDbType.Int, 100);
            cmd.Parameters["@inID"].Value = userid;
            myConn.Open();
            da.Fill(workDS, "IMGTable");
            myConn.Close();
            btImage = (byte[])workDS.Tables[0].Rows[0][2];
            return btImage;
        }


        [WebMethod()]
        public int addImage(int inUploadedBy, byte[] inImageData, string uri)
        {
            int numOfRecordsAffected = 0;
            //Prepare a INSERT SQL template
            string sqlText = "INSERT INTO Image(UploadedBy,ImageData,URI)";
            sqlText += " VALUES (@inUploadedBy,@inImageData,@inURI);";
            //setup the SQL in the cmd object
            cmd.CommandText = sqlText;
            cmd.Parameters.Add("@inUploadedBy", SqlDbType.Int);
            cmd.Parameters["@inUploadedBy"].Value = inUploadedBy;
            cmd.Parameters.Add("@inImageData", SqlDbType.Image);
            cmd.Parameters["@inImageData"].Value = inImageData;
            cmd.Parameters.Add("@inURI", SqlDbType.VarChar);
            cmd.Parameters["@inURI"].Value = uri;
            cmd.Connection = myConn;
            myConn.Open();
            numOfRecordsAffected = cmd.ExecuteNonQuery();
            myConn.Close();

            return numOfRecordsAffected;
        }//addImage() method


        [WebMethod()]

        public DataSet getTenRecentImages()
        {

            DataSet workDS = new DataSet();
            string sqlText = "select * from Image";
            cmd.Connection = myConn;
            da.SelectCommand = cmd;
            cmd.CommandText = sqlText;
            myConn.Open();
            da.Fill(workDS, "IMGTable");
            myConn.Close();
            return workDS;

        }

        [WebMethod()]

        public DataSet getRecordsByUser(int userid)
        {
    
            DataSet workDS = new DataSet();
            string sqlText = "Select * from Image where UploadedBy=@inUploadedBy";
            cmd.Connection = myConn;
            da.SelectCommand = cmd;
            cmd.CommandText = sqlText;
            //select latest 10 image here
            //da = new SqlDataAdapter("select * from Image where ID=@inID", myConn);
            cmd.Parameters.Add("@inUploadedBy", SqlDbType.Int, 100);
            cmd.Parameters["@inUploadedBy"].Value = userid;
            myConn.Open();
            da.Fill(workDS, "IMGTable");
            myConn.Close();
            return workDS;
        }




        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
    }
}
