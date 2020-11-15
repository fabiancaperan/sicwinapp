using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace WinApp
{
    public class Validateldap
    {

        public void validate()
        {
            try
            {
                //Dominio =rbm
                //IPLDAP 192.168.1.49
                //puertoLdap = 389

                LdapConnection connection = new LdapConnection(new LdapDirectoryIdentifier("192.168.1.49",389),null , AuthType.Basic);
                //LdapConnection connection = new LdapConnection("ingetec.loc");
                NetworkCredential credential = new NetworkCredential("sccmadmin", "1nfradddg");
                connection.Credential = credential;
                connection.Bind();
                Console.WriteLine("logged in");
            }
            catch (LdapException lexc)
            {
                String error = lexc.ServerErrorMessage;
                Console.WriteLine(lexc);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
        }

    }
}
