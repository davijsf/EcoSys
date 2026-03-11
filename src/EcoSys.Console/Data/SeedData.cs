using EcoSys.Core.Entities;
using EcoSys.Core.Enums;
using EcoSys.Core.Services;

namespace EcoSys.ConsoleApp.Data;

public static class SeedData
{
    public static Empresa Inicializar(
        ProdutoService produtoService,
        ClienteService clienteService,
        UsuarioService usuarioService)
    {
        Empresa empresa = new Empresa { Nome = "EcoSys" };

        // 1. PRIMEIRO CATEGORIAS
        empresa.Categorias.Add(new Categoria { Nome = "Castanhas" });
        empresa.Categorias.Add(new Categoria { Nome = "Grãos" });
        empresa.Categorias.Add(new Categoria { Nome = "Sementes" });
        empresa.Categorias.Add(new Categoria { Nome = "Mel & Doces" });

        // 2. DEPOIS TAGS (exatamente como usadas nos produtos)
        empresa.Tags.Add(new Tag { Nome = "Natural" });
        empresa.Tags.Add(new Tag { Nome = "Vegano" });
        empresa.Tags.Add(new Tag { Nome = "Sem glúten" });
        empresa.Tags.Add(new Tag { Nome = "Torrado" });
        empresa.Tags.Add(new Tag { Nome = "Rico em proteína" }); 
        empresa.Tags.Add(new Tag { Nome = "Rica em ômega 3" });   
        empresa.Tags.Add(new Tag { Nome = "Integral" });
        empresa.Tags.Add(new Tag { Nome = "Artesanal" });
        empresa.Tags.Add(new Tag { Nome = "Sem açúcar adicionado" });

        // =========================
        // PRODUTOS
        // =========================
        empresa.Produtos.Add(new Produto
        {
            Nome = "Castanha de caju",
            Preco = 45.90,
            UnidadeMedida = UnidadeMedida.QUILO,
            Categoria = empresa.Categorias.First(c => c.Nome == "Castanhas"),
            Tags = new List<Tag> 
            { 
                empresa.Tags.First(t => t.Nome == "Natural"),
                empresa.Tags.First(t => t.Nome == "Vegano"),
                empresa.Tags.First(t => t.Nome == "Sem glúten")
            }
        });

        empresa.Produtos.Add(new Produto
        {
            Nome = "Amendoim torrado",  // ← APENAS 1x!
            Preco = 22.50,
            UnidadeMedida = UnidadeMedida.QUILO,
            Categoria = empresa.Categorias.First(c => c.Nome == "Grãos"), 
            Tags = new List<Tag> 
            { 
                empresa.Tags.First(t => t.Nome == "Torrado"),
                empresa.Tags.First(t => t.Nome == "Vegano"),
                empresa.Tags.First(t => t.Nome == "Rico em proteína")
            }
        });

        empresa.Produtos.Add(new Produto
        {
            Nome = "Linhaça dourada",
            Preco = 18.90,
            UnidadeMedida = UnidadeMedida.QUILO,
            Categoria = empresa.Categorias.First(c => c.Nome == "Sementes"),
            Tags = new List<Tag> 
            { 
                empresa.Tags.First(t => t.Nome == "Integral"),
                empresa.Tags.First(t => t.Nome == "Rica em ômega 3"),
                empresa.Tags.First(t => t.Nome == "Vegano")
            }
        });

        empresa.Produtos.Add(new Produto
        {
            Nome = "Mel de caju",
            Preco = 38.00,
            UnidadeMedida = UnidadeMedida.QUILO,
            Categoria = empresa.Categorias.First(c => c.Nome == "Mel & Doces"),
            Tags = new List<Tag> 
            { 
                empresa.Tags.First(t => t.Nome == "Natural"),
                empresa.Tags.First(t => t.Nome == "Artesanal"),
                empresa.Tags.First(t => t.Nome == "Sem açúcar adicionado")
            }
        });

        // Produtos avulsos (unidade)
        empresa.Produtos.Add(new Produto
        {
            Nome = "Castanha de caju unidade",
            Preco = 2.50,
            UnidadeMedida = UnidadeMedida.UNIDADE,
            Categoria = empresa.Categorias.First(c => c.Nome == "Castanhas"),
            Tags = new List<Tag> 
            { 
                empresa.Tags.First(t => t.Nome == "Natural"),
                empresa.Tags.First(t => t.Nome == "Vegano")
            }
        });

        empresa.Produtos.Add(new Produto
        {
            Nome = "Amendoim unidade",
            Preco = 0.80,
            UnidadeMedida = UnidadeMedida.UNIDADE,
            Categoria = empresa.Categorias.First(c => c.Nome == "Grãos"),
            Tags = new List<Tag> 
            { 
                empresa.Tags.First(t => t.Nome == "Torrado")
            }
        });

        // =========================
        // LOJAS
        // =========================
        Loja aracati = new Loja
        {
            Cidade = "Aracati",
            Endereco = "Travessa da Cultura",
            HorarioAbertura = "08h",
            HorarioFechamento = "16h"
        };

        Loja russas = new Loja
        {
            Cidade = "Russas",
            Endereco = "Rua do Beco",
            HorarioAbertura = "07h",
            HorarioFechamento = "17h"
        };

        empresa.Lojas.Add(aracati);
        empresa.Lojas.Add(russas);

        // =========================
        // FUNCIONÁRIOS
        // =========================
        // Aracati
        usuarioService.AdicionarUsuario(new Funcionario
        {
            Login = "carlos",
            Senha = "111",
            Nome = "Carlos",
            Cargo = Cargo.CAIXA,
            Salario = 1700.00,
            Tipo = TipoUsuario.Funcionario,
            HorarioEntrada = "7h",
            HorarioSaida = "17h",
            RegimeContratual = RegimeContratual.CLT
        });

        usuarioService.AdicionarUsuario(new Funcionario
        {
            Login = "davi12",
            Senha = "222",
            Nome = "Davi",
            Cargo = Cargo.CAIXA,
            Salario = 1700.00,
            Tipo = TipoUsuario.Funcionario,
            RegimeContratual = RegimeContratual.CLT
        });

        usuarioService.AdicionarUsuario(new Funcionario
        {
            Login = "joseh",
            Senha = "1010",
            Nome = "José",
            Cargo = Cargo.GERENTE,
            Salario = 6000.00,
            Tipo = TipoUsuario.Funcionario,
            RegimeContratual = RegimeContratual.PJ
        });

        // Russas
        usuarioService.AdicionarUsuario(new Funcionario
        {
            Login = "pedro01",
            Senha = "0000",
            Nome = "Pedro",
            Cargo = Cargo.CAIXA,
            Salario = 1600.00,
            Tipo = TipoUsuario.Funcionario,
            RegimeContratual = RegimeContratual.CLT
        });

        usuarioService.AdicionarUsuario(new Funcionario
        {
            Login = "luka10",
            Senha = "5555",
            Nome = "Lucas",
            Cargo = Cargo.CAIXA,
            Salario = 1600.00,
            Tipo = TipoUsuario.Funcionario,
            RegimeContratual = RegimeContratual.CLT
        });

        usuarioService.AdicionarUsuario(new Funcionario
        {
            Login = "levi05",
            Senha = "5255",
            Nome = "Levi",
            Cargo = Cargo.REPOSITOR,
            Salario = 1500.00,
            Tipo = TipoUsuario.Funcionario,
            RegimeContratual = RegimeContratual.CLT
        });

        usuarioService.AdicionarUsuario(new Funcionario
        {
            Login = "jairo78",
            Senha = "6655",
            Nome = "Jairo",
            Cargo = Cargo.GERENTE,
            Salario = 5600.00,
            Tipo = TipoUsuario.Funcionario,
            RegimeContratual = RegimeContratual.CLT
        });

        // =========================
        // CLIENTES
        // =========================
        Cliente clienteDv = new Cliente
        {
            Login = "dv",
            Senha = "0",
            Nome = "Davi",
            Email = "dv@email.com"
        };
        empresa.Clientes.Add(clienteDv);

        Cliente clienteJs = new Cliente
        {
            Login = "js",
            Senha = "1",
            Nome = "João",
            Email = "js@email.com"
        };
        empresa.Clientes.Add(clienteJs);

        // Adiciona no UsuarioService (para login funcionar)
        usuarioService.AdicionarUsuario(clienteDv);
        usuarioService.AdicionarUsuario(clienteJs);

        return empresa;
    }
}
