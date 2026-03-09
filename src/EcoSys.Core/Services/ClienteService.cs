namespace EcoSys.Core.Services;
using EcoSys.Core.Entities;

public class ClienteService
{
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
    public Cliente? BuscarClientePorLogin(List<Cliente> clientes, string login)
    {
        // retorna o primeiro cliente da lista | onde o login é igual ao valor do parâmetro
        return clientes.FirstOrDefault(c => c.Login == login);
    }

    // Autenticação de cliente
    public Cliente? Autenticar(List<Cliente> clientes, string login, string senha)
    {
        return clientes.FirstOrDefault(c => c.Login == login && c.Senha == senha);
    }

    // Listar compras do cliente
    public List<Compra> ListarCompras(Cliente cliente)
    {
        return cliente.HistoricoCompras;
    }
}