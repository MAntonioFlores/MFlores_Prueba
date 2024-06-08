using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class NumerosNaturales
    {
        private List<int> Numeros;

        public static NumerosNaturales shared = new NumerosNaturales();

        private NumerosNaturales()
        {
            Numeros = new List<int>();
            for (int i = 0; i < 100; i++)
            {
                Numeros.Add(i);
            }
        }

        public (bool, string) Extraer(int numeroExtraer)
        {
            if (Numeros.Count < 100)
            {
                return (false, "Solo esta permitido extraer un numero de la lista");
            }
            if (Numeros.Remove(numeroExtraer))
            {
                return (true, $"Se elimino el numero: {numeroExtraer} de la lista");
            }
            else
            {
                return (false, $"No se encontro el numero: {numeroExtraer} em la lista");
            }
        }

        public (bool, string) GetNumeroFaltante()
        {
            int numero = 0;
            while (numero < Numeros.Count)
            {
                if (!Numeros.Contains(numero))
                {
                    return (true, $"El número faltante es: {numero}");
                }
                numero++;
            }
            return (false, "No hay números faltantes en la lista.");
        }

    }
}
