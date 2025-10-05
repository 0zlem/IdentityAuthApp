using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericRepository;
using KullaniciYonetimi.Application.Interfaces;
using KullaniciYonetimi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using TS.Result;

namespace KullaniciYonetimi.Application.Auth;

public sealed record UserRolesCommand(Guid UserId, Guid RoleId) : IRequest<Result<string>>;

internal sealed class UserRolesCommandHandler(IUserRoleRepository userRoleRepository, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork) : IRequestHandler<UserRolesCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UserRolesCommand request, CancellationToken cancellationToken)
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
        AppUserRole userRole = new()
        {
            UserId = request.UserId,
            RoleId = request.RoleId
        };

        await userRoleRepository.AddAsync(userRole);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Rol başarıyla kullanıcıya atandı.");
    }
}


