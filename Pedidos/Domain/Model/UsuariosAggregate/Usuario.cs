using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pedidos.Domain.Model.UsuariosAggregate
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; private set; }

        [Required]
        [MaxLength(100)]
        [Column("nome")]
        public string Nome { get; set; } = "";

        [Required]
        [MaxLength(255)]
        [Column("email")]
        public string Email { get; set; } = "";

        [Required]
        [MaxLength(255)]
        [Column("senha")]
        public string Senha { get; set; } = "";

        [Column("ativo")]
        public bool Ativo { get; set; } = true;

        [Column("data_criacao")]
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        public Usuario(string _Email) => Email = _Email;

        public Usuario()
        {
            
        }
    }
}