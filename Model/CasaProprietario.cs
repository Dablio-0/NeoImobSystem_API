using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NeoImobSystem_API.Model
{
    public class CasaProprietario
    {
        public uint Id { get; set; }

        [JsonIgnore]
        public Casa Casa { get; set; }
        public uint? CasaId { get; set; }

        [JsonIgnore]
        public Proprietario Proprietario { get; set; }
        [ForeignKey(""))]
        public uint? ProprietarioId { get; set; }
    }
}
