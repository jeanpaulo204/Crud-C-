using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace exemplo_CRUD
{
    public partial class FormCliente : Form // aqui temos uma classe o que é "classe"  descrevem o conteúdo e o comportamento de entidades.//

    {
        DataTable dataTable;
        Cliente Cliente;
        String fileName = "../../cliente.txt"; //Aqui é Temos Uma Variável,  ou seja contém um endereço de objeto (instância) Aonde Os Dados Do Cliente São Armazenados //
        public FormCliente()
        {
            InitializeComponent();
        }
        private void FormCliente_Shown(object sender, EventArgs e)
        {
            textBoxId.Text = getPrimaryKey().ToString(); //Aqui temos o ID Do Cliente, Pois Facilita ele Ser Localizado
            textBoxCpf.Focus();  //Aqui temos o Cpf Do Cliente
        }
        private void FormCliente_Load(object sender, EventArgs e)
        {
            load();
        }

        private void buttonNovo_Click(object sender, EventArgs e) //aqui é parte dos Botões
        {
            reset();   
        }

        private void buttonSalvar_Click(object sender, EventArgs e) 

        {
            if (labelOperacao.Text == "Adição")
            {
                if (validate())
                {   //No Botão salvar Botamos os Seguinte, Eu Quero Que Class Cliente Salve o Novo Cliente()
                    //Então Botamos Todos os ID das Box do Designer , Para Ter O Salvamento Da Nossa Crud
                    //Exemplo Salve New Cliente() > Cliente.Nome > Escrevendo No Box ..Junior.. > TextBoxNome.Text>Salvando:
                    Cliente = new Cliente();
                    Cliente.Id = Convert.ToInt32(textBoxId.Text);
                    Cliente.Cpf = Convert.ToInt32(textBoxCpf.Text);
                    Cliente.Nome = textBoxNome.Text;
                    Cliente.SobreNome = textBoxSobreNome.Text;
                    Cliente.Cidade = textBoxCidade.Text;
                    Cliente.Bairro = textBoxBairro.Text;
                    Cliente.Pizza = textBoxPizza.Text;
                    Cliente.Tamanho = textBoxTamanho.Text;
                    Cliente.Telefone = textBoxTelefone.Text;
                    File.AppendAllText(fileName, Cliente.add());
                    dataTable.Rows.Add(Cliente.read());
                    MessageBox.Show("Cliente adicionado!");
                    reset();
                }
            }
            else
            {
                if (validate())
                {
                    string[] lines = File.ReadAllLines(fileName); //FileName é Aonde esta Local De Armazenamento Dos Nossos Dados
                    for (int i = 0; i < lines.Length; i++) 
                    {
                        string[] values = lines[i].ToString().Split('/');
                        int id = Convert.ToInt32(values[0]);  //Aqui temos Nosso Id Para Fazer As Busca Para Encontrar Nosso Cliente Mais Facil
                        if (id == Convert.ToInt32(textBoxId.Text))
                        {
                            Cliente = new Cliente(); //Com A Busca Do ID podemos Editar Os Seguintes class Abaixo Cliente.Nome , Cliente.Sobrenome Etc..
                            Cliente.Id = Convert.ToInt32(textBoxId.Text);
                            Cliente.Cpf = Convert.ToInt32(textBoxCpf.Text);
                            Cliente.Nome = textBoxNome.Text;
                            Cliente.SobreNome = textBoxSobreNome.Text;
                            Cliente.Cidade = textBoxCidade.Text;
                            Cliente.Bairro = textBoxBairro.Text;
                            Cliente.Pizza= textBoxPizza.Text;
                            Cliente.Tamanho = textBoxTamanho.Text;
                            Cliente.Telefone = textBoxTelefone.Text;
                            lines[i] = Cliente.edit();
                            break;
                        }
                    }
                    File.WriteAllLines(fileName, lines); //Ele Edita Do Nosso Arquivo Cliente.Txt Lá Em Cima
                    MessageBox.Show("Cliente editado!"); 
                    reset();
                    load();
                }
            }
        }

        private void buttonExcluir_Click(object sender, EventArgs e) //Aqui é Botão Para Excluir Nosso Cliente
        {
            if (labelOperacao.Text == "Edição")
            {          //Aqui Botamos Uma Mensagem  "Deseja excluir esse registro? Sim Ou Não? MessageBoxIcon.Question Questionamento Da Maquina hahaha
                if (MessageBox.Show("Deseja excluir esse registro?", "Cliente", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var file = new List<string>(File.ReadAllLines(fileName));
                    string[] lines = File.ReadAllLines(fileName);
                    for (int i = 0; i < lines.Length; i++)
                    {
                        string[] values = lines[i].ToString().Split('/');
                        Cliente = new Cliente();
                        Cliente.Id = Convert.ToInt32(textBoxId.Text);
                        if (Cliente.remove(Convert.ToInt32(values[0])))
                        {
                            file.RemoveAt(i);
                            break;
                        }
                    }
                    MessageBox.Show("Cliente excluído!");
                    File.WriteAllLines(fileName, file.ToArray());
                    reset();
                    load();
                }
            }
        }

        private void FormFuncionario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBoxId.Text = dataGridViewCliente.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBoxCpf.Text = dataGridViewCliente.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBoxNome.Text = dataGridViewCliente.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBoxSobreNome.Text = dataGridViewCliente.Rows[e.RowIndex].Cells[3].Value.ToString();
                textBoxCidade.Text = dataGridViewCliente.Rows[e.RowIndex].Cells[4].Value.ToString();
                textBoxBairro.Text = dataGridViewCliente.Rows[e.RowIndex].Cells[5].Value.ToString();
                textBoxPizza.Text = dataGridViewCliente.Rows[e.RowIndex].Cells[6].Value.ToString();
                textBoxTamanho.Text = dataGridViewCliente.Rows[e.RowIndex].Cells[7].Value.ToString();
                textBoxTelefone.Text = dataGridViewCliente.Rows[e.RowIndex].Cells[8].Value.ToString();
                labelOperacao.Text = "Edição";
                buttonExcluir.Enabled = true;
            }
            catch { }
        }

        private void load()
        {
            dataTable = new DataTable(); // Aqui Fica Gravamento Visual No Nosso Crud ou Tabela >  Aonde as Informações vão aparecer nos seus lugares:
            dataGridViewCliente.DataSource = null;
            dataGridViewCliente.Rows.Clear();
            dataGridViewCliente.Refresh();

            dataTable.Columns.Add("ID", typeof(int));
            dataTable.Columns.Add("CPF", typeof(int));
            dataTable.Columns.Add("Nome", typeof(string));
            dataTable.Columns.Add("SobreNome", typeof(string));
            dataTable.Columns.Add("Cidade", typeof(string));
            dataTable.Columns.Add("Bairro", typeof(string));
            dataTable.Columns.Add("Pizza", typeof(string));
            dataTable.Columns.Add("Tamanho", typeof(string));
            dataTable.Columns.Add("Telefone", typeof(string));

            dataGridViewCliente.DataSource = dataTable;

            string[] lines = File.ReadAllLines(fileName);
            string[] values;

            foreach (string line in lines)  //Aqui é Marcação do Codigo : 0 é Sempre o Primeiro Da tabela ai Sucessivamente
            {
                values = line.ToString().Split('/');
                Cliente = new Cliente();
                Cliente.Id = Convert.ToInt32(values[0]);
                Cliente.Cpf = Convert.ToInt32(values[1]);
                Cliente.Nome = values[2];
                Cliente.SobreNome = values[3];
                Cliente.Cidade = values[4];
                Cliente.Bairro = values[5];
                Cliente.Pizza = values[6];
                Cliente.Tamanho= values[7];
                Cliente.Telefone = values[8];
                dataTable.Rows.Add(Cliente.read());
            }
        }
        private int getPrimaryKey()
        {
            int id = 0;
            string[] lines = File.ReadAllLines(fileName);
            foreach (string line in lines)
            {
                string[] values = line.ToString().Split('/');
                id = Convert.ToInt32(values[0]);
            }
            return id + 1;
        }

        private void reset () // Botão De Resetar Deixamos o Texto Nulo pois objetivo é Ele Aparecer Em Branco.
        {
            textBoxId.Text = getPrimaryKey().ToString();
            textBoxCpf.Text = "";
            textBoxNome.Text = "";
            textBoxSobreNome.Text = "";
            textBoxCidade.Text = "";
            textBoxBairro.Text = "";
            textBoxPizza.Text = "";
            textBoxTamanho.Text = "";
            textBoxTelefone.Text = "";
            labelOperacao.Text = "Adição";
            buttonExcluir.Enabled = false;
            textBoxCpf.Focus();
        }

        private Boolean validate() //Aqui Temos um BooLean para os Mais Conhecidos o booleano
                                   // A Função Do Boleano é Simples é um tipo de dado primitivo que possui dois valores, que podem ser considerados como 0 ou 1 "verdadeiro ou falso "
        {
            if (textBoxCpf.Text == "")
            {
                MessageBox.Show("Informe o Cpf Do Cliente");
                textBoxCpf.Focus();
                return false;
            }
            else if (textBoxNome.Text == "")
            {
                MessageBox.Show("Informe o nome do Cliente");
                textBoxNome.Focus();
                return false;
            }

            else if (textBoxSobreNome.Text == "")
            {
                MessageBox.Show("Informe o SobreNome Do Cliente");
                textBoxSobreNome.Focus();
                return false;
            }

            else if (textBoxCidade.Text == "")
            {
                MessageBox.Show("Informe a cidade do Cliente");
                textBoxCidade.Focus();
                return false;
            }



            else if (textBoxBairro.Text == "")
            {
                MessageBox.Show("Informe o Bairro do Cliente");
                textBoxBairro.Focus();
                return false;
            }


            else if (textBoxPizza.Text == "")
            {
                MessageBox.Show("Informe a Pizza do Cliente");
                textBoxPizza.Focus();
                return false;
            }


            else if (textBoxTamanho.Text == "")
            {
                MessageBox.Show("Informe o Tamanho da Pizza Do Cliente");
                textBoxTamanho.Focus();
                return false;
            }


            else if (textBoxTelefone.Text == "")
            {
                MessageBox.Show("Informe o Telefone Do Cliente");
                textBoxTelefone.Focus();
                return false;
            }






            else
            {
                return true;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBoxPizza_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void labelOperacao_Click(object sender, EventArgs e)
        {

        }

        private void textBoxSobreNome_TextChanged(object sender, EventArgs e)
        {

        }

    

        private void textBoxTelefone_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBoxTamanho_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridViewCliente_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewPizza add = new NewPizza();
            add.ShowDialog();
        }

        private void textBoxcpf_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
