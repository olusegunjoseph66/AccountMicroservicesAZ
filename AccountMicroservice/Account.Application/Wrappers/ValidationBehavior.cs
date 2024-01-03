using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Account.Application.Wrappers
{
    public class ValidationBehavior : IAsyncActionFilter
    {

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var modelStateEntries = context.ModelState.Where(e => e.Value.Errors.Count > 0).ToArray();
            
            if (modelStateEntries.Any())
            {
                var error = modelStateEntries[0].Value.Errors.FirstOrDefault();
                throw new ValidationException(error.ErrorMessage);
            }

            await next();
        }
    }


    //public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    //    where TRequest : IRequest<TResponse>
    //{
    //    private readonly IEnumerable<IValidator<TRequest>> _validators;

    //    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    //    {
    //        _validators = validators;
    //    }

    //    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    //    {
    //        if (_validators.Any())
    //        {
    //            var context = new ValidationContext<TRequest>(request);
    //            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
    //            var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

    //            if (failures.Count != 0)
    //                throw new Exceptions.ValidationException(failures);
    //        }
    //        return await next();
    //    }
    //}
}
