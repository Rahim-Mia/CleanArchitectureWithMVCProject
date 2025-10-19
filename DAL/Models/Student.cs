using AspDotNetCoreMVCProject.Models.Custom_Validation_Attribute;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AspDotNetCoreMVCProject.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Student Name is Required")]
        [DisplayName("Student Name")]
        [StringLength(50, ErrorMessage = "Student Name can't exceed 50 character.")]
        public string Name { get; set; }
        [Required]
        [ValidMobileNumber]// custom mobile validation attribute 
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Please provide Valid Email address.")]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [ValidAgeRange(18, 30)]

        public int Age { get; set; }
        [Required]
        public string Gender { get; set; }
    }
}
