namespace EcoSys.Core.Entities;

public class Empresa {
    public string Nome {get ; set ;}
    
    public List<Loja> Lojas {get ; set ;} = new List<Loja>();

    // Lista de clientes
    public List<Cliente> Clientes {get ; set;} = new List<Cliente>();  

    // Lista de funcionários
    public List<Funcionario> Funcionarios {get; set;} = new List<Funcionario>(); 

    // Lista de produtos
    public List<Produto> Produtos {get; set ;} = new List<Produto>();

    // Lista de categorias
    public List<Categoria> Categorias {get; set;} = new List<Categoria>();

    // Lista de tags
    public List<Tag> Tags {get; set;} = new List<Tag>();

}