using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginTokenAPI.Models
{
    public class User
    {
        public int ID { get; set; }
        public int CodigoUsuario {get; set;}
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Dituacao { get; set; }
        public string Email { get; set; }
        public int TrocarSenha { get; set; }
        public DateTime DtExpirarSenha { get; set; }
        public int Administrador { get; set; }
        public int CodigoPerfilUsuario { get; set; }
        public int CodigoUnidadeNegocio { get; set; }
        public int CodigoAlternativo { get; set; }
        public string Tipo { get; set; }
    }
}
