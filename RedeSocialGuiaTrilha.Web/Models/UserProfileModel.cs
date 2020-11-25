using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RedeSocialGuiaTrilha.Web.Models
{
    public class UserProfileModel
    {
        public Guid Id { get; set; }
        public IFormFile AvatarFile { get; set; }
        public string Avatar { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public string AboutMe { get; set; }
    }
}
