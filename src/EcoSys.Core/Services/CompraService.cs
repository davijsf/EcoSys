namespace EcoSys.Core.Services;

using EcoSys.Core.Enums;
using EcoSys.Core.Entities;

public class CompraService
{
    private readonly Empresa _empresa;

    public CompraService (Empresa empresa)
    {
        _empresa = empresa ?? throw new ArgumentNullException(nameof(empresa));
    } 

    public Compra RegistrarCompra(Cliente ? cliente, Loja ? loja, List<ItemCompra> itens, CanalVenda canalVenda)
    {

        // Valida itens
        if (itens == null || !itens.Any())
        {
            throw new ArgumentException("Itens não podem estar vazios.");
        }


        Compra compra = new Compra
        {
            Cliente = cliente,
            Loja = loja,
            Itens = itens,
            CanalVenda = canalVenda,
            DataCompra = DateTime.Now,
            Total = itens.Sum(i => i.SubTotal)
        };

        // Salva na empresa (principal)
        _empresa?.Compras.Add(compra);

        // Adiciona no histórico do cliente (se existir)
        if (cliente != null)
        {   
            cliente.HistoricoCompras ??=new List<Compra>();
            cliente.HistoricoCompras.Add(compra);
        }
        return compra;
    }

    // Vendas da empresa 
    public List<Compra> ListarCompras(Cliente cliente)
    {
        return _empresa?.Compras?
        .Where(c => c?.Cliente != null && c.Cliente.Login == cliente.Login)
        .OrderByDescending(c => c.DataCompra)
        .ToList() ?? new List<Compra>();
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