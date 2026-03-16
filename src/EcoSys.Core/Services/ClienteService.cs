namespace EcoSys.Core.Services;
using EcoSys.Core.Entities;


public class ClienteService
{
    private readonly Empresa _empresa; // não nullable, readonly para maior segurança

    public ClienteService (Empresa empresa)
    {
        _empresa = empresa ?? throw new ArgumentNullException(nameof(empresa));
    } 

    public Cliente ? CadastrarCliente(string nome, string email, string senha, string login)
    {
        // Verifica se já existe login
        bool loginExiste = _empresa.Usuarios.Any(c => c.Login == login);

        if(loginExiste)
        {
            return null;
        }

        Cliente cliente = new Cliente
        {
            Nome = nome,
            Email = email,
            Login = login,
            Senha = senha
        };

        // Adiciona à lista de clientes da classe Empresa
        _empresa.Clientes.Add(cliente);
        return cliente;
    }

    // Busca de cliente por login
    // Uso do ? | pode retornar um vazio
    public Cliente? BuscarClientePorLogin(string login)
    {
        // retorna o cliente da lista da empresa
        return _empresa.Clientes.FirstOrDefault(c => c.Login == login);
    }

    public Cliente? BuscarClientePorNome(string nome)
    {   
        // Verifico antes se o Nome é null | evitando warning de nullable
        return _empresa.Clientes.FirstOrDefault(c => 
        c.Nome != null && c.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
    }

    // Listar compras do cliente
    public List<Compra> ListarCompras(Cliente cliente)
    {
        return cliente.HistoricoCompras;
    }
}