using Models.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.UseCases.IncludingUserInteraction.UseCase1
{
    [InputForm]
    public class LoginModel
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? Password { get; set; }

        public string? PasswordConfirmation { get; set; }
    }
}
