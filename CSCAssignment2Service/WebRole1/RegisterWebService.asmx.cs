using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Services;

namespace WebRole1
{
    /// <summary>
    /// Summary description for RegisterWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class RegisterWebService : System.Web.Services.WebService

        //"Data Source=DIT-NB1334607\\SQLEXPRESS;Initial Catalog=CSC_Assignment;Integrated Security=True"
    //Data Source=DIT-NB1333932;Initial Catalog=CSC_Assignment;Integrated Security=True;
    {
        SqlConnection myConn = new SqlConnection("Data Source=DIT-NB1333932;Initial Catalog=CSC_Assignment;Integrated Security=True;");
         SqlDataAdapter userDA;
         SqlCommandBuilder userCB;
         SqlDataReader userDR;
         SqlCommand cmd = new SqlCommand();
        [WebMethod]
        public bool RegisterUser(string username,string password,string dateofbirth,string email)
        {
            string sqlText = " IF NOT EXISTS (SELECT UserName,Email FROM [CSC_Assignment].[dbo].[User] WHERE UserName = @UserName OR Email=@Email) BEGIN INSERT INTO [CSC_Assignment].[dbo].[User] (Username,Password,DateOfBirth,Email) VALUES (@UserName,@Password,@DateOfBirth,@Email)  END;";
      cmd.CommandText = sqlText;
      cmd.Connection = myConn;
            cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 10);
            cmd.Parameters["@UserName"].Value = username;
            cmd.Parameters.Add("@Password", SqlDbType.VarChar, 10);
            cmd.Parameters["@Password"].Value = password;
            cmd.Parameters.Add("@DateOfBirth", SqlDbType.VarChar, 10);
            cmd.Parameters["@DateOfBirth"].Value = dateofbirth;
            cmd.Parameters.Add("@Email", SqlDbType.VarChar, 30);
            cmd.Parameters["@Email"].Value = email;
            myConn.Open();
           int x= cmd.ExecuteNonQuery();
           if (x > 0)
           {
               myConn.Close();
               return true;
           }
           else
           {
               myConn.Close();
               return false;
           }
           
    
          
        }
        [WebMethod()]
        public bool UserLogin(string email, string password)
        {
            try
            {
              
                cmd = new SqlCommand(
                    "SELECT * from [CSC_Assignment].[dbo].[User] where email=@email " + "and password= @password");
 
    
                SqlParameter param1 = new SqlParameter();
                param1.SqlDbType = SqlDbType.NVarChar;
                param1.ParameterName = "@email";
                param1.Value = email;
 
                SqlParameter param2 = new SqlParameter();
                param2.SqlDbType = SqlDbType.NVarChar;
                param2.ParameterName = "@password";
                param2.Value = password;
 
               
                cmd.Parameters.Add(param1); 
                cmd.Parameters.Add(param2);
 
 
                myConn.Open();
                cmd.Connection = myConn;
             userDR= cmd.ExecuteReader();
             if (userDR.HasRows)
             {
                 while (userDR.Read())
                 {
                     return true;
                 }
             }
             else
             {
                 return false;
             }
             userDR.Close();
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message);
                
            }
 
            myConn.Close();
            return true;
        }

    }
}
