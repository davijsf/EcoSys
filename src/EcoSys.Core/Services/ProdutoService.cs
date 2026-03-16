namespace EcoSys.Core.Services;
using EcoSys.Core.Entities;


public class ProdutoService
{
    private readonly Empresa _empresa;

    public ProdutoService (Empresa empresa) {
        _empresa = empresa ?? throw new ArgumentNullException(nameof(empresa));
    }

    public bool CadastrarProduto(Produto novoProduto)
    {   
        foreach(var produto in _empresa.Produtos)
        {
            // Verifica se já existe um produto com o mesmo nome. Se sim, sai da função.
            if (produto.Nome?.Equals(novoProduto.Nome, StringComparison.OrdinalIgnoreCase) == true)
            {
                return false;
            }
        }
        // Se não existir, adiciona à lista de produtos
        _empresa.Produtos.Add(novoProduto);
        return true;
    }

    public List<Produto> ListarProdutos()
    {
        return _empresa.Produtos;
    }

    public Produto? BuscarProdutoPorNome(string nome)
    {   
        return _empresa.Produtos.FirstOrDefault(p => p.Nome?.Equals(nome, StringComparison.OrdinalIgnoreCase) == true);
    }


    public List<Produto> BuscarProdutoPorCategoria(Categoria categoria)
    {
        return _empresa.Produtos
        .Where(p => p.Categoria?.Nome?.Equals(categoria.Nome, StringComparison.OrdinalIgnoreCase)== true)
        .ToList();
    }

    public List<Produto> BuscarProdutoPorTags(Tag tag)
    {
        return _empresa.Produtos
        .Where(p => p.Tags?.Any(t => t.Nome?.Equals(tag.Nome, StringComparison.OrdinalIgnoreCase) == true) 
        == true)
        .ToList();
    }

    public bool RemoverProduto(string nome)
    {
        var produto = BuscarProdutoPorNome(nome);

        if (produto != null)
        {
            _empresa.Produtos.Remove(produto);
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