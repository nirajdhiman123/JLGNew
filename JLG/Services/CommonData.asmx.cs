using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace JLG.Services
{
    /// <summary>
    /// Summary description for CommonData
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CommonData : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod(EnableSession = true)]
        public string DeleteArchiveData(string fromdate, string todate)
        {
            string msg = string.Empty;
            SqlDataReader dbRdr = null;
            SqlCommand dbCmd = null;
            SqlConnection Con = null;
            string results = string.Empty;

            try
            {
                ClsUser objuser = new ClsUser();
                objuser = (ClsUser)Session["objUser"];

                Con = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString);
                Con.Open();

                dbCmd = new SqlCommand();
                dbCmd.Connection = Con;
                dbCmd.CommandType = CommandType.StoredProcedure;

                dbCmd.CommandText = "DeleteArchiveData";
                dbCmd.Parameters.Add("@SP_FromDate", SqlDbType.VarChar, 20).Value = fromdate == "" ? (object)DBNull.Value : fromdate;
                dbCmd.Parameters.Add("@SP_ToDate", SqlDbType.VarChar, 20).Value = todate == "" ? (object)DBNull.Value : todate;
                dbCmd.Parameters.Add("@SP_UserId", SqlDbType.Int, 0).Value = objuser.UserId == 0 ? 0 : objuser.UserId;
                dbCmd.Parameters.Add(new SqlParameter("@SP_MESSAGE", SqlDbType.VarChar, 100)).Direction = ParameterDirection.Output;

                dbRdr = dbCmd.ExecuteReader();

                msg = dbCmd.Parameters["@SP_MESSAGE"].Value.ToString();


            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }
    }
}
