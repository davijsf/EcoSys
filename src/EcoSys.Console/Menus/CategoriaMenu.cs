using EcoSys.Core.Entities;
using EcoSys.Core.Services;

namespace EcoSys.ConsoleApp.Menus;

public class CategoriaMenu
{
    private CategoriaService categoriaService;

    public CategoriaMenu(CategoriaService categoriaService)
    {
        this.categoriaService = categoriaService;
    }

    public void MenuCategorias()
    {
        bool rodando = true;

        while (rodando)
        {
            Console.WriteLine("\n==== MENU CATEGORIAS ====");
            Console.WriteLine("1 - Cadastrar categoria");
            Console.WriteLine("2 - Listar categorias");
            Console.WriteLine("3 - Remover categoria");
            Console.WriteLine("4 - Buscar categoria pelo nome");
            Console.WriteLine("0 - Voltar");

            string opcao = Console.ReadLine()!;

            switch (opcao)
            {
                case "1":
                    CadastrarCategoria();
                    break;

                case "2":
                    ListarCategorias();
                    break;

                case "3":
                    RemoverCategoria();
                    break;
                    
                case "4":
                    BuscarCategoriaPorNome();
                    break;

                case "0":
                    rodando = false;
                    break;
            }
        }
    }

    private void CadastrarCategoria()
    {
        Console.Write("Nome da categoria: ");
        string nome = Console.ReadLine()!;

        categoriaService.CadastrarCategoria(nome);
    }

    private void ListarCategorias()
    {
        var categorias = categoriaService.ListarCategorias();

        foreach (var categoria in categorias)
        {
            Console.WriteLine(categoria.Nome);
        }
    }

    private void RemoverCategoria()
    {
        Console.Write("Nome da categoria: ");
        string nome = Console.ReadLine()!;

        bool resultado = categoriaService.RemoverCategoria(nome);

        string response = resultado ? "Categoria removida!" : "Categoria não encontrada!";
        Console.WriteLine(response);
    }

    private void BuscarCategoriaPorNome()
    {
        Console.Write("Nome da categoria: ");
        string nome = Console.ReadLine()!;

        var resultado = categoriaService.BuscarCategoriaPorNome(nome);

        if (resultado != null)
        {
            Console.WriteLine("\nCategoria encontrada:");
            Console.WriteLine($"Nome: {resultado.Nome}");
        }
        else
        {
            Console.WriteLine("Categoria não encontrada!");
        }
    }
}