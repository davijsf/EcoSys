namespace EcoSys.Core.Entities;

public class Empresa {
    public string ? Nome {get ; set ;}
    
    public List<Loja> Lojas {get ; set ;} = new List<Loja>();
    public List<Cliente> Clientes {get ; set;} = new List<Cliente>();  
    public List<Funcionario> Funcionarios {get; set;} = new List<Funcionario>(); 

    private readonly List<Produto> _produtos = new();
    public IReadOnlyList<Produto> Produtos => _produtos;

    internal void AdicionarProduto(Produto p) => _produtos.Add(p);
    internal void RemoverProduto(Produto p) => _produtos.Remove(p);

    public List<Categoria> Categorias {get; set;} = new List<Categoria>();
    public List<Tag> Tags {get; set;} = new List<Tag>();
    public List<Compra> Compras {get ; set ;} = new List<Compra>();
    public List<Usuario> Usuarios {get ; set ;} = new List<Usuario>();
    
}