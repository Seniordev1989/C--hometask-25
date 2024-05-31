using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;

namespace WebApplication1.Models
{
    public class UserDetails
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(250)]
        public string FirstName { get; set; }
        [MaxLength(250)]
        public string Surname{ get; set; }
        public int Age { get; set; }
        [MaxLength(2)]
        public string Sex { get; set; }
        [MaxLength(20)]
        public string Mobile { get; set; }
        public bool Active{ get; set; }

    }
}