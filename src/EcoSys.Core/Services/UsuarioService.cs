namespace EcoSys.Core.Entities;

public class UsuarioService
{
    private readonly Empresa _empresa;

    public UsuarioService(Empresa empresa)
    {
        _empresa = empresa ?? throw new ArgumentNullException(nameof(empresa));
    }

    public void AdicionarUsuario(Usuario usuario)
    {
        // Adiciona na lista geral de users
        // Mas verifico antes se já existe esse usuario
        if (_empresa.Usuarios.Any( u => u.Login.Equals(usuario.Login, StringComparison.OrdinalIgnoreCase)) == true)
        {
            Console.WriteLine("Usuário já cadastrado.");
            return;
        }

        // Se não, cadastro ele na lista geral de users
        _empresa.Usuarios.Add(usuario);
    }

    public Usuario? Autenticar(string login, string senha)
    {
        // Autenticação na lista geral de usuários
        return _empresa.Usuarios.FirstOrDefault(
            u => u.Login == login && u.Senha == senha
        );
    }
}