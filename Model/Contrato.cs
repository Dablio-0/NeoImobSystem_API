using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NeoImobSystem_API.Model
{
    public class Contrato
    {
        #region Atributos
        public uint Id { get; set; }
        public string Descricao { get; set; }
        public bool Status { get; set; }
        public float Valor { get; set; }
        public uint Parcelas { get; set; }
        public DateTime Periodo { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }


        // Relacionamentos
        [JsonIgnore]
        public Casa Casa { get; set; }
        [ForeignKey("CasaId")]
        public uint? CasaId { get; set; }

        [JsonIgnore]
        public List<ContratoInquilino?> ContratoInquilinos { get; set; } = new List<ContratoInquilino?>();

        [JsonIgnore]
        public List<uint> InquilinosId { get; set; }

        [JsonIgnore]
        public Usuario Usuario { get; set; }
        [ForeignKey("UsuarioId")]
        public uint UsuarioId { get; set; }

        #endregion



    }
}
