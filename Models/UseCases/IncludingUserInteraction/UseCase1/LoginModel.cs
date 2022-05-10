using Models.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.UseCases.IncludingUserInteraction.UseCase1
{
    [AuthenticationForm]
    public class LoginModel
    {
        public string? UserName { get; set; }

        public string? Password { get; set; }

        public string? PasswordConfirmation { get; set; }
    }
}
