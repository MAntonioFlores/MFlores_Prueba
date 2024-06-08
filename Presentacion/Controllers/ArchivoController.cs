using CsvHelper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Presentacion.Controllers
{
    public class ArchivoController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public IActionResult Archivo()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Archivo(IFormFile ArchivoCSV)
        {


            Modelo.Charger charger = new Modelo.Charger();
            Modelo.Company company = new Modelo.Company();
            List<string> listaErrores = new List<string>();

            if (ArchivoCSV == null || ArchivoCSV.Length == 0)
            {

                return BadRequest("No seleccionaste un archivo a subir...");
            }

            string fileName = Path.GetFileName(ArchivoCSV.FileName);

            using (var stream = new MemoryStream())
            {
                ArchivoCSV.CopyTo(stream);
                stream.Position = 0;


                using (TextFieldParser parser = new TextFieldParser(stream))
                {

                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");

                    // Ignorar la primera línea si es el encabezado
                    if (!parser.EndOfData)
                    {

                        parser.ReadLine();
                    }

                    string separador = ",";
                    string linea;
                    // Si el archivo no tiene encabezado, elimina la siguiente línea
                    parser.ReadLine(); // Leer la primera línea pero descartarla porque es el encabezado

                    int NumeroRegistro = 1;

                    while ((linea = parser.ReadLine()) != null)
                    {
                        if (linea != "")
                        {
                            string[] fila = linea.Split(separador);

                            //Remplazar todo estoooo --------------------------
                            charger.id = null;

                            if (fila[0] != "" || fila[0] != null)
                            {
                                charger.id = fila[0];
                            }
                            //------------------------------------


                            // POR ESTO -------------------------
                            charger.id = (fila[0] != "" || fila[0] != null) ? fila[0] : ""; //Acordarse del valor que se ponga por defecto
                            // ---------------------------------

                            charger.Company = new Modelo.Company();

                            company.company_name = (fila[1] != "" || fila[1] != null) ? fila[1] : "";

                            charger.Company.company_name = (fila[1] != "" || fila[1] != null) ? fila[1] : "";
                                
                            company.company_id = (fila[2] != "" || fila[2] != null) ? fila[2] : "";

                            charger.Company.company_id = (fila[2] != "" || fila[2] != null) ? fila[2] : "";

                            charger.amount = (Convert.ToDecimal(fila[3]) != 0) ? Convert.ToDecimal(fila[3]) : 0;

                            charger.status = (fila[4] != "" || fila[4] != null) ? fila[4] : "";

                            charger.created_at = (Convert.ToDateTime(fila[5]) != null) ? Convert.ToDateTime(fila[5]) : default;

                            charger.updated_at = string.IsNullOrWhiteSpace(fila[6]) ? null : Convert.ToDateTime(fila[6]);

                            //if (company.company_id != null || company.company_id != "" && company.company_name != null || company.company_name != "")
                            //{
                            //    Negocio.Company.Add(company);

                            //}

                            //REMPLEAZAR ESTO *----------------------------------------------------------------------
                            //if (charger.id != null || charger.id != "" && charger.amount != null || charger.amount != 0 && charger.status != null || charger.status != ""
                            //    && charger.created_at != null || charger.created_at != Convert.ToDateTime("") && charger.updated_at != Convert.ToDateTime(null)
                            //    || charger.updated_at != Convert.ToDateTime(""))
                            //{

                            //    Negocio.Charger.Add(charger);
                            //}

                            //----------------------------*

                            //POR ESTO -----------------------
                            string mensajeError = Validar(NumeroRegistro, charger, company);

                            if (mensajeError == "")
                            {
                                Negocio.Company.Add(company);

                                Negocio.Charger.Add(charger);
                            }
                            else
                            {
                                listaErrores.Add(mensajeError);
                            }
                        }

                    } //LLAVE DE CIERRE DEL WHILE



                    //ERROR

                    if (listaErrores.Count > 0)
                    {
                        string errorFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "~/Files", "logErrores.txt");
                        //COMO INYECTAR ESTA DEPENDENCIA
                        Directory.CreateDirectory(Path.GetDirectoryName(errorFilePath));

                        System.IO.File.WriteAllLinesAsync(errorFilePath, listaErrores, Encoding.UTF8);
                        HttpContext.Session.SetString("RutaDescarga", errorFilePath);
                    }

                    ViewBag.Text = "El Archivo contiene errores";
                    return PartialView("Modal");
                    return View(); //MODAL //MODAL IF con RAZOR (Session si tiene tiene info (btn de descargar) ("Ocurrieron errores")
                                   //Todos los registros se guardaron con exito
                }
            }
        }

        public string Validar(int numeroRegistro, Modelo.Charger charger, Modelo.Company company)
        {
            string MensajeError = "";

            if (charger.id == "")
            {
                MensajeError += "Falta el IdCharger, ";
            }
            if (company.company_name == "" || charger.Company.company_name == "")
            {
                MensajeError += "Falta el CompanyName, ";
            }
            if (charger.Company.company_id == "" || company.company_id == "")
            {
                MensajeError += "Falta el CompanyId, ";
            }
            if (charger.amount == 0)
            {
                MensajeError += "Falta el Amount, ";
            }
            if (charger.status == "")
            {
                MensajeError += "Falta el Status, ";
            }
            if (charger.created_at == Convert.ToDateTime(null))
            {
                MensajeError += "Falta el Created_at, ";
            }
            if (charger.updated_at == Convert.ToDateTime(null))
            {
                MensajeError += "Falta el Updated_at, ";
            }
            //CON TODOS LOS DEMAS CAMPOS

            if (MensajeError != "")
            {
                MensajeError += " En el registro N:" + numeroRegistro.ToString();
            }

            return MensajeError;
        }

        //AQUI METER EL METODO DE DOWNLOAD

        [HttpGet]
        public IActionResult Download(string pathError)
        {
            HttpContext.Session.SetString("Ruta", pathError);
            string contentType = "text/plain";
            return File(pathError, contentType, Path.GetFileName(pathError));
        }

    }
}
