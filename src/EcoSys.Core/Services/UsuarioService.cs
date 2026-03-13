namespace EcoSys.Core.Entities;

public class UsuarioService
{
    private List<Usuario> usuarios = new();
    Empresa empresa = new Empresa();

    public void AdicionarUsuario(Usuario usuario)
    {
        // Adiciona na lista geral de users
        // Mas verifico antes se já existe esse usuario
        if (usuarios?.Any( u => u.Login.Equals(usuario.Login, StringComparison.OrdinalIgnoreCase)) == true)
        {
            Console.WriteLine("Usuário já cadastrado.");
            return;
        }

        // Se não, cadastro ele na lista geral de users
        usuarios.Add(usuario);


        if (usuario is Cliente cliente)
        {   
            // Forçando o tipo correto
            usuario.Tipo = Enums.TipoUsuario.Cliente;
            // Adiciona à empresa 
            empresa.Clientes.Add(cliente);
        }

        else if (usuario is Funcionario funcionario)
        {   
            usuario.Tipo = Enums.TipoUsuario.Funcionario;
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