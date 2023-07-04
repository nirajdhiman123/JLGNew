using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JLG.Services
{
    /// <summary>
    /// Summary description for GenericHandler
    /// </summary>
    public class GenericHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string f = context.Request.QueryString.Get("f");
                string FileType = string.Empty;
                if (f.IndexOf(".tif") > -1 || f.IndexOf(".tiff") > -1 || f.IndexOf(".pdf") > -1)
                {
                    FileType = context.Request.QueryString.Get("f").IndexOf(".tif") > -1 ? "tif" : "pdf";
                    context.Response.Clear();
                    if (FileType.ToLower() == "tif")
                        context.Response.ContentType = "image/tiff";
                    else
                        context.Response.ContentType = "Application/pdf";
                    context.Response.WriteFile(f);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
                else if (f.IndexOf(".jpeg") > -1 || f.IndexOf(".jpg") > -1 || f.IndexOf(".bmp") > -1 || f.IndexOf(".gif") > -1 || f.IndexOf(".png") > -1)
                {

                    context.Response.Clear();
                    context.Response.ContentType = "image/jpeg";
                    context.Response.WriteFile(f);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
                else if (f.IndexOf(".mp4") > -1 || f.IndexOf(".wav") > -1 || f.IndexOf(".mp3") > -1)
                {

                    context.Response.Clear();
                    //context.Response.ContentType = "image/jpeg";
                    //context.Response.ContentType = "vedio/mp4";
                    context.Response.ContentType = "audio/wav";
                    context.Response.WriteFile(f);

                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
                //else if (f.IndexOf(".mp4") > -1 || f.IndexOf(".wav") > -1 || f.IndexOf(".mp3") > -1)
                //{
                //    byte[] bytes;
                //    //string contentType;
                //    string name;
                //    //bytes = File.ReadAllBytes(f);
                //    using (BinaryReader br = new BinaryReader(File.Open(f, FileMode.Open)))
                //    {
                //        bytes = br.ReadBytes((int)br.BaseStream.Length);
                //    }
                //    //bytes = (byte[])sdr["Data"];
                //    context.Response.ContentType = "audio/mpeg3";
                //    name = Path.GetFileName(f);
                //    context.Response.Clear();
                //    context.Response.Buffer = true;
                //    context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + name);
                //    context.Response.ContentType = "audio/mpeg3";
                //    context.Response.BinaryWrite(bytes);
                //    context.Response.End();

                //}
                //string l = context.Request.QueryString.Get"t"("l");
                //string TodayDate = DateTime.Today.ToString("yyyy-MM-dd");
                //string TempFolder = System.Configuration.ConfigurationManager.AppSettings[l];
                //f = @TempFolder + f;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}