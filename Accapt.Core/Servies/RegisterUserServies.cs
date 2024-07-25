using Accapt.Core.DTOs;
using Accapt.Core.Generator;
using Accapt.Core.Servies.InterFace;
using Accapt.DataLayer.Context;
using Accapt.DataLayer.Entities;
using AccaptFullyVersion.Core.Generator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies
{
    public class RegisterUserServies : IRegisterUserServies
    {
        private readonly AccaptFContext _context;
        public RegisterUserServies(AccaptFContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        public async Task<ReturniStatuceDTO> RegisterUser(RegisterUserDTO newuser)
        {
            if (newuser == null)
                return new ReturniStatuceDTO()
                {
                    ISuucess = false,
                    Message = "Null DTO",
                    Data = null
                };

            var guiId = NameGenerator.GenerateUniqCode();

            bool existGUID = await _context.Users.AnyAsync(u => u.Id == guiId);
            while(existGUID)
            {
                guiId = NameGenerator.GenerateUniqCode();
            }

            var currentDat = DateTime.UtcNow;
            var user = new Users()
            {
                Id = guiId,
                RealFullName = newuser.Name + " " + newuser.Family,
                UserName = newuser.UserName,
                PhoneNumber = newuser.PhoneNumber,
                IsActive = false,
                RegisterDate = currentDat,
                Role = 0,
                VerifyCode = CodeGeneratorForTwoFactory.GenerateSecureRandomNumber().ToString(),
                ExpireAccessDate = currentDat.AddYears(1)
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return new ReturniStatuceDTO()
            {
                ISuucess = true,
                Message = "SuccessFuuly For Register",
                Data = user
            };
        }
    }
}
