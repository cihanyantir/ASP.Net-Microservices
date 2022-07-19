using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = Ordering.Application.Exceptions.ValidationException; //Ordering.Application.Exceptionstaki classı newliyor 32.line

namespace Ordering.Application.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : MediatR.IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        //reaching classes that implemented by IValidator like AbstractValidator on checkoutordervalidators.
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next) //queue with next, next another behaviour
        {
            if(_validators.Any()) // if there is validation
            {                                   //data on request object
                var context = new ValidationContext<TRequest>(request); //on fluent namespace
               var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));//runs all validators and return results
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList(); //our validaton errors
                                                        //Errors ValidationResult'classtan geliyor

                if (failures.Count != 0)
                    throw new ValidationException();
            }

            return await next();
        }
    }
}