namespace EcoSys.Core.Entities;

public class UsuarioService
{
    private List<Usuario> usuarios = new();
    Empresa empresa = new Empresa();

    public void AdicionarUsuario(Usuario usuario)
    {
        // Sempre adiciona na lista geral de users
        usuarios.Add(usuario);


        if (usuario is Cliente cliente)
        {   
            // Adiciona à empresa 
            empresa.Clientes.Add(cliente);
        }

        else if (usuario is Funcionario funcionario)
        {   
            // Adiciona à empresa
            empresa.Funcionarios.Add(funcionario);
        }
    }

    public Usuario? Autenticar(string login, string senha)
    {
        // Autenticação na lista geral de usuários
        return usuarios.FirstOrDefault(
            u => u.Login == login && u.Senha == senha
        );
    }
}