# 🚀 API de Gerenciamento de Pedidos e Estoque

API REST para gerenciamento de pedidos e estoque desenvolvida em **ASP.NET Core 9.0**, oferecendo operações completas de CRUD com autenticação JWT e controle de inventário.

---

## 📋 Sobre o Projeto

Sistema robusto de gerenciamento de pedidos e estoque que permite criar pedidos, gerenciar produtos, controlar inventário e consultar com filtros avançados e paginação.

---

## ✨ Funcionalidades

### 🛒 Gestão de Pedidos
- ✅ **Criar novo pedido**
- ➕ **Adicionar produtos** ao pedido
- ➖ **Remover produtos** do pedido  
- 🔒 **Fechar pedido** (finalizar)
- 📋 **Listar pedidos** com paginação
- 🔍 **Consultar pedido** por ID
- 📊 **Filtros** por status do pedido

### 📦 Gestão de Produtos & Estoque
- 🏷️ **Adicionar/Atualizar produto** com controle de estoque
- 🗑️ **Excluir produto** do catálogo
- 📄 **Listar produtos** com paginação
- 🔄 **Verificação automática** de estoque nas vendas
- ⚠️ **Validação** de disponibilidade para pedidos

---

## 🔒 Regras de Negócio

### 📦 Gestão de Pedidos
- 🚫 **Produtos não podem ser alterados** em pedidos fechados
- 📦 **Pedido precisa ter pelo menos 1 produto** para ser fechado
- 🔐 **Autenticação obrigatória** para operações sensíveis

### 🏷️ Controle de Estoque
- ⚠️ **Não permitir venda** se quantidade em estoque for insuficiente
- 🔄 **Atualização automática** do estoque ao fechar pedido
- 📊 **Validação em tempo real** da disponibilidade
- 🛡️ **Impedir exclusão** de produtos com estoque positivo

---

## 🛠️ Stack Tecnológica

| Tecnologia | Versão | Finalidade |
|------------|--------|------------|
| **ASP.NET Core** | 9.0 | Framework principal |
| **Entity Framework Core** | 9.0 | ORM (InMemory Database) |
| **Swagger/OpenAPI** | - | Documentação da API |
| **JWT Authentication** | - | Autenticação segura |

---

## 🚀 Como Usar

### Pré-requisitos
- .NET 9.0 SDK
- Visual Studio 2022

# Clone o repositório
git clone https://github.com/CrisRogoy/Pedidos.git
