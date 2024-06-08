using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;

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
                return BadRequest("No file selected for upload...");

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
                        parser.ReadLine();

                    string separador = ",";
                    string linea;
                    // Si el archivo no tiene encabezado, elimina la siguiente línea
                    //System.IO.StreamReader archivo = new System.IO.StreamReader(fileName);
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
                                company.company_name = fila[1];

                            }

                            charger.Company.company_id = null;

                            if (fila[2] != "" || fila[2] != null)
                            {
                                company.company_name = fila[2];

                            }
                            company.company_id = null;

                            if (fila[2] != "" || fila[2] != null)
                            {
                                company.company_name = fila[2];

                            }
                            charger.amount = 0;

                            if (fila[1] != "" || fila[1] != null)
                            {
                                charger.amount = Convert.ToDecimal(fila[3]);

                            }
                            charger.status = fila[4];

                            if (fila[1] != "" || fila[1] != null)
                            {
                                company.company_name = fila[1];

                            }
                            charger.created_at = Convert.ToDateTime(fila[5]);

                            if (fila[1] != "" || fila[1] != null)
                            {
                                company.company_name = fila[1];

                            }
                            charger.updated_at = null;

                            if (fila[6] != "")
                            {
                                charger.updated_at = Convert.ToDateTime(fila[6]);
                            }

                            if (company.company_id == "" || company.company_id != "" && company.company_name == "" || company.company_name != "")
                            {
                                Negocio.Company.Add(company);

                            }


                            Negocio.Charger.Add(charger);
                        }
                    }

                    return View();
                }
            }


        }
    }
}
