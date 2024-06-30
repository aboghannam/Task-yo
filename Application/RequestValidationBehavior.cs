using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
      where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        //private readonly ILog _log;

        public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators //,ILog log
        )
        {
            _validators = validators;
            //_log = log;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var name = typeof(TRequest).Name;

            var failures = _validators
              .Select(v => (v.Validate(context)))
              .SelectMany(result => result.Errors)
              .ToList();

            if (failures.Count != 0)
            {
                var message = $"HelpApp Long Running Request: {name} \n";
                foreach (var failure in failures)
                {
                    message += $" {failure.ErrorMessage} \n";
                }

                throw new ApplicationException(message);
            }

            return await next();
        }

    }
}
