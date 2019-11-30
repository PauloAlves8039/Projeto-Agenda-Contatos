/*
 * Arquivo: classe Contato
 * Autor: Paulo Alves
 * Descrição: responsável por conter propriedades da entidade Contato, um construtor personalizado
 * e a sobrescrita do método ToString para a exibição dos registros no ListBox lbxContatos
 * Data: 30/11/2019
*/

namespace AgendaContatos.Entities
{
    public class Contato
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        public Contato(string nome = "", string email = "", string telefone = "")
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
        }

        public override string ToString()
        {
            return string.Format("{0} = {1}, {2}", Nome, Email, Telefone);
        }
    }
}
