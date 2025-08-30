using APICalculos.Application.DTOs;
using APICalculos.Application.Interfaces;
using APICalculos.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace APICalculos.API.Controllers
{
    [ApiController]
    [Route("api/paymenttype")]
    public class PaymentTypeController : ControllerBase
    {
        private readonly IPaymentTypeService _paymentTypeService;

        public PaymentTypeController(IPaymentTypeService paymentTypeService)
        {
            _paymentTypeService = paymentTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PaymentTypeDTO>>> GetPaymentTypeAsync()
        {
            var paymentTypeDto = await _paymentTypeService.GetAllPaymenteTypesAsync();
            return Ok(paymentTypeDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPaymentTypeForId(int id)
        {
            var paymentTypeDto = await _paymentTypeService.GetPaymentTypeForId(id);

            if (paymentTypeDto == null)
                return NotFound($"No se encontro el cliente con el ID {id}");

            return Ok(paymentTypeDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PaymentTypeCreationDTO paymentTypeCreationDTO)
        {
            try
            {
                var paymentTypeDto = await _paymentTypeService.AddPaymenteTypeAsync(paymentTypeCreationDTO);
                return Ok(paymentTypeDto);
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
        public async Task<IActionResult> Put(int id, PaymentTypeCreationDTO paymentTypeCreationDTO)
        {
            try
            {
                await _paymentTypeService.UpdatePaymentTypeAsync(id, paymentTypeCreationDTO);
                return Ok("Se modifico exitosamente");
            }
            catch (KeyNotFoundException)
            {

                return NotFound("Tipo de pago no encontrado");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _paymentTypeService.DeletePaymentTypeAsync(id);
                return Ok("Se ha eliminado el tipo de pago");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Tipo de pago no encontrado");
            }
        }

    }
}
