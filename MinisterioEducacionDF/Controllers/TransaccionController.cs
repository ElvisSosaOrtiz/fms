using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using Shared.Request.TransaccionController;
using Shared.Routing;

namespace MinisterioEducacionDF.Controllers
{
    [Route(TransaccionControllerRoutes.Root)]
    [ApiController]
    public class TransaccionController : ControllerBase
    {
        private readonly ITransaccionService _transaccionService;

        public TransaccionController(ITransaccionService transaccionService)
        {
            _transaccionService = transaccionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransacciones(DateOnly fechaDesde, DateOnly fechaHasta)
        {
            var result = await _transaccionService.GetTransaccionesAsync(fechaDesde, fechaHasta);

            if (result is null) return StatusCode(500);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaccion(RequestOfCreateTransaccion request)
        {
            var result = await _transaccionService.CreateTransaccionAsync(request);

            if (result is null) return BadRequest(new { message = "Could not create a new transaction" });

            return Ok(result);
        }
    }
}
