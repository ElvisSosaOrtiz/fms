using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using Shared.Routing;

namespace MinisterioEducacionDF.Controllers
{
    [Route(ResumenFinancieroRoutes.Root)]
    [ApiController]
    public class ResumenFinancieroController : ControllerBase
    {
        private readonly IResumenFinancieroService _resumenFinancieroService;

        public ResumenFinancieroController(IResumenFinancieroService resumenFinancieroService)
        {
            _resumenFinancieroService = resumenFinancieroService;
        }

        [HttpGet]
        public async Task<IActionResult> GetResumenFinanciero()
        {
            var result = await _resumenFinancieroService.GetResumenFinancieroAsync();

            if (result is null) return StatusCode(500);

            return Ok(result);
        }
    }
}
