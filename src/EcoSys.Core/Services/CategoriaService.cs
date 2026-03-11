namespace EcoSys.Core.Services;
using EcoSys.Core.Entities;

public class CategoriaService
{
    public Empresa? empresa { get; }

    public CategoriaService(Empresa? empresa = null)
    {
        this.empresa = empresa;
    }

    public void CadastrarCategoria (string nomeCategoria)
    {
        // Verifica se a categoria informada já existe
            if (empresa?.Categorias.Any(c =>  
            c.Nome.Equals(nomeCategoria, StringComparison.OrdinalIgnoreCase)) == true)  
            {
                Console.WriteLine("Categoria já cadastrada");
                return;
            }

        // Adiciona à lista de categorias da empresa
        empresa?.Categorias.Add(new Categoria {

            Nome = nomeCategoria

        });

        Console.WriteLine("Categoria cadastrada com sucesso.");
    }

    public List<Categoria> ListarCategorias ()
    {
        return empresa.Categorias;
    }

    public Categoria? BuscarCategoriaPorNome(string nome)
    {
        return empresa?.Categorias.FirstOrDefault(c => 
        c.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
    }

    public bool RemoverCategoria(string nome)
    {
        var categoria = BuscarCategoriaPorNome(nome);
        if (categoria != null)
        {
            empresa.Categorias.Remove(categoria);
            return true;
        }
        return false;
    }
}