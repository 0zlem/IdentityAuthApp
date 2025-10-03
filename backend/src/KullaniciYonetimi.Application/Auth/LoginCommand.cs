using System.Security.Claims;
using GenericRepository;
using KullaniciYonetimi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace KullaniciYonetimi.Application.Auth;

public sealed record LoginCommand(string UserNameOrEmail, string Password)
    : IRequest<Result<LoginCommandResponse>>;

public class LoginCommandResponse
{
    public string Token { get; set; } = default!;
}

internal sealed class LoginCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : IRequestHandler<LoginCommand, Result<LoginCommandResponse>>
{
    public async Task<Result<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        AppUser? user = await userManager.Users.FirstOrDefaultAsync(p => p.Email == request.UserNameOrEmail || p.UserName == request.UserNameOrEmail, cancellationToken);

        if (user is null)
        {
            return Result<LoginCommandResponse>.Failure("Kullanıcı bulunamadı!");
        }

        SignInResult signInResult = await signInManager.CheckPasswordSignInAsync(user, request.Password, true);


        if (signInResult.IsLockedOut)
        {
            TimeSpan? timeSpan = user.LockoutEnd - DateTimeOffset.UtcNow;

            var seconds = timeSpan.HasValue ? Math.Ceiling(timeSpan.Value.TotalSeconds) : 30;

            if (timeSpan is not null)
            {
                return Result<LoginCommandResponse>.Failure($"Şifrenizi 3 kere yanlış girdiğiniz için kullanıcınız {seconds} saniye süreyle bloke edilmiştir.");
            }
            else
            {
                return Result<LoginCommandResponse>.Failure($"Şifreinizi 3 kere yanlış girdiğiniz için kullanıcınız 30 saniye süreyle bloke edilmiştir.");
            }
        }

        if (!signInResult.Succeeded)
            return Result<LoginCommandResponse>.Failure("Şifreniz hatalıdır!");

        // if (signInResult.IsNotAllowed)
        //     return Result<LoginCommandResponse>.Failure("Mail adresiniz onaylanmamıştır.");

        return Result<LoginCommandResponse>.Succeed(new LoginCommandResponse { Token = "Token" });
    }
}


