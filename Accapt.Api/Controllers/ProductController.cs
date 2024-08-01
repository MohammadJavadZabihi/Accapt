using Accapt.Core.DTOs;
using Accapt.Core.Servies.InterFace;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Accapt.Api.Controllers
{
    [Route("api/MangeProduct(V1)")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        #region Injection

        private IProductServies _productServies;
        private IFindeProductServies _findeProductServies;
        private readonly IMapper _mapper;
        public ProductController(IProductServies productServies,
            IFindeProductServies findeProductServies,
            IMapper mapper)
        {
            _productServies = productServies ?? throw new ArgumentException(nameof(productServies));
            _findeProductServies = findeProductServies ?? throw new ArgumentException(nameof(findeProductServies));
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
        }

        #endregion

        #region AddProduct

        [HttpPost("ANP(V1)")]
        [Authorize]
        public async Task<IActionResult> AddProduct(AddProductDTO addProductDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _productServies.AddProduct(addProductDTO);

            if (product == null)
                return BadRequest("Null Exeption");

            return Ok(addProductDTO);
        }

        #endregion

        #region RemoveProduc

        [HttpDelete("DLP(V1)/")]
        [Authorize]
        public async Task<IActionResult> DeletProductByName(SingleProductNameDTO productName)
        {
            if(!ModelState.IsValid || productName.ProductName == null)
                return BadRequest(ModelState);

            var product = await _findeProductServies.FindeProduct(productName.ProductName);

            if (product == null)
                return BadRequest("null product");

            var deletProduct = await _productServies.DeletProduct(product);

            if (!deletProduct)
                return BadRequest();

            return Ok(deletProduct);
        }

        #endregion

        #region UpdateProduct

        [HttpPatch("UPP(V1)/{productName}")]
        public async Task<IActionResult> UpdateProduct(string productName, [FromBody] JsonPatchDocument<ProductUpdateDTO> patchDocument)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _findeProductServies.FindeProduct(productName);

            if (product == null)
                return NotFound();

            var productToPatch = _mapper.Map<ProductUpdateDTO>(product);

            patchDocument.ApplyTo(productToPatch, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _mapper.Map(productToPatch, product);
            await _productServies.Save();

            return Ok(product);
        }

        #endregion
    }
}
