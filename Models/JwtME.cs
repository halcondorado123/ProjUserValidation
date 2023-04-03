using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class JwtME
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }


        public static TokenME ValidateToken(ClaimsIdentity identity, UserInfoME user)
        {
            TokenME token = new TokenME();

            try
            {
                if (user == null)
                {
                    token.success = false;
                    token.message = "Usuario no encontrado";
                    token.result = "";
                }

                else if (identity.Claims.Count() == 0)
                {
                    token.success = false;
                    token.message = "Verificar si se está enviando un Token válido";
                    token.result = "";
                }
                else 
                { 
                    token.success = true;
                    token.message = "Verificado con éxito";
                    token.result = user.ToString();            
                }
            }
            catch (Exception ex)
            {
                token.success = false;
                token.message = "Error: " + ex.Message.ToString();
                token.result = "";
            }

            return token;
        }
    }
}
