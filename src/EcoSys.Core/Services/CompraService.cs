namespace EcoSys.Core.Services;

using EcoSys.Core.Enums;
using EcoSys.Core.Entities;

public class CompraService
{
    public Compra RegistrarCompra( Cliente ? cliente, Loja loja, List<Produto> produtos, CanalVenda canalVenda)
    {
        Compra compra = new Compra
        {
            Cliente = cliente,
            Loja = loja,
            Produtos = produtos,
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

        foreach (var produto in compra.Produtos)
        {
            total += produto.Preco;
        }

        return total;
    }


}