using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NeoImobSystem_API.Model
{
    public class Proprietario
    {
        #region Atributos
        public uint Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }

        // Relacionamentos
        [JsonIgnore]
        public List<CasaProprietario?> CasaProprietarios { get; set; } = new List<CasaProprietario?>();

        public Usuario Usuario { get; set; }
        [ForeignKey("Usuarioid")]
        public uint UsuarioId { get; set; }
        #endregion
    }
}
