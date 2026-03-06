using EcoSys.Core.Enum;
namespace  EcoSys.Core.Entities;

public class Produto
{
    public string Nome {get ; set ;}
    public double Preco {get ; set ;}

    public TipoVenda TipoVenda {get ; set ;}
    
    public Categoria categoria {get ; set ;}

    // Produto pode ter várias tags: Integral | vegano | Sem açúcar [...]
    public List<Tag> Tags {get ; set ;} = new List<Tag>();
}