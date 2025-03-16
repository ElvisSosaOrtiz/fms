using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using Shared.Response.MovimientoController;
using Shared.Routing;

namespace MinisterioEducacionDF.Controllers
{
    [Route(MovimientoControllerRoutes.Root)]
    [ApiController]
    public class MovimientoController : ControllerBase
    {
        private readonly IMovimientoService _movimientoService;

        public MovimientoController(IMovimientoService movimientoService)
        {
            _movimientoService = movimientoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMovimientos(DateOnly? fechaDesde)
        {
            var result = await _movimientoService.GetMovimientosAsync(fechaDesde);

            if (result is null) return StatusCode(500);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovimiento(int id)
        {
            var result = await _movimientoService.GetMovimientoAsync(id);

            if (result is null) return NotFound(new { message = "Could not find transaction movement" });

            return Ok(result);
        }
    }
}
