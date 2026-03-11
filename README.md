EcoSys
Sistema de gerenciamento para uma empresa de varejo de produtos naturais (EcoSys), desenvolvido como projeto acadêmico para a disciplina de LP2 - Laboratório de Programação 2.

O sistema informatiza o fluxo de trabalho de uma rede de lojas físicas em Aracati e Russas-CE, comercializando produtos como castanha de caju, amendoim torrado, linhaça dourada, mel de caju e outros itens naturais.

Funcionalidades Implementadas
text
👤 AUTENTICAÇÃO
├── Funcionários: carlos/111, davi12/222, joseh/1010 (Aracati)
└── Clientes: dv/0, js/1

🛒 MENU FUNCIONÁRIO
├── 1️⃣  Produtos (CRUD + Busca)
├── 2️⃣  Clientes (CRUD)
├── 3️⃣  Tags (CRUD)
├── 4️⃣  Compras (Realizar + Histórico)
└── 5️⃣  Sair

🔍 BUSCA INTELIGENTE
├── "dv" → Cliente Davi
├── "castanha" → Castanha de caju (+unidade)
└── "castanhas" → Todos os produtos
📁 Estrutura do Projeto
text
EcoSys
│
├─ src
│   ├─ EcoSys.Console
│   │   ├── Application.cs (Main)
│   │   ├── Data/SeedData.cs (6 produtos + 9 usuários)
│   │   └── Menus/ (ProdutoMenu, CompraMenu...)
│   │
│   └─ EcoSys.Core
│       ├── Entities/ (Empresa, Produto, Cliente...)
│       ├── Enums/ (Cargo, UnidadeMedida...)
│       └── Services/ (ProdutoService, ClienteService...)
EcoSys.Core - Camada de Domínio
text
📦 Entidades: Empresa, Loja, Funcionario, Cliente, Produto, Categoria, Tag, Compra
⚙️  Services: ProdutoService, ClienteService, UsuarioService, CompraService
🎯 Enums: Cargo, RegimeContratual, UnidadeMedida, TipoUsuario
EcoSys.Console - Interface de Usuário
text
🎮 Menus interativos via terminal
🔄 Validação de entrada
✅ SeedData com dados realistas
🏪 Dados Iniciais Carregados
Categoria	Produtos	Preço/kg	Tags
Castanhas	Castanha de caju	R$45,90	Natural, Vegano
Grãos	Amendoim torrado	R$22,50	Torrado, Rico em proteína
Sementes	Linhaça dourada	R$18,90	Rica em ômega 3
Mel & Doces	Mel de caju	R$38,00	Artesanal
👥 Usuários: 7 funcionários + 2 clientes
🏬 Lojas: Aracati, Russas

🚀 Como Executar
bash
# 1. Clonar repositório
git clone https://github.com/davijsf/EcoSys.git
cd EcoSys

# 2. Executar sistema
dotnet run --project src/EcoSys.Console

# 3. Teste login:
# Funcionário: carlos/111 → Menu completo
# Cliente: dv/0 → Compras + Histórico
✅ Status do Projeto
✅ FUNCIONANDO	🔄 Em Desenvolvimento
Autenticação	Relatórios gerenciais
CRUD Produtos	Estoque por loja
CRUD Clientes	Vendas por funcionário
Busca inteligente	Export PDF
Menus funcionário	Backup automático
SeedData (6 produtos)	Interface web
🛠️ Tecnologias
text
🌐 C# 12 (.NET 8+)
⚡ Console Application
📊 LINQ + Collections
🔍 Dependency Injection
🐛 Git + GitHub
💻 VS Code
📞 Acesso Rápido
text
🚀 INICIAR: dotnet run --project src/EcoSys.Console
🔑 GERENTE: carlos/111
👤 CLIENTE: dv/0
📋 PRODUTOS: 1 → Listar/buscar
🛒 COMPRAS: 4 → Realizar/histórico
✍️ Autor
Davi José da Silva Ferreira
Aracati, Ceará - Brasil
Março 2026
