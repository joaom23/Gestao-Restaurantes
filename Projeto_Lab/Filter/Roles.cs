using Projeto_Lab.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymFitness.Filters
{
    public class Roles : ActionFilterAttribute
    {
        public string Estatuto { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //if (UtilizadorsController.Autenticado(context.HttpContext))
            //{
            if (context.HttpContext.Session.GetString("Estatuto") == Estatuto)
            {
                base.OnActionExecuting(context);
            }
            else
            {
               // Controller c = (context.Controller as Controller);
                //c.ViewData["mensagem"] = "Necessita de ter Perfil " + Estatuto + " para poder aceder a esta parte so site";
                context.Result = new ViewResult { StatusCode = 401, ViewName = "SemPermissoes"/*, ViewData = c.ViewData */};
            }
            //}
            //else
            //{
            //    Controller c = (context.Controller as Controller);
            //    c.ViewData["mensagem"] = "Necessita estar autenticado para poder utilizar o site!";
            //    context.Result = new ViewResult { StatusCode = 401, ViewName = "SemPermissoes", ViewData = c.ViewData };
            //}
        }
    }
}
