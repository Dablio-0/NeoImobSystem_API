using System.Text.Json.Serialization;

namespace NeoImobSystem_API.Model
{
    public class Inquilino
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
        public List<ContratoInquilino?> ContratoInquilinos { get; set; } = new List<ContratoInquilino?>();

        public Usuario Usuario { get; set; }
        public uint UsuarioId { get; set; }
        #endregion
    }
}
