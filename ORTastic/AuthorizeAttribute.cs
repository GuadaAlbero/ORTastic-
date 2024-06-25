using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

    public class AuthorizeAttribute : ActionFilterAttribute
    {

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        //guardo el tipoPerfil para ver si esta logeado el usuario
        var tipoPerfil = context.HttpContext.Session.GetInt32("tipoPerfil");

        if (tipoPerfil == null) { 
            context.Result = new RedirectToActionResult("Login", "Account", null); 
        }
        
   
        base.OnActionExecuting(context);
    }

}

