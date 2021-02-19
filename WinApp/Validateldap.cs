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
        
        public string Validate(string user, string pass)
        {

            try
            {
                IConfigurationRoot conf = _configuration.Build();
                var ldap = conf.GetSection("ldap").GetChildren().ToList();
                int.TryParse(ldap.FirstOrDefault(s => s.Key.Equals("PortServer"))?.Value, out int port);
                string domain = ldap.FirstOrDefault(s => s.Key.Equals("Domain"))?.Value;

                LdapConnection connection = new LdapConnection(new LdapDirectoryIdentifier(ldap.FirstOrDefault(s => s.Key.Equals("UrlServer"))?.Value, port), null, AuthType.Basic);
                NetworkCredential credential = new NetworkCredential(user + domain, pass);
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
