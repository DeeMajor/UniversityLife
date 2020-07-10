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

        [Required]
        [Display(Name = "Cell Number")]
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
