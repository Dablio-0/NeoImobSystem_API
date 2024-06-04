﻿using System.ComponentModel.DataAnnotations;

namespace NeoImobSystem_API.DTO
{
    public class CriarUsuarioDTO
    {
        public string Nome { get; set; }
        [Required] public string Email { get; set; }
        [Required] public string Senha { get; set; }
    }
}
