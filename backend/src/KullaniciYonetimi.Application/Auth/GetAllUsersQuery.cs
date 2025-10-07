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

public sealed record GetAllUsersQuery() : IRequest<Result<List<AppUser>>>;

internal sealed class GetAllUsersHandler(UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor) : IRequestHandler<GetAllUsersQuery, Result<List<AppUser>>>
{
    public async Task<Result<List<AppUser>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {

        var user = await userManager.GetUserAsync(httpContextAccessor.HttpContext!.User);

        if (user is null)
        {
            return Result<List<AppUser>>.Failure("Kullanıcı bulunamadı!");
        }

        var role = await userManager.GetRolesAsync(user);

        if (!role.Contains("Admin"))
        {
            return Result<List<AppUser>>.Failure("Bu işlemi yapma yetkiniz bulunmamaktadır!");
        }

        var users = await userManager.Users.ToListAsync();

        return Result<List<AppUser>>.Succeed(users);
    }
}
