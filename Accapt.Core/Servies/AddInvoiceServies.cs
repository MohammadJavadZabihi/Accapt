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
        private readonly AccaptFContext _context;
        public AddInvoiceServies(AccaptFContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }
        public async Task<AddInvoicesDTO?> AddInvoice(AddInvoicesDTO invoice)
        {
            try
            {
                if(invoice == null) 
                    throw new ArgumentNullException(nameof(invoice));

                Invoice addInvoice = new Invoice()
                {
                    AmountPaid = invoice.AmountPaid,
                    CreditorStatuce = invoice.CreditorStatuce,
                    DateOfSubmitInvoice = invoice.DateOfSubmitInvoice,
                    Description = invoice.Description,
                    Id = invoice.UserId,
                    InvoiceName = invoice.InvoiceName,
                    TotalPrice = invoice.TotalPrice,
                    TypeOfInvoice = invoice.TypeOfInvoice
                };

                await _context.Invoices.AddAsync(addInvoice);
                await _context.SaveChangesAsync();

                //var inv = await _context.Invoices.Fires

                InvoiceDetails addinvoiceDetails = new InvoiceDetails()
                {
                    Discount = invoice.Discount,
                    Id = invoice.UserId,
                    ProductCount = invoice.ProductCount,
                    ProductName = invoice.ProductName,
                    ProductPrice = invoice.ProductPrice,
                    ProductTotalPrice = invoice.ProductTotalPrice,
                    InvoiceId = addInvoice.InvoiceId,
                };

                await _context.InvoiceDetails.AddAsync(addinvoiceDetails);
                await _context.SaveChangesAsync();

                return invoice;
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(nameof(ex.Message));
            }

        }
    }
}
