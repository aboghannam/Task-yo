using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;


namespace Application.ClientData.Commands
{
    public class CreateUserDataCommand: IRequest<Result>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalId { get; set; }

        public class CreateUserDataValidator : AbstractValidator<CreateUserDataCommand>
        {
            public CreateUserDataValidator()
            {
                RuleFor(r => r.FirstName).NotEmpty().NotNull()
                    .WithMessage("FirstName is Required");
                RuleFor(r => r.LastName).NotEmpty().NotNull()
                    .WithMessage("LastName is Required");
                RuleFor(r => r.PhoneNumber).NotEmpty().NotNull()
                    .WithMessage("PhoneNumber is Required")
                    .Length(11)
                    .WithMessage("PhoneNumber musat be 11 number");

            }
        }

        public class Handler : IRequestHandler<CreateUserDataCommand, Result>
        {

            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(CreateUserDataCommand request, CancellationToken cancellationToken)
            {
                var userData = request.Adapt<UserData>();
                userData.CreatedTime = DateTime.UtcNow;
                await _context.userDatas.AddAsync(userData);
                await _context.SaveChangesAsync(cancellationToken);

                return new Result(true, userData, "done");
            }
        }


    }
}
