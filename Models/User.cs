using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Models
{
    public class User : Entity
    {
        //[Required]
        public string Username {get; set;}
        //[EmailAddress]
        //[Required]
        public string Email {get; set;}
        //[JsonIgnore]
        public string Password {get; set;} = "abc";
        public Gender Gender {get; set;}

        public bool ShouldSerializePassword() {
            return Password != null;
        }
    }
}
