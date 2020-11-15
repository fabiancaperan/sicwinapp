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

        public string validate(string user, string pass)
        {
            try
            {
                //Dominio =rbm
                //IPLDAP 192.168.1.49
                //puertoLdap = 389
                var dominio = "rbm";

                LdapConnection connection = new LdapConnection(new LdapDirectoryIdentifier("192.168.1.49",389),null , AuthType.Basic);
                //LdapConnection connection = new LdapConnection("ingetec.loc");
                //NetworkCredential credential = new NetworkCredential("sccmadmin", "1nfradddg");
                NetworkCredential credential = new NetworkCredential(user, pass+"@"+dominio);
                connection.Credential = credential;
                connection.Bind();
                return "LOGIN";                
            }
            catch (LdapException lexc)
            {
                String error = lexc.ServerErrorMessage;
                return lexc.Message;
            }
            catch (Exception exc)
            {
                return exc.Message;
            }
        }

    }
}
