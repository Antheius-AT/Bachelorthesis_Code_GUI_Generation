using Models.Metadata;
using Models.Metadata.Wellknown;
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
        [Editable]
        [StringLength(10)]
        public string? UserName { get; set; }

        [Editable]
        [StringLength(20)]
        public string? Password { get; set; }

        [Editable]
        [StringLength(20)]
        public string? PasswordConfirmation { get; set; }
    }
}
