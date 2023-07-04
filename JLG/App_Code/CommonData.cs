using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace JLG
{
    public class CommonData
    {
        public static DataTable CheckUserLogin(List<string> LoginUser)
        {
            SqlDataAdapter dbRdr = new SqlDataAdapter();
            SqlCommand dbCmd = null;
            SqlConnection dbCon = null;
            DataSet dsData = new DataSet();
            try
            {
                dbCon = CommonDB.OpenConnection();
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbCon;
                dbCmd.CommandType = CommandType.StoredProcedure;

                dbCmd.CommandText = "Sys_CheckLogin";
                dbCmd.Parameters.Add("@SP_LOGINTEXT", SqlDbType.VarChar, 300).Value = LoginUser[0] == "" ? (object)DBNull.Value : LoginUser[0];
                dbCmd.Parameters.Add("@SP_LOGINPASSWORD", SqlDbType.VarChar, 20).Value = LoginUser[1] == "" ? (object)DBNull.Value : LoginUser[1];
                dbRdr.SelectCommand = dbCmd;
                dbRdr.Fill(dsData);

            }
            catch (Exception ex)
            {
                throw ex;
                // MessageBox.Show("Error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dbRdr != null)
                {
                    dbRdr.Dispose();
                }
                if (dbCmd != null)
                {
                    dbCmd.Dispose();
                }
                CommonDB.CloseConnection(dbCon);
            }
            return dsData.Tables[0];

        }

        public static DataTable UserMenuRight(int UserId)
        {
            SqlDataAdapter dbRdr = new SqlDataAdapter();
            SqlCommand dbCmd = null;
            SqlConnection dbCon = null;
            DataSet dsData = new DataSet();
            try
            {
                dbCon = CommonDB.OpenConnection();
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbCon;
                dbCmd.CommandType = CommandType.StoredProcedure;

                dbCmd.CommandText = "Sys_UserMenuRights";
                dbCmd.Parameters.Add("@SP_LOGINID", SqlDbType.Int).Value = UserId == 0 ? (object)DBNull.Value : UserId;
                dbRdr.SelectCommand = dbCmd;
                dbRdr.Fill(dsData);

            }
            catch (Exception ex)
            {
                throw ex;
                // MessageBox.Show("Error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dbRdr != null)
                {
                    dbRdr.Dispose();
                }
                if (dbCmd != null)
                {
                    dbCmd.Dispose();
                }
                CommonDB.CloseConnection(dbCon);
            }
            return dsData.Tables[0];

        }

        public static DataTable UserRights(int UserId)
        {
            SqlDataAdapter dbRdr = new SqlDataAdapter();
            SqlCommand dbCmd = null;
            SqlConnection dbCon = null;
            DataSet dsData = new DataSet();
            try
            {
                dbCon = CommonDB.OpenConnection();
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbCon;
                dbCmd.CommandType = CommandType.StoredProcedure;

                dbCmd.CommandText = "Sys_UserRights";
                dbCmd.Parameters.Add("@SP_LOGINID", SqlDbType.Int).Value = UserId == 0 ? (object)DBNull.Value : UserId;
                dbRdr.SelectCommand = dbCmd;
                dbRdr.Fill(dsData);

            }
            catch (Exception ex)
            {
                throw ex;
                // MessageBox.Show("Error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dbRdr != null)
                {
                    dbRdr.Dispose();
                }
                if (dbCmd != null)
                {
                    dbCmd.Dispose();
                }
                CommonDB.CloseConnection(dbCon);
            }
            return dsData.Tables[0];

        }

        public static String Changepassword(List<string> UserData)
        {
            SqlDataReader dbRdr = null;
            SqlCommand dbCmd = null;
            SqlConnection dbCon = null;
            string results = string.Empty;
            try
            {
                dbCon = CommonDB.OpenConnection();
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbCon;
                dbCmd.CommandType = CommandType.StoredProcedure;

                dbCmd.CommandText = "Sys_ChangePassword";
                dbCmd.Parameters.Add("@SP_USERID", SqlDbType.Int).Value = UserData[0] == "" ? (object)DBNull.Value : UserData[0];
                dbCmd.Parameters.Add("@SP_NEWPASSWORD", SqlDbType.VarChar, 20).Value = UserData[1] == "" ? (object)DBNull.Value : UserData[1];
                dbCmd.Parameters.Add("@SP_OLDPASSWORD", SqlDbType.VarChar, 20).Value = UserData[2] == "" ? (object)DBNull.Value : UserData[2];
                dbCmd.Parameters.Add(new SqlParameter("@SP_MESSAGE", SqlDbType.VarChar, 500)).Direction = ParameterDirection.Output;
                dbRdr = dbCmd.ExecuteReader();

                results = dbCmd.Parameters["@SP_MESSAGE"].Value.ToString();

            }

            catch (Exception ex)
            {
                throw ex;
                // MessageBox.Show("Error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dbRdr != null)
                {
                    dbRdr.Dispose();
                }
                if (dbCmd != null)
                {
                    dbCmd.Dispose();
                }
                CommonDB.CloseConnection(dbCon);
            }
            return results;

        }

        public static DataTable GetUserDetails(string LoginText)
        {
            SqlDataAdapter dbRdr = new SqlDataAdapter();
            SqlCommand dbCmd = null;
            SqlConnection dbCon = null;
            DataSet dsData = new DataSet();
            try
            {
                dbCon = CommonDB.OpenConnection();
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbCon;
                dbCmd.CommandType = CommandType.StoredProcedure;

                dbCmd.CommandText = "USP_DETAILSUSER";
                dbCmd.Parameters.Add("@SP_LoginText", SqlDbType.VarChar, 100).Value = LoginText;
                dbRdr.SelectCommand = dbCmd;
                dbRdr.Fill(dsData);

            }
            catch (Exception ex)
            {
                throw ex;
                // MessageBox.Show("Error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dbRdr != null)
                {
                    dbRdr.Dispose();
                }
                if (dbCmd != null)
                {
                    dbCmd.Dispose();
                }
                CommonDB.CloseConnection(dbCon);
            }
            return dsData.Tables[0];

        }

        public static DataTable UserDisplayData(int UserId)
        {
            SqlDataAdapter dbRdr = new SqlDataAdapter();
            SqlCommand dbCmd = null;
            SqlConnection dbCon = null;
            DataSet dsData = new DataSet();
            try
            {
                dbCon = CommonDB.OpenConnection();
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbCon;
                dbCmd.CommandType = CommandType.StoredProcedure;

                dbCmd.CommandText = "USP_GETDETAILSUSER";
                dbCmd.Parameters.Add("@USERID", SqlDbType.Int).Value = UserId == 0 ? (object)DBNull.Value : UserId;
                dbRdr.SelectCommand = dbCmd;
                dbRdr.Fill(dsData);

            }
            catch (Exception ex)
            {
                throw ex;
                // MessageBox.Show("Error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dbRdr != null)
                {
                    dbRdr.Dispose();
                }
                if (dbCmd != null)
                {
                    dbCmd.Dispose();
                }
                CommonDB.CloseConnection(dbCon);
            }
            return dsData.Tables[0];

        }

        internal static string CheckExistingUser(string loginText)
        {
            string readUserName = "";
            string resUser = "";
            SqlDataReader dbRdrUser = null;
            SqlCommand dbCmdUser = null;
            SqlConnection dbConUser = null;
            
            try
            {
                dbConUser = CommonDB.OpenConnection();
                dbCmdUser = new SqlCommand("SELECT Login_Text FROM SYS_USERMASTER Where Login_Text = '" + loginText + "'", dbConUser);               
                dbRdrUser = dbCmdUser.ExecuteReader();
                if (dbRdrUser.HasRows)
                {
                    dbRdrUser.Read();
                    resUser = dbRdrUser["Login_Text"].ToString();
                }
                else
                {
                    resUser = "";
                }
                //resUser = dbRdrUser["Login_Text"].ToString();
                //if(dbRdrUser["Login_Text"].ToString() == "")
                //{
                    
                //}
                //else
                //{
                    
                //}
            }

            catch (Exception ex)
            {
                throw ex;
                // MessageBox.Show("Error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dbRdrUser != null)
                {
                    dbRdrUser.Dispose();
                }
                if (dbCmdUser != null)
                {
                    dbCmdUser.Dispose();
                }
                CommonDB.CloseConnection(dbConUser);
            }
            return resUser;
        }

        public static String[] SaveUserData(List<string> UserData)
        {
            SqlDataReader dbRdr = null;
            SqlCommand dbCmd = null;
            SqlConnection dbCon = null;
            string[] results = new string[2];
            try
            {
                dbCon = CommonDB.OpenConnection();
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbCon;
                dbCmd.CommandType = CommandType.StoredProcedure;

                dbCmd.CommandText = "Sys_UserMasterAddEdit";
                dbCmd.Parameters.Add("@SP_USERID", SqlDbType.Int, 0).Value = UserData[0] == "" ? (object)DBNull.Value : UserData[0];
                dbCmd.Parameters.Add("@SP_EMPID", SqlDbType.Int, 0).Value = UserData[1] == "" ? (object)DBNull.Value : UserData[1];
                dbCmd.Parameters.Add("@SP_EMPNAME", SqlDbType.VarChar, 300).Value = UserData[2] == "" ? (object)DBNull.Value : UserData[2];
                dbCmd.Parameters.Add("@SP_LOGIN_TEXT", SqlDbType.VarChar, 100).Value = UserData[3] == "" ? (object)DBNull.Value : UserData[3];
                dbCmd.Parameters.Add("@SP_LOGINPASSWORD", SqlDbType.VarChar, 20).Value = UserData[4] == "" ? (object)DBNull.Value : UserData[4];
                dbCmd.Parameters.Add("@SP_USER_TYPE", SqlDbType.VarChar, 10).Value = UserData[5] == "" ? (object)DBNull.Value : UserData[5];
                dbCmd.Parameters.Add("@SP_LOGINID", SqlDbType.Int, 0).Value = UserData[6] == "" ? (object)DBNull.Value : UserData[6];

                dbCmd.Parameters.Add(new SqlParameter("@SP_MESSAGE", SqlDbType.VarChar, 500)).Direction = ParameterDirection.Output;
                dbCmd.Parameters.Add(new SqlParameter("@SP_RETURNVALUE", SqlDbType.SmallInt, 0)).Direction = ParameterDirection.Output;
                //dbCmd.Parameters.Add(new SqlParameter("@RETURNSTATUS", SqlDbType.SmallInt, 0)).Direction = ParameterDirection.Output;

                dbRdr = dbCmd.ExecuteReader();

                results[0] = dbCmd.Parameters["@SP_MESSAGE"].Value.ToString();
                results[1] = dbCmd.Parameters["@SP_RETURNVALUE"].Value.ToString();

            }

            catch (Exception ex)
            {
                throw ex;
                // MessageBox.Show("Error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dbRdr != null)
                {
                    dbRdr.Dispose();
                }
                if (dbCmd != null)
                {
                    dbCmd.Dispose();
                }
                CommonDB.CloseConnection(dbCon);
            }
            return results;

        }

        public static String InsertUser(ClsUser UserData)
        {
            SqlDataReader dbRdr = null;
            SqlCommand dbCmd = null;
            SqlConnection dbCon = null;
            string[] results = new string[2];
            try
            {
                dbCon = CommonDB.OpenConnection();
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbCon;
                dbCmd.CommandType = CommandType.StoredProcedure;

                dbCmd.CommandText = "USP_INSERTUSER";
                dbCmd.Parameters.Add("@EmpName", SqlDbType.VarChar, 100).Value = UserData.EmpName == "" ? (object)DBNull.Value : UserData.EmpName;
                dbCmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 100).Value = UserData.FirstName == "" ? (object)DBNull.Value : UserData.FirstName;
                dbCmd.Parameters.Add("@LastName", SqlDbType.VarChar, 100).Value = UserData.LastName == "" ? (object)DBNull.Value : UserData.LastName;
                dbCmd.Parameters.Add("@LoginText", SqlDbType.VarChar, 100).Value = UserData.LoginText == "" ? (object)DBNull.Value : UserData.LoginText;
                dbCmd.Parameters.Add("@EmpCode", SqlDbType.VarChar, 300).Value = UserData.EmpCode == "" ? (object)DBNull.Value : UserData.EmpCode;
                dbCmd.Parameters.Add("@Login_Password", SqlDbType.VarChar, 300).Value = UserData.Login_Password == "" ? (object)DBNull.Value : UserData.Login_Password;
                dbCmd.Parameters.Add("@EmailId", SqlDbType.VarChar, 100).Value = UserData.EmailID == "" ? (object)DBNull.Value : UserData.EmailID;
                dbCmd.Parameters.Add("@IsActive", SqlDbType.VarChar, 20).Value = UserData.IsActive == "" ? (object)DBNull.Value : UserData.IsActive;
                dbCmd.Parameters.Add("@GroupId", SqlDbType.Int, 0).Value = UserData.GroupId == 0 ? (object)DBNull.Value : UserData.GroupId;
                dbCmd.Parameters.Add("@BranchID", SqlDbType.Int, 0).Value = UserData.BranchId == 0 ? (object)DBNull.Value : UserData.BranchId;
                dbCmd.Parameters.Add("@BranchList", SqlDbType.VarChar, 300).Value = UserData.BranchList == "" ? (object)DBNull.Value : UserData.BranchList;
                dbCmd.Parameters.Add("@UserType", SqlDbType.Char, 1).Value = UserData.UserType == "" ? (object)DBNull.Value : UserData.UserType;
                dbCmd.Parameters.Add("@UserId", SqlDbType.Int, 0).Value = UserData.UserId == 0 ? (object)DBNull.Value : UserData.UserId;
                dbCmd.Parameters.Add("@reset", SqlDbType.VarChar, 50).Value = UserData.reset == "" ? (object)DBNull.Value : UserData.reset;
                dbCmd.Parameters.Add("@UserStatus", SqlDbType.VarChar, 50).Value = "B";
                dbCmd.Parameters.Add("@dashboard", SqlDbType.VarChar, 50).Value = UserData.dashboard == "" ? (object)DBNull.Value : UserData.dashboard;
                dbCmd.Parameters.Add("@MobileNo", SqlDbType.VarChar, 20).Value = UserData.MobileNo == "" ? (object)DBNull.Value : UserData.MobileNo;


                ///
                //////////////////////////////////////////////////// Changes Done By Kapil Singhal on 9th Dec. 2022 //////////////////////////////////////////////////////////////////

                dbCmd.Parameters.Add("@inactivationDate", SqlDbType.DateTime).Value = UserData.inActivationDate == DateTime.MinValue ? (object)DBNull.Value : UserData.inActivationDate;
                dbCmd.Parameters.Add("@inActivationDone_By", SqlDbType.Int).Value = UserData.inActivationDone_By == 0 ? (object)DBNull.Value : UserData.inActivationDone_By;

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///


                dbCmd.Parameters.Add(new SqlParameter("@SP_MESSAGE", SqlDbType.VarChar, 500)).Direction = ParameterDirection.Output;
                dbCmd.Parameters.Add(new SqlParameter("@SP_RETURNVALUE", SqlDbType.SmallInt, 0)).Direction = ParameterDirection.Output;
                //dbCmd.Parameters.Add(new SqlParameter("@RETURNSTATUS", SqlDbType.SmallInt, 0)).Direction = ParameterDirection.Output;

                dbRdr = dbCmd.ExecuteReader();

                results[0] = dbCmd.Parameters["@SP_MESSAGE"].Value.ToString();
                results[1] = dbCmd.Parameters["@SP_RETURNVALUE"].Value.ToString();

            }

            catch (Exception ex)
            {
                throw ex;
                // MessageBox.Show("Error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dbRdr != null)
                {
                    dbRdr.Dispose();
                }
                if (dbCmd != null)
                {
                    dbCmd.Dispose();
                }
                CommonDB.CloseConnection(dbCon);
            }
            return results[0];

        }

        public static String[] DeleteUserRights(int UserId)
        {
            SqlDataReader dbRdr = null;
            SqlCommand dbCmd = null;
            SqlConnection dbCon = null;
            string[] results = new string[2];
            try
            {
                dbCon = CommonDB.OpenConnection();
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbCon;
                dbCmd.CommandType = CommandType.StoredProcedure;

                dbCmd.CommandText = "Sys_UserMenuRightsDelete";
                dbCmd.Parameters.Add("@SP_USERID", SqlDbType.Int).Value = UserId == 0 ? (object)DBNull.Value : UserId;

                dbCmd.Parameters.Add(new SqlParameter("@SP_MESSAGE", SqlDbType.VarChar, 500)).Direction = ParameterDirection.Output;
                dbCmd.Parameters.Add(new SqlParameter("@SP_RETURNVALUE", SqlDbType.SmallInt, 0)).Direction = ParameterDirection.Output;
                //dbCmd.Parameters.Add(new SqlParameter("@RETURNSTATUS", SqlDbType.SmallInt, 0)).Direction = ParameterDirection.Output;

                dbRdr = dbCmd.ExecuteReader();

                results[0] = dbCmd.Parameters["@SP_MESSAGE"].Value.ToString();
                results[1] = dbCmd.Parameters["@SP_RETURNVALUE"].Value.ToString();

            }

            catch (Exception ex)
            {
                throw ex;
                // MessageBox.Show("Error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dbRdr != null)
                {
                    dbRdr.Dispose();
                }
                if (dbCmd != null)
                {
                    dbCmd.Dispose();
                }
                CommonDB.CloseConnection(dbCon);
            }
            return results;

        }

        public static String[] SaveUserRights(List<string> UserData)
        {
            SqlDataReader dbRdr = null;
            SqlCommand dbCmd = null;
            SqlConnection dbCon = null;
            string[] results = new string[2];
            try
            {
                dbCon = CommonDB.OpenConnection();
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbCon;
                dbCmd.CommandType = CommandType.StoredProcedure;

                dbCmd.CommandText = "Sys_UserMenuRightsAdd";
                dbCmd.Parameters.Add("@SP_USERID", SqlDbType.Int, 0).Value = UserData[0] == "" ? (object)DBNull.Value : UserData[0];
                dbCmd.Parameters.Add("@SP_MENUID", SqlDbType.VarChar, 10).Value = UserData[1] == "" ? (object)DBNull.Value : UserData[1];

                dbCmd.Parameters.Add(new SqlParameter("@SP_MESSAGE", SqlDbType.VarChar, 500)).Direction = ParameterDirection.Output;
                dbCmd.Parameters.Add(new SqlParameter("@SP_RETURNVALUE", SqlDbType.SmallInt, 0)).Direction = ParameterDirection.Output;
                //dbCmd.Parameters.Add(new SqlParameter("@RETURNSTATUS", SqlDbType.SmallInt, 0)).Direction = ParameterDirection.Output;

                dbRdr = dbCmd.ExecuteReader();

                results[0] = dbCmd.Parameters["@SP_MESSAGE"].Value.ToString();
                results[1] = dbCmd.Parameters["@SP_RETURNVALUE"].Value.ToString();

            }

            catch (Exception ex)
            {
                throw ex;
                // MessageBox.Show("Error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dbRdr != null)
                {
                    dbRdr.Dispose();
                }
                if (dbCmd != null)
                {
                    dbCmd.Dispose();
                }
                CommonDB.CloseConnection(dbCon);
            }
            return results;

        }

        public static String[] SaveUserAccessRights(List<string> AccessData)
        {
            SqlDataReader dbRdr = null;
            SqlCommand dbCmd = null;
            SqlConnection dbCon = null;
            string[] results = new string[2];
            try
            {
                dbCon = CommonDB.OpenConnection();
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbCon;
                dbCmd.CommandType = CommandType.StoredProcedure;

                dbCmd.CommandText = "Sys_UserAccessRights";
                dbCmd.Parameters.Add("@SP_USERID", SqlDbType.Int, 0).Value = AccessData[0] == "" ? (object)DBNull.Value : AccessData[0];
                dbCmd.Parameters.Add("@SP_USERTYPE", SqlDbType.VarChar, 10).Value = AccessData[1] == "" ? (object)DBNull.Value : AccessData[1];
                dbCmd.Parameters.Add("@SP_BRANCHID", SqlDbType.VarChar, 50).Value = AccessData[2] == "" ? (object)DBNull.Value : AccessData[2];
                dbCmd.Parameters.Add("@SP_DASHBOARD", SqlDbType.VarChar, 50).Value = AccessData[3] == "" ? (object)DBNull.Value : AccessData[3];
                dbCmd.Parameters.Add("@SP_DEPT", SqlDbType.Char, 10).Value = AccessData[4] == "" ? (object)DBNull.Value : AccessData[4];

                dbCmd.Parameters.Add(new SqlParameter("@SP_MESSAGE", SqlDbType.VarChar, 500)).Direction = ParameterDirection.Output;
                dbCmd.Parameters.Add(new SqlParameter("@SP_RETURNVALUE", SqlDbType.SmallInt, 0)).Direction = ParameterDirection.Output;
                //dbCmd.Parameters.Add(new SqlParameter("@RETURNSTATUS", SqlDbType.SmallInt, 0)).Direction = ParameterDirection.Output;

                dbRdr = dbCmd.ExecuteReader();

                results[0] = dbCmd.Parameters["@SP_MESSAGE"].Value.ToString();
                results[1] = dbCmd.Parameters["@SP_RETURNVALUE"].Value.ToString();

            }

            catch (Exception ex)
            {
                throw ex;
                // MessageBox.Show("Error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dbRdr != null)
                {
                    dbRdr.Dispose();
                }
                if (dbCmd != null)
                {
                    dbCmd.Dispose();
                }
                CommonDB.CloseConnection(dbCon);
            }
            return results;

        }

        public static DataTable GetMenuRights(int groupid)
        {
            SqlDataAdapter dbRdr = new SqlDataAdapter();
            SqlCommand dbCmd = null;
            SqlConnection dbCon = null;
            DataSet dsData = new DataSet();
            try
            {
                dbCon = CommonDB.OpenConnection();
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbCon;
                dbCmd.CommandType = CommandType.StoredProcedure;

                dbCmd.CommandText = "USP_GETMENURIGHTS";
                dbCmd.Parameters.Add("@groupid", SqlDbType.Int, 0).Value = groupid;
                dbRdr.SelectCommand = dbCmd;
                dbRdr.Fill(dsData);

            }
            catch (Exception ex)
            {
                throw ex;
                // MessageBox.Show("Error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dbRdr != null)
                {
                    dbRdr.Dispose();
                }
                if (dbCmd != null)
                {
                    dbCmd.Dispose();
                }
                CommonDB.CloseConnection(dbCon);
            }
            return dsData.Tables[0];

        }

        internal static DataTable GetUserMaster()
        {
            SqlDataAdapter dbRdrUserMaster = new SqlDataAdapter();
            SqlCommand dbCmdUserMaster = null;
            SqlConnection dbConUserMaster = null;
            DataSet dsDataUserMaster = new DataSet();
            try
            {
                dbConUserMaster = CommonDB.OpenConnection();
                dbCmdUserMaster = new SqlCommand();
                dbCmdUserMaster.Connection = dbConUserMaster;
                dbCmdUserMaster.CommandType = CommandType.StoredProcedure;

                dbCmdUserMaster.CommandText = "USP_GetUserMaster";
                dbRdrUserMaster.SelectCommand = dbCmdUserMaster;
                dbRdrUserMaster.Fill(dsDataUserMaster);

            }
            catch (Exception ex)
            {
                throw ex;
                // MessageBox.Show("Error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dbRdrUserMaster != null)
                {
                    dbRdrUserMaster.Dispose();
                }
                if (dbCmdUserMaster != null)
                {
                    dbCmdUserMaster.Dispose();
                }
                CommonDB.CloseConnection(dbConUserMaster);
            }
            return dsDataUserMaster.Tables[0];
        }

        public static DataTable GetGroup()
        {
            SqlDataAdapter dbRdr = new SqlDataAdapter();
            SqlCommand dbCmd = null;
            SqlConnection dbCon = null;
            DataSet dsData = new DataSet();
            try
            {
                dbCon = CommonDB.OpenConnection();
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbCon;
                dbCmd.CommandType = CommandType.StoredProcedure;

                dbCmd.CommandText = "USP_GETALLGROUP";
                //dbCmd.Parameters.Add("@groupid", SqlDbType.Int, 0).Value = groupid;
                dbRdr.SelectCommand = dbCmd;
                dbRdr.Fill(dsData);

            }
            catch (Exception ex)
            {
                throw ex;
                // MessageBox.Show("Error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dbRdr != null)
                {
                    dbRdr.Dispose();
                }
                if (dbCmd != null)
                {
                    dbCmd.Dispose();
                }
                CommonDB.CloseConnection(dbCon);
            }
            return dsData.Tables[0];
        }

        public static DataTable GetKeyValues(string keytype,string Subkeytype)
        {

            SqlDataAdapter dbRdr = new SqlDataAdapter();
            SqlCommand dbCmd = null;
            SqlConnection dbCon = null;
            DataSet dsData = new DataSet();
            try
            {
                dbCon = CommonDB.OpenConnection();
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbCon;
                dbCmd.CommandType = CommandType.StoredProcedure;

                dbCmd.CommandText = "USP_GETKEYVALUES";
                dbCmd.Parameters.Add("@SP_KeyType", SqlDbType.VarChar, 20).Value = keytype;
                dbCmd.Parameters.Add("@SP_SubKeyType", SqlDbType.VarChar, 50).Value = Subkeytype;
                dbRdr.SelectCommand = dbCmd;
                dbRdr.Fill(dsData);

            }
            catch (Exception ex)
            {
                throw ex;
                // MessageBox.Show("Error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dbRdr != null)
                {
                    dbRdr.Dispose();
                }
                if (dbCmd != null)
                {
                    dbCmd.Dispose();
                }
                CommonDB.CloseConnection(dbCon);
            }
            return dsData.Tables[0];
        }
        public static DataTable GetBranch()
        {

            SqlDataAdapter dbRdr = new SqlDataAdapter();
            SqlCommand dbCmd = null;
            SqlConnection dbCon = null;
            DataSet dsData = new DataSet();
            try
            {
                dbCon = CommonDB.OpenConnection();
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbCon;
                dbCmd.CommandType = CommandType.StoredProcedure;

                dbCmd.CommandText = "USP_GETALLBRANCH";
                //dbCmd.Parameters.Add("@groupid", SqlDbType.Int, 0).Value = groupid;
                dbRdr.SelectCommand = dbCmd;
                dbRdr.Fill(dsData);

            }
            catch (Exception ex)
            {
                throw ex;
                // MessageBox.Show("Error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dbRdr != null)
                {
                    dbRdr.Dispose();
                }
                if (dbCmd != null)
                {
                    dbCmd.Dispose();
                }
                CommonDB.CloseConnection(dbCon);
            }
            return dsData.Tables[0];
        }

        public static DataTable GetGroupUser()
        {
            SqlDataAdapter dbRdr = new SqlDataAdapter();
            SqlCommand dbCmd = null;
            SqlConnection dbCon = null;
            DataSet dsData = new DataSet();

            try
            {
                dbCon = CommonDB.OpenConnection();
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbCon;
                dbCmd.CommandType = CommandType.StoredProcedure;

                dbCmd.CommandText = "USP_GETGROUPUSER";
                dbRdr.SelectCommand = dbCmd;
                dbRdr.Fill(dsData);

            }
            catch (Exception ex)
            {
                throw ex;
                // MessageBox.Show("Error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dbRdr != null)
                {
                    dbRdr.Dispose();
                }
                if (dbCmd != null)
                {
                    dbCmd.Dispose();
                }
                CommonDB.CloseConnection(dbCon);

            }

            return dsData.Tables[0];
        }

       public static DataTable GetMenu()
        {
            SqlDataAdapter dbRdr = new SqlDataAdapter();
            SqlCommand dbCmd = null;
            SqlConnection dbCon = null;
            DataSet dsData = new DataSet();
            try
            {
                dbCon = CommonDB.OpenConnection();
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbCon;
                dbCmd.CommandType = CommandType.StoredProcedure;

                dbCmd.CommandText = "USP_GETMENU";
                //dbCmd.Parameters.Add("@groupid", SqlDbType.Int, 0).Value = groupid;
                dbRdr.SelectCommand = dbCmd;
                dbRdr.Fill(dsData);

            }
            catch (Exception ex)
            {
                throw ex;
                // MessageBox.Show("Error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dbRdr != null)
                {
                    dbRdr.Dispose();
                }
                if (dbCmd != null)
                {
                    dbCmd.Dispose();
                }
                CommonDB.CloseConnection(dbCon);
            }
            return dsData.Tables[0];

        }
        public static String ResetPassword(string Id, string UserId,string ResetPWD)
        {
            string results = string.Empty;
            SqlDataReader dbRdr = null;
            SqlCommand dbCmd = null;
            SqlConnection dbCon = null;
            DataSet dsData = new DataSet();
            try
            {
                dbCon = CommonDB.OpenConnection();
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbCon;
                dbCmd.CommandType = CommandType.StoredProcedure;

                dbCmd.CommandText = "Sys_ResetPassword";
                dbCmd.Parameters.Add("@SP_ID", SqlDbType.Int).Value = Id == "" ? (object)DBNull.Value : Id;
                dbCmd.Parameters.Add("@SP_USERID", SqlDbType.Int).Value = UserId == "" ? (object)DBNull.Value : UserId;
                dbCmd.Parameters.Add("@SP_ResetPWD", SqlDbType.VarChar).Value = ResetPWD == "" ? (object)DBNull.Value : ResetPWD;
                dbCmd.Parameters.Add(new SqlParameter("@SP_MESSAGE", SqlDbType.VarChar, 500)).Direction = ParameterDirection.Output;
                dbRdr = dbCmd.ExecuteReader();

                results = dbCmd.Parameters["@SP_MESSAGE"].Value.ToString();

            }

            catch (Exception ex)
            {
                throw ex;
                // MessageBox.Show("Error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                if (dbRdr != null)
                {
                    dbRdr.Dispose();
                }
                if (dbCmd != null)
                {
                    dbCmd.Dispose();
                }
                CommonDB.CloseConnection(dbCon);
            }

            return results;
        }

        public static String UpdateLoginDate(int UserId)
        {
            string results = string.Empty;
            SqlDataReader dbRdr = null;
            SqlCommand dbCmd = null;
            SqlConnection dbCon = null;
            DataSet dsData = new DataSet();
            try
            {
                dbCon = CommonDB.OpenConnection();
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbCon;
                dbCmd.CommandType = CommandType.StoredProcedure;

                dbCmd.CommandText = "Sys_UpdateLoginStatus";
                dbCmd.Parameters.Add("@SP_USERID", SqlDbType.Int).Value = UserId == 0 ? (object)DBNull.Value : UserId;
                dbCmd.Parameters.Add(new SqlParameter("@SP_MESSAGE", SqlDbType.VarChar, 500)).Direction = ParameterDirection.Output;
                dbRdr = dbCmd.ExecuteReader();

                results = dbCmd.Parameters["@SP_MESSAGE"].Value.ToString();

            }

            catch (Exception ex)
            {
                throw ex;
                // MessageBox.Show("Error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                if (dbRdr != null)
                {
                    dbRdr.Dispose();
                }
                if (dbCmd != null)
                {
                    dbCmd.Dispose();
                }
                CommonDB.CloseConnection(dbCon);
            }

            return results;
        }
        public static DataTable DisplayGroupUser(string GROUPID)
        {
            SqlDataAdapter dbRdr = new SqlDataAdapter();
            SqlCommand dbCmd = null;
            SqlConnection dbCon = null;
            DataSet dsData = new DataSet();

            try
            {
                dbCon = CommonDB.OpenConnection();
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbCon;
                dbCmd.CommandType = CommandType.StoredProcedure;

                dbCmd.CommandText = "USP_GETUSERGROUPDETAILS";
                dbCmd.Parameters.Add("@GROUPID", SqlDbType.VarChar, 20).Value = GROUPID == "" ? (object)DBNull.Value : GROUPID;
                dbRdr.SelectCommand = dbCmd;
                dbRdr.Fill(dsData);

            }
            catch (Exception ex)
            {
                throw ex;
                // MessageBox.Show("Error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dbRdr != null)
                {
                    dbRdr.Dispose();
                }
                if (dbCmd != null)
                {
                    dbCmd.Dispose();
                }
                CommonDB.CloseConnection(dbCon);
            }

            return dsData.Tables[0];
        }

        public static String InsertGroup(string GroupName, string IsActive)
        {
            SqlDataReader dbRdr = null;
            SqlCommand dbCmd = null;
            SqlConnection dbCon = null;
            string results = string.Empty;
            try
            {
                dbCon = CommonDB.OpenConnection();
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbCon;
                dbCmd.CommandType = CommandType.StoredProcedure;

                dbCmd.CommandText = "Sys_GroupAdd";
                dbCmd.Parameters.Add("@GROUPNAME", SqlDbType.VarChar, 30).Value = GroupName == "" ? (object)DBNull.Value : GroupName;
                dbCmd.Parameters.Add("@ISACTIVE", SqlDbType.VarChar, 10).Value = IsActive == "" ? (object)DBNull.Value : IsActive;

                dbRdr = dbCmd.ExecuteReader();

                while (dbRdr.Read())
                {
                    results = dbRdr["result"].ToString();
                }

            }

            catch (Exception ex)
            {
                throw ex;
                // MessageBox.Show("Error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dbRdr != null)
                {
                    dbRdr.Dispose();
                }
                if (dbCmd != null)
                {
                    dbCmd.Dispose();
                }
                CommonDB.CloseConnection(dbCon);
            }
            return results;

        }

        public static String DeleteGroupUser(string GroupID)
        {
            SqlDataReader dbRdr = null;
            SqlCommand dbCmd = null;
            SqlConnection dbCon = null;
            string results = string.Empty;
            try
            {
                dbCon = CommonDB.OpenConnection();
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbCon;
                dbCmd.CommandType = CommandType.StoredProcedure;

                dbCmd.CommandText = "USP_DELETEGROUPDETAILS";
                dbCmd.Parameters.Add("@GROUPID", SqlDbType.Int, 0).Value = GroupID == "" ? (object)DBNull.Value : GroupID;
                dbRdr = dbCmd.ExecuteReader();


            }

            catch (Exception ex)
            {
                throw ex;
                // MessageBox.Show("Error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dbRdr != null)
                {
                    dbRdr.Dispose();
                }
                if (dbCmd != null)
                {
                    dbCmd.Dispose();
                }
                CommonDB.CloseConnection(dbCon);
            }
            return results;


        }

        public static String InsertGroupUser(string MenuID, string GroupID)
        {
            SqlDataReader dbRdr = null;
            SqlCommand dbCmd = null;
            SqlConnection dbCon = null;
            string results = string.Empty;
            try
            {
                dbCon = CommonDB.OpenConnection();
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbCon;
                dbCmd.CommandType = CommandType.StoredProcedure;

                dbCmd.CommandText = "Sys_UserMenuRightsAdd";
                dbCmd.Parameters.Add("@MENUID", SqlDbType.NVarChar, 200).Value = MenuID == "" ? (object)DBNull.Value : MenuID;
                dbCmd.Parameters.Add("@GROUPID", SqlDbType.Int, 0).Value = GroupID == "" ? (object)DBNull.Value : GroupID;

                dbRdr = dbCmd.ExecuteReader();


            }

            catch (Exception ex)
            {
                throw ex;
                // MessageBox.Show("Error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dbRdr != null)
                {
                    dbRdr.Dispose();
                }
                if (dbCmd != null)
                {
                    dbCmd.Dispose();
                }
                CommonDB.CloseConnection(dbCon);
            }
            return results;


        }

        public static DataTable GetLocationData(int EditId)
        {
            SqlDataAdapter dbRdr = new SqlDataAdapter();
            SqlCommand dbCmd = null;
            SqlConnection dbCon = null;
            DataSet dsData = new DataSet();
            try
            {
                dbCon = CommonDB.OpenConnection();
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbCon;
                dbCmd.CommandType = CommandType.StoredProcedure;

                dbCmd.CommandText = "USP_GETLOCATIONDATA";
                dbCmd.Parameters.Add("@SP_EditId", SqlDbType.Int).Value = EditId == 0 ? (object)DBNull.Value : EditId;
                dbRdr.SelectCommand = dbCmd;
                dbRdr.Fill(dsData);

            }
            catch (Exception ex)
            {
                throw ex;
                // MessageBox.Show("Error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dbRdr != null)
                {
                    dbRdr.Dispose();
                }
                if (dbCmd != null)
                {
                    dbCmd.Dispose();
                }
                CommonDB.CloseConnection(dbCon);
            }
            return dsData.Tables[0];

        }

        public static String InsertLocation(string LocName, string IsActive, int EditId, int UserId)
        {
            SqlDataReader dbRdr = null;
            SqlCommand dbCmd = null;
            SqlConnection dbCon = null;
            string[] results = new string[2];
            try
            {
                dbCon = CommonDB.OpenConnection();
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbCon;
                dbCmd.CommandType = CommandType.StoredProcedure;

                dbCmd.CommandText = "Sys_LocationAddEdit";
                dbCmd.Parameters.Add("@SP_LocationName", SqlDbType.VarChar, 100).Value = LocName == "" ? (object)DBNull.Value : LocName;
                dbCmd.Parameters.Add("@SP_IsActive", SqlDbType.Char, 1).Value = IsActive == "" ? (object)DBNull.Value : IsActive;
                dbCmd.Parameters.Add("@SP_EditId", SqlDbType.Int, 0).Value = EditId;
                dbCmd.Parameters.Add("@SP_UserId", SqlDbType.Int, 0).Value = UserId;


                dbCmd.Parameters.Add(new SqlParameter("@SP_MESSAGE", SqlDbType.VarChar, 500)).Direction = ParameterDirection.Output;
                //dbCmd.Parameters.Add(new SqlParameter("@SP_RETURNVALUE", SqlDbType.SmallInt, 0)).Direction = ParameterDirection.Output;
                //dbCmd.Parameters.Add(new SqlParameter("@RETURNSTATUS", SqlDbType.SmallInt, 0)).Direction = ParameterDirection.Output;

                dbRdr = dbCmd.ExecuteReader();

                results[0] = dbCmd.Parameters["@SP_MESSAGE"].Value.ToString();
                //results[1] = dbCmd.Parameters["@SP_RETURNVALUE"].Value.ToString();

            }

            catch (Exception ex)
            {
                throw ex;
                // MessageBox.Show("Error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dbRdr != null)
                {
                    dbRdr.Dispose();
                }
                if (dbCmd != null)
                {
                    dbCmd.Dispose();
                }
                CommonDB.CloseConnection(dbCon);
            }
            return results[0];

        }

        public static DataTable GetBranchData(int EditId)
        {
            SqlDataAdapter dbRdr = new SqlDataAdapter();
            SqlCommand dbCmd = null;
            SqlConnection dbCon = null;
            DataSet dsData = new DataSet();
            try
            {
                dbCon = CommonDB.OpenConnection();
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbCon;
                dbCmd.CommandType = CommandType.StoredProcedure;

                dbCmd.CommandText = "USP_GETBRANCHDATA";
                dbCmd.Parameters.Add("@SP_EditId", SqlDbType.Int).Value = EditId == 0 ? (object)DBNull.Value : EditId;
                dbRdr.SelectCommand = dbCmd;
                dbRdr.Fill(dsData);

            }
            catch (Exception ex)
            {
                throw ex;
                // MessageBox.Show("Error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dbRdr != null)
                {
                    dbRdr.Dispose();
                }
                if (dbCmd != null)
                {
                    dbCmd.Dispose();
                }
                CommonDB.CloseConnection(dbCon);
            }
            return dsData.Tables[0];

        }
        public static String InsertBranch(ClsBranchData objBranch, int EditId, int UserId)
        {
            SqlDataReader dbRdr = null;
            SqlCommand dbCmd = null;
            SqlConnection dbCon = null;
            string[] results = new string[2];
            try
            {
                dbCon = CommonDB.OpenConnection();
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbCon;
                dbCmd.CommandType = CommandType.StoredProcedure;

                dbCmd.CommandText = "Sys_BranchAddEdit";
                dbCmd.Parameters.Add("@SP_BranchName", SqlDbType.VarChar, 200).Value = objBranch.BranchName == "" ? (object)DBNull.Value : objBranch.BranchName;
                dbCmd.Parameters.Add("@SP_LocId", SqlDbType.Int, 0).Value = objBranch.LocId == 0 ? (object)DBNull.Value : objBranch.LocId;
                dbCmd.Parameters.Add("@SP_LocName", SqlDbType.VarChar, 100).Value = objBranch.LocName == "" ? (object)DBNull.Value : objBranch.LocName;
                dbCmd.Parameters.Add("@SP_Address1", SqlDbType.VarChar, 250).Value = objBranch.Address1 == "" ? (object)DBNull.Value : objBranch.Address1;
                dbCmd.Parameters.Add("@SP_Address2", SqlDbType.VarChar, 250).Value = objBranch.Address2 == "" ? (object)DBNull.Value : objBranch.Address2;
                dbCmd.Parameters.Add("@SP_Address3", SqlDbType.VarChar, 250).Value = objBranch.Address3 == "" ? (object)DBNull.Value : objBranch.Address3;
                dbCmd.Parameters.Add("@SP_Address4", SqlDbType.VarChar, 250).Value = objBranch.Address4 == "" ? (object)DBNull.Value : objBranch.Address4;
                dbCmd.Parameters.Add("@SP_Pincode", SqlDbType.VarChar, 50).Value = objBranch.Pincode == "" ? (object)DBNull.Value : objBranch.Pincode;
                dbCmd.Parameters.Add("@SP_City", SqlDbType.VarChar, 50).Value = objBranch.City == "" ? (object)DBNull.Value : objBranch.City;
                dbCmd.Parameters.Add("@SP_State", SqlDbType.VarChar, 50).Value = objBranch.State == "" ? (object)DBNull.Value : objBranch.State;
                dbCmd.Parameters.Add("@SP_EmailId", SqlDbType.VarChar, 100).Value = objBranch.EmailId == "" ? (object)DBNull.Value : objBranch.EmailId;
                dbCmd.Parameters.Add("@SP_PhoneNo", SqlDbType.VarChar, 50).Value = objBranch.PhoneNo == "" ? (object)DBNull.Value : objBranch.PhoneNo;
                dbCmd.Parameters.Add("@SP_IsActive", SqlDbType.Char, 1).Value = objBranch.IsActive == "" ? (object)DBNull.Value : objBranch.IsActive;
                dbCmd.Parameters.Add("@SP_EditId", SqlDbType.Int, 0).Value = EditId;
                dbCmd.Parameters.Add("@SP_UserId", SqlDbType.Int, 0).Value = UserId;


                dbCmd.Parameters.Add(new SqlParameter("@SP_MESSAGE", SqlDbType.VarChar, 500)).Direction = ParameterDirection.Output;
                //dbCmd.Parameters.Add(new SqlParameter("@SP_RETURNVALUE", SqlDbType.SmallInt, 0)).Direction = ParameterDirection.Output;
                //dbCmd.Parameters.Add(new SqlParameter("@RETURNSTATUS", SqlDbType.SmallInt, 0)).Direction = ParameterDirection.Output;

                dbRdr = dbCmd.ExecuteReader();

                results[0] = dbCmd.Parameters["@SP_MESSAGE"].Value.ToString();
                //results[1] = dbCmd.Parameters["@SP_RETURNVALUE"].Value.ToString();

            }

            catch (Exception ex)
            {
                throw ex;
                // MessageBox.Show("Error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dbRdr != null)
                {
                    dbRdr.Dispose();
                }
                if (dbCmd != null)
                {
                    dbCmd.Dispose();
                }
                CommonDB.CloseConnection(dbCon);
            }
            return results[0];

        }

        public static DataTable GetReasonData(string EditId)
        {
            SqlDataAdapter dbRdr = new SqlDataAdapter();
            SqlCommand dbCmd = null;
            SqlConnection dbCon = null;
            DataSet dsData = new DataSet();
            try
            {
                dbCon = CommonDB.OpenConnection();
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbCon;
                dbCmd.CommandType = CommandType.StoredProcedure;

                dbCmd.CommandText = "USP_GETREASONDATA";
                dbCmd.Parameters.Add("@SP_EditId", SqlDbType.Char,10).Value = EditId == "" ? (object)DBNull.Value : EditId;
                dbRdr.SelectCommand = dbCmd;
                dbRdr.Fill(dsData);

            }
            catch (Exception ex)
            {
                throw ex;
                // MessageBox.Show("Error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dbRdr != null)
                {
                    dbRdr.Dispose();
                }
                if (dbCmd != null)
                {
                    dbCmd.Dispose();
                }
                CommonDB.CloseConnection(dbCon);
            }
            return dsData.Tables[0];

        }

        public static String InsertReason(string tag,string DocName,string Remark, string IsActive, string EditId, int UserId)
        {
            SqlDataReader dbRdr = null;
            SqlCommand dbCmd = null;
            SqlConnection dbCon = null;
            string[] results = new string[2];
            try
            {
                dbCon = CommonDB.OpenConnection();
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbCon;
                dbCmd.CommandType = CommandType.StoredProcedure;

                dbCmd.CommandText = "Sys_ReasonAddEdit";
                dbCmd.Parameters.Add("@SP_tag", SqlDbType.Char, 1).Value = tag == "" ? (object)DBNull.Value : tag;
                dbCmd.Parameters.Add("@SP_DocName", SqlDbType.VarChar, 50).Value = DocName == "" ? (object)DBNull.Value : DocName;
                dbCmd.Parameters.Add("@SP_Reason", SqlDbType.VarChar, 250).Value = Remark == "" ? (object)DBNull.Value : Remark;
                dbCmd.Parameters.Add("@SP_IsActive", SqlDbType.Char, 1).Value = IsActive == "" ? (object)DBNull.Value : IsActive;
                dbCmd.Parameters.Add("@SP_EditId", SqlDbType.Char, 10).Value = EditId == "" ? (object)DBNull.Value : EditId;
                dbCmd.Parameters.Add("@SP_UserId", SqlDbType.Int, 0).Value = UserId;


                dbCmd.Parameters.Add(new SqlParameter("@SP_MESSAGE", SqlDbType.VarChar, 500)).Direction = ParameterDirection.Output;
                //dbCmd.Parameters.Add(new SqlParameter("@SP_RETURNVALUE", SqlDbType.SmallInt, 0)).Direction = ParameterDirection.Output;
                //dbCmd.Parameters.Add(new SqlParameter("@RETURNSTATUS", SqlDbType.SmallInt, 0)).Direction = ParameterDirection.Output;

                dbRdr = dbCmd.ExecuteReader();

                results[0] = dbCmd.Parameters["@SP_MESSAGE"].Value.ToString();
                //results[1] = dbCmd.Parameters["@SP_RETURNVALUE"].Value.ToString();

            }

            catch (Exception ex)
            {
                throw ex;
                // MessageBox.Show("Error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dbRdr != null)
                {
                    dbRdr.Dispose();
                }
                if (dbCmd != null)
                {
                    dbCmd.Dispose();
                }
                CommonDB.CloseConnection(dbCon);
            }
            return results[0];

        }

        public static DataTable UserDisplayProfileData(int UserId)
        {
            SqlDataAdapter dbRdr = new SqlDataAdapter();
            SqlCommand dbCmd = null;
            SqlConnection dbCon = null;
            DataSet dsData = new DataSet();
            try
            {
                dbCon = CommonDB.OpenConnection();
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbCon;
                dbCmd.CommandType = CommandType.StoredProcedure;

                dbCmd.CommandText = "USP_GETProfileUserDetail";
                dbCmd.Parameters.Add("@USERID", SqlDbType.Int).Value = UserId == 0 ? (object)DBNull.Value : UserId;
                dbRdr.SelectCommand = dbCmd;
                dbRdr.Fill(dsData);

            }
            catch (Exception ex)
            {
                throw ex;
                // MessageBox.Show("Error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dbRdr != null)
                {
                    dbRdr.Dispose();
                }
                if (dbCmd != null)
                {
                    dbCmd.Dispose();
                }
                CommonDB.CloseConnection(dbCon);
            }
            return dsData.Tables[0];

        }

        public static DataTable GetProfileMenu(int groupId)
        {
            SqlDataAdapter dbRdr = new SqlDataAdapter();
            SqlCommand dbCmd = null;
            SqlConnection dbCon = null;
            DataSet dsData = new DataSet();
            try
            {
                dbCon = CommonDB.OpenConnection();
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbCon;
                dbCmd.CommandType = CommandType.StoredProcedure;

                dbCmd.CommandText = "USP_GETProfileMenu";
                dbCmd.Parameters.Add("@GROUPID", SqlDbType.Int, 0).Value = groupId;
                dbRdr.SelectCommand = dbCmd;
                dbRdr.Fill(dsData);

            }
            catch (Exception ex)
            {
                throw ex;
                // MessageBox.Show("Error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dbRdr != null)
                {
                    dbRdr.Dispose();
                }
                if (dbCmd != null)
                {
                    dbCmd.Dispose();
                }
                CommonDB.CloseConnection(dbCon);
            }
            return dsData.Tables[0];

        }

        #region "New added code by Manish"
        public static String InsertBC(string BCName, string IsActive, int EditId)
        {
            SqlDataReader dbRdr = null;
            SqlCommand dbCmd = null;
            SqlConnection dbCon = null;
            string[] results = new string[2];
            try
            {
                dbCon = CommonDB.OpenConnection();
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbCon;
                dbCmd.CommandType = CommandType.StoredProcedure;

                dbCmd.CommandText = "Sys_BCAddEdit";
                dbCmd.Parameters.Add("@SP_BCName", SqlDbType.VarChar, 200).Value = BCName == "" ? (object)DBNull.Value : BCName;
                dbCmd.Parameters.Add("@SP_IsActive", SqlDbType.Char, 1).Value = IsActive == "" ? (object)DBNull.Value : IsActive;
                dbCmd.Parameters.Add("@SP_EditId", SqlDbType.Int, 0).Value = EditId;
                


                dbCmd.Parameters.Add(new SqlParameter("@SP_MESSAGE", SqlDbType.VarChar, 500)).Direction = ParameterDirection.Output;
                //dbCmd.Parameters.Add(new SqlParameter("@SP_RETURNVALUE", SqlDbType.SmallInt, 0)).Direction = ParameterDirection.Output;
                //dbCmd.Parameters.Add(new SqlParameter("@RETURNSTATUS", SqlDbType.SmallInt, 0)).Direction = ParameterDirection.Output;

                dbRdr = dbCmd.ExecuteReader();

                results[0] = dbCmd.Parameters["@SP_MESSAGE"].Value.ToString();
                //results[1] = dbCmd.Parameters["@SP_RETURNVALUE"].Value.ToString();

            }

            catch (Exception ex)
            {
                throw ex;
                // MessageBox.Show("Error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dbRdr != null)
                {
                    dbRdr.Dispose();
                }
                if (dbCmd != null)
                {
                    dbCmd.Dispose();
                }
                CommonDB.CloseConnection(dbCon);
            }
            return results[0];

        }

        public static DataTable GetBC(int BC_Id, String showActiveOnly)
        {
            SqlDataAdapter dbRdr = new SqlDataAdapter();
            SqlCommand dbCmd = null;
            SqlConnection dbCon = null;
            DataSet dsData = new DataSet();

            try
            {
                dbCon = CommonDB.OpenConnection();
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbCon;
                dbCmd.CommandType = CommandType.StoredProcedure;

                dbCmd.CommandText = "USP_GETBC";
                dbCmd.Parameters.Add("@BC_Id", SqlDbType.Int, 0).Value = BC_Id;
                dbCmd.Parameters.Add("@ShowActiveOnly", SqlDbType.VarChar, 8).Value = showActiveOnly;
                dbRdr.SelectCommand = dbCmd;
                dbRdr.Fill(dsData);

            }
            catch (Exception ex)
            {
                throw ex;
                // MessageBox.Show("Error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dbRdr != null)
                {
                    dbRdr.Dispose();
                }
                if (dbCmd != null)
                {
                    dbCmd.Dispose();
                }
                CommonDB.CloseConnection(dbCon);

            }

            return dsData.Tables[0];
        }

        public static DataTable GetMailerInfo(string bc_name)
        {
            SqlDataAdapter dbRdr = new SqlDataAdapter();
            SqlCommand dbCmd = null;
            SqlConnection dbCon = null;
            DataSet dsData = new DataSet();

            try
            {
                dbCon = CommonDB.OpenConnection();
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbCon;
                dbCmd.CommandType = CommandType.StoredProcedure;

                dbCmd.CommandText = "USP_GetMailer_Data_ByName";
                dbCmd.Parameters.Add("@SP_BC_Name", SqlDbType.VarChar,bc_name.Length).Value =bc_name;
                dbRdr.SelectCommand = dbCmd;
                dbRdr.Fill(dsData);

            }
            catch (Exception ex)
            {
                throw ex;
                // MessageBox.Show("Error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dbRdr != null)
                {
                    dbRdr.Dispose();
                }
                if (dbCmd != null)
                {
                    dbCmd.Dispose();
                }
                CommonDB.CloseConnection(dbCon);

            }

            return dsData.Tables[0];
        }

        public static String InsertMailList(ClsBCMailList objMail)
        {
            SqlDataReader dbRdr = null;
            SqlCommand dbCmd = null;
            SqlConnection dbCon = null;
            string[] results = new string[2];
            try
            {
                dbCon = CommonDB.OpenConnection();
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbCon;
                dbCmd.CommandType = CommandType.StoredProcedure;

                dbCmd.CommandText = "Sys_BCMailAddEdit";
                dbCmd.Parameters.Add("@PDD_BC_Name", SqlDbType.VarChar, objMail.PDD_BC_Name.Length).Value = objMail.PDD_BC_Name == "" ? (object)DBNull.Value : objMail.PDD_BC_Name;
                dbCmd.Parameters.Add("@PDD_BC_Code", SqlDbType.VarChar, 100).Value = objMail.PDD_BC_Code == "" ? (object)DBNull.Value : objMail.PDD_BC_Code;
                dbCmd.Parameters.Add("@PDD_BC_To_Email", SqlDbType.VarChar, objMail.PDD_BC_To_Email.Length).Value = objMail.PDD_BC_To_Email == "" ? (object)DBNull.Value : objMail.PDD_BC_To_Email;
                dbCmd.Parameters.Add("@PDD_BC_CC_Email", SqlDbType.VarChar, 100).Value = objMail.PDD_BC_CC_Email == "" ? (object)DBNull.Value : objMail.PDD_BC_CC_Email;
                dbCmd.Parameters.Add("@PDD_BC_BCC_Email", SqlDbType.VarChar, objMail.PDD_BC_BCC_Email.Length).Value = objMail.PDD_BC_BCC_Email == "" ? (object)DBNull.Value : objMail.PDD_BC_BCC_Email;
                dbCmd.Parameters.Add("@Report_Type", SqlDbType.VarChar, 100).Value = objMail.Report_Type == "" ? (object)DBNull.Value : objMail.Report_Type;
                dbCmd.Parameters.Add("@IsActive", SqlDbType.Char, 1).Value = objMail.IsActive == "" ? (object)DBNull.Value : objMail.IsActive;
                

                dbCmd.Parameters.Add(new SqlParameter("@SP_MESSAGE", SqlDbType.VarChar, 500)).Direction = ParameterDirection.Output;
                //dbCmd.Parameters.Add(new SqlParameter("@SP_RETURNVALUE", SqlDbType.SmallInt, 0)).Direction = ParameterDirection.Output;
                //dbCmd.Parameters.Add(new SqlParameter("@RETURNSTATUS", SqlDbType.SmallInt, 0)).Direction = ParameterDirection.Output;

                dbRdr = dbCmd.ExecuteReader();

                results[0] = dbCmd.Parameters["@SP_MESSAGE"].Value.ToString();
                //results[1] = dbCmd.Parameters["@SP_RETURNVALUE"].Value.ToString();

            }

            catch (Exception ex)
            {
                throw ex;
                // MessageBox.Show("Error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dbRdr != null)
                {
                    dbRdr.Dispose();
                }
                if (dbCmd != null)
                {
                    dbCmd.Dispose();
                }
                CommonDB.CloseConnection(dbCon);
            }
            return results[0];

        }

        #endregion
    }
}