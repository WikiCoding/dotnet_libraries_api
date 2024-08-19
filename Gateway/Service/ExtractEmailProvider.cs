using Microsoft.AspNetCore.Identity;
using Yarp.ReverseProxy.Transforms;
using Yarp.ReverseProxy.Transforms.Builder;

namespace Gateway.Service
{
    public class ExtractEmailProvider : ITransformProvider
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;

        //public ExtractEmailProvider(IHttpContextAccessor httpContextAccessor)
        //{
        //    _httpContextAccessor = httpContextAccessor;
        //}
        private readonly UserManager<IdentityUser> _userManager;

        public ExtractEmailProvider(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public void Apply(TransformBuilderContext context)
        {
            context.AddRequestTransform(async transformContext =>
            {
                //var httpContext = _httpContextAccessor.HttpContext;
                //if (httpContext != null) return;

                //var userManager = httpContext.RequestServices.GetRequiredService<UserManager<IdentityUser>>();

                //var user = await _userManager.GetUserAsync(httpContext.User);
                var user = await _userManager.GetUserAsync(transformContext.HttpContext.User);
                if (user != null)
                {
                    transformContext.ProxyRequest.Headers.Add("X-User-Email", user.Email);
                }
            });
        }

        public void ValidateCluster(TransformClusterValidationContext context)
        {
            
        }

        public void ValidateRoute(TransformRouteValidationContext context)
        {
            
        }
    }
}
