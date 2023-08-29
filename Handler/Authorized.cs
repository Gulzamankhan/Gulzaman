using E_Commerce.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace E_Commerce.Handler
{
    public class Authorized : ActionFilterAttribute, IAuthorizationFilter
    {
        public string Roles { get; set; }
        private List<string> rolelist=new();
        private bool IsAthenticated { get; set; } = false;
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            rolelist=(Roles ?? "").Split(new char[] {',',' '},StringSplitOptions.RemoveEmptyEntries).ToList();
          var db=(AppDbContext)context.HttpContext.RequestServices.GetService(typeof(AppDbContext));
              var userid = context.HttpContext.Session.GetString(Globalconfig.LoginSessionName);
       var loggedinuser=db.GetloggedinUser();
            if (loggedinuser!=null)
            {
                if(rolelist.Any())
                {
                    //foreach (var requiredrole in rolelist)
                    //{
                    //    foreach (var userrole in db.Loggedinuser.Role)
                    //    {
                    //        if(requiredrole==userrole)
                    //        {
                    //            IsAthenticated = true;
                    //        }
                            
                    //    }
                    //    if (IsAthenticated) break;
                    //}
                 IsAthenticated=rolelist.Any(rr => loggedinuser.Role.Any(ur => rr == ur));
                }
                else
                {
                    IsAthenticated = true;
                }
              //  chek roles and stutus etc
            }
            if(!IsAthenticated)
            {
                  context.Result=new RedirectResult("~/Login");
            }
            
        }
    }
}
