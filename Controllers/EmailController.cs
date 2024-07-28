using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Email.Data;
using Email.Services;
using Microsoft.AspNetCore.Mvc;

namespace Email.Controllers
{
    public class EmailController  : Controller {
        public readonly EmailContext _context;
        private readonly EmailRepository _emailrepository;


        public EmailController(EmailContext context, EmailRepository emailRepository){
        _context = context;
        _emailrepository = emailRepository;
        }

        public IActionResult Index(){
        return View();
        }

        public IActionResult Enviar(int id) {
        var user = _context.Users.FirstOrDefault(e => e.Id == id);
        if(user != null){

                var subject = "¡Recuperacion de contraseña! | CV Central";
                var mensajeUser = $"Hola, {user.Nombre}\nEsta es tu contraseña: {user.Apellido}.";
                _emailrepository.SendEmail( user.Email, subject, mensajeUser, user);

                ViewData["Mensaje"] = "Tu contraseña ha sido enviada al correo";
        }
        return RedirectToAction("Index");
        }
    }
}