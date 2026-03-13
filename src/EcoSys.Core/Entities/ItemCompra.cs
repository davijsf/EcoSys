namespace EcoSys.Core.Entities;

public class ItemCompra
{
    public Produto Produto {get ; set ;}

    // Tipo double | DEVIDO UNIDADE MEDIDA PODER SER EM 'QUILO'
    public double Quantidade {get ; set ;}
    public double PrecoUnitario {get ; set ;}
    
    // SubTotal = Quantidade x PrecoUnitario
    public double SubTotal => Quantidade * PrecoUnitario;
}