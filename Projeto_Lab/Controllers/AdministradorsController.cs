using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GymFitness.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Projeto_Lab.Data;
using Projeto_Lab.Models;


namespace Projeto_Lab.Controllers
{
   [Roles(Estatuto = "Administrador")]
    public class AdministradorsController : Controller
    {
        private readonly LabContext _context;
        private readonly IHostEnvironment _he;

        public AdministradorsController(LabContext context, IHostEnvironment he)
        {
            _context = context;
            _he = he;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CriarAdmin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CriarAdmin(IFormCollection dados) { 
            Utilizador ut = _context.Utilizadors.FirstOrDefault(x => x.Username == dados["Username"].ToString() || x.Email == dados["Email"].ToString());


            if (ut != null)
            {
                if (ut.Username == dados["Username"].ToString())
                {
                    ModelState.AddModelError("Username", "Utilizador já se encontra registado. Utilize outro username.");
                }
                if (ut.Email == dados["Email"].ToString())
                {
                    ModelState.AddModelError("Email", "Este email já se encontra registado. Utilize outro email.");
                }
            }

            if (ModelState.IsValid)
            {
                Utilizador u = new Utilizador();

                u.Username = dados["Username"];
                u.Email = dados["Email"];
                u.Pass = "ok";
                u.Estado = 1;

                _context.Utilizadors.Add(u);
                _context.SaveChanges();


                Utilizador utl = _context.Utilizadors.FirstOrDefault(x => x.Username == dados["Username"].ToString());


                Administrador a = new Administrador();

                a.IdA = utl.Id;
                //a.CriadoPor = user.id;
                _context.Administradors.Add(a);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        //Importante a parte do include do navegation para futuras ações 
        public IActionResult ListaAceitarRestaurantes(string Type, string Message)
        {
            ViewBag.Type = Type;
            ViewBag.Message = Message;

            return View(_context.Restaurantes.Include(x=>x.IdRNavigation)
                .Include(x=>x.RegistadoPorNavigation)
                .ThenInclude(x => x.IdANavigation)
                .Where(x => x.IdRNavigation.Estado == 0));
        }
        
        public IActionResult VerRestaurantesAtivos()
        {
            return View(_context.Restaurantes.Include(x => x.IdRNavigation)
                .Include(x => x.RegistadoPorNavigation)
                .ThenInclude(x => x.IdANavigation)
                .Where(x => x.IdRNavigation.Estado == 1));
        }
   
        public IActionResult AceitaRestaurante(int id)
        {

            Utilizador u = _context.Utilizadors.FirstOrDefault(x => x.Id == id);
            Restaurante r = _context.Restaurantes.FirstOrDefault(x => x.IdR == id);
           
            u.Estado = 1;
            r.RegistadoPor = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));

            _context.Utilizadors.Update(u);
            _context.Restaurantes.Update(r);

            _context.SaveChanges();
            return RedirectToAction("ListaAceitarRestaurantes",  new { Type = "success", Message = "Restaurante " + r.Nome +  "foi aceite com sucesso!!" });
        }

        public IActionResult RejeitaRestaurante(int id)
        {
            Utilizador ut = _context.Utilizadors.FirstOrDefault(x => x.Id == id);
            Restaurante r = _context.Restaurantes.FirstOrDefault(x => x.IdR == id);
            PratoDium pd = _context.PratoDia.FirstOrDefault(x => x.IdR == id);
            //RestTipo rt = _context.RestTipos.FirstOrDefault(x => x.IdR == id);

            string caminho = Path.Combine(_he.ContentRootPath, "wwwroot/Imagens/Registos/Restaurantes");
            string caminhoTotal = Path.Combine(caminho, Path.GetFileName(r.Foto));

            System.IO.File.Delete(caminhoTotal);

            if (pd != null) { _context.PratoDia.Remove(pd); }
            _context.Utilizadors.Remove(ut);
            _context.Restaurantes.Remove(r);
            _context.SaveChanges();

            return RedirectToAction("ListaAceitarRestaurantes", new { Type = "success", Message = "Restaurante " + r.Nome + "foi suspenso com sucesso!!" });
        }

        public IActionResult Suspensoes()
        {
                    return View(_context.Utilizadors.Where(x => x.Estado >= 1 && x.Administrador == null));
          
        }   

        public IActionResult Suspender(int id)
        {
            return View(_context.Utilizadors.FirstOrDefault(x => x.Id == id));
        }

        [HttpPost]
        public IActionResult Suspender(IFormCollection dados, int id)
        {

            if (String.IsNullOrEmpty(dados["Motivo"]))
            {
                ModelState.AddModelError("Motivo", "Este campo é obrigatório");
            }

            if (ModelState.IsValid)
            {
                if (contains(id))
                {
                    Utilizador ut = _context.Utilizadors.FirstOrDefault(x => x.Id == id);
                    Suspenso s = _context.Suspensos.FirstOrDefault(x => x.IdU == id);

                    ut.Estado = 2;
                    s.Motivo = dados["Motivo"].ToString();
                    //s.IdU = id;
                    //s.IdAdm = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
                    s.DataBloqueio = DateTime.Now.Date;
                    _context.Utilizadors.Update(ut);
                    _context.Suspensos.Update(s);
                    _context.SaveChanges();
                }
                else
                {  
                    
                    Utilizador ut = _context.Utilizadors.FirstOrDefault(x => x.Id == id);
                    Suspenso s = new Suspenso();

                    ut.Estado = 2;
                    s.Motivo = dados["Motivo"].ToString();
                    s.IdU = id;
                    s.IdAdm = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
                    s.DataBloqueio = DateTime.Now.Date;
                    _context.Utilizadors.Update(ut);
                    _context.Suspensos.Add(s);
                    _context.SaveChanges();
                    
                }
            }
    
         return RedirectToAction("Suspensoes");
       }

        public IActionResult Desbloquear(int id)
        {
            Utilizador ut = _context.Utilizadors.FirstOrDefault(x=>x.Id == id);
            Suspenso s = _context.Suspensos.FirstOrDefault(x=>x.IdU == id);
 
            _context.Suspensos.Remove(s);
            _context.SaveChanges();

            Suspenso sus = new Suspenso();
            ut.Estado = 1;
       
            sus.DataDesbloqueio = DateTime.Now.Date;
            sus.Motivo = "";
            sus.IdU = id;
            sus.IdAdm = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));

            _context.Suspensos.Add(sus);
            _context.Utilizadors.Update(ut);
            _context.SaveChanges();

            return RedirectToAction("Suspensoes");
        }

        private bool AdministradorExists(int id)
        {
            return _context.Administradors.Any(e => e.IdA == id);
        }

        bool contains(int id)
        {
            if(_context.Suspensos.Any(x=>x.IdU == id))
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
