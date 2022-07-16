#pragma warning disable CS8602 // Dereference of a possibly null reference.

using BMTDb.WebUI.Identity;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BMTDb.WebUI.Filters
{
    public class UserActivityFilter : IAsyncActionFilter
    {
        private readonly ApplicationContext _context;

        public UserActivityFilter(ApplicationContext context)
        {
            _context = context;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await next();
            var user = context.HttpContext.User.Identity.Name;
            if (user != null)
            {
                int data;

                var routeData = context.RouteData;
                var controller = routeData.Values["controller"];
                var action = routeData.Values["action"];
                var id = routeData.Values["id"];

                var url = $"{controller}/{action}/{id}";

                if (url == $"Movie/Details/{id}")
                {
                    //var arguments = context.ActionArguments;
                    //var value = arguments.FirstOrDefault().Value;

                    data = Convert.ToInt32(id);
                    var ipAddress = context.HttpContext.Connection.RemoteIpAddress.ToString();

                    await SaveUserActivity(data, url, user, ipAddress);
                }
            }
        }

        public async Task SaveUserActivity(int data, string url, string user, string ipAddress)
        {
            if (!string.IsNullOrEmpty(user))
            {
                var userActivity = new UserActivity
                {
                    Data = data,
                    Url = url,
                    UserName = user,
                    IpAddress = ipAddress,
                };

                _context.UserActivities.Add(userActivity);
                await _context.SaveChangesAsync();
            }
        }

        public List<UserActivity> GetUserActivity(string username)
        {
            var context = _context;

            return context.UserActivities.AsQueryable()
                        .Where(i => i.UserName == username).ToList();
        }
    }
}