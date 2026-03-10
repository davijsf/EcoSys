namespace EcoSys.Core.Entities;

using EcoSys.Core.Enums;

public class Funcionario
{
    public string Nome {get ; set ;}

    // Utilizando Enum: CAIXA, REPOSITOR E GERENTE
    public Cargo Cargo {get ; set ;}
    
    public double Salario {get ; set ;}
    
    public string HorarioEntrada {get ; set ;}
    public string HorarioSaida {get ; set ;}

    public RegimeContratual RegimeContratual {get ; set ;}
}