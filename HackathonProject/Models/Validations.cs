using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HackathonProject.Models
{
    public class UniqueAttribute : ValidationAttribute
    {
        private static HackathonEntities1 entities = new HackathonEntities1();

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string values = value.ToString();

                int Aadharcount = entities.AllUsers.Where(x => x.AadhaarNumber == values).ToList().Count();

                int Mobilecount = entities.AllUsers.Where(x => x.MobileNumber == values).ToList().Count();

                if (Aadharcount != 0)
                    return new ValidationResult("Aadhar Already Exist");

                if (Mobilecount != 0)
                    return new ValidationResult("Mobile Number Already Exist");
            }
            return ValidationResult.Success;
        }
    }

    public class AgeValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var user = (AllUser)validationContext.ObjectInstance;

            var age = DateTime.Today.Year - user.DateOfBirth.Year;

            if (!(age >= 18))
                return new ValidationResult("Age should be greater than 18 years ");

            return ValidationResult.Success;
        }
    }


    public class Validations
    {
        [Required]
        [Display(Name = "Name")]
        public string UserName { get; set; }

        [Required]
        [AgeValidation]
        [Display(Name = "Date of Birth")]
        public System.DateTime DateOfBirth { get; set; }

        [Required]
        [Unique]
        [Display(Name = "Mobile Number")]
        [RegularExpression(@"[9876]\d{9}", ErrorMessage = "Please Enter Valid Mobile Number ")]
        public string MobileNumber { get; set; }

        [Required]
        [Unique]
        [Display(Name = "Aadhar Number")]
        [RegularExpression(@"\$?\d{12}", ErrorMessage = "Please Enter Valid Aadhar Number ")]
        public string AadhaarNumber { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string UserAddress { get; set; }
    }

    [MetadataTypeAttribute(typeof(Validations))]
    public partial class AllUser
    {

    }

}