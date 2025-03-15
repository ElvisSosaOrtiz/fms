namespace MinisterioEducacionDF.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ServiceContracts;
    using Shared.Routing;

    [Route(CuentaControllerRoutes.Root)]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        private readonly ICuentaService _cuentaService;

        public CuentaController(ICuentaService cuentaService)
        {
            _cuentaService = cuentaService;
        }

        [HttpGet]
        public IActionResult GetCuentas()
        {
            var result = _cuentaService.GetCuentas();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCuentaById(int id)
        {
            var result = await _cuentaService.GetCuentaByIdAsync(id);

            if (result is null) return NotFound(new { message = "Could not find account" });

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> CloseCuentaById(int id)
        {
            await _cuentaService.CloseCuentaByIdAsync(id);
            return Ok(new { message = "Account closed successfully!" });
        }
    }
}
