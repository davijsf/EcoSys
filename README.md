# EcoSys

Sistema de gerenciamento para uma empresa de varejo de **produtos naturais**, desenvolvido como projeto acadêmico para a disciplina de **LP2**.

O objetivo do sistema é informatizar o fluxo de trabalho da empresa, que possui lojas físicas e comercializa produtos como castanha, amendoim, linhaça, mel de caju e outros itens naturais.

---

## Estrutura do Projeto

O projeto está organizado em duas camadas principais:

```
EcoSys
│
├─ src
│   ├─ EcoSys.Console
│   │   Aplicação de console responsável pela interação com o usuário
│   │
│   └─ EcoSys.Core
│       Contém as entidades, enums e regras centrais do sistema
```

### EcoSys.Core

Responsável pela modelagem do domínio do sistema.

**Entities**

* Empresa
* Loja
* Funcionario
* Cliente
* Produto
* Categoria
* Tag
* Compra

**Enums**

* Cargo
* RegimeContratual
* TipoVenda

### EcoSys.Console

Projeto responsável por executar o sistema e permitir interação via terminal.

---

## Regras de Negócio Modeladas

* A empresa possui **múltiplas lojas físicas**.

* Cada loja possui **funcionários com cargos específicos**.

* Funcionários possuem:

  * salário
  * horário de entrada e saída
  * regime contratual (CLT ou CNPJ)

* A loja comercializa **produtos naturais**.

* Cada produto:

  * pertence a **uma única categoria**
  * possui **múltiplas tags**
  * pode ser vendido por **unidade ou por quilo**.

---

## Tecnologias Utilizadas

* C#
* .NET
* Git
* VS Code

---

## Como executar o projeto

1. Clone o repositório

```
git clone <url-do-repositorio>
```

2. Acesse a pasta do projeto

```
cd EcoSys
```

3. Execute o projeto de console

```
dotnet run --project src/EcoSys.Console
```

---

## Autor

Davi
Bacharelado em Ciência da Computação
