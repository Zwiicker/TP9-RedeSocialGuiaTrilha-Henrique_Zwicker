using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RedeSocialGuiaTrilha.Core.Models
{
    public class UserProfileModel
    {
        public Guid Id { get; set; }
        public string Avatar { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public string AboutMe { get; set; }
    }
}
