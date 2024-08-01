using Accapt.Core.DTOs;
using Accapt.Core.Servies.InterFace;
using Accapt.DataLayer.Context;
using Accapt.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies
{
    public class ProductServies : IProductServies
    {
        private readonly AccaptFContext _context;
        private readonly IFindUserServies _findUserServies;
        private readonly IFindeProductServies _findeProductServies;
        public ProductServies(AccaptFContext context,
            IFindUserServies findUserServies,
            IFindeProductServies findeProductServies)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
            _findUserServies = findUserServies ?? throw new ArgumentException(nameof(findUserServies));
            _findeProductServies = findeProductServies;

        }
        public async Task<object?> AddProduct(AddProductDTO addProduct)
        {
            if (addProduct == null)
                return "Null Exeption";

            try
            {
                var user = await _findUserServies.FindUserByUserName(addProduct.UserName);

                if (user == null)
                    return "Cannot find the Main User";

                Product product = new Product
                {
                    CatrgoryId = addProduct.CatrgoryId,
                    ProductName = addProduct.Productname,
                    Description = addProduct.Description,
                    Price = addProduct.Price,
                    ProductCount = addProduct.ProductCount,
                    UserId = user.Id,
                    Users = user
                };

                await _context.AddAsync(product);
                await _context.SaveChangesAsync();

                return product;
            }
            catch (Exception ex)
            {
                return "Error Message is : " + ex.Message;
            }
        }

        public async Task<bool> DeletProduct(Product product)
        {
            if (product == null)
                return false;

            try
            {
                _context.products.Remove(product);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
