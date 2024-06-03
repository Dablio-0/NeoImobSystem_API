using System.ComponentModel.DataAnnotations;

namespace NeoImobSystem_API.DTO
{
    public class CriarCasaDTO
    {
        [Required] public string Endereco { get; set; }
        public uint NumeroSalas { get; set; }
        public string Tipo { get; set; }
        [Required] public string CEP { get; set; }
        public uint ContratoId { get; set; }
        [Required] public uint UsuarioId { get; set; }
    }
}
