using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KullaniciYonetimi.Application.Auth;
using MediatR;
using TS.Result;

namespace KullaniciYonetimi.WebAPI.Modules;

public static class AuthModule
{
    public static void RegisterAuthRoutes(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder groupBuilder = app.MapGroup("/auth").WithTags("Auth");

        groupBuilder.MapPost("/register", async (ISender sender, RegisterCommand request, CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(request, cancellationToken);
            return response.IsSuccessful ? Results.Ok(response) : Results.BadRequest(response);
        }).Produces<Result<string>>();

        groupBuilder.MapPost("/changePassword", async (ISender sender, ChangePasswordCommand request, CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(request, cancellationToken);
            return response.IsSuccessful ? Results.Ok(response) : Results.BadRequest(response);
        }).Produces<Result<string>>();

        groupBuilder.MapPost("/forgotPassword", async (ISender sender, ForgotPasswordCommand request, CancellationToken cancellationToken) =>
       {
           var response = await sender.Send(request, cancellationToken);
           return response.IsSuccessful ? Results.Ok(response) : Results.BadRequest(response);
       }).Produces<Result<string>>();


        groupBuilder.MapPost("/login", async (ISender sender, LoginCommand request, CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(request, cancellationToken);

            return response.IsSuccessful ? Results.Ok(response) : Results.BadRequest(response);
        }).Produces<Result<LoginCommandResponse>>();

        groupBuilder.MapPost("/role", async (ISender sender, RoleCommand request, CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(request, cancellationToken);

            return response.IsSuccessful ? Results.Ok(response) : Results.BadRequest(response);
        }).Produces<Result<string>>();

        groupBuilder.MapGet("/roles", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(new GetAllRolesQuery(), cancellationToken);
            return response.IsSuccessful ? Results.Ok(response) : Results.BadRequest(response);
        }).Produces<Result<List<string>>>();

        groupBuilder.MapPost("/userRole/create", async (ISender sender, UserRolesCommand request, CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(request, cancellationToken);

            return response.IsSuccessful ? Results.Ok(response) : Results.BadRequest(response);
        }).Produces<Result<string>>();

    }

}
