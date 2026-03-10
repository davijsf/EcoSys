namespace EcoSys.Core.Entities;

using EcoSys.Core.Enums;

public class Compra
{
    // Cliente pode ser nulo. Representando compra sem cadastro na loja fisica
    public Cliente ? Cliente {get; set ;}

    public Loja Loja {get ; set ;}

    // Ecommerce ou Loja física
    public CanalVenda canalVenda {get ; set;}
    
    // Lista de produtos
    public List<ItemCompra> Itens {get ; set ;} = new List<ItemCompra>();

    // lançar data na hora da compra
    public DateTime dataCompra {get ; set ;}
}