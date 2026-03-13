namespace EcoSys.Core.Entities;

public class Loja : Empresa
{
    public string Cidade{get ; set ;}
    public string Endereco {get ; set ;}
    public string HorarioAbertura {get ; set ;}
    public string HorarioFechamento {get ; set ;}

    public List<Funcionario> Funcionarios {get ; set ;} = new List<Funcionario>(); 
}