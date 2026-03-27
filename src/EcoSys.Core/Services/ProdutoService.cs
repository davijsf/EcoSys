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
        bool jaExiste = _empresa.Produtos.Any(p => 
        p.Nome?.Equals(novoProduto.Nome, StringComparison.OrdinalIgnoreCase) == true);

        if (jaExiste) return false;

        // Inserção auto - codigo de barras:
        novoProduto.Id = GerarCodigoBarras();

        // Se não existir, adiciona à lista de produtos
        _empresa.AdicionarProduto(novoProduto);
        return true;
    }

    private string GerarCodigoBarras()
    {
        int nextNumber = _empresa.Produtos.Count + 1;
        return nextNumber.ToString("D8");
    }

    public IReadOnlyList<Produto> ListarProdutos()
    {
        return _empresa.Produtos;
    }

    public Produto? BuscarProduto(string busca)
    {   
        return _empresa.Produtos.FirstOrDefault(p => 
        p.Id == busca ||
        p.Nome?.Equals(busca, StringComparison.OrdinalIgnoreCase) == true);
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

    public bool RemoverProduto(string busca)
    {
        var produto = BuscarProduto(busca);

        if (produto != null)
        {
            _empresa.RemoverProduto(produto);
            return true;
        }

        return false;
    }

    public bool AtualizarPrecoProduto(string nome, double novoPreco)
    {
        var produto = BuscarProduto(nome);

        if (produto != null)
        {
            produto.Preco = novoPreco;
            return true;
        }

        return false;
    }
}