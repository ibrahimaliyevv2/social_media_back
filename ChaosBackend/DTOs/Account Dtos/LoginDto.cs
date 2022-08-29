using System;
using FluentValidation;

namespace ChaosBackend.DTOs.AccountDtos
{
	public class LoginDto
	{
		public string UserName { get; set; }
		public string Password { get; set; }

	}

	public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
			RuleFor(x => x.UserName).MinimumLength(4).NotEmpty().MaximumLength(25);
			RuleFor(x => x.Password).MinimumLength(6).NotEmpty().MaximumLength(20);

		}
    }
}

