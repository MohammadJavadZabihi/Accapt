using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.DataLayer.Entities
{
    public class Users
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(200)]
        public string RealFullName { get; set; }

        [Required]
        public DateTime RegisterDate { get; set; }

        [Required]
        [MaxLength(500)]
        public string Email { get; set; }

        [Required]
        [MaxLength(200)]
        public string VerifyCode { get; set; }

        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public int Role { get; set; }

        [Required]
        public DateTime ExpireAccessDate { get; set; }
    }
}
