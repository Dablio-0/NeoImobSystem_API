using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NeoImobSystem_API.Model
{
    public class ContratoInquilino
    {
        public uint Id { get; set; }

        public Contrato Contrato { get; set; }
        [ForeignKey("ContratoId")]
        public uint? ContratoId { get; set; }

        public Inquilino Inquilino { get; set; }
        [ForeignKey("InquilinoId")]
        public uint? InquilinoId { get; set; }
    }
}
