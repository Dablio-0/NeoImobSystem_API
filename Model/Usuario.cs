namespace NeoImobSystem_API.Model
{
    public class Usuario
    {
        #region Atributos
        public uint Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; } = string.Empty;

        //Relacionamentos (Listagem de Informações)
        public ICollection<Casa> Casas { get; set; }
        public ICollection<Contrato> Contratos { get; set; }
        public ICollection<Inquilino> Inquilinos { get; set; }
        public ICollection<Proprietario> Proprietarios { get; set; }
        #endregion 
    }
}
