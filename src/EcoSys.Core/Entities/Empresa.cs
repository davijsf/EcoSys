namespace EcoSys.Core.Entities;

public class Empresa {
    public string Nome {get ; set ;}
    public List<Loja> Lojas {get ; set ;} = new List<Loja>();

    // Lista de clientes
    public List<Cliente> Clientes {get ; set;} = new List<Cliente>();   

}