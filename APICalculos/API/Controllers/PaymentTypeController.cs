using APICalculos.Application.DTOs.PaymentType;
using APICalculos.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APICalculos.API.Controllers
{
    [ApiController]
    [Route("api/paymenttype")]
    [Authorize]
    public class PaymentTypeController : BaseController
    {
        private readonly IPaymentTypeService _service;

        public PaymentTypeController(IPaymentTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<PaymentTypeDTO>>> Get(string? search)
        {
            var storeId = GetStoreIdFromToken();
            var result = await _service.GetAllPaymentTypeAsync(storeId, search);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var storeId = GetStoreIdFromToken();
            var result = await _service.GetPaymentTypeForId(id, storeId);

            if (result == null)
                return NotFound("Tipo de pago no encontrado");

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PaymentTypeCreationDTO dto)
        {
            var storeId = GetStoreIdFromToken();
            try
            {
                var result = await _service.AddPaymenteTypeAsync(storeId, dto);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, PaymentTypeCreationDTO dto)
        {
            var storeId = GetStoreIdFromToken();
            try
            {
                await _service.UpdatePaymentTypeAsync(id, storeId, dto);
                return Ok("Se modificó exitosamente");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Tipo de pago no encontrado");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var storeId = GetStoreIdFromToken();
            try
            {
                await _service.DeletePaymentTypeAsync(id, storeId);
                return Ok("Se ha eliminado el tipo de pago");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Tipo de pago no encontrado");
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}
