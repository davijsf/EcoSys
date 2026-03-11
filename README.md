🌿 EcoSys

O EcoSys é um sistema de gerenciamento desenvolvido em C# (.NET) para auxiliar no controle de uma empresa de varejo de produtos naturais.

O sistema informatiza processos básicos de uma rede de lojas físicas, permitindo o gerenciamento de produtos, clientes, funcionários e compras através de uma interface interativa via terminal.

Este projeto foi desenvolvido como atividade acadêmica da disciplina Laboratório de Programação 2 (LP2), com foco na aplicação de conceitos de Programação Orientada a Objetos, arquitetura em camadas e boas práticas de desenvolvimento em C#.


🎯 Objetivos do Sistema

O sistema busca resolver tarefas comuns de uma empresa de varejo, como:

gerenciamento de produtos

cadastro e controle de clientes

autenticação de usuários

registro de compras

organização de produtos por categorias e tags

navegação por menus no terminal

🧱 Arquitetura do Projeto

O projeto foi estruturado em duas camadas principais.

EcoSys.Core

Responsável pela regra de negócio e domínio da aplicação.

Contém:

entidades do sistema

enumerações

serviços responsáveis pelas operações

EcoSys.Console

Responsável pela interface com o usuário.

Contém:

menus interativos

fluxo de execução do sistema

inicialização de dados

Estrutura do Projeto
EcoSys
│
├─ src
│   ├─ EcoSys.Console
│   │   ├── Application.cs
│   │   ├── Data
│   │   │    └── SeedData.cs
│   │   └── Menus
│   │
│   └─ EcoSys.Core
│       ├── Entities
│       ├── Enums
│       └── Services


🛠️ Tecnologias Utilizadas

C#

.NET

Programação Orientada a Objetos

LINQ

Console Application

Git

GitHub

👨‍💻 Autor

Davi José da Silva Ferreira
Aracati - Ceará, Brasil
2026
