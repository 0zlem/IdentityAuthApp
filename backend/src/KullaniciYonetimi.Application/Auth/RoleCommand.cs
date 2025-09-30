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

public sealed record RoleCommand(string Name) : IRequest<Result<string>>;

internal sealed class RoleCommandHandler(RoleManager<AppRole> roleManager, IUnitOfWork unitOfWork) : IRequestHandler<RoleCommand, Result<string>>
{
    public async Task<Result<string>> Handle(RoleCommand request, CancellationToken cancellationToken)
    {
        AppRole role = new()
        {
            Name = request.Name
        };

        await roleManager.CreateAsync(role);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Rol başarıyla eklendi.");
    }
}
