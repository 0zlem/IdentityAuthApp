using FluentValidation;
using GenericRepository;
using KullaniciYonetimi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using TS.Result;

namespace KullaniciYonetimi.Application.Auth;

public sealed record RegisterCommand(
    string Email,
    string UserName,
    string FirstName,
    string LastName,
    string Password
) : IRequest<Result<string>>;

internal sealed class RegisterCommandHandler(UserManager<AppUser> userManager, IUnitOfWork unitOfWork) : IRequestHandler<RegisterCommand, Result<string>>
{
    public async Task<Result<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        AppUser user = new()
        {
            Email = request.Email,
            UserName = request.UserName,
            FirstName = request.FirstName,
            LastName = request.LastName,
        };

        IdentityResult result = await userManager.CreateAsync(user, request.Password);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            return Result<string>.Failure(errors);
        }

        return Result<string>.Succeed("Kayıt başarılı!");

    }
}


