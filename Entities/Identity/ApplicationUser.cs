using System;
using Microsoft.AspNetCore.Identity;

namespace BlogApi.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string ImagePerfil { get; set; }
    }
}