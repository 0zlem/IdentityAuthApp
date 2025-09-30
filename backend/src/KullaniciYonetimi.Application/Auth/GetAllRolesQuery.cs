using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KullaniciYonetimi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace KullaniciYonetimi.Application.Auth;

public sealed record GetAllRolesQuery() : IRequest<Result<List<AppRole>>>;

internal sealed class GetAllRolesQueryHandler(RoleManager<AppRole> roleManager) : IRequestHandler<GetAllRolesQuery, Result<List<AppRole>>>
{
    public async Task<Result<List<AppRole>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await roleManager.Roles.ToListAsync();

        return Result<List<AppRole>>.Succeed(roles);
    }
}
