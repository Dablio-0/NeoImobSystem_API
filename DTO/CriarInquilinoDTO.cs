using System.ComponentModel.DataAnnotations;

namespace NeoImobSystem_API.DTO
{
    public class CriarInquilinoDTO
    {
        [Required] public string Nome { get; set; }
        public string Email { get; set; }
        [Required] public string Telefone { get; set; }
        [Required] public string CPF { get; set; }
        [Required] public DateTime DataNascimento { get; set; }
        [Required] public uint UsuarioId { get; set; }
    }
}
