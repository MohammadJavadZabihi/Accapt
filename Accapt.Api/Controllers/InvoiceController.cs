using Accapt.Core.DTOs;
using Accapt.Core.Servies.InterFace;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Accapt.Api.Controllers
{
    [Route("api/InvoiceManger(V1)")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IAddInvoiceServies _addInvoiceServies;
        public InvoiceController(IAddInvoiceServies addInvoiceServies)
        {
            _addInvoiceServies = addInvoiceServies ?? throw new ArgumentException(nameof(addInvoiceServies));
        }

        [HttpPost("ADINV(V1)")]
        public async Task<IActionResult> AddInvoices(AddInvoicesDTO addInvoicesDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var addInvoice = await _addInvoiceServies.AddInvoice(addInvoicesDTO);
            if(addInvoice == null)
                return BadRequest();

            return Ok(new
            {
                Statuce = true,
                Invoice = addInvoice
            });
        }
    }
}
