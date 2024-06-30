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
using Microsoft.EntityFrameworkCore;
using Application.ClientData.Commands;


namespace Application.ClientData.Queries
{
    public class GetUserQuery : IRequest<Result>
    {
        public int Id { get; set; }

        public class GetUserValidator : AbstractValidator<GetUserQuery>
        {
            public GetUserValidator()
            {
                RuleFor(r => r.Id).NotEmpty().NotNull().NotEqual(0)
                    .WithMessage("Id is Required");
            }
        }

        public class Handler : IRequestHandler<GetUserQuery, Result>
        {

            private readonly IAppDbContext _context;
            private readonly IValidator<GetUserQuery> _validator;


            public Handler(IAppDbContext context, IValidator<GetUserQuery> validator)
            {
                _context = context;
                _validator = validator;
            }

            public async Task<Result> Handle(GetUserQuery request, CancellationToken cancellationToken)
            {
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    // Handle validation errors (throw exception, return specific error response, etc.)
                    throw new ValidationException(validationResult.Errors);
                }

                var userData = await _context.userDatas.FindAsync(request.Id);
                if (userData == null)
                    return new Result(false, message: "not found");
                return new Result(true, userData, "done");
            }
        }


    }
}
