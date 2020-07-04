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
        [JsonProperty(PropertyName = "studentno")]
        public int StudentNo { get; set; }

        [Required]
        [JsonProperty(PropertyName = "firstname")]
        public string FirstName { get; set; }

        [Required]
        [JsonProperty(PropertyName = "lastname")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [Required]
        [JsonProperty(PropertyName = "homeaddress")]
        public string HomeAddress { get; set; }

        [Required]
        [JsonProperty(PropertyName = "mobileno")]
        public string MobileNo { get; set; }


        [Required]
        [JsonProperty(PropertyName = "isactive")]
        public bool IsActive { get; set; }

        [Required]
        [JsonProperty(PropertyName = "imageurl")]
        public string ImageUrl { get; set; }
    }
}
