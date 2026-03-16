namespace EcoSys.Core.Services;
using EcoSys.Core.Entities;

public class CategoriaService
{
    private readonly Empresa _empresa;

    public CategoriaService(Empresa empresa)
    {
        _empresa = empresa ?? throw new ArgumentNullException(nameof(empresa));
    }

    public void CadastrarCategoria (string nomeCategoria)
    {
        // Verifica se a categoria informada já existe
            if (_empresa?.Categorias.Any(c =>  
            c.Nome.Equals(nomeCategoria, StringComparison.OrdinalIgnoreCase)) == true)  
            {
                Console.WriteLine("Categoria já cadastrada");
                return;
            }

        // Adiciona à lista de categorias da empresa
        _empresa?.Categorias.Add(new Categoria {

            Nome = nomeCategoria

        });

        Console.WriteLine("Categoria cadastrada com sucesso.");
    }

    public List<Categoria> ListarCategorias ()
    {
        return _empresa.Categorias;
    }

    public Categoria? BuscarCategoriaPorNome(string nome)
    {
        return _empresa?.Categorias.FirstOrDefault(c => 
        c.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
    }

    public bool RemoverCategoria(string nome)
    {
        var categoria = BuscarCategoriaPorNome(nome);
        if (categoria != null)
        {
            _empresa.Categorias.Remove(categoria);
            return true;
        }
        return false;
    }
}