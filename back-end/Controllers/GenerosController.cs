using back_end.Entidades;
using back_end.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

        public GenerosController(IRepositorio repositorio, WeatherForecastController weatherForecastController)
        {
            this.repositorio = repositorio;
            this.weatherForecastController = weatherForecastController;
        }

        [HttpGet]
        public List<Genero> Get()
        {
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
            var genero = await repositorio.ObtenerPorId(Id);
            if (genero == null)
            {
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
