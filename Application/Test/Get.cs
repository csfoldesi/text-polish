using Application.Common.Interfaces;
using MediatR;

namespace Application.Test;

public class Get
{
    public class Query : IRequest<string> { }

    public class Handler : IRequestHandler<Query, string>
    {
        private readonly IAIService _AIService;

        public Handler(IAIService aIService)
        {
            _AIService = aIService;
        }

        public async Task<string> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _AIService.TestAsync();

            return result;
        }
    }
}
