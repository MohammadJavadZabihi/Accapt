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
    public class AddInvoiceServies : IAddInvoiceServies
    {
        private AccaptFContext _context;
        public AddInvoiceServies(AccaptFContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Invoice?> AddInvoice(AddInvoicesDTO details)
        {
            try
            {
                if(details != null)
                {
                    InvoiceDetails invDetails = new InvoiceDetails
                    {
                        Id = details.UserId,
                        Discount = details.Discount,
                        ProductCount = details.ProductCount,
                        ProductName = details.ProductName,
                        ProductPrice = details.ProductPrice,
                        ProductTotalPrice = details.ProductTotalPrice
                    };

                    await _context.InvoiceDetails.AddAsync(invDetails);
                    await _context.SaveChangesAsync();

                    Invoice invoice = new Invoice
                    {
                        AmountPaid = details.AmountPaid,
                        DateOfSubmitInvoice = details.DateOfSubmitInvoice,
                        Description = details.Description,
                        Id = details.UserId,
                        InvoiceId = details.InvoiceId,
                        InvoiceName = details.InvoiceName,
                        TotalDiscount = details.TotalDiscount,
                        TotalPrice = details.TotalPrice,
                        TypeOfInvoice = details.TypeOfInvoice,
                        
                    };
                }

                return null;

            }
            catch
            {
                return null;
            }
        }
    }
}
