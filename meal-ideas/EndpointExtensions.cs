using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace meal_ideas;

public static class EndpointExtensions
{
    public static RouteHandlerBuilder MediateGet<T>(this WebApplication application, string path) 
        where T : IHttpResult, new()
    {
        return application.MapGet(path,
            (IMediator mediator) => mediator.Send(new T()));
    }
    
    public static RouteHandlerBuilder MediatePost<T>(this WebApplication application, string path) 
        where T : IHttpResult
    {
        return application.MapPost(path, 
            (IMediator mediator, [FromBody]T request) => mediator.Send(request));
    }
}