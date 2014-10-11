using System;
using System.Web.UI;
using System.Xml;
using System.Web;
using System.Collections;
using PruebaCodigoBizagi.App_Code;

namespace PruebaCodigoBizagi
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                System.Diagnostics.Debug.WriteLine("wujuuu post back");
            }
        }

        protected void submit(object sender, EventArgs e)
        {
            if (System.IO.Path.GetFileName(File.PostedFile.FileName).EndsWith(".xpdl"))
            {
                if ((File.PostedFile != null) && (File.PostedFile.ContentLength > 0))
                {
                    string fn = System.IO.Path.GetFileName(File.PostedFile.FileName);
                    string SaveLocation = Server.MapPath("Data") + "\\" + fn;
                    try
                    {
                        File.PostedFile.SaveAs(SaveLocation);

                        EntidadDeValidacion entidad = new EntidadDeValidacion(SaveLocation);
                        string json = entidad.validarReglasBPMN(SaveLocation);
                        System.Diagnostics.Debug.WriteLine(json);
                        Response.Clear();
                        Response.ContentType = "application/json; charset=utf-8";
                        Response.Write(json);
                        //Response.End();

                        Response.Redirect("About.aspx", false);
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                        //Response.Write("El archivo se ha cargado.");
                    }
                    catch (Exception ex)
                    {
                        Response.Write("Error : " + ex.Message);
                        //Nota: Exception.Message devuelve un mensaje detallado que describe la excepción actual. 
                        //Por motivos de seguridad, no se recomienda devolver Exception.Message a los usuarios finales de 
                        //entornos de producción. Sería más aconsejable poner un mensaje de error genérico. 
                    }
                }
                else
                {
                    Response.Write("Seleccione un archivo que cargar.");
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Archivo no es xpdl");
                Response.Write("Seleccione un archivo que cargar.");
            }
        }
    }
}