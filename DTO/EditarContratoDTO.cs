using System.ComponentModel.DataAnnotations;

namespace NeoImobSystem_API.DTO
{
    public class EditarContratoDTO
    {
        [Required] public string Descricao { get; set; }
        [Required] public bool Status { get; set; } = false;
        [Required] public float Valor { get; set; }
        [Required] public uint Parcelas { get; set; }
        [Required] public DateTime Inicio { get; set; }
        [Required] public DateTime Fim { get; set; }
        public uint CasaId { get; set; }
        public List<uint?> InquilinosId { get; set; }
        [Required] public uint UsuarioId { get; set; }
    }
}
