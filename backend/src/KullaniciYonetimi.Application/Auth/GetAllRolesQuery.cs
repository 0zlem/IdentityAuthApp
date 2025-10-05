using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KullaniciYonetimi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace KullaniciYonetimi.Application.Auth;

public sealed record GetAllRolesQuery() : IRequest<Result<List<AppRole>>>;

internal sealed class GetAllRolesQueryHandler(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor) : IRequestHandler<GetAllRolesQuery, Result<List<AppRole>>>
{
    public async Task<Result<List<AppRole>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {

        var user = await userManager.GetUserAsync(httpContextAccessor.HttpContext!.User);

        if (user is null)
        {
            return Result<List<AppRole>>.Failure("Kullanıcı bulunamadı!");
        }

        var role = await userManager.GetRolesAsync(user);

        if (!role.Contains("Admin"))
        {
            return Result<List<AppRole>>.Failure("Bu işlemi yapma yetkiniz bulunmamaktadır!");
        }

        var roles = await roleManager.Roles.ToListAsync();

        return Result<List<AppRole>>.Succeed(roles);
    }
}
