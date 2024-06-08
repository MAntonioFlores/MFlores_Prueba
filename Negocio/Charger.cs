namespace Negocio
{
    public class Charger
    {
        public static (bool, string, Exception) Add(Modelo.Charger charger)
        {
            try
            {
                using (Datos.MfloresPruebaContext context = new Datos.MfloresPruebaContext())
                {
                    Datos.Charger charger1 = new Datos.Charger();

                    charger1.Id = charger.id;
                    charger1.Amount = charger.amount;
                    charger1.Status = charger.status;
                    charger1.CreatedAt = charger.created_at;
                    charger1.UpdatedAt = charger.updated_at;
                    charger1.CompanyId = charger.Company.company_id;

                    context.Chargers.Add(charger1);
                    var rowAffected = context.SaveChanges();
                    if (rowAffected > 0)
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
                return (false, "Ocurrio un error: " + ex, ex);
            }
        }
    }
}
