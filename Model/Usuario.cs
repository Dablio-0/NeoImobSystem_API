using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public List<Casa> Casas { get; set; } = new List<Casa>();
        [JsonIgnore]
        public List<Contrato> Contratos { get; set; } = new List<Contrato>();
        [JsonIgnore]
        public List<Inquilino> Inquilinos { get; set; } = new List<Inquilino>();
        [JsonIgnore]
        public List<Proprietario> Proprietarios { get; set; } = new List<Proprietario>();
        #endregion 
    }
}
