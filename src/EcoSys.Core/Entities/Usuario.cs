using EcoSys.Core.Enums;

namespace EcoSys.Core.Entities;

public class Usuario
{
    public string Login { get; set; } = "";
    public string Senha { get; set; } = "";

    public TipoUsuario Tipo { get; set; } // funcionário ou cliente
}