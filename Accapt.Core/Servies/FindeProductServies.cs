using Accapt.Core.Servies.InterFace;
using Accapt.DataLayer.Context;
using Accapt.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies
{
    public class FindeProductServies : IFindeProductServies
    {
        private readonly AccaptFContext _context;
        public FindeProductServies(AccaptFContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        public async Task<Product?> FindeProduct(string productName)
        {
            return await _context.products.FirstOrDefaultAsync(p => p.ProductName == productName);
        }
    }
}
