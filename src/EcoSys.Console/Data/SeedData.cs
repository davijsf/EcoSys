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
        // =========================
        // EMPRESA
        // =========================
        Empresa empresa = new Empresa
        {
            Nome = "EcoSys"
        };

        // =========================
        // PRODUTOS
        // =========================
       // Na seção PRODUTOS do SeedData.Inicializar(), substitua por:

// =========================
// PRODUTOS - LOJA DE CASTANHAS E GRÃOS
// =========================

    produtoService.CadastrarProduto(new Produto
    {
        Nome = "Castanha de caju",
        Preco = 45.90,
        UnidadeMedida = UnidadeMedida.QUILO,
        Categoria = new Categoria { Nome = "Castanhas" },
        Tags = new List<Tag> 
        { 
            new Tag { Nome = "Natural" },
            new Tag { Nome = "Vegano" },
            new Tag { Nome = "Sem glúten" }
        }
    });

    produtoService.CadastrarProduto(new Produto
    {
        Nome = "Amendoim torrado",
        Preco = 22.50,
        UnidadeMedida = UnidadeMedida.QUILO,
        Categoria = new Categoria { Nome = "Grãos" },
        Tags = new List<Tag> 
        { 
            new Tag { Nome = "Torrado" },
            new Tag { Nome = "Vegano" },
            new Tag { Nome = "Rico em proteína" }
        }
    });

    produtoService.CadastrarProduto(new Produto
    {
        Nome = "Linhaça dourada",
        Preco = 18.90,
        UnidadeMedida = UnidadeMedida.QUILO,
        Categoria = new Categoria { Nome = "Sementes" },
        Tags = new List<Tag> 
        { 
            new Tag { Nome = "Integral" },
            new Tag { Nome = "Rica em ômega 3" },
            new Tag { Nome = "Vegano" }
        }
    });

    produtoService.CadastrarProduto(new Produto
    {
        Nome = "Mel de caju",
        Preco = 38.00,
        UnidadeMedida = UnidadeMedida.QUILO,
        Categoria = new Categoria { Nome = "Mel & Doces" },
        Tags = new List<Tag> 
        { 
            new Tag { Nome = "Natural" },
            new Tag { Nome = "Artesanal" },
            new Tag { Nome = "Sem açúcar adicionado" }
        }
    });

    // Produtos avulsos (unidade)
    produtoService.CadastrarProduto(new Produto
    {
        Nome = "Castanha de caju unidade",
        Preco = 2.50,
        UnidadeMedida = UnidadeMedida.UNIDADE,
        Categoria = new Categoria { Nome = "Castanhas" },
        Tags = new List<Tag> 
        { 
            new Tag { Nome = "Natural" },
            new Tag { Nome = "Vegano" }
        }
    });

    produtoService.CadastrarProduto(new Produto
    {
        Nome = "Amendoim unidade",
        Preco = 0.80,
        UnidadeMedida = UnidadeMedida.UNIDADE,
        Categoria = new Categoria { Nome = "Grãos" },
        Tags = new List<Tag> 
        { 
            new Tag { Nome = "Torrado" }
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
            Tipo = TipoUsuario.Funcionario
        });

        usuarioService.AdicionarUsuario(new Funcionario
        {
            Login = "davi12",
            Senha = "222",
            Nome = "Davi",
            Cargo = Cargo.CAIXA,
            Tipo = TipoUsuario.Funcionario
        });

        usuarioService.AdicionarUsuario(new Funcionario
        {
            Login = "joseh",
            Senha = "1010",
            Nome = "José",
            Cargo = Cargo.GERENTE,
            Tipo = TipoUsuario.Funcionario
        });

        // Russas
        usuarioService.AdicionarUsuario(new Funcionario
        {
            Login = "pedro01",
            Senha = "0000",
            Nome = "Pedro",
            Cargo = Cargo.CAIXA,
            Tipo = TipoUsuario.Funcionario
        });

        usuarioService.AdicionarUsuario(new Funcionario
        {
            Login = "luka10",
            Senha = "5555",
            Nome = "Lucas",
            Cargo = Cargo.CAIXA,
            Tipo = TipoUsuario.Funcionario
        });

        usuarioService.AdicionarUsuario(new Funcionario
        {
            Login = "levi05",
            Senha = "5255",
            Nome = "Levi",
            Cargo = Cargo.REPOSITOR,
            Tipo = TipoUsuario.Funcionario
        });

        usuarioService.AdicionarUsuario(new Funcionario
        {
            Login = "jairo78",
            Senha = "6655",
            Nome = "Jairo",
            Cargo = Cargo.GERENTE,
            Tipo = TipoUsuario.Funcionario
        });

        // =========================
        // CLIENTES
        // =========================
        
        // 1. ADICIONA DIRETO na empresa.Clientes (para busca no login)
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

        // 2. ADICIONA no UsuarioService (para Autenticar funcionar)
        usuarioService.AdicionarUsuario(clienteDv);
        usuarioService.AdicionarUsuario(clienteJs);

        return empresa;
    }
}
