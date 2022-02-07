using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exemplo_CRUD
{
    public class Cliente
    {
        public int Id { get; set; }
        public int Cpf { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Pizza { get; set; }
        public string Tamanho { get; set; }
        public string Telefone { get; set; }
     

        public string[] read()
        {
            string[] row = new string[9];
            row[0] = Id.ToString();
            row[1] = Cpf.ToString();
            row[2] = Nome;
            row[3] = SobreNome;
            row[4] = Cidade;
            row[5] = Bairro;
            row[6] = Pizza;
            row[7] = Tamanho;
            row[8] = Telefone;
           



            return row;
        }
        public string add()
        {
            string row = Id.ToString() + "/";
            row += Cpf.ToString() + "/";
            row += Nome + "/";
            row += SobreNome + "/";
            row += Cidade + "/";
            row += Bairro+ "/";
            row += Pizza + "/";
            row += Tamanho + "/";
            row += Telefone;



            return row;
        }
        public string edit()
        {
            string row = Id.ToString() + "/";
            row += Cpf.ToString() + "/";
            row += Nome + "/";
            row += SobreNome + "/";
            row += Cidade + "/";
            row += Bairro + "/";
            row += Pizza + "/";
            row += Tamanho + "/";
            row += Telefone + "/";



            return row;
        }
        public Boolean remove(int id)
        {
            if (id == Id)
            {
                return true;
            }
            return false;
        }
    }
}
