using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedeSocialGuiaTrilha.Web.Models
{
    public class PostModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
        public IFormFile PictureFile { get; set; }
        public string Picture { get; set; }
        public DateTime PublishDateTime { get; set; }
    }
}
