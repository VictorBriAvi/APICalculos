using APICalculos.DTOs;
using APICalculos.Entidades;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace APICalculos.Controllers
{
    [Route("api/categoriaServicios")]
    [ApiController]
    public class CategoriasServiciosController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public CategoriasServiciosController(MyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoriasServiciosDTO>> GetAllCategorias()
        {
            return await _context.CategoriasServicios.ProjectTo<CategoriasServiciosDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        [HttpGet("buscarPorCategoriaId/{id:int}")]
        public async Task<ActionResult<CategoriasServiciosDTO>> BuscarPorCategoriaId(int id)
        {
            var categoriaId = await _context.CategoriasServicios.FirstOrDefaultAsync(g => g.CategoriasServiciosId == id);

            if (categoriaId is null)
            {
                var mensajeError = $"No se encontró ningúna categoria con el Id '{id}'.";
                return StatusCode((int)HttpStatusCode.NotFound, mensajeError);
            }

            var categoriaDTO = _mapper.Map<CategoriasServiciosDTO>(categoriaId); // Mapear la entidad a un DTO

            return Ok(categoriaDTO);
        
        }
        [HttpPost]
        public async Task<ActionResult> AgregarCategoria (CategoriasServiciosCreacionDTO categoriasServiciosCreacionDTO)
        {
            var existeNombreCategoria = await _context.CategoriasServicios.AnyAsync(g => g.NombreCategoriaServicio.Replace("", "").Trim() == categoriasServiciosCreacionDTO.NombreCategoriaServicio.Replace(" ", "").Trim());

            if (string.IsNullOrWhiteSpace(categoriasServiciosCreacionDTO.NombreCategoriaServicio))
            {
                return BadRequest("EL nombre de categoria no puede estar vacio");
                
            }

            if (existeNombreCategoria)
            {
                var texto = $"Este nombre de categoria ya existe";
                return BadRequest(texto);
            }

            var categoriaServicio = _mapper.Map<CategoriasServicios>(categoriasServiciosCreacionDTO);
            _context.Add(categoriaServicio);
            await _context.SaveChangesAsync();
            var mensaje = "Se agrego correctamente la nueva categoria de servicio";
            return Ok(mensaje);

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> ModificarCategoria(CategoriasServiciosCreacionDTO categoriasServiciosCreacionDTO, int id) 
        {

            var existeNombreCategoria = await _context.CategoriasServicios.AnyAsync(g => g.NombreCategoriaServicio.Replace(" ", "").Trim() == categoriasServiciosCreacionDTO.NombreCategoriaServicio.Replace(" ", "").Trim());

            var categoriaServicioDB = await _context.CategoriasServicios.AsTracking().FirstOrDefaultAsync(a => a.CategoriasServiciosId == id);

            if (existeNombreCategoria)
            {
                var texto = $"Este tipo de nombre de categoria ya existe :/";
                return BadRequest(texto);
            }

            if (categoriaServicioDB is null)
            {

                return NotFound();
            }

            if (!string.IsNullOrWhiteSpace(categoriasServiciosCreacionDTO.NombreCategoriaServicio))
            {
                categoriaServicioDB.NombreCategoriaServicio = categoriasServiciosCreacionDTO.NombreCategoriaServicio;
                
            }
            await _context.SaveChangesAsync();
            var mensaje = $"Se modifico correctamente el nombre de la categoria servicio";
            return Ok(mensaje);
        }

        [HttpDelete("{id:int}")]

        public async Task<ActionResult> DeleteCategoriaServicio(int id)
        {
            var categoriaServicioId = await _context.CategoriasServicios.FirstOrDefaultAsync(g => g.CategoriasServiciosId == id);

            if (categoriaServicioId is null)
            {
                var mensajeNoSeEncuentra = "Este tipo de categoria no se encuentra";
                return StatusCode((int)HttpStatusCode.NotFound, mensajeNoSeEncuentra);

            }

            _context.Remove(categoriaServicioId);
            await _context.SaveChangesAsync();
            var mensaje = $"se ha eliminado un tipo de gasto";
            return Ok(mensaje);
        }


    }
}
