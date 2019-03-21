using System.ComponentModel.DataAnnotations;

namespace CreditCards.ViewModels
{
    public class NewCreditCardApplicationDetails
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please provide a first name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please provide a last name")]
        public string LastName{ get; set; }

        [Display(Name = "Frequent Flyer Number")]
        [Required(ErrorMessage = "Please provide a frequent flyer number")]
        public string FrequentFlyerNumber { get; set; }

        [Display(Name = "Age (in years)")]
        [Required(ErrorMessage = "Please provide an age in years")]
        [Range(18,int.MaxValue, ErrorMessage = "You must be at least 18 years old")]
        public int? Age { get; set; }

        [Display(Name = "Gross Income")]
        [Required(ErrorMessage = "Please provide your gross income")]        
        public decimal? GrossAnnualIncome { get; set; }
    }
}
