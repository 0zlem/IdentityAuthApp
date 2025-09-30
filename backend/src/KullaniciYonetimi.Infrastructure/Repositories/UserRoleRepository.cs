using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericRepository;
using KullaniciYonetimi.Application.Interfaces;
using KullaniciYonetimi.Domain.Entities;
using KullaniciYonetimi.Infrastructure.Context;

namespace KullaniciYonetimi.Infrastructure.Repositories;

internal sealed class UserRoleRepository : Repository<AppUserRole, AppDbContext>, IUserRoleRepository
{
    public UserRoleRepository(AppDbContext context) : base(context)
    {
    }
}
