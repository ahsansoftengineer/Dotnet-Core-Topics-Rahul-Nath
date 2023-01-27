using Microsoft.FeatureManagement;

namespace B7FeatureFlag
{
  public class BrowserFilter : IFeatureFilter
  {
    private readonly IHttpContextAccessor httpContextAccessor;

    public BrowserFilter(IHttpContextAccessor httpContextAccessor)
    {
      this.httpContextAccessor = httpContextAccessor;
    }

    public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
    {
      if (httpContextAccessor.HttpContext != null)
      {
        var userAgent = httpContextAccessor.HttpContext.Request.Headers.UserAgent.ToString();
        var settings = context.Parameters.Get<BrowserFilterSettins>();
        return Task.FromResult(settings.Allowed.Any(a => userAgent.Contains(a)));
      }
      return Task.FromResult(false);
    }
  }
  public class BrowserFilterSettins
  {
    public string[] Allowed { get; set; }
  }

}
