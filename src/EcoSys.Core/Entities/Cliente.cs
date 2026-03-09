namespace EcoSys.Core.Entities;

public class Cliente
{
    public string Nome {get ; set ;}

    // Email e senha podem ser nulos, caso seja Loja física.
    public string? Email {get ; set ;}
    public string? Senha {get ; set ;}
    public string? Login {get ; set ;}
    
    // Lista de compras do Cliente
    public List<Compras> HistoricoCompras {get ; set ;} = new List<Compras>();
}