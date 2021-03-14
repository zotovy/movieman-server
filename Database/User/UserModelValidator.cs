using FluentValidation;

namespace Database.User {
    public class UserModelValidator : AbstractValidator<UserModel> {
        public void Id() => RuleFor(user => user.Id).NotEmpty();
        public void Name() => RuleFor(user => user.Name).Matches(Domain.ValueObjects.Name.Validator);
        public void Email() => RuleFor(user => user.Email).Matches(Domain.ValueObjects.Email.Validator);
        public void Password() => RuleFor(user => user.Password).Matches(Domain.ValueObjects.Password.Validator);
    }
}