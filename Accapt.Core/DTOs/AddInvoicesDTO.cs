using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.DTOs
{
    public class AddInvoicesDTO
    {
        public string InvoiceName { get; set; } = string.Empty;

        public string TypeOfInvoice { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public int ProductPrice { get; set; }

        [Required]
        public int ProductCount { get; set; }

        [Required]
        public int Discount { get; set; }

        [Required]
        public decimal ProductTotalPrice { get; set; }

        public decimal TotalPrice { get; set; }

        [Required]
        public decimal AmountPaid { get; set; }

        [Required]
        public int TotalDiscount { get; set; }

        [Required]
        public DateTime DateOfSubmitInvoice { get; set; }

        [Required]
        public int InvoiceId { get; set; }

        [MaxLength(800)]
        public string Description { get; set; }
    }
}
