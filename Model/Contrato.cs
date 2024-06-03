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
        public Usuario Usuario { get; set; }
        public uint UsuarioId { get; set; }
        #endregion



    }
}
