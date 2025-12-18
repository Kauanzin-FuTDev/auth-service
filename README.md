# 🔐 auth-service

Serviço de autenticação e autorização em .NET 8, organizado em camadas,
com foco em login e segurança de API.  
Este projeto é a base de um **auth microservice** para aplicações modernas.

> ⚙️ Arquitetura inspirada em Clean/DDD com separação clara:
> Domain → Application → Infraestructer → IOC

---

## 🧩 Descrição

Este serviço fornece:
- Autenticação de usuários
- Validação de credenciais
- Emissão de tokens JWT (ainda nao implementado)
- Esqueleto de API REST para login e proteção de rotas

⚠️ **Este projeto está em desenvolvimento (MVP/POC).**

---

## 🛠️ Tecnologias

| Item | Detalhes |
|------|----------|
| Linguagem | C# |
| Plataforma | .NET 7/8 |
| Arquitetura | Camadas, IOC |
| Repositórios de domínio | `Domain`, `Application` |
| Dependency Injection | `IOC` |
| Infraestrutura | `Infraestructer` |
| Solução IDE | Visual Studio / Rider |

---

## 🚀 Como rodar local

### Pré-requisitos

- [.NET SDK 7 ou 8](https://dotnet.microsoft.com/)
- IDE: Visual Studio / Rider / VSCode


---

