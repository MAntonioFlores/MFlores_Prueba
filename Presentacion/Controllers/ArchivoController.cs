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
        public IActionResult Archivo()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Archivo(IFormFile ArchivoCSV)
        {

            Modelo.Charger charger = new Modelo.Charger();
            Modelo.Company company = new Modelo.Company();

            if (ArchivoCSV == null || ArchivoCSV.Length == 0)
            {

                return BadRequest("No seleccionaste un archivo a subir...");
            }

            string fileName = Path.GetFileName(ArchivoCSV.FileName);

            List<string> errores = new List<string>();

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

                    while ((linea = parser.ReadLine()) != null)
                    {
                        if (linea != "")
                        {
                            string[] fila = linea.Split(separador);

                            charger.id = null;

                            if (fila[0] != "" || fila[0] != null)
                            {
                                charger.id = fila[0];
                            }

                            charger.Company = new Modelo.Company();

                            company.company_name = null;

                            if (fila[1] != "" || fila[1] != null)
                            {
                                company.company_name = fila[1];

                            }

                            charger.Company.company_name = null;

                            if (fila[1] != "" || fila[1] != null)
                            {
                                charger.Company.company_name = fila[1];

                            }

                            charger.Company.company_id = null;

                            if (fila[2] != "" || fila[2] != null)
                            {
                                charger.Company.company_id = fila[2];

                            }
                            company.company_id = null;

                            if (fila[2] != "" || fila[2] != null)
                            {
                                company.company_id = fila[2];

                            }
                            charger.amount = 0;

                            if (fila[3] != "" || fila[3] != null)
                            {
                                charger.amount = Convert.ToDecimal(fila[3]);

                            }
                            charger.status = null;

                            if (fila[4] != "" || fila[4] != null)
                            {
                                charger.status = fila[4];

                            }

                            charger.created_at = Convert.ToDateTime(fila[5]);


                            charger.updated_at = null;

                            if (fila[6] != "")
                            {
                                charger.updated_at = Convert.ToDateTime(fila[6]);
                            }

                            if (company.company_id != null || company.company_id != "" && company.company_name != null || company.company_name != "")
                            {
                                Negocio.Company.Add(company);

                            }
                            if (charger.id != null || charger.id != "" && charger.amount != null || charger.amount != 0 && charger.status != null || charger.status != "" 
                                && charger.created_at != null || charger.created_at != Convert.ToDateTime("") && charger.updated_at != Convert.ToDateTime(null)
                                || charger.updated_at != Convert.ToDateTime(""))
                            {

                                Negocio.Charger.Add(charger);
                            }
                            //else
                            //{
                            //    errores.Add(string.Format(",", fila));
                            //}
                        }
                        //if (errores.Count > 0)
                        //{
                        //    string errorFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "Files", "logErrores.txt");
                        //    Directory.CreateDirectory(Path.GetDirectoryName(errorFilePath));

                        //    System.IO.File.WriteAllLinesAsync(errorFilePath, errores, Encoding.UTF8);
                        //    HttpContext.Session.SetString("RutaDescarga", errorFilePath);
                        //}
                    }

                        return View();
                }
            }
        }

        //[HttpPost]
        //public IActionResult GetAll(ML.Empresa empresa)
        //{
        //    HttpPostedFileBase file = Request.Files["ArchivoErrorTxt"];

        //    ML.Result resultError = Readfile(file);
        //    if (resultError.Objects.Count > 0)
        //    {
        //        string fileError = Server.MapPath(@"~\Files\logErrores.txt");
        //        using (Streamwriter writer = new Streamwriter(fileError))
        //        {
        //            foreach (string In in resultError.Objects)
        //            {
        //                writer.WriteLine(ln);
        //            }
        //        }
        //        HttpContext.Session.SetString("RutaDescarga", fileError);
        //        ViewBag.Mensaje = "Ocurrio un error al insertar las empresas, para mayor información descargue el Archivo Log.txt";

        //    }
        //}
    }
}
