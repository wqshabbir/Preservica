using Backend.Models;
using FluentValidation;

namespace Backend.Validation
{
    public class CustomerDetailsValidator : AbstractValidator<Customer>
    {
        public CustomerDetailsValidator()
        {

            RuleFor(c => c.Id)
                .NotNull()
                .WithMessage("Customer Id is null");

            RuleFor(c => c.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Name is not provided");

            RuleFor(c => c)
                .Must(MustHaveAtleastOneContactDetailsProvided)
                .WithMessage("None of the contact details provided");
        }

        private bool MustHaveAtleastOneContactDetailsProvided(Customer customer)
        {
            return new[] { customer.Email, customer.PostalAddress, customer.PhoneNumber }.Any(customerAnyContact => !string.IsNullOrEmpty(customerAnyContact));
        }
    }
}
