using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityLife.Models
{
    public class Student
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [Required]
        [MaxLength(8), MinLength(8)]
        [Display(Name = "Student Number")]
        [JsonProperty(PropertyName = "studentno")]
        public int StudentNo { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [JsonProperty(PropertyName = "firstname")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [JsonProperty(PropertyName = "lastname")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Home Address")]
        [JsonProperty(PropertyName = "homeaddress")]
        public string HomeAddress { get; set; }

        [Required(ErrorMessage = "You must provide a phone number")]
        [Display(Name = "Cell Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        [JsonProperty(PropertyName = "mobileno")]
        public string MobileNo { get; set; }


        [Required]
        [Display(Name = "Is Active")]
        [JsonProperty(PropertyName = "isactive")]
        public bool IsActive { get; set; }

        
        [Display(Name="Image")]
        [JsonProperty(PropertyName = "imageurl")]
        public string ImageUrl { get; set; }
    }
}
