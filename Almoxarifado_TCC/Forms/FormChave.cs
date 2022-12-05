using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Almoxarifado_TCC.Forms
{
    public partial class Chave : Form
    {
        public Chave()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //instancia de conexão
            ClassConexao con = new ClassConexao();
            //obtive a conexao
            MySqlConnection conexao = con.getConexao();
            String consulta;
            if (txtPesquisar.Text == "") //Campo vazio lista tudo
            {
                consulta = "SELECT num_chave,sala_lab,stats,obs from tb_chave";
            }
            else //Se tiver informação lista
            {
                consulta = "SELECT num_chave,sala_lab,stats,obs from tb_chave where num_chave ='" + txtPesquisar.Text + "'";
            }
            //Monta meu comando sql
            MySqlCommand commando = new MySqlCommand(consulta, conexao);
            conexao.Open();//Abro minha conexao
            //monto a tabela de dados
            MySqlDataAdapter dados = new MySqlDataAdapter(commando);
            //Crio uma nova tabela de dados
            DataTable dtChave = new DataTable();

            dados.Fill(dtChave);//manipulação dos dados
            dtvChave.DataSource = dtChave;//chamo o caminho dos dados
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            //vai pra outro form
        }

        private void Chave_Load(object sender, EventArgs e)
        {
            ClassConexao con = new ClassConexao();
            //obtive a conexao
            MySqlConnection conexao = con.getConexao();
            String consulta;
            consulta = "SELECT num_chave,sala_lab,stats,obs from tb_chave";
            MySqlCommand commando = new MySqlCommand(consulta, conexao);
            conexao.Open();//Abro minha conexao
            MySqlDataAdapter dados = new MySqlDataAdapter(commando);
            //Crio uma nova tabela de dados
            DataTable dtChave = new DataTable();

            dados.Fill(dtChave);//manipulação dos dados
            dtvChave.DataSource = dtChave;//chamo o caminho dos dados
            conexao.Close();

            //////

            dtvChave.BorderStyle = BorderStyle.None;
            dtvChave.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dtvChave.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtvChave.DefaultCellStyle.SelectionBackColor = Color.FromArgb(1, 99, 148);
            dtvChave.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dtvChave.BackgroundColor = Color.White;

            dtvChave.EnableHeadersVisualStyles = false;
            dtvChave.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dtvChave.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 75, 112);
            dtvChave.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

        }
    }
}
