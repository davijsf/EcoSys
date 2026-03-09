namespace EcoSys.Core.Services;
using EcoSys.Core.Entities;


public class ProdutoService
{
    public void CadastrarProduto(List<Produto> produtos, Produto novoProduto)
    {
        produtos.Add(novoProduto);
    }

    public List<Produto> ListarProdutos(List<Produto> produtos)
    {
        return produtos;
    }

    public Produto? BuscarProdutoPorNome(List<Produto> produtos, string nome)
    {   
        foreach (var produto in produtos)
        {
            if (produto.Nome.ToLower() == nome.ToLower())
            {
                return produto;
            }
        }
        return null;
    }

    public List<Produto> BuscarProdutoPorCategoria(List<Produto> produtos, Categoria categoria)
    {
        // Cria uma lista vazia para os produtos encontrados
        List<Produto> resultado = new List<Produto>();

        foreach(var produto in produtos)
        {   // Verifica se a categoria é a mesma
            if (produto.categoria == categoria)
            {
                // Se sim, adiciona o produto à lista de resultados
                resultado.Add(produto);
            }
        }
        return resultado;
    }

    public List<Produto> BuscarProdutoPorTags(List<Produto> produtos, Tag tag)
    {
        // Cria uma lista vazia para os produtos encontrados
        List<Produto> resultado = new List<Produto>();

        foreach (var produto in produtos)
        {      // Verifica se têm produtos com as tags pedidas
            if (produto.Tags.Contains(tag))
            {   // Se sim, adiciona à lista de resultados
                resultado.Add(produto);
            }
        }      
        return resultado;
    }
}