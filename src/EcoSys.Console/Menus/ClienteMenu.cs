using EcoSys.Core.Services;

namespace EcoSys.ConsoleApp.Menus;

public class ClienteMenu
{
    private ClienteService clienteService;
    private ProdutoService produtoService;

    public ClienteMenu(ClienteService clienteService)
    {
        this.clienteService = clienteService;
    }

    public void MenuClientes()
    {}
}