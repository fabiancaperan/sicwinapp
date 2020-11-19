using System;
using System.DirectoryServices.Protocols;
using System.Net;

namespace WinApp
{
    public class ValidateLdap
    {
        private const string Login = "LOGIN";
        private const string ErrorServer = "No se ha podido conectar con el servidor";
        private const string ErrorUser = "Usuario o Contraseña Inválido";
        private const string UrlServer = "192.168.1.49";
        private const int PortServer = 389;

        public string Validate(string user, string pass)
        {
            try
            {
                LdapConnection connection = new LdapConnection(new LdapDirectoryIdentifier(UrlServer, PortServer),null , AuthType.Basic);
                NetworkCredential credential = new NetworkCredential(user, pass);
                connection.Credential = credential;
                connection.Bind();
                return Login;                
            }
            catch (LdapException ex)
            {
                Console.WriteLine(ex.Message);
                return ex.Message;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                return exc.Message;
            }
        }

    }
}
