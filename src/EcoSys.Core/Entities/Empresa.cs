namespace EcoSys.Core.Entities;

public class Empresa {
    private string Nome {get ; set ;}
    private List<Loja> Lojas {get ; set ;} = new List<Loja>();

}