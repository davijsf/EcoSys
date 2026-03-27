using EcoSys.Core.Services;
using EcoSys.ConsoleApp.Menus;
using EcoSys.Core.Entities;
using EcoSys.Core.Enums;
using EcoSys.ConsoleApp.Data;
using EcoSys.ConsoleApp.Validation;

namespace EcoSys.ConsoleApp;


public class Application
{
    public static void Main(string[] args)
    {
        // 1. Empresa vazia
        Empresa empresa = new Empresa { Nome = "EcoSys" };

        // 2. Services VAZIOS (sem empresa)
        ProdutoService produtoService = new ProdutoService(empresa);
        ClienteService clienteService = new ClienteService(empresa);
        UsuarioService usuarioService = new UsuarioService(empresa);
        CategoriaService categoriaService = new CategoriaService(empresa);
        TagService tagService = new TagService(empresa);
        CompraService compraService = new CompraService(empresa);

        // 3. POPULA empresa
        empresa = SeedData.Inicializar(empresa, produtoService, clienteService, usuarioService);

        // 4. Menus
        ProdutoMenu produtoMenu = new ProdutoMenu(produtoService);
        CategoriaMenu categoriaMenu = new CategoriaMenu(categoriaService);
        TagMenu tagMenu = new TagMenu(tagService);
        CompraMenu compraMenu = new CompraMenu(compraService, produtoService, clienteService);

        // Validation
        Valid valid = new Valid();


        bool rodando = true;

        // sistema de login
        while (rodando)
        {

            Console.WriteLine("-- Bem-vindo ao ECOSYS! --");
            Console.WriteLine("1 - LOGIN");
            Console.WriteLine("2 - CADASTRO");
            Console.WriteLine("3 - SAIR");


            Console.Write("Digite: ");
            string op = Console.ReadLine()?.Trim() ?? "";

            switch(op)
            {
                case "1":
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
                            return; // Sai do programa
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
                    else if (usuario is Funcionario funcionario)
                    {
                        MenuFuncionario(funcionario, produtoMenu, categoriaMenu, tagMenu, compraMenu, empresa);
                    }

                    else
                    {
                        Console.WriteLine("Tipo de usuário não reconhecido");
                        Console.ReadKey();
                        continue;
                    }


                    //pergunta "continuar?" APÓS o menu sair
                    Console.Write("\nDeseja fazer outro login? [s/n]: ");
                    if (Console.ReadLine()?.Trim().ToLower() != "s")
                        break;
                break;


                case "2":
                    Console.Clear();
                    Console.WriteLine("\tCadastro de clientes - EcoSys");
                    
                    string nomeCliente = valid.LerTextoObrigatorio("Nome: ", 2, 50);

                    string emailCliente = valid.LerEmailValido("Email: ");

                    string loginCliente = valid.LerLoginValido("Login: ");

                    string senhaConfirmada = string.Empty;

                    Console.Write("Senha: ");
                    string senhaCliente = Console.ReadLine()?.Trim() ?? "";

                    do 
                    {
                        Console.Write("Confirme a senha: ");
                        string verifySenha = Console.ReadLine()?.Trim() ?? "";

                        if (senhaCliente ==  verifySenha)
                        {
                            senhaConfirmada = senhaCliente;
                            break;   
                        }

                        Console.WriteLine("Senha não confirmada. Tente novamente");
                        Console.Write("Senha: ");
                        senhaCliente = Console.ReadLine()?.Trim() ?? "";
                    } while (true);
                    
                    var novoCliente = clienteService.CadastrarCliente(nomeCliente, emailCliente, senhaConfirmada, loginCliente);
                    string msg = novoCliente != null 
                    ? $"Cliente {novoCliente.Nome}, cadastrado!" : "Erro: Login já existe";

                    Console.WriteLine(msg);
                    Console.ReadKey();
                break;


                case "3":
                    Console.WriteLine("Saindo ...");
                    rodando = false;
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
            Console.WriteLine("5 - Ver histórico de compras");

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
                    Console.Write("Nome(ou código) do produto: ");
                    string busca = Console.ReadLine()?.Trim() ?? "";
                        
                    var produto = produtoService.BuscarProduto(busca);
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
                        Console.WriteLine($"{i.Produto?.Nome} - {i.Quantidade}x - R$ {i.PrecoUnitario}");
                    }

                    Console.ReadKey();
                    break;

                case "4":
                   if (!carrinho.Any())
                    {
                        Console.WriteLine("Carrinho vazio! Adicione itens primeiro.");
                        Console.ReadKey();
                        break;
                    }

                    Console.WriteLine("\n -- FINALIZAR COMPRA --");
                    
                    var itensCompra = new List<ItemCompra>(carrinho);

                    // Calcula total da compra
                    double totalCompra = itensCompra.Sum(i => (double)i.SubTotal);
                    Console.WriteLine($"Total: R$ {totalCompra}");

                    Loja ? loja = null;

                    // Registra compra com carrinho
                    var compra = compraService.RegistrarCompra(cliente, loja , itensCompra, CanalVenda.LOJA_FISICA);

                    // Limpa carrinho após a compra
                    carrinho.Clear();

                    Console.WriteLine($"\nCompra registrada com {itensCompra.Count} item(ns)!");
                    Console.ReadKey();
                    break;

                case "5":
                    var compras = compraService.ListarCompras(cliente);

                    if (!compras.Any())
                    {
                        Console.WriteLine("Nenhuma compra encontrada.");
                        Console.ReadKey();
                        break;
                    }

                    int numCompra = 1;
                    int numItem = 1;
                    foreach(var buy in compras) 
                    {
                        Console.WriteLine($"\n--- Compra #{numCompra++} ---");
                        Console.WriteLine($"Data: {buy.DataCompra: dd/MM/yyyy HH:mm}");

                        if (buy.Itens == null || !buy.Itens.Any())
                        {
                            Console.WriteLine(" -> Nenhum item nesta compra.");
                        } 

                        else
                        {
                            double total = 0;
                            numItem = 1;
                            foreach (var i in buy.Itens)
                            {
                                Console.WriteLine($"\n-- Item: #{numItem++}");
                                Console.WriteLine($"Nome: {i.Produto?.Nome}");
                                Console.WriteLine($"Preço unitário: R$ {i.PrecoUnitario}");
                                Console.WriteLine($"Quantidade: {i.Quantidade}");
                                Console.WriteLine($"SubTotal: R$ {i.SubTotal}");
                                total += i.SubTotal;
                            }
                             Console.WriteLine($"\nTOTAL COMPRA: R$ {total:C}");
                        } 
                    }
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
    Funcionario funcionario,
    ProdutoMenu produtoMenu,
    CategoriaMenu categoriaMenu,
    TagMenu tagMenu,
    CompraMenu compraMenu,
    Empresa empresa)
{
    bool rodando = true;

    while (rodando)
    {
        Console.Clear();

        Console.WriteLine("==== LOJA FÍSICA ====");
        Console.WriteLine($"Usuário: {funcionario.Login}");
        Console.WriteLine($"Cargo: {funcionario.Cargo}");

        Console.WriteLine("\n1 - Produtos");
        Console.WriteLine("2 - Categorias");
        Console.WriteLine("3 - Tags");
        Console.WriteLine("4 - Compras");
        Console.WriteLine("5 - Relatório de vendas");
        Console.WriteLine("0 - Logout");

        string opcao = Console.ReadLine()!;

        switch (opcao)
        {
            case "1":

                // Somente GERENTE pode gerenciar produtos
                if (funcionario.Cargo.HasFlag(Cargo.GERENTE))
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
                if (funcionario.Cargo.HasFlag(Cargo.GERENTE))
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
                if (funcionario.Cargo.HasFlag(Cargo.GERENTE)|| funcionario.Cargo.HasFlag(Cargo.REPOSITOR))
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
                if (funcionario.Cargo == Cargo.CAIXA || funcionario.Cargo == Cargo.GERENTE)
                {
                    compraMenu.MenuCompras();
                }
                else
                {
                    Console.WriteLine("Acesso permitido apenas para CAIXA ou GERENTE.");
                    Console.ReadKey();
                }

                break;


            // Somente GERENTE
            case "5":
                if (funcionario.Cargo != Cargo.GERENTE)
                {
                    Console.WriteLine("Acesso apenas para GERENTE.");
                    break;
                }

                Console.Write("Mês [1-12]: ");
                string monthStr = Console.ReadLine()?.Trim() ?? "";
                if (!int.TryParse(monthStr, out int month) || month < 1 || month > 12)
                {
                    Console.WriteLine("Mês inválido! Use 1-12.");
                    Console.ReadKey();
                    break;
                }

                Console.Write("Ano [ex: 2026]: ");
                string yearStr = Console.ReadLine()?.Trim() ?? "";
                if (!int.TryParse(yearStr, out int year) || year < 2000 || year > 2100)
                {
                    Console.WriteLine("Ano inválido! Use 2000-2100.");
                    Console.ReadKey();
                    break;
                }


                var vendasMes = empresa.Compras
                    .Where (c => c.DataCompra.Year == year && c.DataCompra.Month == month)
                    .OrderBy(c => c.DataCompra)
                    .ToList();

                if (!vendasMes.Any())
                {
                    Console.WriteLine($"\nNenhuma venda em {month:D2}/{year}.");
                    Console.ReadKey();
                    break;
                }

                Console.WriteLine("\n ============================");
                Console.WriteLine("\n Relatório vendas -- EcoSys");
                double totalGeral = 0;
                int totalCompras = vendasMes.Count;

                foreach (var venda in vendasMes)
                {
                    Console.WriteLine($"\nCompra #{venda.DataCompra:HH/mm/ss}");
                    Console.WriteLine($"   Data: {venda.DataCompra:dd/MM/yyyy HH:mm}");
                    Console.WriteLine($"   Cliente: {venda.Cliente?.Nome}");
                    Console.WriteLine($"   Loja: {venda.Loja?.Cidade}");
                    Console.WriteLine($"   Itens: {venda.Itens?.Count ?? 0}");
                    Console.WriteLine($"   Total: R$ {venda.Total:C}");
                    
                    totalGeral += venda.Total;
                }

                Console.WriteLine($"\n\tRESUMO {month:D2}/{year}:");
                Console.WriteLine($"   Total Compras: {totalCompras}");
                Console.WriteLine($"   Faturamento: R$ {totalGeral:C}");

                Console.ReadKey();       

                break;

            case "0":
                rodando = false;
                break;
        }
    }
}
}
