namespace EcoSys.Core.Services;
using EcoSys.Core.Entities;

public class ClienteService
{
    public Empresa? empresa;

    public ClienteService (Empresa empresa = null)
    {
        this.empresa = empresa;
    } 

//     public Cliente CadastrarCliente(Empresa empresa, string nome, string email, string senha, string login)
//     {
//     // Verifica se já existe login
//     bool loginExiste = empresa.Clientes.Any(c => c.Login == login);

//     if(loginExiste)
//     {
//         throw new Exception("Login já existente.");
//     }

//     Cliente cliente = new Cliente
//     {
//         Nome = nome,
//         Email = email,
//         Login = login,
//         Senha = senha
//     };

//     // Adiciona à lista de clientes da classe Empresa
//     empresa.Clientes.Add(cliente);
//     return cliente;
// }

    // Busca de cliente por login
    // Uso do ? | pode retornar um vazio
    public Cliente? BuscarClientePorLogin(string login)
    {
        // retorna o cliente da lista da empresa
        return empresa.Clientes.FirstOrDefault(c => c.Login == login);
    }

    public Cliente? BuscarClientePorNome(string nome)
    {   
        // Verifico antes se o Nome é null | evitando warning de nullable
        return empresa.Clientes.FirstOrDefault(c => 
        c.Nome != null && c.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
    }

    // Listar compras do cliente
    public List<Compra> ListarCompras(Cliente cliente)
    {
        return cliente.HistoricoCompras;
    }
}