using back_end.Entidades;
using back_end.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.Controllers
{
    [Route("api/generos")] //Endpoint
    //[Route("api/[controller]")]
    [ApiController]
    public class GenerosController : ControllerBase
    {
        private readonly IRepositorio repositorio;
        private readonly WeatherForecastController weatherForecastController;
        private readonly ILogger<GenerosController> logger;

        public GenerosController(IRepositorio repositorio, WeatherForecastController weatherForecastController, ILogger<GenerosController> logger)
        {
            this.repositorio = repositorio;
            this.weatherForecastController = weatherForecastController;
            this.logger = logger;
        }

        [HttpGet]
        public List<Genero> Get()
        {
            logger.LogInformation("Vamos a mostrar los generos");
            return repositorio.ObtenerTodosLosGeneros();
        }


        [HttpGet("guid")]//api/generos/guid
        public ActionResult<Guid> GetGUID()
        {
            return Ok(new
            {
                GUID_GenerosController = repositorio.ObtenerGUID(),
                GUID_WeatherForecastController = weatherForecastController.ObtenerGUIDWeatherForescastController()
            });
        }

        //[HttpGet("ejemplo")]
        //[HttpGet("{Id}")]
        [HttpGet("{Id:int}")] //api/generos/3/Felipe
        public async Task<ActionResult<Genero>> Get(int Id, [FromHeader] string nombre)
        {
            logger.LogDebug($"Obteniendo un género por el id {Id}");
            var genero = await repositorio.ObtenerPorId(Id);
            if (genero == null)
            {
                logger.LogWarning($"No pudimos encontrar el género de id {Id}");
                return NotFound();
            }

            return genero;
            //return Ok(genero); //IActionResult
            //return Ok("Felipe");
        }

        [HttpPost]
        public ActionResult Post([FromBody] Genero genero)
        {
            repositorio.CrearGenero(genero);
            return NoContent();
        }

    }
}
