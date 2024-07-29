using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.DTOs
{
    public class UserUpdateAccountViewModel
    {
        [Display(Name = "UserName")]
        [MaxLength(150, ErrorMessage = "Invalid UserName Input")]
        public string UserName { get; set; } = string.Empty;

        [Display(Name = "Email")]
        [MaxLength(150, ErrorMessage = "Invalid Email Input")]
        //[EmailAddress(ErrorMessage = "Email Most be like (example@gmail.com)")]
        public string Email { get; set; } = string.Empty;
    }
}
