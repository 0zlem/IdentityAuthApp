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

public sealed record ChangePasswordUsingTokenCommand(string Email, string NewPassword, string Token) : IRequest<Result<string>>;

internal sealed class ChangePasswordUsingTokenCommandHandler(UserManager<AppUser> userManager, IUnitOfWork unitOfWork) : IRequestHandler<ChangePasswordUsingTokenCommand, Result<string>>
{
    public async Task<Result<string>> Handle(ChangePasswordUsingTokenCommand request, CancellationToken cancellationToken)
    {
        AppUser? user = await userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            return Result<string>.Failure("Kullanıcı bulunamadı!!!");
        }

        IdentityResult result = await userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            return Result<string>.Failure(errors);
        }

        return Result<string>.Succeed("");

    }
}