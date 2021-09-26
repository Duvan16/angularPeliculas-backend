using back_end.Entidades;
using back_end.Repositorios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.Controllers
{
    [Route("api/generos")] //Endpoint
    //[Route("api/[controller]")]
    public class GenerosController : ControllerBase
    {
        private readonly IRepositorio repositorio;

        public GenerosController(IRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        [HttpGet]
        public List<Genero> Get()
        {
            return repositorio.ObtenerTodosLosGeneros();
        }

        //[HttpGet("ejemplo")]
        //[HttpGet("{Id}")]
        [HttpGet("{Id:int}/{nombre=Roberto}")] //api/generos/3/Felipe
        public ActionResult<Genero> Get(int Id, string nombre)
        {
            var genero = repositorio.ObtenerPorId(Id);
            if (genero == null)
            {
                return NotFound();
            }

            return genero;
            //return Ok(genero); //IActionResult
            //return Ok("Felipe");
        }

    }
}
