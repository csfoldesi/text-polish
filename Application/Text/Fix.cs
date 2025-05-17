using Application.Common.Interfaces;
using Domain;
using MediatR;

namespace Application.Text
{
    public class Fix
    {
        public class Query : IRequest<string>
        {
            public required string Text { get; set; }
        }

        public class Handler : IRequestHandler<Query, string>
        {
            private readonly IAIService _AIService;
            private readonly IDataContext _dataContext;

            public Handler(IAIService aIService, IDataContext dataContext)
            {
                _AIService = aIService;
                _dataContext = dataContext;
            }

            public async Task<string> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _AIService.FixTextAsync(request.Text);
                var tokenUsage = new TokenUsage
                {
                    Model = result.Model ?? "",
                    InputTokenCount = result.InputTokenCount,
                    OutputTokenCount = result.OutputTokenCount,
                };
                _dataContext.TokenUsages.Add(tokenUsage);
                try
                {
                    await _dataContext.SaveChangesAsync(cancellationToken);
                    return result.Result ?? "";
                }
                catch (Exception)
                {
                    return "Error";
                }
            }
        }
    }
}
