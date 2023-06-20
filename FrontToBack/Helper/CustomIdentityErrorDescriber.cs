using Microsoft.AspNetCore.Identity;

namespace FrontToBack.Helper
{
    public class CustomIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError PasswordRequiresNonAlphanumeric()
        {  
                          return new IdentityError 
                          { Code = nameof(PasswordRequiresNonAlphanumeric),
                             Description = "simvol olmalidir....."
                           }; 
         }
    }
}
