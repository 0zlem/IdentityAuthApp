using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericRepository;
using KullaniciYonetimi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TS.Result;

namespace KullaniciYonetimi.Application.Auth;

public sealed record ChangePasswordCommand(Guid Id, string CurrentPassword, string NewPassword) : IRequest<Result<string>>;

public class ChangePasswordCommandHandler(UserManager<AppUser> userManager, IUnitOfWork unitOfWork) : IRequestHandler<ChangePasswordCommand, Result<string>>
{
    public async Task<Result<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {

        AppUser? user = await userManager.FindByIdAsync(request.Id.ToString());

        if (user is null)
        {
            return Result<string>.Failure("Kullanıcı bulunamadı!!!");
        }

        IdentityResult result = await userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            return Result<string>.Failure(errors);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Şifre başarıyla değiştirildi.");

    }
}
