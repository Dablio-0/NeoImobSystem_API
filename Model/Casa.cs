using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NeoImobSystem_API.Model
{
    public class Casa
    {
        #region Atributos
        public uint Id { get; set; }
        public string Endereco { get; set; }
        public uint NumeroSalas { get; set; }
        public string Tipo { get; set; }
        public string CEP { get; set; }

        // Relacionamentos
        [JsonIgnore]
        public Contrato Contrato { get; set; }
        [ForeignKey("ContratoId")]
        public uint? ContratoId { get; set; }

        [JsonIgnore]
        public Usuario Usuario { get; set; }
        [ForeignKey("Usuarioid")]
        public uint UsuarioId { get; set; }
        #endregion
    }
}
