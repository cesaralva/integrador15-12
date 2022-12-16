using System.Net.Mail;
using System.Net;
using WebApiCondominio.Models;

namespace WebApiCondominio.Utils
{
    public class Correo
    {
        string fromRemitente = "maruiz1093@gmail.com";
        string fromPassword = "obsfsmmhlqbeaegq";
        public string EnviarCorreo(Usuario obj)
        {
            string msg = "";
            try
            {
                var fromAddress = new MailAddress(fromRemitente, "Restablecer Contraseña");
                var toAddress = new MailAddress(obj.Email, "Restablecer Contraseña");

                const string subject = "Restablecimiento de contraseña";

                string body = "Hola "+obj.Nombres+" "+obj.Apellidos+
                    "\nSu codigo de verificación para la actualización de su contraseña es el siguiente" +
                    ".\nCodigo Verificacion: "+obj.CodigoVerificacion+
                    "\n\nRecuerde que este codigo solo es válido por los proximos 10 minutos al momento de haber generado la recuperación para la nueva contraseña.";


                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                MailMessage mMailMessage = new MailMessage(fromAddress, toAddress);

                mMailMessage.IsBodyHtml = true;
                mMailMessage.Subject = subject;
                mMailMessage.Body = body;

                smtp.Send(mMailMessage);

                msg = "OK";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return msg;
        }
    }
}
