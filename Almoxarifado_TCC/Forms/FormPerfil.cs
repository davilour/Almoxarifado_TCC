using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading; // Usado para fechar e abrir uma pagina
using MySql.Data.MySqlClient;

namespace Almoxarifado_TCC.Forms
{
    public partial class Perfil : Form
    {
        Thread nt; // Usado para fechar o programa
        public int codigoid;
        public string nome;
        public string email;

        public Perfil(int codigoid)
        {
            this.codigoid = codigoid;
            InitializeComponent();
        }

        private void btnDesconectar_Click(object sender, EventArgs e)// Fecha o programa
        {
            Application.Exit();
            nt = new Thread(novoform);
            nt.SetApartmentState(ApartmentState.STA);
            nt.Start();
        }

        private void novoform()// define o programa que vai ser aberto
        {
            Application.Run(new Login());
        }

        private void Perfil_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("id admin: " + this.codigoid);
            ClassUsuario usu = new ClassUsuario();//chamo classe usuario
            ClassConexao con = new ClassConexao();//chamo a classe conexao
            String logar = "SELECT nome_admin,email FROM tb_admin where id_admin=@id";
            MySqlConnection conexao = con.getConexao();
            MySqlCommand comando = new MySqlCommand(logar, conexao);
            conexao.Open();
            comando.Parameters.AddWithValue("@id", this.codigoid);

            MySqlDataReader registro = comando.ExecuteReader();//executa a consulta
            registro.Read();
            nome = Convert.ToString(registro["nome_admin"]);
            email = Convert.ToString(registro["email"]);


            lblNome.Text = nome;
            lblEmail.Text = email;
        }
    }
}
