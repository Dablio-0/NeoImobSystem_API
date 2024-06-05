using System.ComponentModel.DataAnnotations;

namespace NeoImobSystem_API.DTO
{
    public class EditarCasaDTO
    {
        [Required] public string Endereco { get; set; }
        public uint NumeroSalas { get; set; }
        public string Tipo { get; set; }
        [Required] public string CEP { get; set; }
        public List<uint?> ProprietariosId { get; set; }
        public uint ContratoId { get; set; } = 0;
        [Required] public uint UsuarioId { get; set; }
    }
}
