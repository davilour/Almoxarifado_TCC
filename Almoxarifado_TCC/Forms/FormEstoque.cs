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
    public partial class Estoque : Form
    {
        public Estoque()
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
                consulta = "SELECT * from tb_item";
            }
            else //Se tiver informação lista
            {
                consulta = "SELECT * from tb_item where nome_item ='" + txtPesquisar.Text + "'";
            }
            //Monta meu comando sql
            MySqlCommand commando = new MySqlCommand(consulta, conexao);
            conexao.Open();//Abro minha conexao
            //monto a tabela de dados
            MySqlDataAdapter dados = new MySqlDataAdapter(commando);
            //Crio uma nova tabela de dados
            DataTable dtEstoque = new DataTable();

            dados.Fill(dtEstoque);//manipulação dos dados
            dtvEstoque.DataSource = dtEstoque;//chamo o caminho dos dados
            conexao.Close();
        }

        private void dtvEstoque_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Estoque_Load(object sender, EventArgs e)
        {
            ClassConexao con = new ClassConexao();
            //obtive a conexao
            MySqlConnection conexao = con.getConexao();
            String consulta;
            consulta = "SELECT * from tb_item";
            MySqlCommand commando = new MySqlCommand(consulta, conexao);
            conexao.Open();//Abro minha conexao
            MySqlDataAdapter dados = new MySqlDataAdapter(commando);
            //Crio uma nova tabela de dados
            DataTable dtEstoque = new DataTable();

            dados.Fill(dtEstoque);//manipulação dos dados
            dtvEstoque.DataSource = dtEstoque;//chamo o caminho dos dados
            conexao.Close();

            /////

            dtvEstoque.BorderStyle = BorderStyle.None;
            dtvEstoque.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dtvEstoque.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtvEstoque.DefaultCellStyle.SelectionBackColor = Color.FromArgb(1, 99, 148);
            dtvEstoque.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dtvEstoque.BackgroundColor = Color.White;

            dtvEstoque.EnableHeadersVisualStyles = false;
            dtvEstoque.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dtvEstoque.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 75, 112);
            dtvEstoque.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }
    }
}
