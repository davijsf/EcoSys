using EcoSys.Core.Services;
using EcoSys.ConsoleApp.Menus;
using EcoSys.Core.Entities;
using EcoSys.Core.Enums;
using EcoSys.ConsoleApp.Data;

namespace EcoSys.ConsoleApp;

public class Application
{
    public static void Main(string[] args)
    {
        // Services
        ProdutoService produtoService = new ProdutoService();
        CategoriaService categoriaService = new CategoriaService();
        TagService tagService = new TagService();
        ClienteService clienteService = new ClienteService();
        CompraService compraService = new CompraService();
        UsuarioService usuarioService = new UsuarioService();

        // Criação dos objetos | manual
        Empresa empresa = SeedData.Inicializar(
        produtoService,
        clienteService,
        usuarioService
        );

        // Menus funcionário
        ProdutoMenu produtoMenu = new ProdutoMenu(produtoService);
        CategoriaMenu categoriaMenu = new CategoriaMenu(categoriaService);
        TagMenu tagMenu = new TagMenu(tagService);
        CompraMenu compraMenu = new CompraMenu(compraService, produtoService, clienteService);

        bool rodando = true;

        while (rodando)
        {
            Console.Clear();
            Console.WriteLine("==== LOGIN ECOSYS ====");
            Console.Write("Login: ");
            string login = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Senha: ");
            string senha = Console.ReadLine()?.Trim() ?? "";

            var usuario = usuarioService.Autenticar(login, senha);

            if (usuario == null)
            {
                Console.WriteLine("Login inválido!");
                Console.Write("Tentar novamente? [s/n]: ");
                string tentarNovamente = Console.ReadLine()?.Trim().ToLower() ?? "n";
                
                if (tentarNovamente != "s")
                {
                    Console.WriteLine("Programa encerrado.");
                    rodando = false;
                    return; // Sai do Main
                }
                continue; // Tenta login novamente
            }


            // Cliente
            if (usuario.Tipo == TipoUsuario.Cliente)
            {       
                Cliente? cliente = empresa.Clientes.FirstOrDefault(c => c.Login == usuario.Login);
                if (cliente == null)
                {
                    Console.WriteLine("Cliente não encontrado!");
                    Console.ReadKey();
                    continue; //Volta pro login ao invés de return
                }
                MenuCliente(produtoService, compraService, cliente);
            }

            // Funcionário
            else
            {
                MenuFuncionario(usuario, produtoMenu, categoriaMenu, tagMenu, compraMenu);
            }


            //pergunta "continuar?" APÓS o menu sair
            Console.Write("\nDeseja fazer outro login? [s/n]: ");
            if (Console.ReadLine()?.Trim().ToLower() != "s")
            {
                Console.WriteLine("Programa encerrado!");
                break;
            }
        }
    }

    // =========================
    // MENU CLIENTE (ECOMMERCE)
    // =========================

    static void MenuCliente(
    ProdutoService produtoService,
    CompraService compraService,
    Cliente cliente)
        {
        bool rodando = true;

        List<ItemCompra> carrinho = new List<ItemCompra>();

        while (rodando)
        {
            Console.Clear();

            Console.WriteLine("==== ECOMMERCE ====");
            Console.WriteLine("1 - Ver produtos");
            Console.WriteLine("2 - Adicionar produto ao carrinho");
            Console.WriteLine("3 - Ver carrinho");
            Console.WriteLine("4 - Finalizar compra");
            Console.WriteLine("0 - Logout");

            string opcao = Console.ReadLine()!;

            switch (opcao)
            {
                case "1":
                    var produtos = produtoService.ListarProdutos();
                    foreach (var p in produtos)
                    {
                        Console.WriteLine($"{p.Nome} [{p.UnidadeMedida}]");
                        Console.WriteLine($"  Categoria: {p.Categoria?.Nome} | R$ {p.Preco}");
                        Console.WriteLine($"  Tags: {string.Join(", ", p.Tags.Select(t => t.Nome))}");
                        Console.WriteLine();
                    }
                    Console.ReadKey();
                    break;

                case "2":
                    Console.Write("Nome do produto: ");
                    string nome = Console.ReadLine()?.Trim() ?? "";
                    
                    var produto = produtoService.BuscarProdutoPorNome(nome);
                    if (produto == null)
                    {
                        Console.WriteLine("Produto não encontrado.");
                        Console.ReadKey();
                        break;
                    }

                    Console.WriteLine($"\n{produto.Nome}");
                    Console.WriteLine($"   Preço: R$ {produto.Preco:F2} por {produto.UnidadeMedida}");
                    Console.WriteLine($"   Categoria: {produto.Categoria?.Nome}");

                    Console.Write("\nQuantidade: ");
                    if (!double.TryParse(Console.ReadLine(), out double quantidade) || quantidade <= 0)
                    {
                        Console.WriteLine("Quantidade inválida!");
                        Console.ReadKey();
                        break;
                    }

                    // CALCULO por unidade de medida
                    double precoTotalItem;
                    string unidadeTexto;
                    
                    if (produto.UnidadeMedida == UnidadeMedida.QUILO)
                    {
                        precoTotalItem = produto.Preco * quantidade;
                        unidadeTexto = $"{quantidade}kg";
                    }
                    else // UNIDADE
                    {
                        int qtdInteira = (int)quantidade;
                        precoTotalItem = produto.Preco * qtdInteira;
                        unidadeTexto = $"{qtdInteira}x";
                    }

                    ItemCompra item = new ItemCompra
                    {
                        Produto = produto,
                        Quantidade = quantidade,
                        PrecoUnitario = produto.Preco
                    };

                    carrinho.Add(item);
                    
                    Console.WriteLine($"\n{produto.Nome} adicionado!");
                    Console.WriteLine($"   {unidadeTexto} × R$ {produto.Preco:F2} = R$ {precoTotalItem:F2}");
                    Console.ReadKey();
                    break;


                case "3":

                    Console.WriteLine("==== CARRINHO ====");

                    foreach (var i in carrinho)
                    {
                        Console.WriteLine($"{i.Produto.Nome} - {i.Quantidade}x - R$ {i.PrecoUnitario}");
                    }

                    Console.ReadKey();
                    break;

                case "4":

                    if (carrinho.Count == 0)
                    {
                        Console.WriteLine("Carrinho vazio.");
                        Console.ReadKey();
                        break;
                    }

                    Loja loja = new Loja();

                    var compra = compraService.RegistrarCompra(
                        cliente,
                        loja,
                        carrinho,
                        CanalVenda.ECOMMERCE
                    );

                    double total = compraService.CalcularTotal(compra);

                    Console.WriteLine($"Compra realizada! Total: R$ {total}");

                    carrinho.Clear();

                    Console.ReadKey();
                    break;

                case "0":
                    rodando = false;
                    break;
            }
        }
    }

    // =========================
    // MENU FUNCIONÁRIO
    // =========================

    static void MenuFuncionario(
    Usuario usuario,
    ProdutoMenu produtoMenu,
    CategoriaMenu categoriaMenu,
    TagMenu tagMenu,
    CompraMenu compraMenu)
{
    bool rodando = true;

    while (rodando)
    {
        Console.Clear();

        Console.WriteLine("==== LOJA FÍSICA ====");
        Console.WriteLine($"Usuário: {usuario.Login}");
        Console.WriteLine($"Cargo: {usuario.Cargo}");
        Console.WriteLine();

        Console.WriteLine("1 - Produtos");
        Console.WriteLine("2 - Categorias");
        Console.WriteLine("3 - Tags");
        Console.WriteLine("4 - Compras");
        Console.WriteLine("0 - Logout");

        string opcao = Console.ReadLine()!;

        switch (opcao)
        {
            case "1":

                // Somente GERENTE pode gerenciar produtos
                if (usuario.Cargo == Cargo.GERENTE)
                {
                    produtoMenu.MenuProdutos();
                }
                else
                {
                    Console.WriteLine("Acesso permitido apenas para GERENTE.");
                    Console.ReadKey();
                }

                break;

            case "2":

                // Somente GERENTE
                if (usuario.Cargo == Cargo.GERENTE)
                {
                    categoriaMenu.MenuCategorias();
                }
                else
                {
                    Console.WriteLine("Acesso permitido apenas para GERENTE.");
                    Console.ReadKey();
                }

                break;

            case "3":

                // GERENTE ou REPOSITOR
                if (usuario.Cargo == Cargo.GERENTE || usuario.Cargo == Cargo.REPOSITOR)
                {
                    tagMenu.MenuTags();
                }
                else
                {
                    Console.WriteLine("Acesso permitido apenas para GERENTE ou REPOSITOR.");
                    Console.ReadKey();
                }

                break;

            case "4":

                // CAIXA ou GERENTE
                if (usuario.Cargo == Cargo.CAIXA || usuario.Cargo == Cargo.GERENTE)
                {
                    compraMenu.MenuCompras();
                }
                else
                {
                    Console.WriteLine("Acesso permitido apenas para CAIXA ou GERENTE.");
                    Console.ReadKey();
                }

                break;

            case "0":
                rodando = false;
                break;
        }
    }
}
}
