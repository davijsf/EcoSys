namespace EcoSys.Core.Entities;

using EcoSys.Core.Enums;

public class Compras
{
    // Cliente pode ser nulo. Representando compra sem cadastro na loja fisica
    public Cliente ? cliente {get; set ;}

    public double valor_total {get ; set ;}

    public Loja loja {get ; set ;}

    // Ecommerce ou Loja física
    public CanalVenda canalVenda {get ; set;}
    
    // Lista de produtos
    public List<Produto> Produtos {get ; set ;} = new List<Produto>();

    // lançar data na hora da compra
    public DateTime dataCompra {get ; set ;}
}