using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericRepository;
using KullaniciYonetimi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using TS.Result;

namespace KullaniciYonetimi.Application.Auth;

public sealed record LogoutCommand() : IRequest<Result<string>>;

public class LogoutCommandHandler(SignInManager<AppUser> signInManager) : IRequestHandler<LogoutCommand, Result<string>>
{
    public async Task<Result<string>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        await signInManager.SignOutAsync();

        return Result<string>.Succeed("Çıkış yapıldı.");
    }
}
