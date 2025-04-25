using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elena.Domain.Entities
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public TipoUsuario Tipo { get; set; } // Tipo de usuário, como Admin, etc.

        // Chave estrangeira para a tabela Pessoa
        public Guid PessoaId { get; set; }

        // Relacionamento com a tabela Pessoa
        public Pessoa Pessoa { get; set; }
    }


}
