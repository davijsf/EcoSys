using EcoSys.Core.Entities;
using EcoSys.Core.Services;
using EcoSys.Core.Enums;

namespace EcoSys.ConsoleApp.Menus;

public class CompraMenu
{
    private CompraService compraService;
    private ProdutoService produtoService;
    private ClienteService clienteService;


    // Construtor
    public CompraMenu (
        CompraService compraService,
        ProdutoService produtoService,
        ClienteService clienteService
    )
    {
        this.compraService = compraService;
        this.produtoService = produtoService;
        this.clienteService = clienteService;
    }

    public void MenuCompras()
    {
        bool rodando = true;

        while (rodando) {
            Console.WriteLine("\n==== MENU COMPRAS ====");
            Console.WriteLine("1 - Registrar compra");
            Console.WriteLine("2 - Ver histórico de compras do cliente");
            Console.WriteLine("0 - Voltar");

            string opcao = Console.ReadLine()!;

            switch (opcao)
            {
                case "1":
                    RegistrarCompra();
                    break;

                case "2":
                    ListarCompras();
                    break;
            }
            

        }
    }


    private void RegistrarCompra()
    {
        Console.WriteLine("Login do Cliente [ou ENTER, se loja física]: ");
        string loginCliente = Console.ReadLine()!;

        Cliente ? cliente = null;

        // Se o nome cliente nao é null e nem tem espaco em branco...
        if(!string.IsNullOrWhiteSpace(loginCliente))
        {
            cliente = clienteService.BuscarClientePorLogin(loginCliente);

            if (cliente == null)
            {
                Console.WriteLine("Cliente não encontrado.");
                return;
            }
        }

        List<ItemCompra> itens = new List<ItemCompra>();

        bool adicionando = true;

        while (adicionando)
        {
            Console.Write("Nome do produto: ");
            string nomeProduto = Console.ReadLine()!;

            var produto = produtoService.BuscarProdutoPorNome(nomeProduto);

            if (produto == null)
            {
                Console.WriteLine("Produto não encontrado.");
                continue;
            }   
            
            Console.Write("Quantidade: ");
            int qtd = int.Parse(Console.ReadLine()!);

            ItemCompra item = new ItemCompra{
                Produto = produto,
                Quantidade = qtd,
                PrecoUnitario = produto.Preco
            };

            itens.Add(item);
            
            Console.Write("Adicionar outro produto ? [s / n]");
            string resp = Console.ReadLine()!;

            if (resp.ToLower() != "s")
            {
                adicionando = false;
            }
        } 

        Console.WriteLine("Canal da Venda: ");
        Console.WriteLine("1 - Loja física.");
        Console.WriteLine("2 - Ecommerce.");

        string canal = Console.ReadLine()!;

        CanalVenda canalVenda = canal == "2" ? CanalVenda.ECOMMERCE : CanalVenda.LOJA_FISICA;

        Loja loja = new Loja
        {
            Cidade = "Aracati"
        };

        var compra = compraService.RegistrarCompra(cliente, loja, canalVenda);

        double total = compraService.CalcularTotal(compra);
        Console.WriteLine($"Compra resgitrada! Total: R$ {total}");
    }

    private void ListarCompras()
    {
        Console.Write("Login do cliente: ");
        string loginCliente = Console.ReadLine()!;

        var cliente = clienteService.BuscarClientePorLogin(loginCliente);

        if (cliente == null)
        {
            Console.WriteLine("Cliente não encontrado.");
            return;
        }

        var compras = clienteService.ListarCompras(cliente);

        foreach (var compra in compras)
        {
            Console.WriteLine("\nCompra: ");
            
            foreach(var item in compra.Itens)
            {   
                Console.WriteLine($"{item.Produto.Nome} - {item.Quantidade} - R$ {item.SubTotal}");                
            }
        }


    }
}

