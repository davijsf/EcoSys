using EcoSys.Core.Services;
using EcoSys.ConsoleApp.Menus;
using EcoSys.Core.Entities;

namespace EcoSys.ConsoleApp;

public class Application
{
    public static void main(string[] args)
    {
        // Services
        ProdutoService produtoService = new ProdutoService();
        CategoriaService categoriaService = new CategoriaService();
        TagService tagService = new TagService();
        ClienteService clienteService = new ClienteService();
        CompraService compraService = new CompraService();

        // Menus
        ProdutoMenu produtoMenu = new ProdutoMenu(produtoService);
        CategoriaMenu categoriaMenu = new CategoriaMenu(categoriaService);
        TagMenu tagMenu = new TagMenu(tagService);
        CompraMenu compraMenu = new CompraMenu(compraService, produtoService, clienteService);

        bool rodando = true;

        while (rodando)
        {
            Console.WriteLine("\n==== ECOSYS ====");
            Console.WriteLine("1 - Produtos");
            Console.WriteLine("2 - Categorias");
            Console.WriteLine("3 - Tags");
            Console.WriteLine("4 - Compras");
            Console.WriteLine("0 - Sair");

            Console.Write("\nEscolha uma opção: ");
            string opcao = Console.ReadLine()!;

            switch (opcao)
            {
                case "1":
                    produtoMenu.MenuProdutos();
                    break;

                case "2":
                    categoriaMenu.MenuCategorias();
                    break;

                case "3":
                    tagMenu.MenuTags();
                    break;

                case "4":
                    compraMenu.MenuCompras();
                    break;

                case "0":
                    rodando = false;
                    break;

                default: 
                    Console.WriteLine("Opção inválida.");
                    Console.ReadKey();
                    break;
            }
        }
        Console.WriteLine("Sistema encerrado.");
    }
}