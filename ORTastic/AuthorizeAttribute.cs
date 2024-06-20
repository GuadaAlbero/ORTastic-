using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

    public class AuthorizeAttribute : ActionFilterAttribute
    {

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        //guardo en username el valor que se encuentra en username de mi sesion
        var username = context.HttpContext.Session.GetString("Username");

        if (!string.IsNullOrEmpty(username))
        {

        }
        else {
            context.Result = new RedirectToActionResult("Login", "Account", null);
        }

        base.OnActionExecuting(context);
    }

}

