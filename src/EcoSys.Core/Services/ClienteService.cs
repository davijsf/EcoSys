namespace EcoSys.Core.Services;
using EcoSys.Core.Entities;

public class ClienteService
{
    private List<Cliente> clientes = new List<Cliente>();

    public Cliente CadastrarCliente(Empresa empresa, string nome, string email, string senha, string login)
    {
    // Verifica se já existe login
    bool loginExiste = empresa.Clientes.Any(c => c.Login == login);

    if(loginExiste)
    {
        throw new Exception("Login já existente.");
    }

    Cliente cliente = new Cliente
    {
        Nome = nome,
        Email = email,
        Login = login,
        Senha = senha
    };

    // Adiciona à lista de clientes da classe Empresa
    empresa.Clientes.Add(cliente);
    return cliente;
}

    // Busca de cliente por login
    // Uso do ? | pode retornar um vazio
    public Cliente? BuscarClientePorLogin(string login)
    {
        // retorna o primeiro cliente da lista | onde o login é igual ao valor do parâmetro
        return clientes.FirstOrDefault(c => c.Login == login);
    }

    public Cliente? BuscarClientePorNome(string nome)
    {   
        foreach (var cliente in clientes)
        {
            if (cliente.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase))
            {
                return cliente;
            }
        }
        return null;
    }
    // Autenticação de cliente
    public Cliente? Autenticar(string login, string senha)
    {
        return clientes.FirstOrDefault(c => c.Login == login && c.Senha == senha);
    }

    // Listar compras do cliente
    public List<Compra> ListarCompras(Cliente cliente)
    {
        return cliente.HistoricoCompras;
    }
}