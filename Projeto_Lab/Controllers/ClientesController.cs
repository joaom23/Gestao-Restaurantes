using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymFitness.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projeto_Lab.Data;
using Projeto_Lab.Models;

namespace Projeto_Lab.Controllers
{
    [Roles(Estatuto = "Cliente")]
    public class ClientesController : Controller
    { 
        private readonly LabContext _context;

        public ClientesController(LabContext context)
        {
            _context = context;
        }

        public IActionResult ViewInicialCliente()
        {
            //ViewBag.tags = _context.PratoDia.ToList();

            return View();
        }

        public IActionResult ListaRestFav()
        { 
            var userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));

            return View(_context.ListaFavRestaurantes.Include(x=>x.IdRNavigation).ThenInclude(x=>x.IdRNavigation).Where(x=>x.IdC == userId));
            }

     
        public IActionResult AddRestFav(int id, int notif)
        {
            ViewBag.noti = true;

            var userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));

            ListaFavRestaurante list = new ListaFavRestaurante();

            list.IdC = userId;
            list.IdR = id;
            if(notif == 1)
            {
                list.Notifica = true;
            }
            else if(notif == 0)
            {
                list.Notifica = false;
            }
            _context.ListaFavRestaurantes.Add(list);
            _context.SaveChanges();

            return RedirectToAction("VerMaisInfo", "Home", new { id = id});
        }
        public IActionResult RemoveFavRest(int id)
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));

            ListaFavRestaurante list = _context.ListaFavRestaurantes.FirstOrDefault(x => x.IdC == userId && x.IdR == id);


            _context.ListaFavRestaurantes.Remove(list);
            //_context.Entry(list).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();



            return RedirectToAction("ListaRestFav");
        }

        public IActionResult RemoveNoti(int id)
        {
            ViewBag.noti = false;

            var userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));

            ListaFavRestaurante list = _context.ListaFavRestaurantes.FirstOrDefault(x => x.IdR == id && x.IdC == userId);

            list.Notifica = false;

            _context.ListaFavRestaurantes.Update(list);
            _context.SaveChanges();


            return RedirectToAction("ListaRestFav");
        }

        public IActionResult AddNoti(int id)
        {
            ViewBag.noti = true;

            var userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));

            ListaFavRestaurante list = _context.ListaFavRestaurantes.FirstOrDefault(x => x.IdR == id && x.IdC == userId);

            list.Notifica = true;

            _context.ListaFavRestaurantes.Update(list);
            _context.SaveChanges();


            return RedirectToAction("ListaRestFav");
        }


        public IActionResult ListaPratFav()
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));

            return View(_context.ListaFavPratos.Include(x=>x.Prato).Where(x=>x.IdCl == userId));
        }

        public IActionResult AddPratFav(int id, int notif, int idR)
        {
            ViewBag.noti = true;

            var userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));

            ListaFavPrato list = new ListaFavPrato();

            list.IdCl = userId;
            list.PratoId = id;
            if (notif == 1)
            {
                list.Notifica = true;
            }
            else if (notif == 0)
            {
                list.Notifica = false;
            }
            _context.ListaFavPratos.Add(list);
            _context.SaveChanges();

            return RedirectToAction("VerPrato", "Home", new {id = idR });
        }

        public IActionResult RemoveFavPrat(int id)
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));

            ListaFavPrato list = _context.ListaFavPratos.FirstOrDefault(x => x.IdCl == userId && x.PratoId == id);


            _context.ListaFavPratos.Remove(list);
            //_context.Entry(list).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();



            return RedirectToAction("ListaPratFav");
        }

        public IActionResult RemoveNotiP(int id)
        {
            ViewBag.noti = false;

            var userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));

            ListaFavPrato list = _context.ListaFavPratos.FirstOrDefault(x => x.IdCl == userId && x.PratoId == id);

            list.Notifica = false;

            _context.ListaFavPratos.Update(list);
            _context.SaveChanges();


            return RedirectToAction("ListaPratFav");
        }

        public IActionResult AddNotiP(int id)
        {
            ViewBag.noti = true;

            var userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));

            ListaFavPrato list = _context.ListaFavPratos.FirstOrDefault(x => x.IdCl == userId && x.PratoId == id);

            list.Notifica = true;

            _context.ListaFavPratos.Update(list);
            _context.SaveChanges();


            return RedirectToAction("ListaPratFav");
        }

        bool ContainsRest(int id)
        {
            if (_context.ListaFavRestaurantes.Any(x => x.IdR == id))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

            public  IActionResult ProcurarPratos(string nome)
            {
            ViewBag.nome = nome;

            if (string.IsNullOrEmpty(nome) == false)
            {

                return View(_context.PratoDia.Where(x => x.IdPdNavigation.Nome.Contains(nome) || x.IdPdNavigation.Tipo.Contains(nome)).Include(x => x.IdPdNavigation).AsNoTracking());
                //return View(_context.PratoDia.Where(x => x.Tipo.Contains(nome)));

            }
            else
            {
                return View(_context.PratoDia.Include(x => x.IdPdNavigation).AsNoTracking());
            }
            //foreach(var item in _context.PratoDia.Include(x => x.IdPdNavigation))
            //{

            //}
            //var pratos = from p in _context.PratoDia.Include(x=>x.IdPdNavigation)
            //             select p;

            //if (!String.IsNullOrEmpty(nome))
            //{

            //    pratos = pratos.Where(x => x.IdPdNavigation.Nome.Contains(nome) || x.IdPdNavigation.Tipo.Contains(nome)).Include(x => x.IdPdNavigation).AsNoTracking();

            //    return View(pratos);
            //}
            //else
            //{
            //    return View(pratos.AsNoTracking().ToList());
            //}
        }

        public IActionResult VerNotis()
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));

            ICollection <Notificaçao> notis = _context.Notificaçao.Where(x => x.IdC == userId).ToList();

            foreach(Notificaçao noti in notis)
            {
                if(noti.isRead == false)
                {
                    noti.isRead = true;
                }

                _context.Notificaçao.Update(noti);
                _context.SaveChanges();
            }
       
            return View(_context.Notificaçao.Where(x=>x.IdC == userId));
        }

        public IActionResult DeleteNotis(int id)
        {
            Notificaçao n = _context.Notificaçao.FirstOrDefault(x => x.IdNoti == id);
            _context.Remove(n);
            _context.SaveChanges();

            return RedirectToAction("VerNotis");
        }
    }
}
