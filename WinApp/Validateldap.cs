using System;
using System.DirectoryServices.Protocols;
using System.IO;
using System.Linq;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace WinApp
{
    public class ValidateLdap
    {
        private readonly ConfigurationBuilder _configuration;
        public ValidateLdap()
        {
            _configuration = new ConfigurationBuilder();
            _configuration.SetBasePath(Directory.GetCurrentDirectory());
            _configuration.AddJsonFile(path: "appsettings.json", false, true);
        }

        private const string Login = "LOGIN";
        private const string ErrorServer = "No se ha podido conectar con el servidor";
        private const string ErrorUser = "Usuario o Contraseña Inválido";
        

        public string Validate(string user, string pass)
        {

            try
            {
                IConfigurationRoot conf = _configuration.Build();
                var ldap =conf.GetSection("ldap").GetChildren().ToList();
                int.TryParse(ldap[0].Value, out int port);
                
                LdapConnection connection = new LdapConnection(new LdapDirectoryIdentifier(ldap[1].Value, port),null , AuthType.Basic);
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
