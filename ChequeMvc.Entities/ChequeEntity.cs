using ChequeMvc.Services.Shared.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace ChequeMvc.Entities
{
    /// <summary>
    /// One of the design tenets of MVC is DRY ("Don't Repeat Yourself").
    /// ASP.NET MVC encourages you to specify functionality or behavior only once, and then have it be reflected everywhere in an app.
    /// This reduces the amount of code you need to write and makes the code you do write less error prone, easier to test,
    /// and easier to maintain.
    /// The validation support provided by MVC and Entity Framework Core Code First is a good example of the DRY principle in action.
    /// You can declaratively specify validation rules in one place (in the model class) and the rules are enforced everywhere in the app.
    /// Source: https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/validation?view=aspnetcore-2.1
    /// </summary>
    public class ChequeEntity
    {
        [Display(Name = "Payee")]
        [Required(ErrorMessage = "Payee is a required field.")]
        public string chequePayee { get; set; }

        [Display(Name = "Amount")]
        [DataType(DataType.Currency)]
        [Range(0.01, 10000000000000, ErrorMessage = "Amount must be between {1} and {2}.")]
        [Required(ErrorMessage = "Amount is a required field.")]
        public decimal? chequeAmount { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [MustBeTodayOrAfter(ErrorMessage = "Date must be from today.")]
        [Required(ErrorMessage = "Date is a required field.")]
        public DateTime? chequeDate { get; set; }
    }
}
