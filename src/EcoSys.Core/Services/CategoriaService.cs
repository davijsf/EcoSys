namespace EcoSys.Core.Services;
using EcoSys.Core.Entities;

public class CategoriaService
{
    private List<Categoria> categorias = new List<Categoria>();

    public void CadastrarCategoria (string nomeCategoria)
    {
        // Verifica se a categoria informada já existe
        foreach (var categoria in categorias)
        {
            if (categoria.Nome.ToLower() == nomeCategoria.ToLower())
            {
                Console.WriteLine("Categoria já cadastrada");
                return;
            }
        }

        // Criar nova categoria
        Categoria novaCategoria = new Categoria
        {
            Id = categorias.Count + 1,
            Nome = nomeCategoria
        };

        // Adiciona à lista
        categorias.Add(novaCategoria);

        Console.WriteLine("Categoria cadastrada com sucesso.");
    }

    public List<Categoria> ListarCategorias ()
    {
        return categorias;
    }

    public Categoria? BuscarCategoriaPorNome(string nome)
    {
        foreach (var categoria in categorias)
        {
            if (categoria.Nome.ToLower() == nome.ToLower())
            {
                return categoria;
            }
        }
        return null;
    }
}