using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Company
    {
        public static (bool, string, Exception) Add(Datos.Company company)
        {
			try
			{
				using (Datos.MfloresPruebaContext context = new Datos.MfloresPruebaContext())
				{

                    int rowsAffected = context.Database.ExecuteSqlRaw($"AddCompany '{company.company_id}','{company.company_name}'");

                    if (rowsAffected > 0)
                    {
                        return (true, "Registro Agregado", null);
                    }
                    else
                    {
                        return (false, "Registro No Agregado", null);
                    }
                }
			}
			catch (Exception ex)
			{

				throw;
			}
        }
    }
}
