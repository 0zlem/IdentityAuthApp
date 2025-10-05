using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericRepository;
using KullaniciYonetimi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using TS.Result;

namespace KullaniciYonetimi.Application.Auth;

public sealed record RoleCommand(string Name) : IRequest<Result<string>>;

internal sealed class RoleCommandHandler(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork) : IRequestHandler<RoleCommand, Result<string>>
{
    public async Task<Result<string>> Handle(RoleCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.GetUserAsync(httpContextAccessor.HttpContext!.User);

        if (user is null)
        {
            return Result<string>.Failure("Kullanıcı bulunamadı!");
        }

        var roles = await userManager.GetRolesAsync(user);

        if (!roles.Contains("Admin"))
        {
            return Result<string>.Failure("Bu işlemi yapma yetkiniz bulunmamaktadır!");
        }

        AppRole role = new()
        {
            Name = request.Name
        };

        await roleManager.CreateAsync(role);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Rol başarıyla eklendi.");
    }
}
