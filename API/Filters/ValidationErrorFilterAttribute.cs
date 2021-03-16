using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters {
    public class ValidationErrorFilter: Attribute, IExceptionFilter {
        public void OnException(ExceptionContext context) {
            var exceptionType = context.Exception.GetType();

            if (exceptionType == typeof(ArgumentException)) {
                Console.WriteLine(context.Exception.Message);
                
                context.Result = new JsonResult(new Dictionary<string, dynamic> {
                    { "success", false },
                    { "error", "validation-error" }
                });
                context.ExceptionHandled = true;
            }
        }
    }
}