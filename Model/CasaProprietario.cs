using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NeoImobSystem_API.Model
{
    public class CasaProprietario
    {
        public uint Id { get; set; }

        public Casa Casa { get; set; }
        [ForeignKey("CasaId")]
        public uint? CasaId { get; set; }

        public Proprietario Proprietario { get; set; }
        [ForeignKey("ProprietarioId")]
        public uint? ProprietarioId { get; set; }
    }
}
