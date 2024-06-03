using System.ComponentModel.DataAnnotations;

namespace NeoImobSystem_API.DTO
{
    public class CriarContratoDTO
    {
        [Required] string Descricao { get; set; }
        [Required] public bool Status { get; set; } = false;
        [Required] public float Valor { get; set; }
        [Required] public uint Parcelas { get; set; }
        [Required] public DateTime Periodo { get; set; }
        [Required] public DateTime Inicio { get; set; }
        [Required] public DateTime Fim { get; set; }
        [Required] public uint UsuarioId { get; set; }
    }
}
