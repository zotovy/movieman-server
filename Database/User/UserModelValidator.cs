using FluentValidation;

namespace Database.User {
    public class UserModelValidator : AbstractValidator<UserModel> {
        public void Id() => RuleFor(user => user.Id).NotEmpty();
        public void Name() => RuleFor(user => user.Name).Matches(Domain.ValueObjects.User.Name.Validator);
        public void Email() => RuleFor(user => user.Email).Matches(Domain.ValueObjects.User.Email.Validator);
        public void Password() => RuleFor(user => user.Password).Matches(Domain.ValueObjects.User.Password.Validator);
    }
}