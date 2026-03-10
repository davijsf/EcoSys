namespace  EcoSys.Core.Entities;
using EcoSys.Core.Enums;

public class Produto
{
    public string Nome {get ; set ;}
    public double Preco {get ; set ;}

    public UnidadeMedida UnidadeMedida {get ; set ;}
    
    public Categoria Categoria {get ; set ;}

    // Produto pode ter várias tags: Integral | vegano | Sem açúcar [...]
    public List<Tag> Tags {get ; set ;} = new List<Tag>();
}