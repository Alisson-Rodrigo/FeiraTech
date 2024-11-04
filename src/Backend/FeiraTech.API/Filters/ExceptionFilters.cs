using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using FeiraTech.Exceptions.ExceptionsBase;
using FeiraTech.Exceptions;
using FeiraTech.Communication.Response;

namespace FeiraTech.API.Filters
{
    public class ExceptionsFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            // Verificar se a exceção é do tipo MyRecipesBookExceptions
            if (context.Exception is FeiraTechExceptions)
            {
                HandleProjectException(context);
            }
            // Se não for, então é um erro desconhecido.
            else
            {
                ThrowUnknowException(context);
            }
        }

        // Nessa função é tratado a exceção do projeto e retornando o erro para o cliente.
        private void HandleProjectException(ExceptionContext context)
        {
            // Se a exceção for do tipo ErrorOnValidationException, então é retornado um BadRequest com a lista de erros.
            if (context.Exception is ErrorOnValidationException)
            {
                //Pegar a exceção e transformar em um objeto do tipo ErrorOnValidationException
                var exception = context.Exception as ErrorOnValidationException;

                //Retornar um BadRequest com a lista de erros
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new BadRequestObjectResult(new ResponseErrosJson(exception!.ErrorsMessages));
            }
        }

        private void ThrowUnknowException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new ObjectResult(new ResponseErrosJson(ResourceMessagesExceptions.UNKNOW_ERROR));
        }
    }

}

