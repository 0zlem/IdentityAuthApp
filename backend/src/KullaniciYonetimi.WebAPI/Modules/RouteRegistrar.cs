using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KullaniciYonetimi.WebAPI.Modules;

public static class RouteRegistrar
{
    public static void RegisterRoutes(this IEndpointRouteBuilder app)
    {
        app.RegisterAuthRoutes();
    }

}
