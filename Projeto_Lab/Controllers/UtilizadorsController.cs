using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Projeto_Lab.Data;
using Projeto_Lab.Models;

namespace Projeto_Lab.Controllers
{
    public class UtilizadorsController : Controller
    {
        public TipoServico Servico { get; set; }
        private readonly LabContext _context;
        private readonly IHostEnvironment _he;
        //private readonly IEmailSender _emailSender;

        public string _pass { get; set; }
        public UtilizadorsController(LabContext context, IHostEnvironment he)// IEmailSender emailsender)
        {
            _context = context;
            _he = he;
            //_emailSender = emailsender;
        }

        // GET: Utilizadors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Utilizadors.ToListAsync());
        }

        public IActionResult RegistarCliente()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistarCliente(IFormCollection dados)
        {
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

                Encriptar(dados["Pass"].ToString());
                u.Username = dados["Username"];
                u.Email = dados["Email"];
                u.Pass = _pass;
                u.Estado = 1;

                _context.Utilizadors.Add(u);
                _context.SaveChanges();


                Utilizador utl = _context.Utilizadors.FirstOrDefault(x => x.Username == dados["Username"].ToString());


                Cliente c = new Cliente();

                c.Nome = dados["Nome"];
                c.IdC = utl.Id;
                _context.Clientes.Add(c);
                _context.SaveChanges();

                //var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(utl.Email));
                //var callbackUrl = Url.Action("ConfirmarEmail", "Utilizadors", new { userId = utl.Id, code = code }, Request.Scheme);

                // _emailSender.SendEmailAsync(utl.Email, "Confirmar Email",
                //       $"Confirme o seu email clicando <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>AQUI</a>. A sua Password é <b>" + utl.Pass + "</b>");

                //return View("RegistoConcluido");

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult ConfirmarEmail(int userId, string code)
        {
            var resultado = "Ocorreu um erro ao confirmar o seu email.";
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            if (code == _context.Utilizadors.FirstOrDefault(x => x.Id == userId).Email)
            {
                var ut = _context.Utilizadors.FirstOrDefault(x => x.Id == userId);
                ut.Estado = 1;
                _context.Utilizadors.Update(ut);
                _context.SaveChanges();
                resultado = $"Email confirmado. Clique <a href = \"/Utilizadors/Login\">Aqui</a> para efetuar o login";
            }

            ViewBag.Result = resultado;
            return View();
        }


        public IActionResult RegistarRestaurante()
        {

            //ViewBag.TS = new SelectList(_context.TipoServicos, "Nome");

            //ViewBag.Ddesc = new List<string>()
            //{
            //    "Segunda", "Terça", "Quarta", "Quinta", "Sexta", "Sábado", "Domingo"
            //};

            //ViewBag.TS = new List<string>()
            //{
            //    "Take-away", "Restaurante", "Ambos"
            //};
            ViewData["Ddesc"] = new SelectList(new List<string>() { "Segunda", "Terça", "Quarta", "Quinta", "Sexta", "Sábado", "Domingo" });
            ViewData["TS"] = new SelectList(_context.TipoServicos.Select(x=>x.Nome));

            return View();
        }

        [HttpPost]
        public IActionResult RegistarRestaurante(IFormCollection dados, IFormFile foto)
        {
            if (foto != null)
            {
                if (Path.GetExtension(foto.FileName) != ".jpg")
                {
                    ModelState.AddModelError("Foto", "Tem de ser JPG.");
                }
            }

            Utilizador ut = _context.Utilizadors.FirstOrDefault(x => x.Username == dados["Username"].ToString() || x.Email == dados["Email"].ToString());
            

            if (ut != null)
            {
                if (ut.Username == dados["Username"].ToString())
                {
                    ModelState.AddModelError("IdRNavigation.Username", "Utilizador já se encontra registado. Utilize outro username.");
                }
                if (ut.Email == dados["Email"].ToString())
                {
                    ModelState.AddModelError("IdRNavigation.Email", "Este email já se encontra registado. Utilize outro email.");
                }
            }

            if (ModelState.IsValid)
            {

                Utilizador u = new Utilizador();

                Encriptar(dados["Pass"].ToString());
                u.Username = dados["Username"];
                u.Email = dados["Email"];
                u.Pass = _pass;
                u.Estado = 0;

                _context.Utilizadors.Add(u);
      
                _context.SaveChanges();


                Utilizador utl = _context.Utilizadors.FirstOrDefault(x => x.Username == dados["Username"].ToString());

                string caminho = Path.Combine(_he.ContentRootPath, "wwwroot/Imagens/Registos/Restaurantes");
                string caminhoTotal = Path.Combine(caminho, Path.GetFileName(foto.FileName));
                FileStream fs = new FileStream(caminhoTotal, FileMode.Create);
                foto.CopyTo(fs);
                fs.Close();

                Restaurante r = new Restaurante();

                //    ViewBag.Ddesc = new List<string>()
                //{
                //    "Segunda", "Terça", "Quarta", "Quinta", "Sexta", "Sábado", "Domingo"
                //};

                //    ViewBag.TS = new List<string>()
                //{
                //    "Take-away", "Restaurante", "Ambos"
                //};
                ViewData["Ddesc"] = new SelectList(new List<string>() { "Segunda", "Terça", "Quarta", "Quinta", "Sexta", "Sábado", "Domingo" });
                ViewData["TS"] = new SelectList(_context.TipoServicos.Select(x => x.Nome));

                r.IdR = utl.Id;
                r.Nome = dados["Nome"];
                r.Telefone = Convert.ToInt32(dados["Telefone"]);
                r.Localizacao = dados["Localizacao"];
                r.DiaDesc = dados["DiaDesc"];
                r.TipoServico = dados["TipoServico"];
                r.Foto = Path.GetFileName(foto.FileName);
                r.HoraAbertura = TimeSpan.Parse(dados["HoraAbertura"]);
                r.HoraFecho = TimeSpan.Parse(dados["HoraFecho"]);

                _context.Restaurantes.Add(r);           
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            return View();
        }


        public IActionResult RegistarAdmin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistarAdmin(IFormCollection dados)
        {
           // Utilizador ut = _context.Utilizadors.FirstOrDefault(x => x.Username == dados["Username"].ToString() || x.Email == dados["Email"].ToString());


            //if (ut != null)
            //{
            //    if (ut.Username == dados["Username"].ToString())
            //    {
            //        ModelState.AddModelError("Username", "Utilizador já se encontra registado. Utilize outro username.");
            //    }
            //    if (ut.Email == dados["Email"].ToString())
            //    {
            //        ModelState.AddModelError("Email", "Este email já se encontra registado. Utilize outro email.");
            //    }
            //}

            if (!_context.Administradors.Any())
            {
                if (ModelState.IsValid)
                {
                    Encriptar(dados["Pass"].ToString());
                    Utilizador u = new Utilizador();
                    u.Username = dados["Username"];
                    u.Email = dados["Email"];
                    u.Pass = _pass;
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
            }
            else
            {
                return View("SemPermissoesAdmin");
            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(IFormCollection dados)
        {
            Utilizador ut = _context.Utilizadors.Include(x => x.Administrador).Include(x => x.Cliente).Include(x => x.Restaurante).
              FirstOrDefault(x => x.Username == dados["Username"].ToString());
            if (ut != null)
            {

                //Desincriptar(ut.Pass);
                //if (dados["Pass"].ToString() != _pass)
                //{
                //    ModelState.AddModelError("Pass", "Password errada!");
                //}

                if (ut.Estado == 0)
                {
                    ModelState.AddModelError("Username", "Este utilizador ainda não confirmou o e-mail ou não foi aceite por um administrador.");
                }
                else
                {
                    if (ut.Estado == 2)
                    {
                        ModelState.AddModelError("Username", "Este utilizador foi suspenso.");
                        Suspenso s = _context.Suspensos.OrderByDescending(x => x.DataBloqueio).FirstOrDefault(x => x.IdU == ut.Id);
                        ViewBag.Motivo = s.Motivo;
                        ViewBag.Data = s.DataBloqueio;
                    }
                }
            }
            else
            {
                ModelState.AddModelError("Username", "Este utilizador não está registado");
            }

            if (ModelState.IsValid)
            {
                HttpContext.Session.SetString("Nome", ut.Username);
                HttpContext.Session.SetInt32("Id", ut.Id);
                if (ut.Administrador != null)
                {
                    HttpContext.Session.SetString("Estatuto", "Administrador");
                    return RedirectToAction("Index", "Home");
                }
                else if (ut.Cliente != null)
                {
                    HttpContext.Session.SetString("Estatuto", "Cliente");
                    HttpContext.Session.SetInt32("UserId", ut.Cliente.IdC);
                    return RedirectToAction("Index", "Home");
                }
                else if (ut.Restaurante != null)
                {
                    HttpContext.Session.SetString("Estatuto", "Restaurante");
                    HttpContext.Session.SetInt32("UserId", ut.Restaurante.IdR);
                    return RedirectToAction("Index", "Home");
                }
                //return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Logout()
        {
            ;
            HttpContext.Response.Cookies.Delete("CookieDeSessao");
            return RedirectToAction("Index", "Home");
        }

        public static bool Administrador(HttpContext context)
        {
            if (context.Session.GetString("Estatuto") == "Administrador")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool Cliente(HttpContext context)
        {
            if (context.Session.GetString("Estatuto") == "Cliente")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool Restaurante(HttpContext context)
        {
            if (context.Session.GetString("Estatuto") == "Restaurante")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool Autenticado(HttpContext context)
        {
            if (context.Session.GetInt32("UserId") != null || context.Session.GetInt32("Id") != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Encriptar(string pass)
        {
            Byte[] encriptar = System.Text.Encoding.Unicode.GetBytes(pass);
            _pass = Convert.ToBase64String(encriptar);
            //_pass.ToHashSet();
        }

        public void Desincriptar(string pass)
        {
            Byte[] desincriptar = Convert.FromBase64String(pass);
            _pass = System.Text.Encoding.Unicode.GetString(desincriptar);
        }

        //public void BuildEmailTemplate(int id)
        //{
        //    string caminho = Path.Combine(_he.ContentRootPath, "Views/Utilizadores/Text.cshtml");
        //    string mensagem = System.IO.File.ReadAllText(caminho);

        //    var regInfo = _context.Utilizadors.Where(x => x.Id == id).FirstOrDefault();

        //    var url = "https://localhost:44329/" + "Utilizadores/Confirm?ID=" + id;

        //    mensagem = mensagem.Replace("@ViewBag.LinkConfirmacao", url);
        //    mensagem = mensagem.ToString();
        //    BuildEmailTemplate("A tua conta foi criada com sucesso", mensagem, regInfo.Email);
        //}

        //public static void BuildEmailTemplate(string v, string mensagem, string email)
        //{
        //    string from, to, bcc, cc, subject, body;
        //    from = "jmfernandes8@gmail.com";
        //    to = email.Trim();
        //    bcc = "";
        //    cc = "";
        //    subject = v;
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append(mensagem);
        //    body = sb.ToString();
        //    MailMessage mail = new MailMessage();

        //    mail.From = new MailAddress(from);
        //    mail.To.Add(new MailAddress(to));

        //    if (!string.IsNullOrEmpty(bcc))
        //    {
        //        mail.Bcc.Add(new MailAddress(bcc));
        //    }
        //    if (!string.IsNullOrEmpty(cc))
        //    {
        //        mail.CC.Add(new MailAddress(cc));
        //    }

        //    mail.Subject = subject;
        //    mail.Body = body;
        //    mail.IsBodyHtml = true;
        //    SendEmail(mail);

        //}

        //public static void SendEmail(MailMessage mail)
        //{
        //    SmtpClient client = new SmtpClient();
        //    client.Host = "smtp.gmail.com";
        //    client.Port = 587;
        //    client.EnableSsl = true;
        //    client.UseDefaultCredentials = false;
        //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    client.Credentials = new System.Net.NetworkCredential("jmfernandes8@gmail.com", "");

        //    try
        //    {
        //        client.Send(mail);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
    }
}
    
 


