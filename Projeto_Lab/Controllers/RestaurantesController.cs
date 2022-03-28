using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GymFitness.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Projeto_Lab.Data;
using Projeto_Lab.Models;
using Projeto_Lab.Controllers;

namespace Projeto_Lab.Controllers
{
    //[Roles(Estatuto = "Restaurante")]
    public class RestaurantesController : Controller
    {
        private readonly LabContext _context;
        private readonly IHostEnvironment _he;
        public RestaurantesController(LabContext context, IHostEnvironment he)
        {
            _he = he;
            _context = context;
        }

        public IActionResult ViewInicialRestaurante()
        {
            return View();
        }

        public IActionResult CriarPratoDia()
        {
            ViewData["Prato"] = new SelectList(_context.Pratos.Select(x => x.Nome));

            return View();
        }

        [HttpPost]
        public IActionResult CriarPratoDia(IFormCollection dados, IFormFile foto)
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));

            Restaurante r = _context.Restaurantes.FirstOrDefault(x => x.IdR == userId);
            PratoDium pdia = _context.PratoDia.FirstOrDefault(x => x.DataPrato == Convert.ToDateTime(dados["date"]));

            //if(r != null)
            //{
            //    //if (r.DiaDesc == Convert.ToDateTime(dados["date"]).DayOfWeek.ToString())
            //    //{
            //    //    ModelState.AddModelError("DataPrato", "Não pode criar prato do dia no dia de descanço.");
            //    //}
            //    //if (pr.Nome == dados["Nome"].ToString())
            //    //{
            //    //    ModelState.AddModelError("Id_pd", "Este prato já se encontra registado.");
            //    //}
            //    //if (Convert.ToDateTime(dados["date"]).ToString() == null)
            //    //{
            //    //    ModelState.AddModelError("DataPrato", "Campo obrigatório.");
            //    //}
            //}

            //if(pdia != null)
            //{
            //    ModelState.AddModelError("DataPrato", "Já existe um prato para esse dia.");
            //}

            if (ModelState.IsValid)
            {
                //Prato pr = new Prato();

                //pr.Nome = dados["Nome"];
                //pr.Tipo = dados["Tipo"];

                //_context.Pratos.Add(pr);
                //_context.SaveChanges();

                ViewData["Prato"] = new SelectList(_context.Pratos.Select(x => x.Nome));

                string caminho = Path.Combine(_he.ContentRootPath, "wwwroot/Imagens/Pratos");
                string caminhoTotal = Path.Combine(caminho, Path.GetFileName(foto.FileName));
                FileStream fs = new FileStream(caminhoTotal, FileMode.Create);
                foto.CopyTo(fs);
                fs.Close();


                Prato p = _context.Pratos.FirstOrDefault(x => x.Nome == dados["Nome"].ToString());

                PratoDium pd = new PratoDium();

                pd.IdPd = p.IdP;
                pd.IdR = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
                pd.Tipo = p.Tipo;
                pd.Preco = Convert.ToInt32(dados["Preco"]);
                pd.Descricao = dados["Descricao"];
                pd.Foto = Path.GetFileName(foto.FileName);
                pd.DataPrato = Convert.ToDateTime(dados["date"]);

                _context.PratoDia.Add(pd);
                _context.SaveChanges();

                //foreach(var item in _context.ListaFavRestaurantes)
                //{
                //    if(item.Notifica == true && item.IdR == userId)
                //    {
                //        Notificaçao n = new Notificaçao();

                //        n.IdC = item.IdC;
                //        n.IdR = userId;
                //        n.Mensagem = "O Restaurante" + item.IdRNavigation.Nome + "adicionou um prato do dia!";
                //        n.isRead = false;

                //        _context.Notificaçao.Add(n);
                //        _context.SaveChanges();
                //    }
                //}
                ICollection<ListaFavRestaurante> listaRest = _context.ListaFavRestaurantes.Where(x => x.IdR == userId).ToList();
                ICollection<ListaFavPrato> listaPrato = _context.ListaFavPratos.Where(x => x.PratoId == p.IdP).ToList();

                if (listaRest.Count() > 0)
                {
                    foreach (ListaFavRestaurante lista in listaRest)
                    {
                        Notificaçao n = new Notificaçao();

                        n.IdC = lista.IdC;
                        n.IdR = lista.IdR;
                        n.Mensagem = lista.IdRNavigation.Nome + " adicionou um prato do dia!";
                        n.isRead = false;
                        n.Data = DateTime.Now;

                        _context.Notificaçao.Add(n);
                        _context.SaveChanges();
                    }

                }
                //Adicionar notificação de prato

                //if (listaPrato.Count() > 0)
                //{
                //    foreach (ListaFavPrato lista in listaPrato)
                //    {
                //        Notificaçao n = new Notificaçao();

                //        n.IdC = lista.IdCl;
                //        n.Idp = lista.PratoId;
                //        n.Mensagem = lista.Prato + " foi adicionado!";
                //        n.isRead = false;
                //        n.Data = DateTime.Now;

                //        _context.Notificaçao.Add(n);
                //        _context.SaveChanges();
                //    }

                //}

                return View();
            }

            return View();
        }

        public IActionResult VerPratosDoDia()
        {
            var restId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));

            return View(_context.PratoDia.Where(x => x.IdR == restId));
        }

        public IActionResult RemoverPrato(int id)
        {
            var restId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));

            PratoDium p = _context.PratoDia.FirstOrDefault(x => x.IdPd == id && x.IdR == restId);

            _context.PratoDia.Remove(p);
            _context.SaveChanges();

            return RedirectToAction("VerPratosDoDia");
        }

        public IActionResult AlterarData(int id)
        {
            var restId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
            PratoDium p = _context.PratoDia.FirstOrDefault(x => x.IdPd == id && x.IdR == restId);

            return PartialView("_alterarData", p);
        }
        [HttpPost]
        public string AlterarData(int id, IFormCollection dados)
        {
            var restId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));

            PratoDium p = _context.PratoDia.FirstOrDefault(x => x.IdPd == id && x.IdR == restId);

            p.DataPrato = Convert.ToDateTime(dados["date"]);
            _context.PratoDia.Update(p);
            _context.SaveChanges();

            return p.DataPrato.ToString();
        }

        public IActionResult AdicionarNovoPrato()
        {
            return View();
        }

        [HttpPost]
       public IActionResult AdicionarNovoPrato(IFormCollection dados)
        {
            Prato prato = _context.Pratos.FirstOrDefault(x => x.Nome == dados["Nome"].ToString());

            if(prato != null)
            {
                ModelState.AddModelError("Nome", "Já existe um prato com esse nome!");
            }

            if (ModelState.IsValid)
            {
                Prato p = new Prato();

                p.Nome = dados["Nome"];
                p.Tipo = dados["Tipo"];
                _context.Pratos.Add(p);
                _context.SaveChanges();

                return RedirectToAction("CriarPratoDia");
            }

            return View();
        }

    }
}
