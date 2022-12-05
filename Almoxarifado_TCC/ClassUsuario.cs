using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace Almoxarifado_TCC

{
    class ClassUsuario
    {
        public int codigo;
        public string nome;
        public string cpf;
        public string telefone;
        public string email;
        public string login;
        public string senha;
        public bool logado;
        public string perfil;

        public void usuario()
        {
            this.codigo = 0;
            this.nome = "";
            this.cpf = "";
            this.telefone = "";
            this.email = "";
            this.perfil = "";
        }

        public string getMD5hash(string senha) //criptografia da senha
        {
            System.Security.Cryptography.MD5 mds = System.Security.Cryptography.MD5.Create();
            byte[] imputBytes = System.Text.Encoding.ASCII.GetBytes(senha);
            byte[] hash = mds.ComputeHash(imputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }
       
        
    }
}