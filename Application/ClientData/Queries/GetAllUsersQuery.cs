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


namespace Application.ClientData.Queries
{
    public class GetAllUsersQuery: IRequest<Result>
    {
        public class Handler : IRequestHandler<GetAllUsersQuery, Result>
        {

            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
            {
                var usersData = await _context.userDatas.ToListAsync();
                return new Result(true, usersData, "done");
            }
        }


    }
}
