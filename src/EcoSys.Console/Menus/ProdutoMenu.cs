using EcoSys.Core.Entities;
using EcoSys.Core.Services;

namespace EcoSys.ConsoleApp.Menus;

public class ProdutoMenu
{
    private ProdutoService produtoService;

    public ProdutoMenu(ProdutoService produtoService)
    {
        this.produtoService = produtoService;
    }

    public void MenuProdutos()
    {
        bool rodando = true;

        while(rodando)
        {
            Console.WriteLine("\n==== MENU PRODUTOS ====");
            Console.WriteLine("1 - Cadastrar produtos.");
            Console.WriteLine("2 - Listar produtos.");
            Console.WriteLine("3 - Remover produto.");
            Console.WriteLine("4 - Atualizar preço de produto.");
            Console.WriteLine("5 - Buscar produto pelo nome.");
            Console.WriteLine("6 - Buscar produto pela categoria.");
            Console.WriteLine("7 - Buscar produto pela tag.");
            Console.WriteLine("0 - Voltar");

            string opcao = Console.ReadLine()!;

            switch (opcao)
            {
                case "1":
                    CadastrarProduto();
                    break;

                case "2":
                    ListarProdutos();
                    break;

                case "3":
                    RemoverProduto();
                    break;

                case "4":
                    AtualizarPrecoProduto();
                    break;

                case "5":
                    BuscarProdutoPorNome();
                    break;

                case "6":
                    BuscarProdutoPorCategoria();
                    break;

                case "7":
                    BuscarProdutoPorTag();
                    break;

                case "0":
                    rodando = false;
                    break;

            }

        }
    }


    private void CadastrarProduto()
    {
        Console.Write("Nome do produto: ");
        string nome = Console.ReadLine()!;

        Console.Write("Preço: R$ ");
        double preco = double.Parse(Console.ReadLine()!);

        Produto produto = new Produto
        {
            Nome = nome,
            Preco = preco
        };

        produtoService.CadastrarProduto(produto);
    }

    private void ListarProdutos()
    {
        var produtos = produtoService.ListarProdutos();

        foreach (var produto in produtos)
        {
            Console.WriteLine($"{produto.Nome} - R$ {produto.Preco}");
        }
    }

    private void RemoverProduto()
    {
        Console.Write("Nome do produto: ");
        string nome = Console.ReadLine()!;

        var resultado = produtoService.RemoverProduto(nome);
        string response = resultado ? "Produto removido!" : "Produto não existe!";
        
        Console.WriteLine(response);
    }

    private void AtualizarPrecoProduto()
    {
        Console.Write("Nome do produto: ");
        string nome = Console.ReadLine()!;

        Console.Write("Novo preço: R$ ");
        double novoPreco = double.Parse(Console.ReadLine()!);

        var resultado = produtoService.AtualizarPrecoProduto(nome, novoPreco); 

        string response = resultado ? "Preço atualizado com sucesso!" : "Produto não existe!";
        Console.WriteLine(response);
    }

    private void BuscarProdutoPorNome()
    {
        Console.Write("Nome do produto: ");
        string nome = Console.ReadLine()!;

        var resultado = produtoService.BuscarProdutoPorNome(nome);

        if (resultado != null)
        {
            Console.WriteLine("\nProduto encontrado:");
            Console.WriteLine($"Nome: {resultado.Nome}");
            Console.WriteLine($"Preço: R$ {resultado.Preco}");
        }
        else
        {
            Console.WriteLine("Produto não encontrado!");
        }
    }

    private void BuscarProdutoPorCategoria()
    {   
        Console.WriteLine("Nome da categoria: ");
        string nomeCategoria = Console.ReadLine()!;

        Categoria categoria = new Categoria
        {
            Nome = nomeCategoria
        };

        var resultados = produtoService.BuscarProdutoPorCategoria(categoria);

        if (resultados.Count > 0)
        {
            Console.WriteLine("\nProdutos encontrados:");

            foreach (var produto in resultados)
            {
                Console.WriteLine($"{produto.Nome} - R$ {produto.Preco}");
            }
        }
        else
        {
            Console.WriteLine("Nenhum produto encontrado nessa categoria.");
        }
    }

    private void BuscarProdutoPorTag()
    {
        Console.WriteLine("Nome da tag: ");
        string nomeTag = Console.ReadLine()!;

        Tag tag = new Tag
        {
            Nome = nomeTag
        };

        var resultados = produtoService.BuscarProdutoPorTags(tag);

        if (resultados.Count > 0)
        {
            Console.WriteLine("\nProdutos encontrados:");

            foreach (var produto in resultados)
            {
                Console.WriteLine($"{produto.Nome} - R$ {produto.Preco}");
            }
        }
        else
        {
            Console.WriteLine("Nenhum produto encontrado com essa tag.");
        }
    }
}