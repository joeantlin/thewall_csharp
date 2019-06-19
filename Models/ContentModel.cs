using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheWall.Models
{
    public class FormContent
    {
        [Required]
        public string Content {get;set;}
    }
}