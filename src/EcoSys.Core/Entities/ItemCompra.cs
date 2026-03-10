using EcoSys.Core.Entities;

namespace EcoSys.Core.Entities;

public class ItemCompra
{
    public Produto Produto {get ; set ;}

    public double Quantidade {get ; set ;}
    public double PrecoUnitario {get ; set ;}
    
    // SubTotal = Quantidade x PrecoUnitario
    public double SubTotal => Quantidade * PrecoUnitario;
}