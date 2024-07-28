using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using Email.Models;

namespace Email.Services
{
    public class EmailRepository
    {
        // Configuración del correo electrónico
        private readonly EmailModel _emailSettings;

        // Constructor que inicializa la configuración del correo electrónico
        public EmailRepository(IOptions<EmailModel> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        // Método para enviar un correo electrónico
        public void SendEmail(string Email, string subject, string body, User user)
        {
            // Imprime el correo electrónico en la consola (para depuración)
            Console.WriteLine(Email);

            // Crea un nuevo mensaje de correo electrónico
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail)); // Remitente
            emailMessage.To.Add(new MailboxAddress("", Email)); // Destinatario
            emailMessage.Subject = subject; // Asunto del correo
            emailMessage.Body = new TextPart("plain") { Text = $"Hola, {user.Nombre},\nEsta es tu contraseña {user.Apellido}." }; // Cuerpo del correo

            // Envío del correo electrónico utilizando un cliente SMTP
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect(_emailSettings.SmtpServer, _emailSettings.Port, false); // Conexión al servidor SMTP
                client.Authenticate(_emailSettings.Username, _emailSettings.Password); // Autenticación
                client.Send(emailMessage); // Envío del correo
                client.Disconnect(true); // Desconexión del servidor
            }
        }

        // Método para enviar un correo de confirmación de registro
        public void ConfirmRegistration(string Email, string subject, string body, User user)
        {
            // Crea un nuevo mensaje de correo electrónico
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail)); // Remitente
            emailMessage.To.Add(new MailboxAddress("", Email)); // Destinatario
            emailMessage.Subject = subject; // Asunto del correo
            emailMessage.Body = new TextPart("plain") { Text = $"Bienvenid@ a CVCentral {user.Nombre}\n Tu registro se completó correctamente, ya eres uno de nuestros usuarios." }; // Cuerpo del correo

            // Envío del correo electrónico utilizando un cliente SMTP
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect(_emailSettings.SmtpServer, _emailSettings.Port, false); // Conexión al servidor SMTP
                client.Authenticate(_emailSettings.Username, _emailSettings.Password); // Autenticación
                client.Send(emailMessage); // Envío del correo
                client.Disconnect(true); // Desconexión del servidor
            }
        }

    }
}