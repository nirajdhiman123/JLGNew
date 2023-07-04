using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace JLG
{
    public class ClsUploadData
    {
        public static DataTable GetGridData(int UserID)
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

                dbCmd.CommandText = "USP_GetUploadPDDData";
                dbCmd.Parameters.AddWithValue("@SP_UserID", UserID);
                dbRdr.SelectCommand = dbCmd;
                dbRdr.Fill(dsData);
            }
            catch (Exception ex)
            {
                CommonDB.ClassLogError("ClsUploadData", "GetGridData", Convert.ToString(ex.Message));
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

        internal static DataTable GetUploadedFileDetails(int id)
        {
            SqlDataAdapter dbRdrUploadedFile = new SqlDataAdapter();
            SqlCommand dbCmdUploadedFile = null;
            SqlConnection dbConUploadedFile = null;
            DataSet dsDataUploadedFile = new DataSet();
            try
            {
                dbConUploadedFile = CommonDB.OpenConnection();
                dbCmdUploadedFile = new SqlCommand();
                dbCmdUploadedFile.Connection = dbConUploadedFile;
                dbCmdUploadedFile.CommandType = CommandType.StoredProcedure;

                dbCmdUploadedFile.CommandText = "USP_GetUploadedFilesData";
                dbCmdUploadedFile.Parameters.AddWithValue("@userID", id);
                
                dbRdrUploadedFile.SelectCommand = dbCmdUploadedFile;
                dbRdrUploadedFile.Fill(dsDataUploadedFile);
            }
            catch (Exception ex)
            {
                CommonDB.ClassLogError("ClsUploadData", "GetSearchData", Convert.ToString(ex.Message));
            }
            finally
            {
                if (dbRdrUploadedFile != null)
                {
                    dbRdrUploadedFile.Dispose();
                }
                if (dbCmdUploadedFile != null)
                {
                    dbCmdUploadedFile.Dispose();
                }
                CommonDB.CloseConnection(dbConUploadedFile);
            }
            return dsDataUploadedFile.Tables[0];
        }

        public static String UploadPDDData(ClsUploadPDDData objPData, int UserID, string strFileName)
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

                //dbCmd.CommandText = "USP_InsertPDD_Data";
                dbCmd.CommandText = "USP_UploadPDD_Data";

                dbCmd.Parameters.AddWithValue("@SP_BranchId", objPData.BranchId);
                dbCmd.Parameters.AddWithValue("@SP_LAN", objPData.LAN);
                dbCmd.Parameters.AddWithValue("@SP_Barcode", objPData.Barcode);
                dbCmd.Parameters.AddWithValue("@SP_State", objPData.State);
                dbCmd.Parameters.AddWithValue("@SP_BCName", objPData.BC_Name);
                dbCmd.Parameters.AddWithValue("@SP_CustomerName", objPData.Customer_Name);
                dbCmd.Parameters.AddWithValue("@SP_Exaternalcustno", objPData.Exaternalcustno);
                dbCmd.Parameters.AddWithValue("@SP_BCBranch", objPData.BC_Branch);
                dbCmd.Parameters.AddWithValue("@SP_CenterName", objPData.Center_Name);
                dbCmd.Parameters.AddWithValue("@SP_GroupName", objPData.Group_Name);
                dbCmd.Parameters.AddWithValue("@SP_Disbursed_Amount", objPData.Disbursed_Amount);
                dbCmd.Parameters.AddWithValue("@SP_Disbursement_Mode", objPData.Disbursement_Mode);
                dbCmd.Parameters.AddWithValue("@SP_DisbursementDate", objPData.Disbursement_Date);
                dbCmd.Parameters.AddWithValue("@SP_InwardDate", objPData.Inward_Date);
                dbCmd.Parameters.AddWithValue("@SP_ErrorFileName", strFileName);
                dbCmd.Parameters.AddWithValue("@SP_UploadFileId", objPData.UploadFileID);
                dbCmd.Parameters.AddWithValue("@SP_UserId", UserID);


                dbCmd.Parameters.Add(new SqlParameter("@SP_MESSAGE", SqlDbType.VarChar, 500)).Direction = ParameterDirection.Output;
                dbCmd.Parameters.Add(new SqlParameter("@SP_RETURNVALUE", SqlDbType.SmallInt, 0)).Direction = ParameterDirection.Output;
                //dbCmd.Parameters.Add(new SqlParameter("@RETURNSTATUS", SqlDbType.SmallInt, 0)).Direction = ParameterDirection.Output;

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

        public static int FileExist(string filename)
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

                dbCmd.CommandText = "USP_GETFILEEXIST";

                dbCmd.Parameters.AddWithValue("@FileName", filename);

                object result = dbCmd.ExecuteScalar();

                int fileId = Convert.ToInt32(result);

                return fileId;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int GetFileUpload(ClsUploadFile objUploadFile)
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

                dbCmd.CommandText = "USP_GETFILEID";

                dbCmd.Parameters.AddWithValue("@FileName", objUploadFile.FileName);
                dbCmd.Parameters.AddWithValue("@FilePath", objUploadFile.FilePath);
                dbCmd.Parameters.AddWithValue("@UploadedBy", objUploadFile.UploadedBy);

                object result = dbCmd.ExecuteScalar();

                int fileId = Convert.ToInt32(result);

                return fileId;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static DataTable GetSearchData(string searchval)
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

                dbCmd.CommandText = "USP_GetSearchPDDData";
                dbCmd.Parameters.AddWithValue("@SP_SearchVal", searchval);
                dbRdr.SelectCommand = dbCmd;
                dbRdr.Fill(dsData);
            }
            catch (Exception ex)
            {
                CommonDB.ClassLogError("ClsUploadData", "GetSearchData", Convert.ToString(ex.Message));
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

        internal static void UpdateFileUploadTime(int fileUploadID)
        {
            SqlDataAdapter dbRdrFlagUpdateTime = new SqlDataAdapter();
            SqlCommand dbCmdFlagUpdateTime = null;
            SqlConnection dbConFlagUpdateTime = null;
            DataSet dsDataFlagUpdateTime = new DataSet();
            try
            {
                dbConFlagUpdateTime = CommonDB.OpenConnection();
                dbCmdFlagUpdateTime = new SqlCommand();
                dbCmdFlagUpdateTime.Connection = dbConFlagUpdateTime;
                dbCmdFlagUpdateTime.CommandType = CommandType.StoredProcedure;

                dbCmdFlagUpdateTime.CommandText = "USP_UpdateUploadTime";
                dbCmdFlagUpdateTime.Parameters.AddWithValue("@fileID", fileUploadID);
                
                dbRdrFlagUpdateTime.SelectCommand = dbCmdFlagUpdateTime;
                dbRdrFlagUpdateTime.Fill(dsDataFlagUpdateTime);
            }
            catch (Exception ex)
            {
                CommonDB.ClassLogError("ClsUploadData", "GetSearchData", Convert.ToString(ex.Message));
            }
            finally
            {
                if (dbRdrFlagUpdateTime != null)
                {
                    dbRdrFlagUpdateTime.Dispose();
                }
                if (dbCmdFlagUpdateTime != null)
                {
                    dbCmdFlagUpdateTime.Dispose();
                }
                CommonDB.CloseConnection(dbConFlagUpdateTime);
            }
        }

        public static DataTable GetSearchAllData(string searchval)
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

                dbCmd.CommandText = "USP_GetSearchAllPDDData";
                dbCmd.Parameters.AddWithValue("@SP_SearchVal", searchval);
                dbRdr.SelectCommand = dbCmd;
                dbRdr.Fill(dsData);
            }
            catch (Exception ex)
            {
                CommonDB.ClassLogError("ClsUploadData", "GetSearchData", Convert.ToString(ex.Message));
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
        public static String UpdateVerificationStatus(string[] objData, int Userid)
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

                dbCmd.CommandText = "USP_UpdateVerificationStatus";
                dbCmd.Parameters.Add("@SP_ProcessId", SqlDbType.Int, 0).Value = objData[0] == "" ? (object)DBNull.Value : objData[0];
                dbCmd.Parameters.Add("@SP_Status", SqlDbType.VarChar, 10).Value = objData[1] == "" ? (object)DBNull.Value : objData[1];
                dbCmd.Parameters.Add("@SP_RejectReason", SqlDbType.VarChar, 100).Value = objData[2] == "" ? (object)DBNull.Value : objData[2];
                dbCmd.Parameters.Add("@SP_Remark", SqlDbType.VarChar, 100).Value = objData[3] == "" ? (object)DBNull.Value : objData[3];
                dbCmd.Parameters.Add("@SP_DisbNewDate", SqlDbType.VarChar, 20).Value = objData[4] == "" ? (object)DBNull.Value : objData[4];
                dbCmd.Parameters.Add("@SP_UserId", SqlDbType.Int, 0).Value = Userid == 0 ? (object)DBNull.Value : Userid;

                dbCmd.Parameters.Add(new SqlParameter("@SP_MESSAGE", SqlDbType.VarChar, 500)).Direction = ParameterDirection.Output;
                dbCmd.Parameters.Add(new SqlParameter("@SP_RETURNVALUE", SqlDbType.SmallInt, 0)).Direction = ParameterDirection.Output;

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

        public static DataTable GetArchiveData(string fromDate, string ToDate)
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

                dbCmd.CommandText = "USP_GetArchiveData";
                dbCmd.Parameters.AddWithValue("@SP_FromDate", fromDate);
                dbCmd.Parameters.AddWithValue("@SP_ToDate", ToDate);
                dbRdr.SelectCommand = dbCmd;
                dbRdr.Fill(dsData);
            }
            catch (Exception ex)
            {
                CommonDB.ClassLogError("ClsUploadData", "GetArchiveData", Convert.ToString(ex.Message));
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

        public static DataTable GetReprocessData(string fromDate, string ToDate)
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

                dbCmd.CommandText = "USP_GetReprocessData";
                dbCmd.Parameters.AddWithValue("@SP_FromDate", fromDate);
                dbCmd.Parameters.AddWithValue("@SP_ToDate", ToDate);
                dbRdr.SelectCommand = dbCmd;
                dbRdr.Fill(dsData);
            }
            catch (Exception ex)
            {
                CommonDB.ClassLogError("ClsUploadData", "GetReprocessData", Convert.ToString(ex.Message));
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

        public static DataTable GetDashboardData(string fromDate, string ToDate)
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

                dbCmd.CommandText = "USP_GetDashboardData";
                dbCmd.Parameters.AddWithValue("@SP_FromDate", fromDate);
                dbCmd.Parameters.AddWithValue("@SP_ToDate", ToDate);
                dbRdr.SelectCommand = dbCmd;
                dbRdr.Fill(dsData);
            }
            catch (Exception ex)
            {
                CommonDB.ClassLogError("ClsUploadData", "GetDashboardData", Convert.ToString(ex.Message));
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

        public static DataTable GetReportData(string fromDate, string ToDate, string rName, string tag)
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

                dbCmd.CommandText = "USP_GetReportData";
                dbCmd.Parameters.AddWithValue("@SP_FromDate", fromDate);
                dbCmd.Parameters.AddWithValue("@SP_ToDate", ToDate);
                dbCmd.Parameters.AddWithValue("@SP_rTyep", rName);
                dbCmd.Parameters.AddWithValue("@SP_Tag", tag);
                dbRdr.SelectCommand = dbCmd;
                dbRdr.Fill(dsData);
            }
            catch (Exception ex)
            {
                CommonDB.ClassLogError("ClsUploadData", "GetReportData", Convert.ToString(ex.Message));
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

        internal static void UpdateFileReadCompleteDateTime(int uploadFileID)
        {
            SqlDataAdapter dbRdrReadFlag = new SqlDataAdapter();
            SqlCommand dbCmdReadFlag = null;
            SqlConnection dbConReadFlag = null;
            DataSet dsDataReadFlag = new DataSet();
            try
            {
                dbConReadFlag = CommonDB.OpenConnection();
                dbCmdReadFlag = new SqlCommand();
                dbCmdReadFlag.Connection = dbConReadFlag;
                dbCmdReadFlag.CommandType = CommandType.StoredProcedure;

                dbCmdReadFlag.CommandText = "USP_UpdateReadFlag";
                dbCmdReadFlag.Parameters.AddWithValue("@fileID", uploadFileID);

                dbRdrReadFlag.SelectCommand = dbCmdReadFlag;
                dbRdrReadFlag.Fill(dsDataReadFlag);
            }
            catch (Exception ex)
            {
                CommonDB.ClassLogError("ClsUploadData", "GetSearchData", Convert.ToString(ex.Message));
            }
            finally
            {
                if (dbRdrReadFlag != null)
                {
                    dbRdrReadFlag.Dispose();
                }
                if (dbCmdReadFlag != null)
                {
                    dbCmdReadFlag.Dispose();
                }
                CommonDB.CloseConnection(dbConReadFlag);
            }
        }

        public static DataTable GetDashboardReportData(string fromDate, string ToDate, string rName, string tag)
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

                dbCmd.CommandText = "USP_GetDashboardReportData";
                dbCmd.Parameters.AddWithValue("@SP_FromDate", fromDate);
                dbCmd.Parameters.AddWithValue("@SP_ToDate", ToDate);
                dbCmd.Parameters.AddWithValue("@SP_rTyep", rName);
                dbCmd.Parameters.AddWithValue("@SP_Tag", tag);
                dbRdr.SelectCommand = dbCmd;
                dbRdr.Fill(dsData);
            }
            catch (Exception ex)
            {
                CommonDB.ClassLogError("ClsUploadData", "GetDashboardReportData", Convert.ToString(ex.Message));
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

        public static DataTable GetRejectedData(string fromDate, string ToDate, string tag)
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

                dbCmd.CommandText = "USP_GetRejectedData";
                dbCmd.Parameters.AddWithValue("@SP_FromDate", fromDate);
                dbCmd.Parameters.AddWithValue("@SP_ToDate", ToDate);
                dbCmd.Parameters.AddWithValue("@SP_Tag", tag);
                dbRdr.SelectCommand = dbCmd;
                dbRdr.Fill(dsData);
            }
            catch (Exception ex)
            {
                CommonDB.ClassLogError("ClsUploadData", "GetRejectedData", Convert.ToString(ex.Message));
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

        public static DataTable GetMISReportData(string fromDate, string ToDate)
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

                dbCmd.CommandText = "USP_GetMISReportData";
                dbCmd.Parameters.AddWithValue("@SP_FromDate", fromDate);
                dbCmd.Parameters.AddWithValue("@SP_ToDate", ToDate);
                dbRdr.SelectCommand = dbCmd;
                dbRdr.Fill(dsData);
            }
            catch (Exception ex)
            {
                CommonDB.ClassLogError("ClsUploadData", "GetMISReportData", Convert.ToString(ex.Message));
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

        public static DataTable GetMISReportPurgeData(string fromDate, string ToDate)
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

                dbCmd.CommandText = "USP_GetMISReportPurgeData";
                dbCmd.Parameters.AddWithValue("@SP_FromDate", fromDate);
                dbCmd.Parameters.AddWithValue("@SP_ToDate", ToDate);
                dbRdr.SelectCommand = dbCmd;
                dbRdr.Fill(dsData);
            }
            catch (Exception ex)
            {
                CommonDB.ClassLogError("ClsUploadData", "GetMISReportData", Convert.ToString(ex.Message));
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

        public static String UpdateInwardDate(string barcode, int Userid, string[] data)
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

                dbCmd.CommandText = "USP_UpdateInwardDate";
                dbCmd.Parameters.Add("@SP_Barcode", SqlDbType.VarChar, 20).Value = barcode == "" ? (object)DBNull.Value : barcode;
                dbCmd.Parameters.Add("@SP_Location", SqlDbType.Int, 0).Value = data[0] == "" ? (object)DBNull.Value : data[0];
                dbCmd.Parameters.Add("@SP_PODNumber", SqlDbType.VarChar, 50).Value = data[1] == "" ? (object)DBNull.Value : data[1];
                dbCmd.Parameters.Add("@SP_CourierName", SqlDbType.VarChar, 200).Value = data[2] == "" ? (object)DBNull.Value : data[2];
                dbCmd.Parameters.Add("@SP_ReceivedDate", SqlDbType.VarChar, 20).Value = data[3] == "" ? (object)DBNull.Value : data[3];
                dbCmd.Parameters.Add("@SP_Remark", SqlDbType.VarChar, 250).Value = data[4] == "" ? (object)DBNull.Value : data[4];
                dbCmd.Parameters.Add("@SP_UserId", SqlDbType.Int, 0).Value = Userid == 0 ? (object)DBNull.Value : Userid;

                dbCmd.Parameters.Add(new SqlParameter("@SP_MESSAGE", SqlDbType.VarChar, 500)).Direction = ParameterDirection.Output;
                dbCmd.Parameters.Add(new SqlParameter("@SP_RETURNVALUE", SqlDbType.SmallInt, 0)).Direction = ParameterDirection.Output;

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

        public static DataTable GetInwardData(int UserID)
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

                dbCmd.CommandText = "USP_GetInwardData";
                dbCmd.Parameters.AddWithValue("@SP_UserID", UserID);
                dbRdr.SelectCommand = dbCmd;
                dbRdr.Fill(dsData);
            }
            catch (Exception ex)
            {
                CommonDB.ClassLogError("ClsUploadData", "GetInwardData", Convert.ToString(ex.Message));
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

        public static String InsertInwardFile(string barcode, int Userid, string[] data)
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

                dbCmd.CommandText = "USP_InsertInwardFile";
                dbCmd.Parameters.Add("@SP_Barcode", SqlDbType.VarChar, 20).Value = barcode == "" ? (object)DBNull.Value : barcode;
                dbCmd.Parameters.Add("@SP_Location", SqlDbType.Int, 0).Value = data[0] == "" ? (object)DBNull.Value : data[0];
                dbCmd.Parameters.Add("@SP_PODNumber", SqlDbType.VarChar, 50).Value = data[1] == "" ? (object)DBNull.Value : data[1];
                dbCmd.Parameters.Add("@SP_CourierName", SqlDbType.VarChar, 200).Value = data[2] == "" ? (object)DBNull.Value : data[2];
                dbCmd.Parameters.Add("@SP_ReceivedDate", SqlDbType.VarChar, 20).Value = data[3] == "" ? (object)DBNull.Value : data[3];
                dbCmd.Parameters.Add("@SP_Remark", SqlDbType.VarChar, 250).Value = data[4] == "" ? (object)DBNull.Value : data[4];
                dbCmd.Parameters.Add("@SP_UserId", SqlDbType.Int, 0).Value = Userid == 0 ? (object)DBNull.Value : Userid;

                dbCmd.Parameters.Add(new SqlParameter("@SP_MESSAGE", SqlDbType.VarChar, 500)).Direction = ParameterDirection.Output;
                dbCmd.Parameters.Add(new SqlParameter("@SP_RETURNVALUE", SqlDbType.SmallInt, 0)).Direction = ParameterDirection.Output;

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

        public static DataTable GetDVURejectedData(string searchval)
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

                dbCmd.CommandText = "USP_GetDVURejectedData";
                dbCmd.Parameters.AddWithValue("@SP_SearchVal", searchval);
                dbRdr.SelectCommand = dbCmd;
                dbRdr.Fill(dsData);
            }
            catch (Exception ex)
            {
                CommonDB.ClassLogError("ClsUploadData", "GetSearchData", Convert.ToString(ex.Message));
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

        internal static DataTable GetAutoBarcodeData()
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

                dbCmd.CommandText = "USP_GetAutoBarcode";
                //dbCmd.Parameters.AddWithValue("@SP_SearchVal", searchval);
                dbRdr.SelectCommand = dbCmd;
                dbRdr.Fill(dsData);
            }
            catch (Exception ex)
            {
                CommonDB.ClassLogError("ClsUploadData", "GetSearchData", Convert.ToString(ex.Message));
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

        public static String UpdateReprocessStatus(string[] objData, int Userid)
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

                dbCmd.CommandText = "USP_UpdateReprocessStatus";
                dbCmd.Parameters.Add("@SP_ProcessId", SqlDbType.Int, 0).Value = objData[0] == "" ? (object)DBNull.Value : objData[0];
                dbCmd.Parameters.Add("@SP_Status", SqlDbType.VarChar, 10).Value = objData[1] == "" ? (object)DBNull.Value : objData[1];
                dbCmd.Parameters.Add("@SP_Remark", SqlDbType.VarChar, 100).Value = objData[2] == "" ? (object)DBNull.Value : objData[2];
                dbCmd.Parameters.Add("@SP_UserId", SqlDbType.Int, 0).Value = Userid == 0 ? (object)DBNull.Value : Userid;

                dbCmd.Parameters.Add(new SqlParameter("@SP_MESSAGE", SqlDbType.VarChar, 500)).Direction = ParameterDirection.Output;
                dbCmd.Parameters.Add(new SqlParameter("@SP_RETURNVALUE", SqlDbType.SmallInt, 0)).Direction = ParameterDirection.Output;

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

        public static String UpdateFile_LockUnlock(int ProcessId, int Userid, string IsLoack)
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

                dbCmd.CommandText = "USP_UpdateFile_LockUnlock";
                dbCmd.Parameters.Add("@SP_ProcessId", SqlDbType.Int, 0).Value = ProcessId == 0 ? (object)DBNull.Value : ProcessId;
                dbCmd.Parameters.Add("@SP_UserId", SqlDbType.Int, 0).Value = Userid == 0 ? (object)DBNull.Value : Userid;
                dbCmd.Parameters.Add("@SP_IsLock", SqlDbType.Char, 1).Value = IsLoack == "" ? (object)DBNull.Value : IsLoack;

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

        public static String UploadArchivalData(ClsUploadArchivalData objPData, int UserID, string strFileName)
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

                //dbCmd.CommandText = "USP_UpdateArchival_Data";
                dbCmd.CommandText = "USP_UploadArchival_Data";

                dbCmd.Parameters.AddWithValue("@SP_Barcode", objPData.Barcode);
                dbCmd.Parameters.AddWithValue("@SP_ArchivalDate", objPData.ArchivalDate);
                dbCmd.Parameters.AddWithValue("@SP_LotNo", objPData.LotNo);
                dbCmd.Parameters.AddWithValue("@SP_CartonNo", objPData.CartonNo);
                dbCmd.Parameters.AddWithValue("@SP_FileNo", objPData.FileNo);
                dbCmd.Parameters.AddWithValue("@SP_ErrorFileName", strFileName);
                dbCmd.Parameters.AddWithValue("@SP_UploadFileId", objPData.UploadFileID);
                dbCmd.Parameters.AddWithValue("@SP_UserId", UserID);


                dbCmd.Parameters.Add(new SqlParameter("@SP_MESSAGE", SqlDbType.VarChar, 500)).Direction = ParameterDirection.Output;
                dbCmd.Parameters.Add(new SqlParameter("@SP_RETURNVALUE", SqlDbType.SmallInt, 0)).Direction = ParameterDirection.Output;
                //dbCmd.Parameters.Add(new SqlParameter("@RETURNSTATUS", SqlDbType.SmallInt, 0)).Direction = ParameterDirection.Output;

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


        public static String UploadPurgingData(string Barcode, int UserID, string strFileName,string strFilePath)
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

                dbCmd.CommandText = "USP_UploadPurging_Data";

                dbCmd.Parameters.AddWithValue("@SP_Barcode",Barcode);
                dbCmd.Parameters.AddWithValue("@SP_FileName", strFileName);
                dbCmd.Parameters.AddWithValue("@SP_FilePath", strFilePath);
                dbCmd.Parameters.AddWithValue("@SP_UserId", UserID);

                dbCmd.Parameters.Add(new SqlParameter("@SP_MESSAGE", SqlDbType.VarChar, 500)).Direction = ParameterDirection.Output;
                dbCmd.Parameters.Add(new SqlParameter("@SP_RETURNVALUE", SqlDbType.SmallInt, 0)).Direction = ParameterDirection.Output;
                //dbCmd.Parameters.Add(new SqlParameter("@RETURNSTATUS", SqlDbType.SmallInt, 0)).Direction = ParameterDirection.Output;

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

        public static DataTable GetArchivalGridData(int UserID)
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

                dbCmd.CommandText = "USP_GetUploadArchivalData";
                dbCmd.Parameters.AddWithValue("@SP_UserID", UserID);
                dbRdr.SelectCommand = dbCmd;
                dbRdr.Fill(dsData);
            }
            catch (Exception ex)
            {
                CommonDB.ClassLogError("ClsUploadData", "GetArchivalGridData", Convert.ToString(ex.Message));
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

        public static DataTable GetPurgingGridData(int UserID)
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

                dbCmd.CommandText = "USP_GetUploadPurgingData";
                dbCmd.Parameters.AddWithValue("@SP_UserID", UserID);
                dbRdr.SelectCommand = dbCmd;
                dbRdr.Fill(dsData);
            }
            catch (Exception ex)
            {
                CommonDB.ClassLogError("ClsUploadData", "GetArchivalGridData", Convert.ToString(ex.Message));
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

        public static DataTable GetWIMSInwardReportData(string fromDate, string ToDate, string CourierName)
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

                dbCmd.CommandText = "USP_GetWIMSInwardReport";
                dbCmd.Parameters.AddWithValue("@SP_FromDate", fromDate);
                dbCmd.Parameters.AddWithValue("@SP_ToDate", ToDate);
                dbCmd.Parameters.AddWithValue("@SP_CourierName", CourierName);
                
                dbRdr.SelectCommand = dbCmd;
                dbRdr.Fill(dsData);
            }
            catch (Exception ex)
            {
                CommonDB.ClassLogError("ClsUploadData", "GetWIMSInwardReportData", Convert.ToString(ex.Message));
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

        public static string DeleteDataByBarcode(string Barcode,string strFileName, int UserID)
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

                //dbCmd.CommandText = "USP_UpdateArchival_Data";
                dbCmd.CommandText = "USP_DeletePDDMasterData";

                dbCmd.Parameters.AddWithValue("@SP_Barcode", Barcode);
                dbCmd.Parameters.AddWithValue("@SP_FileName", strFileName);
                dbCmd.Parameters.AddWithValue("@SP_UserId", UserID);

                dbCmd.Parameters.Add(new SqlParameter("@SP_MESSAGE", SqlDbType.VarChar, 500)).Direction = ParameterDirection.Output;
                dbCmd.Parameters.Add(new SqlParameter("@SP_RETURNVALUE", SqlDbType.SmallInt, 0)).Direction = ParameterDirection.Output;
                //dbCmd.Parameters.Add(new SqlParameter("@RETURNSTATUS", SqlDbType.SmallInt, 0)).Direction = ParameterDirection.Output;

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

        public static DataTable GetPDDDeletedData(int UserID)
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

                dbCmd.CommandText = "USP_GetPDDDeletedData";
                dbCmd.Parameters.AddWithValue("@SP_UserID", UserID);
                dbRdr.SelectCommand = dbCmd;
                dbRdr.Fill(dsData);
            }
            catch (Exception ex)
            {
                CommonDB.ClassLogError("ClsUploadData", "GetArchivalGridData", Convert.ToString(ex.Message));
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

        internal static DataTable GetErrorLog(string fileName)
        {
            SqlDataAdapter dbRdrErrorLogFile = new SqlDataAdapter();
            SqlCommand dbCmdErrorLogFile = null;
            SqlConnection dbConErrorLogFile = null;
            DataSet dsDataErrorLogFile = new DataSet();
            try
            {
                dbConErrorLogFile = CommonDB.OpenConnection();
                dbCmdErrorLogFile = new SqlCommand();
                dbCmdErrorLogFile.Connection = dbConErrorLogFile;
                dbCmdErrorLogFile.CommandType = CommandType.StoredProcedure;

                dbCmdErrorLogFile.CommandText = "USP_GetErrorFileLog";
                dbCmdErrorLogFile.Parameters.AddWithValue("@fileName", fileName);                
                dbRdrErrorLogFile.SelectCommand = dbCmdErrorLogFile;
                dbRdrErrorLogFile.Fill(dsDataErrorLogFile);
            }
            catch (Exception ex)
            {
                CommonDB.ClassLogError("ClsUploadData", "GetMISReportData", Convert.ToString(ex.Message));
            }
            finally
            {
                if (dbRdrErrorLogFile != null)
                {
                    dbRdrErrorLogFile.Dispose();
                }
                if (dbCmdErrorLogFile != null)
                {
                    dbCmdErrorLogFile.Dispose();
                }
                dbConErrorLogFile.Close();
            }
            return dsDataErrorLogFile.Tables[0];
        }

        public static DataTable GetParentChildSheetData(string fromDate, string ToDate)
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

                dbCmd.CommandText = "USP_GetParentChildSheet";
                dbCmd.Parameters.AddWithValue("@SP_FromDate", fromDate);
                dbCmd.Parameters.AddWithValue("@SP_ToDate", ToDate);
                dbRdr.SelectCommand = dbCmd;
                dbRdr.Fill(dsData);
            }
            catch (Exception ex)
            {
                CommonDB.ClassLogError("ClsUploadData", "GetMISReportData", Convert.ToString(ex.Message));
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

        internal static void UpdateFlagForFileUploaded(int fileUploadID, char uploadStatus, char readStatus, string fileType, int brID)
        {
            SqlDataAdapter dbRdrFlag = new SqlDataAdapter();
            SqlCommand dbCmdFlag = null;
            SqlConnection dbConFlag = null;
            DataSet dsDataFlag = new DataSet();
            try
            {
                dbConFlag = CommonDB.OpenConnection();
                dbCmdFlag = new SqlCommand();
                dbCmdFlag.Connection = dbConFlag;
                dbCmdFlag.CommandType = CommandType.StoredProcedure;

                dbCmdFlag.CommandText = "USP_UpdateFlag";
                dbCmdFlag.Parameters.AddWithValue("@fileID", fileUploadID);
                dbCmdFlag.Parameters.AddWithValue("@uploadStatus", uploadStatus);
                dbCmdFlag.Parameters.AddWithValue("@readStatus", readStatus);
                dbCmdFlag.Parameters.AddWithValue("@fileType", fileType);
                dbCmdFlag.Parameters.AddWithValue("@branchID", brID);
                dbRdrFlag.SelectCommand = dbCmdFlag;
                dbRdrFlag.Fill(dsDataFlag);
            }
            catch (Exception ex)
            {
                CommonDB.ClassLogError("ClsUploadData", "GetSearchData", Convert.ToString(ex.Message));
            }
            finally
            {
                if (dbRdrFlag != null)
                {
                    dbRdrFlag.Dispose();
                }
                if (dbCmdFlag != null)
                {
                    dbCmdFlag.Dispose();
                }
                CommonDB.CloseConnection(dbConFlag);
            }
        }

        internal static DataTable GetUnreadFileID(string FileType)
        {
            SqlDataAdapter dbRdrUnreadFile = new SqlDataAdapter();
            SqlCommand dbCmdUnreadFile = null;
            SqlConnection dbConUnreadFile = null;
            DataSet dsDataUnreadFile = new DataSet();
            try
            {
                dbConUnreadFile = CommonDB.OpenConnection();
                dbCmdUnreadFile = new SqlCommand();
                dbCmdUnreadFile.Connection = dbConUnreadFile;
                dbCmdUnreadFile.CommandType = CommandType.StoredProcedure;

                dbCmdUnreadFile.CommandText = "USP_GetUnreadFile";
                dbCmdUnreadFile.Parameters.AddWithValue("@fileType", FileType);

                dbRdrUnreadFile.SelectCommand = dbCmdUnreadFile;
                dbRdrUnreadFile.Fill(dsDataUnreadFile);
            }
            catch (Exception ex)
            {
                CommonDB.ClassLogError("ClsUploadData", "GetSearchData", Convert.ToString(ex.Message));
            }
            finally
            {
                if (dbRdrUnreadFile != null)
                {
                    dbRdrUnreadFile.Dispose();
                }
                if (dbCmdUnreadFile != null)
                {
                    dbCmdUnreadFile.Dispose();
                }
                CommonDB.CloseConnection(dbConUnreadFile);
            }
            return dsDataUnreadFile.Tables[0];
        }

        internal static DataTable GetCurrentFile(int fileID)
        {
            SqlDataAdapter dbRdrUnreadFile = new SqlDataAdapter();
            SqlCommand dbCmdUnreadFile = null;
            SqlConnection dbConUnreadFile = null;
            DataSet dsDataUnreadFile = new DataSet();
            try
            {
                dbConUnreadFile = CommonDB.OpenConnection();
                dbCmdUnreadFile = new SqlCommand();
                dbCmdUnreadFile.Connection = dbConUnreadFile;
                dbCmdUnreadFile.CommandType = CommandType.StoredProcedure;

                dbCmdUnreadFile.CommandText = "USP_GetCurrentFile";
                dbCmdUnreadFile.Parameters.AddWithValue("@fileID", fileID);

                dbRdrUnreadFile.SelectCommand = dbCmdUnreadFile;
                dbRdrUnreadFile.Fill(dsDataUnreadFile);
            }
            catch (Exception ex)
            {
                CommonDB.ClassLogError("ClsUploadData", "GetSearchData", Convert.ToString(ex.Message));
            }
            finally
            {
                if (dbRdrUnreadFile != null)
                {
                    dbRdrUnreadFile.Dispose();
                }
                if (dbCmdUnreadFile != null)
                {
                    dbCmdUnreadFile.Dispose();
                }
                CommonDB.CloseConnection(dbConUnreadFile);
            }
            return dsDataUnreadFile.Tables[0];
        }
    }

    
}