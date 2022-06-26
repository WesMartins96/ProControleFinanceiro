using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.API
{

    //Configuração para o uso de JWT
    public static class Settings
    {
        // será com base nessa chave q o token do usuario sera gerado
        public static string ChaveSecreta = Guid.NewGuid().ToString();
    }
}