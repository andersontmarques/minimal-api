# MinimalAPI

API desenvolvida em .NET 8 com arquitetura minimalista para gerenciamento de usuários e autenticação.

## Tecnologias Implementadas

### Backend
- **.NET 8** - Framework principal
- **ASP.NET Core Minimal APIs** - Endpoints simplificados
- **BCrypt.Net** - Criptografia de senhas
- **JWT (System.IdentityModel.Tokens.Jwt)** - Autenticação via tokens

### Banco de Dados
- **MongoDB** - Banco de dados NoSQL
- **MongoDB.Driver** - Driver oficial para .NET
- **Redis** - Cache em memória para sessões

### Containerização
- **Docker** - Containerização da aplicação
- **Alpine Linux** - Imagem base otimizada

### Orquestração
- **Kubernetes** - Orquestração de containers
- **Namespace**: `mini-api`

## Endpoints

### Usuários
- **GET** `/users` - Lista todos os usuários
- **POST** `/users` - Cria novo usuário
  ```json
  {
    "name": "string",
    "email": "string",
    "password": "string"
  }
  ```

### Autenticação
- **POST** `/auth/login` - Autenticação de usuário
  ```json
  {
    "email": "string",
    "password": "string"
  }
  ```
  **Resposta (200 OK)**:
  ```json
  {
    "success": true,
    "message": "Login successful",
    "token": "jwt_token_here"
  }
  ```
  **Resposta (401 Unauthorized)**: Credenciais inválidas

### Documentação
- **GET** `/swagger` - Documentação interativa da API

## Configuração

### Desenvolvimento Local
```json
{
  "ConnectionStrings": {
    "MongoDB": "mongodb://localhost:27017",
    "Redis": "localhost:6379"
  }
}
```

### Produção (Kubernetes)
```json
{
  "ConnectionStrings": {
    "MongoDB": "mongodb://mongodb-service:27017",
    "Redis": "redis-service:6379"
  }
}
```

## Deploy

### Docker
```bash
docker build -t minimalapi:latest .
docker run -p 8080:8080 minimalapi:latest
```

### Kubernetes
```bash
kubectl apply -f k8s-deployment.yaml
```

## Recursos Kubernetes
- **MongoDB**: Deployment + Service
- **Redis**: Deployment + Service  
- **MinimalAPI**: Deployment + LoadBalancer Service
- **Namespace**: `mini-api`