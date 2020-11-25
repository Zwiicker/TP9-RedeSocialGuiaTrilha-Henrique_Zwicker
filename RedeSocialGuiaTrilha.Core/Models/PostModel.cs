using System;
using System.Collections.Generic;
using System.Text;

namespace RedeSocialGuiaTrilha.Core.Models
{
    public class PostModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
        public string Picture { get; set; }
        public DateTime PublishDateTime { get; set; }
    }
}
