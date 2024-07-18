using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Softka.Utils.Exceptions
{
    public class ErrorExceptions
    {
        //Not found
        public static ProblemDetails CreateNotFound()
        {
            return new ProblemDetails
            {
                Title = "Not found",
                Status = StatusCodes.Status404NotFound,
                Detail = "The request has not been found"
            };
        }

        //Server error 500
        public static ProblemDetails CreateServerError()
        {
            return new ProblemDetails
            {
                Title = "Server error",
                Status = StatusCodes.Status500InternalServerError,
                Detail = "An error has occurred while processing your request"
            };
        }

        //Badrequest
        public static ProblemDetails CreateBadRequest()
        {
            return new ProblemDetails
            {
                Title = "Bad Request",
                Status = StatusCodes.Status400BadRequest,
                Detail = "Bad Request. Please check the submitted data and try again."
            };
        }

        //Status Ok
        public static ProblemDetails CreateOk()
        {
            return new ProblemDetails
            {
                Title = "Status ok",
                Status = StatusCodes.Status200OK,
                Detail = "The request has been resolve"
            };
        }
    }
}