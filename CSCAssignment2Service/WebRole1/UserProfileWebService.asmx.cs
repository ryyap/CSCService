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
    /// Summary description for UserProfileWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class UserProfileWebService : System.Web.Services.WebService
    {
        SqlCommand custCMD = new SqlCommand();
        SqlCommand cmd = new SqlCommand();

        protected SqlConnection myConn = new SqlConnection("Data Source=DIT-NB1333932;Initial Catalog=CSC_Assignment;Integrated Security=True");

        protected SqlDataAdapter da = new SqlDataAdapter();

        protected SqlCommandBuilder cb;



        [WebMethod()]
        public DataSet getUserProfile(int userid)
        {
            DataSet workDS = new DataSet();
            string sqlText = "Select * from [CSC_Assignment].[dbo].[User] where ID=@inID";
            cmd.Connection = myConn;
            da.SelectCommand = cmd;
            cmd.CommandText = sqlText;
            //select latest 10 image here
            //da = new SqlDataAdapter("select * from Image where ID=@inID", myConn);
            
            cmd.Parameters.Add("@inID", SqlDbType.Int, 100);
            cmd.Parameters["@inID"].Value = userid;
            myConn.Open();
            da.Fill(workDS,"userTable");
            myConn.Close();
            return workDS;
        }


        [WebMethod()]
       public int updateUserProfile(int userid, string password, string username, string dob, string email )
{
            int numOfRecordsAffected = 0;
DataSet workDS = new DataSet();
string sqlText = "UPDATE [CSC_Assignment].[dbo].[User] SET Username=@inUsername,DateOfBirth=@inDateOfBirth,Email=@inEmail Where ";
sqlText += " ID=@inID and Password=@inPassword";
cmd.Connection = myConn;
da.SelectCommand = cmd;
cmd.CommandText = sqlText;
cmd.Parameters.Add("@inUsername", SqlDbType.VarChar, 100);
cmd.Parameters["@inUsername"].Value = username;
cmd.Parameters.Add("@inDateOfBirth", SqlDbType.VarChar, 100);
cmd.Parameters["@inDateOfBirth"].Value = dob;
cmd.Parameters.Add("@inEmail", SqlDbType.VarChar, 100);
cmd.Parameters["@inEmail"].Value = email;
cmd.Parameters.Add("@inID", SqlDbType.Int);
cmd.Parameters["@inID"].Value = userid;
cmd.Parameters.Add("@inPassword", SqlDbType.VarChar, 100);
cmd.Parameters["@inPassword"].Value = password;
        
myConn.Open();
numOfRecordsAffected = cmd.ExecuteNonQuery();
myConn.Close();
return numOfRecordsAffected;
}



        [WebMethod()]
        public int changePassword(int userid, string password, string newpass)
        {
            int numOfRecordsAffected = 0;
            DataSet workDS = new DataSet();
            string sqlText = "UPDATE [CSC_Assignment].[dbo].[User] SET Password=@inNewPassword Where ";
            sqlText += " ID=@inID and Password=@inPassword";
            cmd.Connection = myConn;
            da.SelectCommand = cmd;
            cmd.CommandText = sqlText;
            cmd.Parameters.Add("@inNewPassword", SqlDbType.VarChar, 100);
            cmd.Parameters["@inNewPassword"].Value = newpass;
            cmd.Parameters.Add("@inID", SqlDbType.Int);
            cmd.Parameters["@inID"].Value = userid;
            cmd.Parameters.Add("@inPassword", SqlDbType.VarChar, 100);
            cmd.Parameters["@inPassword"].Value = password;

            myConn.Open();
            numOfRecordsAffected = cmd.ExecuteNonQuery();
            myConn.Close();
            return numOfRecordsAffected;
        }

    }
}
