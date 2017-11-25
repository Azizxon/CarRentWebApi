using System;
using System.IO;
using CarRent.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace CarRentWebAPI.Filters
{
    public class ExceptionFilter:ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            Log.Error(context.Exception,"Error occured");
            switch (context.Exception)
            {
                case CarNotFountException exception:
                    context.Result=new NotFoundObjectResult(exception.Message);
                    return;
                case UserNotFoundException exception:
                    context.Result=new NotFoundObjectResult(exception.Message);
                    return;
                case CarIsUnvailableException exception:
                    context.Result=new BadRequestObjectResult(exception.Message);
                    return;
                case ArgumentException exception:
                    context.Result=new BadRequestObjectResult(exception.Message);
                    return;
                case InvalidOperationException exception:
                    context.Result=new BadRequestObjectResult(exception.Message);
                    return;
                case IOException exception: 
                    context.Result=new ObjectResult("Problems with file system")
                    {
                        StatusCode =503
                    };
                    return;
                    default: context.Result=new ObjectResult("unknown error occured")
                    {
                        StatusCode = 500
                    };return;
            }
        }

    }
}
