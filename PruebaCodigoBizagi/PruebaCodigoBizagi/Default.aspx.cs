using System;
using System.Web.UI;

namespace PruebaCodigoBizagi
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void submit(object sender, EventArgs e)
        {
            if(System.IO.Path.GetFileName(File.PostedFile.FileName).EndsWith(".xpdl")){
                if ((File.PostedFile != null) && (File.PostedFile.ContentLength > 0))
                {
                    string fn = System.IO.Path.GetFileName(File.PostedFile.FileName);
                    string SaveLocation = Server.MapPath("Data") + "\\" + fn;
                    try
                    {
                        File.PostedFile.SaveAs(SaveLocation);
                        Response.Write("El archivo se ha cargado.");
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
            else{
                Response.Write("Seleccione un archivo que cargar.");
            }
        }
    }
}