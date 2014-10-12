using System;
using System.Web.UI;
using System.Xml;
using System.Web;
using System.Collections;
using PruebaCodigoBizagi.App_Code;
using System.Threading;

namespace PruebaCodigoBizagi
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void submit(object sender, EventArgs e)
        {
            string path = System.IO.Path.GetFileName(File.PostedFile.FileName);
            if ((File.PostedFile != null) && (File.PostedFile.ContentLength > 0))
            {
                if (path.EndsWith(".xpdl"))
                {
                    string SaveLocation = Server.MapPath("Data") + "\\" + path;
                    try
                    {
                        File.PostedFile.SaveAs(SaveLocation);

                        EntidadDeValidacion entidad = new EntidadDeValidacion(SaveLocation);
                        string json = entidad.validarReglasBPMN();

                        HttpContext _context = HttpContext.Current;
                        _context.Items.Add("json", json);
                        Server.Transfer("Validacion.aspx");

                        //Session["fromSender"] = json;
                        //Response.Redirect("About.aspx", false);
                        //HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }
                    catch (ThreadAbortException ex)
                    {

                    }
                }
                else
                {
                    error.Text += "La extensión del archivo debe ser .xpdl.";
                }
            }
            else
            {
                error.Text += "Seleccione un archivo que cargar.";
            }
        }
    }
}