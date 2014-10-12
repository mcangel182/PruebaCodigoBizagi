using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using PruebaCodigoBizagi.App_Code;
using System.Collections.Generic;
using System.Text;

namespace PruebaCodigoBizagi
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string response = Session["fromSender"].ToString(); 

            HttpContext _context = HttpContext.Current;
            string response = _context.Items["json"].ToString();
            
            if (!string.IsNullOrEmpty(response))
            {
                string list = "";
                if (response.Contains("PASO_VALIDACIONES"))
                {
                    titulo.Text += "No se encontraron errores en el diagrama:";
                    list += "<li class=\"check\"><h5>Su diagrama pasó todas las validaciones</h5><br></li>";
                    if(response.Contains("Sample.xpdl"))
                        imagenDiagrama.Text = "<img src=\"Images/0.png\" />";
                }
                else
                {
                    JavaScriptSerializer jsonSerialiser = new JavaScriptSerializer();
                    List<Validacion> validaciones = jsonSerialiser.Deserialize<List<Validacion>>(response);
               
                    titulo.Text += "Se encontraron los siguientes errores en el diagrama:";
                    foreach (Validacion validacion in validaciones)
                    {
                        list += "<li class=\"warning\">" + validacion.mensaje + "</li>";
                    }

                    if (validaciones[0].rutaDiagrama.Contains("Sample 1.xpdl"))
                    {
                        imagenDiagrama.Text = "<img src=\"Images/1.png\" />";
                    }
                    else if (validaciones[0].rutaDiagrama.Contains("Sample 2.xpdl"))
                    {
                        imagenDiagrama.Text = "<img src=\"Images/2.png\" />";
                    }
                    else if (validaciones[0].rutaDiagrama.Contains("Sample 3.xpdl"))
                    {
                        imagenDiagrama.Text = "<img src=\"Images/3.png\" />";
                    }
                    else if (validaciones[0].rutaDiagrama.Contains("Sample 4.xpdl"))
                    {
                        imagenDiagrama.Text = "<img src=\"Images/4.png\" />";
                    }
                }
                listaValidaciones.Text = list;
            }
        }
    }
}