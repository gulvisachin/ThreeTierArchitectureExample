using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking.Entities
{
    //Identity - Membership program : Authentication (Login to system) & Authorization (Access Rights)
    //Authorization :
    //Register: Identity user class - Id(GUID),UserName,Password,Email,Phone
                //SignInManager - Check user signIn
                //UserManager - Store user data in DB,get user info from DB,add role to user
                //claims - Piece of information about user
                //ClaimsIdentity - list of Claims
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? Address { get; set; }
        public string? Pincode { get; set; }
        public string? Phone { get; set; }
    }
}
