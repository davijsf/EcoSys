using EcoSys.Core.Enums;

namespace EcoSys.Core.Entities;

public class Cliente : Usuario
{

    public string ? Nome {get ; set ;}
    
    // Email e senha podem ser nulos, caso seja Loja física.
    public string? Email {get ; set ;}
    
    // Histórico de compras do Cliente
    public List<Compra> HistoricoCompras {get ; set ;} = new List<Compra>();
}