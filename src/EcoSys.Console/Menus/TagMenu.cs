using EcoSys.Core.Entities;
using EcoSys.Core.Services;

namespace EcoSys.ConsoleApp.Menus;

public class TagMenu
{
    private TagService tagService;

    public TagMenu(TagService tagService)
    {
        this.tagService = tagService;
    }

    public void MenuTags()
    {
        bool rodando = true;

        while (rodando)
        {
            Console.WriteLine("\n==== MENU TAGS ====");
            Console.WriteLine("1 - Cadastrar tag");
            Console.WriteLine("2 - Listar tags");
            Console.WriteLine("3 - Remover tag");
            Console.WriteLine("4 - Buscar tag pelo nome");
            Console.WriteLine("0 - Voltar");

            string opcao = Console.ReadLine()!;

            switch (opcao)
            {
                case "1":
                    CadastrarTag();
                    break;

                case "2":
                    ListarTags();
                    break;

                case "3":
                    RemoverTag();
                    break;

                case "4":
                    BuscarTagsPorNome();
                    break;

                case "0":
                    rodando = false;
                    break;
            }
        }
    }

    private void CadastrarTag()
    {
        Console.Write("Nome da tag: ");
        string nome = Console.ReadLine()!;

        tagService.CadastrarTag(nome);
    }

    private void ListarTags()
    {
        var tags = tagService.ListarTags();

        foreach (var tag in tags)
        {
            Console.WriteLine(tag.Nome);
        }
    }

    private void RemoverTag()
    {
        Console.Write("Nome da tag: ");
        string nome = Console.ReadLine()!;

        bool resultado = tagService.RemoverTag(nome);

        string response = resultado ? "Tag removida!" : "Tag não encontrada!";
        Console.WriteLine(response);
    }

    private void BuscarTagsPorNome()
    {
        Console.Write("Nome da Tag: ");
        string nome = Console.ReadLine()!;

        var resultado = tagService.BuscarTagsPorNome(nome);

        if (resultado != null)
        {
            Console.WriteLine("\nTag encontrada:");
            Console.WriteLine($"Nome: {resultado.Nome}");
        }
        else
        {
            Console.WriteLine("Tag não encontrada!");
        }
    }
}