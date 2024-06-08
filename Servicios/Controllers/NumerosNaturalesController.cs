using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Servicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumerosNaturalesController : ControllerBase
    {

        public readonly Modelo.NumerosNaturales _numerosNaturales = Modelo.NumerosNaturales.shared;

        [HttpPost]
        [Route("/Extraer/{numero}")]
        public IActionResult ExtrerNumero(int numero)
        {
            if (numero < 0 || numero > 100)
            {
                return BadRequest("El numero debe ser igual o mayor a 0 y menor o igual a 100");
            }
            var result = _numerosNaturales.Extraer(numero);
            if (result.Item1)
            {
                return Ok(new { Correcto = result.Item1, Mensaje = result.Item2 });
            }
            else
            {
                return BadRequest(new { Correcto = result.Item1, Mensaje = result.Item2 });
            }
        }
        [HttpGet]
        [Route("/GetNumeroFaltante")]
        public IActionResult GetNumeroFaltante()
        {
            var result = _numerosNaturales.GetNumeroFaltante();
            if (result.Item1)
            {
                return Ok(new { Correcto = result.Item1, Mensaje = result.Item2 });
            }
            else
            {
                return BadRequest(new { Correcto = result.Item1, Mensaje = result.Item2 });
            }
        }
    }
}