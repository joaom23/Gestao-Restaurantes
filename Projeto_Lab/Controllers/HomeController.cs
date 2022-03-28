using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GymFitness.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Projeto_Lab.Data;
using Projeto_Lab.Models;

namespace Projeto_Lab.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LabContext _context;

        public HomeController(ILogger<HomeController> logger, LabContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            if (UtilizadorsController.Cliente(HttpContext)) {
                var userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
                ViewBag.noti = _context.Notificaçao.Where(x => x.IdC == userId && x.isRead == false).Count();
                    }
            return View();
        }

        public IActionResult ListaRestaurantes()
        {
            return View(_context.Restaurantes.Include(x => x.IdRNavigation)
                .Include(x => x.RegistadoPorNavigation)
                .ThenInclude(x => x.IdANavigation)
                .Where(x => x.IdRNavigation.Estado == 1));
        }

        public IActionResult VerPrato(int id)
        {
            
            //ICollection<PratoDium> prato = _context.PratoDia.Where(x => x.IdR == id).ToList();
            //int[] ids = new int[prato.Count];
            //bool[] res = new bool[2];
            //int i = 0;
            ////ViewBag.contemPrato = ContainsPrato();
            //foreach (PratoDium p in prato)
            //{
            //    //ids[i] = p.IdPd;
            //    //ViewBag.contemPrato = ContainsPrato(p.IdPd);
            //    res[i] = ContainsPrato(p.IdPd);
            //     i++;
            //}
            ViewData["id"] = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
            //ViewBag.contemPrato = res;
       

            return View(_context.PratoDia.Where(x => x.IdR == id));
        }

        public IActionResult VerMaisInfo(int id)
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));

            ListaFavRestaurante list = _context.ListaFavRestaurantes.Include(x => x.IdRNavigation).ThenInclude(x => x.IdRNavigation).FirstOrDefault(x => x.IdC == userId);

            //if (contains(id))
            //{
            //    ViewBag.contem = 1;
            //}
            //else
            //{
            //    ViewBag.contem = 0;
            //}

            ViewBag.contemRest = ContainsRest(id);
            

            return View(_context.Restaurantes.Include(x=>x.IdRNavigation).Where(x => x.IdR == id));
        }

        bool ContainsRest(int id)
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));

            if (_context.ListaFavRestaurantes.Any(x => x.IdR == id && x.IdC == userId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

       bool ContainsPrato(int id)
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));

            if (_context.ListaFavPratos.Any(x => x.PratoId == id && x.IdCl == userId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
