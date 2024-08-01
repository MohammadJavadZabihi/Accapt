using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.DataLayer.Entities
{
    public class ProductCatrgory
    {
        public ProductCatrgory()
        {
            
        }

        [Key]
        public int CatrgoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public string CatrgoryName { get; set; }

    }
}
