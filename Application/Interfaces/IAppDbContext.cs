﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<UserData> userDatas { set; get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}