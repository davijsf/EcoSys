namespace EcoSys.Core.Services;
using EcoSys.Core.Entities;


public class ProdutoService
{
    public Empresa? empresa { get; }

    public ProdutoService (Empresa? empresa = null) {
        this.empresa = empresa;
    }

    public bool CadastrarProduto(Produto novoProduto)
    {
        if (empresa == null) 
        {
            Console.WriteLine("ERRO: ProdutoService sem empresa!");
            return false;
        }
        
        foreach(var produto in empresa.Produtos)
        {
            // Verifica se já existe um produto com o mesmo nome. Se sim, sai da função.
            if (produto.Nome.Equals(novoProduto.Nome, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
        }
        // Se não existir, adiciona à lista de produtos
        empresa.Produtos.Add(novoProduto);
        return true;
    }

    public List<Produto> ListarProdutos()
    {
        return empresa.Produtos;
    }

    public Produto? BuscarProdutoPorNome(string nome)
    {   
        return empresa.Produtos.FirstOrDefault(p => p.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
    }

    public List<Produto> BuscarProdutoPorCategoria(Categoria categoria)
    {
        // Cria uma lista vazia para os produtos encontrados
        List<Produto> resultado = new List<Produto>();

        foreach(var produto in empresa.Produtos)
        {   // Verifica se a categoria é a mesma
            if (produto.Categoria.Nome.Equals(categoria.Nome, StringComparison.OrdinalIgnoreCase))
            {
                // Se sim, adiciona o produto à lista de resultados
                resultado.Add(produto);
            }
        }
        return resultado;
    }

    public List<Produto> BuscarProdutoPorTags(Tag tag)
    {
        // Cria uma lista vazia para os produtos encontrados
        List<Produto> resultado = new List<Produto>();

        // percorre produtos
        foreach (var produto in empresa.Produtos)
        {   
            // percorre tags do produto 
            foreach (var t in produto.Tags)
            {
                if (t.Nome.Equals(tag.Nome, StringComparison.OrdinalIgnoreCase))
                {
                    resultado.Add(produto);
                    break; // Evita add o mesmo produto duas vezes
                }
            }
        }
        return resultado;
    }

    public bool RemoverProduto(string nome)
    {
        var produto = BuscarProdutoPorNome(nome);

        if (produto != null)
        {
            empresa.Produtos.Remove(produto);
            return true;
        }

        return false;
    }

    public bool AtualizarPrecoProduto(string nome, double novoPreco)
    {
        var produto = BuscarProdutoPorNome(nome);

        if (produto != null)
        {
            produto.Preco = novoPreco;
            return true;
        }

        return false;
    }
}