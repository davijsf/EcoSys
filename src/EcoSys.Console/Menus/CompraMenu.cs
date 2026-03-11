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
            Console.WriteLine("1 - Realizar compra");
            Console.WriteLine("2 - Ver histórico de compras");
            Console.WriteLine("0 - Voltar");

            Console.Write("Esolha: ");
            string opcao = Console.ReadLine()!;

            switch (opcao)
            {
                case "1":
                    RegistrarCompra();
                    break;

                case "2":
                    ListarCompras();
                    break;

                case "0":
                    rodando = false;
                    break;
            }
        }
    }


    private void RegistrarCompra()
    {
        Console.Write("Login: ");
        string loginCliente = Console.ReadLine()!.Trim();

        Cliente ? clienteEncontrado = clienteService.BuscarClientePorLogin(loginCliente);

        // Se o nome cliente nao é null e nem tem espaco em branco...
        if(clienteEncontrado == null)
        {
            Console.WriteLine("Cliente não encontrado.");
            Console.ReadKey();
            return;
        }

        Cliente cliente = clienteEncontrado;

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
            double qtd = double.Parse(Console.ReadLine()!);

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

        Console.Write("Digite: ");
        string canal = Console.ReadLine()!;

        CanalVenda canalVenda = canal == "2" ? CanalVenda.ECOMMERCE : CanalVenda.LOJA_FISICA;

        // inicio a loja com valor indefinido, caso seja ecommerce
        Loja loja = new Loja
        {
            Cidade = ""
        };

        if (canalVenda == CanalVenda.LOJA_FISICA)
        {
            Console.WriteLine("1 - Aracati");
            Console.WriteLine("2 - Russas");

            Console.Write("Digite: ");
            string city = Console.ReadLine()!;

            // Caso seja loja física, defino a cidade da loja aqui
            loja.Cidade = city;
        }

        var compra = compraService.RegistrarCompra(cliente, loja, itens, canalVenda);
    
        double total = compraService.CalcularTotal(compra);
        Console.WriteLine($"Compra resgitrada! Total: R$ {total}");
        Console.ReadKey();
    }

    private void ListarCompras()
    {
        Console.Write("Login: ");
        string loginClienteRaw = Console.ReadLine() ?? "";
        string loginCliente = loginClienteRaw.Trim();

        if (string.IsNullOrWhiteSpace(loginCliente))
        {
            Console.WriteLine("Login inválido.");
            Console.ReadKey();
            return;
        }

        Cliente? cliente = clienteService.BuscarClientePorLogin(loginCliente);
        if (cliente == null)
        {
            Console.WriteLine("Cliente não encontrado.");
            Console.ReadKey();
            return;
        }

        var compras = clienteService.ListarCompras(cliente);

        // Verifica se não tem compras
        if (!compras.Any())
        {
            Console.WriteLine($"Cliente '{cliente.Nome}' não tem compras.");
            Console.ReadKey();
            return;            
        }

        Console.WriteLine($"\nHistórico de {cliente.Nome}: ");
        int numCompra = 1;

        foreach (var compra in compras)
        {
            Console.WriteLine($"\n--- Compra #{numCompra++} ---");
            Console.WriteLine($"Data: {compra.DataCompra}");
            Console.WriteLine($"Canal: {compra.CanalVenda}");

            if (compra.CanalVenda == CanalVenda.LOJA_FISICA) 
            {
                Console.WriteLine($"Loja: {compra.Loja?.Cidade}");
                Console.WriteLine($"Total: R$ {compraService.CalcularTotal(compra):F2}");
            }
            
            foreach(var item in compra.Itens)
            {
                Console.WriteLine($"  • {item.Produto.Nome} x{item.Quantidade} = R$ {item.SubTotal:F2}");
            }
        }   
    }
}

