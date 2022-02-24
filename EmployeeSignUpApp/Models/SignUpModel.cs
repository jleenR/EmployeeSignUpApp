using EmployeeSignUpApp.Models.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeSignUpApp.Models
{
    public class SignUpModel

    {
        [Required(ErrorMessage = "Please provide first name.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please provide last name.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please provide email address.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "You must have a password.")]
        public string Password { get; set; }

        public List<SelectListItem>? Genderlist { get; set; }

        public int GenderId { get; set; }

        public List<SelectListItem>? MaritalStatuslist { get; set; }

        public int MaritalStatusId { get; set; }
    }
}