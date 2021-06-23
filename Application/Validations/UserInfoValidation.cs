﻿using Application.Commands.HomeCommands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validations
{
    public class UserInfoValidation : AbstractValidator<AddUserRequestCommand>
    {
        public UserInfoValidation()
        {
            RuleFor(f => f.UserName).NotEmpty().NotEqual("ccc").WithMessage("用户名称错误");
            RuleFor(f => f).Must((n, o) =>
            {
                return n.Password == o.Password;
            });
        }
    }
}
