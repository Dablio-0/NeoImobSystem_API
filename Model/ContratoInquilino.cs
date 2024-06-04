using System.Text.Json.Serialization;

namespace NeoImobSystem_API.Model
{
    public class ContratoInquilino
    {
        public uint Id { get; set; }

        [JsonIgnore]
        public Contrato Contrato { get; set; }
        public uint? ContratoId { get; set; }

        [JsonIgnore]
        public Inquilino Inquilino { get; set; }
        public uint? InquilinoId { get; set; }
    }
}
