namespace EcoSys.Core.Entities;

public class Cliente
{
    public string Nome {get ; set ;}
    public string Email {get ; set ;}

    public List<Compras> HistoricoCompras {get ; set ;} = new List<Compras>();
}