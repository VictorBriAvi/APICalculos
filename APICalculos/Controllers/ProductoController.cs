using APICalculos.DTOs;
using APICalculos.Entidades;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace APICalculos.Controllers
{
    [ApiController]
    [Route("api/productos")]

    
    public class ProductoController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public ProductoController(MyDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            
        }
        //Aca estamos solicitando toda la List o IEnumerable de la base de datos
        //[HttpGet]
        //public async Task<List<Producto>>Get()
        //{
        //    return await _context.Productos.ToListAsync();
        //}
        
        [HttpGet]

        public async Task<IEnumerable<ProductoDTO>> BuscandoConMapperYDTO()
        {
            return await _context.Productos
             .ProjectTo<ProductoDTO>(_mapper.ConfigurationProvider).ToListAsync();


        }

        [HttpGet("buscarConPaginacion/{pagina:int}/{tamanoPagina:int}")]

        public async Task<IEnumerable<ProductoDTO>> BuscarPaginacion(int pagina, int tamanoPagina)
        {
            var productosQuery = _context.Productos
                .ProjectTo<ProductoDTO>(_mapper.ConfigurationProvider);

            var productosPaginados = await productosQuery
                .Skip((pagina - 1) * tamanoPagina)
                .Take(tamanoPagina)
                .ToListAsync();

            return productosPaginados;


        }

        [HttpGet("buscarProductoPorId/{id:int}")]
        public async Task<ActionResult<Producto>> BuscarProductoPorId(int id)
        {
            var productoId = await _context.Productos.FirstOrDefaultAsync(g => g.ProductoId == id);

            if (productoId is null)
            {
                var mensajeError = $"No se encontró ningún producto con el Id '{id}'.";
                return StatusCode((int)HttpStatusCode.NotFound, mensajeError);

            }

            return productoId;
        }

        //Aca estamos buscando el producto por nombre del producto
        [HttpGet("buscarpProductoPorCodigo/{nombre}")]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> BuscarProductoPorCodigo(string nombre)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                var todosLosProductos = await BuscandoConMapperYDTO();
                return Ok(todosLosProductos);
            } 
            else
            {
                var productoPorNombre = await _context.Productos.
                    Where(g => g.NombreProducto.ToLower() == nombre.ToLower())
                    .ProjectTo<ProductoDTO>(_mapper.ConfigurationProvider).ToListAsync();

                if (productoPorNombre.Count == 0)
                {
                    var mensajeError = $"No se encontró ningún producto con el código '{nombre}'.";
                    return StatusCode((int)HttpStatusCode.NotFound, mensajeError);
                }
                return Ok(productoPorNombre);

            }


            /*
            var producto = await _context.Productos.FirstOrDefaultAsync(g => g.NombreProducto.ToLower() == nombre.ToLower());
            Console.WriteLine(producto);
            if (producto is null)
            {
                var mensajeError = $"No se encontró ningún producto con el código '{nombre}'.";
                return StatusCode((int)HttpStatusCode.NotFound, mensajeError);

            }

            return producto;
            */
        }

        //Aca estamos agrengado nuevos productos
        [HttpPost]
        public async Task<ActionResult> PostConMapperDTO (ProductoCreacionDTO productoCreacionDTO)
        {
            //existeCodigoProducto y existeNombreProducto
            //Estan buscando el nombre y codigo para que no sea iguales a los ya registrados, se esta filtrando para que no se agregen con espacios
            var existeCodigoProducto = await _context.Productos.AnyAsync(g => g.CodigoProducto.Replace(" ","").Trim() == productoCreacionDTO.CodigoProducto.Replace(" ", "").Trim());
            var existeNombreProducto = await _context.Productos.AnyAsync(g => g.NombreProducto.Replace(" ", "").Trim() == productoCreacionDTO.NombreProducto.Replace(" ", "").Trim());

            if (string.IsNullOrWhiteSpace(productoCreacionDTO.CodigoProducto) || string.IsNullOrWhiteSpace(productoCreacionDTO.NombreProducto))
            {
                return BadRequest("no puede estar vacío");
            }

            if (existeCodigoProducto)
            {
                var texto = $"Este codigo ya esta existente";
                return BadRequest(texto);

            };

            if (existeNombreProducto)
            {
                var texto = $"El nombre del producto ya esta existente";
                return BadRequest(texto);

            };
            var producto = _mapper.Map<Producto>(productoCreacionDTO);
                _context.Add(producto);
                await _context.SaveChangesAsync();
                var mensaje = "Se agrego correctamente :D";
                return Ok(mensaje);

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(ProductoCreacionDTO productoCreacionDTO,int id)
        {

            //existeCodigoProducto y existeNombreProducto
            //Estan buscando el nombre y codigo para que no sea iguales a los ya registrados, se esta filtrando para que no se agregen con espacios


            var existeCodigoProducto = await _context.Productos.AnyAsync(g => g.CodigoProducto.Replace(" ", "").Trim() == productoCreacionDTO.CodigoProducto.Replace(" ", "").Trim());
            var existeNombreProducto = await _context.Productos.AnyAsync(g => g.NombreProducto.Replace(" ", "").Trim() == productoCreacionDTO.NombreProducto.Replace(" ", "").Trim());
            
            //productoDB busca el primer valor Id con el ingresado
            var productoDB = await _context.Productos.AsTracking().FirstOrDefaultAsync(a => a.ProductoId == id);


            if (!string.IsNullOrWhiteSpace(productoCreacionDTO.CodigoProducto))
            {
                productoDB.CodigoProducto = productoCreacionDTO.CodigoProducto;
            }
            if (!string.IsNullOrWhiteSpace(productoCreacionDTO.NombreProducto))
            {
                productoDB.NombreProducto = productoCreacionDTO.NombreProducto;
            }
            if (!string.IsNullOrWhiteSpace(productoCreacionDTO.DescripcionProducto))
            {
                productoDB.DescripcionProducto = productoCreacionDTO.DescripcionProducto;
            }


            if (productoCreacionDTO.PrecioProducto > 0)
            {
                productoDB.PrecioProducto = productoCreacionDTO.PrecioProducto;
            }

            if (productoCreacionDTO.Stock > 0)
            {
                productoDB.Stock = productoCreacionDTO.Stock;
            }

            if (existeCodigoProducto )
            {
                var texto = $"Este codigo ya esta existente";
                return BadRequest(texto);
            }

            if (existeNombreProducto)
            {
                var texto = $"El nombre del producto ya esta existente";
                return BadRequest(texto);

            };

            if (productoDB is null)
            {
                return NotFound();

            }

            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete("{id:int}")]

        public async Task<ActionResult>Delete(int id)
        {
            //productoDB busca el primer valor Id con el ingresado

            var productoId = await _context.Productos.FirstOrDefaultAsync(g => g.ProductoId == id);

            if (productoId is null)
            {
                var textoNoEncontrado = "Este producto no existe";
                return StatusCode((int)HttpStatusCode.NotFound, textoNoEncontrado);

            }

            _context.Remove(productoId);
            await _context.SaveChangesAsync();
            var mensaje = $"Se ha Eliminado el Producto";
            return Ok(mensaje);
        }
    }
}
