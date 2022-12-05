using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading; // Usado para fechar e abrir uma pagina
using MySql.Data.MySqlClient;


namespace Almoxarifado_TCC
{
    public partial class Login : Form
    {

        Thread nt; // Usado para fechar o programa
        public int codigoid;
        public Login()
        {
            InitializeComponent();

            // Cores do progama
            panelLogo.BackColor = RGBColors.color1;
            panelLine1.BackColor = Color.DimGray;
            panelLine2.BackColor = Color.DimGray;
            iconAviso.ForeColor = RGBColors.color1;
        }

        // Código que permite ao usuario mover o programa pela tela
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReliaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        //--------------------------------------------------------------------------------------------
        
        //Structs que define as cores do programa
        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(0, 75, 112);// Azul 0, 122, 204
            public static Color color2 = Color.FromArgb(15, 15, 15);// Preto
        }


        //Buttons
        private void txtUsuario_Enter(object sender, EventArgs e)// Opção que define quando o usuario seleciona a caixa de texto [CPF]
        {
            if(txtUsuario.Text == "CPF"){
                txtUsuario.Text = "";
                txtUsuario.ForeColor = RGBColors.color2;
            }
        }

        private void txtUsuario_Leave(object sender, EventArgs e)// Opção que define quando o usuario sai da caixa de texto [CPF]
        {
            if (txtUsuario.Text == "")
            {
                txtUsuario.Text = "CPF";
                txtUsuario.ForeColor = RGBColors.color2;
            }
        }

        private void txtSenha_Enter(object sender, EventArgs e)// Opção que define quando o usuario seleciona a caixa de texto [SENHA]
        {
            if (txtSenha.Text == "SENHA")
            {
                txtSenha.Text = "";
                txtSenha.ForeColor = RGBColors.color2;
                txtSenha.UseSystemPasswordChar = true;
            }
        }

        private void txtSenha_Leave(object sender, EventArgs e)// Opção que define quando o usuario sai da caixa de texto [SENHA]
        {
            if (txtSenha.Text == "")
            {
                txtSenha.Text = "SENHA";
                txtSenha.ForeColor = RGBColors.color2;
                txtSenha.UseSystemPasswordChar = false;
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)// Botão que minimiza o programa
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)// Botão que fecha o programa
        {
            Application.Exit();
        }

        private void Login_MouseDown(object sender, MouseEventArgs e)// Opção que permite ao usuario movimentar a caixa de login pela tela
        {
            ReliaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnLogin_Click(object sender, EventArgs e)// Códigos funcionais do botão de Avançar
        {
            verificacao();// Void que verifica se as informações inseridas no login estão corretas ou não
        }

        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)// Permite que o usuario passe de CPF para SENHA ao clicar ENTER
        {
            if (e.KeyCode == Keys.Enter && txtUsuario.Text != "CPF" && txtUsuario.Text != "")
            {
                txtSenha.Focus();
            }
        }

        private void txtSenha_KeyDown(object sender, KeyEventArgs e)// Permite que o usuario passe de SENHA para AVANÇAR ao clicar ENTER
        {
            if (e.KeyCode == Keys.Enter && txtSenha.Text != "CPF" && txtSenha.Text != "")
            {
                verificacao();
            }
        }

        public void LerId()
        {
            ClassUsuario usu = new ClassUsuario();//chamo classe usuario
            ClassConexao con = new ClassConexao();//chamo a classe conexao

            MySqlConnection conexao2 = con.getConexao();
            conexao2.Open();

            string consulta_id = "select id_admin from tb_admin where cpf=@cpf";

            MySqlCommand comando2 = new MySqlCommand(consulta_id, conexao2);
            comando2.Parameters.AddWithValue("@cpf", txtUsuario.Text);
            MySqlDataReader registro2 = comando2.ExecuteReader();//executa a consulta
            registro2.Read();
            this.codigoid = Convert.ToInt32(registro2["id_admin"]);

            //MessageBox.Show("id admin: " + this.codigoid);
            conexao2.Close();
        }

        private void verificacao() // Verifica se as informações inseridas no login estão corretas ou não
        {
            ClassUsuario usu = new ClassUsuario();//chamo classe usuario
            ClassConexao con = new ClassConexao();//chamo a classe conexao
            String logar = "SELECT * FROM tb_admin where cpf=@cpf AND senha=@senha";
            MySqlConnection conexao = con.getConexao();
            MySqlCommand comando = new MySqlCommand(logar, conexao);
            conexao.Open();
            comando.Parameters.AddWithValue("@cpf", txtUsuario.Text);
            comando.Parameters.AddWithValue("@senha", txtSenha.Text);

            MySqlDataReader registro = comando.ExecuteReader();//executa a consulta

            iconAviso.Visible = false;
            lblAviso.Visible = false;
            lblAviso.Text = "";

            if (registro.HasRows)
            {
                registro.Read();
                usu.login = Convert.ToString(registro["cpf"]);
                usu.senha = Convert.ToString(registro["senha"]);
                usu.logado = true;
                LerId();
                Application.Exit(); // Fecha o programa atual e abre um novo
                nt = new Thread(novoform);
                nt.SetApartmentState(ApartmentState.STA);
                nt.Start();
            }

            else if (txtUsuario.Text == "CPF" || txtSenha.Text == "SENHA")
            {
                iconAviso.Visible = true;
                lblAviso.Visible = true;

                if (txtUsuario.Text == "CPF")
                {
                    lblAviso.Text = "O campo CPF esta em branco";
                }
                if (txtSenha.Text == "SENHA")
                {
                    lblAviso.Text = "O campo SENHA esta em branco";
                }
            }

            else
            {
                iconAviso.Visible = true;
                lblAviso.Visible = true;
                lblAviso.Text = "Usuario ou senha invalidos";
            }

        }

        private void novoform()// define o programa que vai ser aberto
        {
            Application.Run(new TelaPrincipal(this.codigoid));
        }
    }
}
