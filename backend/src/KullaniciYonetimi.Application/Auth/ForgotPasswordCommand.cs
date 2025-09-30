using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericRepository;
using KullaniciYonetimi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TS.Result;

namespace KullaniciYonetimi.Application.Auth;

public sealed record ForgotPasswordCommand(string Email) : IRequest<Result<ForgotPasswordResponse>>;

public class ForgotPasswordResponse
{
    public string Token { get; set; } = default!;
}


internal sealed class ForgotPasswordCommandHandler(UserManager<AppUser> userManager, IUnitOfWork unitOfWork) : IRequestHandler<ForgotPasswordCommand, Result<ForgotPasswordResponse>>
{
    public async Task<Result<ForgotPasswordResponse>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        AppUser? user = await userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            return Result<ForgotPasswordResponse>.Failure("Kullanıcı bulunamadı!!!");
        }

        string token = await userManager.GeneratePasswordResetTokenAsync(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<ForgotPasswordResponse>.Succeed(new ForgotPasswordResponse { Token = token });
    }
}