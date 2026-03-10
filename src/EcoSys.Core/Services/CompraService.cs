namespace EcoSys.Core.Services;

using EcoSys.Core.Enums;
using EcoSys.Core.Entities;

public class CompraService
{
    List<ItemCompra> itens = new List<ItemCompra>();

    public Compra RegistrarCompra( Cliente ? cliente, Loja loja, CanalVenda canalVenda)
    {
        Compra compra = new Compra
        {
            Cliente = cliente,
            Loja = loja,
            Itens = itens,
            canalVenda = canalVenda
        };
        // Verifica se o cliente não é nullo | caso seja loja física
        if (cliente != null)
        {
            cliente.HistoricoCompras.Add(compra);
        }
        
        return compra;
    }

    public List<Compra> ListarCompras(Cliente cliente)
    {
        return cliente.HistoricoCompras;
    }

    public double CalcularTotal(Compra compra)
    {   
        double total = 0;

        foreach (var item in compra.Itens)
        {
            // Soma de todos os itens
            total += item.SubTotal;
        }

        return total;
    }


}