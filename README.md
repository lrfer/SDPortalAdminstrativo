# Portal admistrativo
## Documentação

## Compilação

- Necessário a versão do .net 6.0 tanto SDK quanto Run time https://dotnet.microsoft.com/en-us/download
- Para start a aplicação precisa-se do comando``` dotnet run + (porta que deseja que ela suba) ex: dotnet run 51855```  
- Através da aplicação no one drive só necessita digitar ```./WS.SERVER + PORTA``` nela NÃO É SE NECESSÁRIO NENHUM DOWNLOAD DO .NET


# Objetos

- Produto : {
  "produtctId": null,
  "name": null,
  "quantity": 0,
  "price": 0.0
}

- Cliente : {
  "clientId": null,
  "name": null
}


# Operações
### Cliente
- inserirCliente(CID, "dados do cliente")
--- inserirCliente(string,string)
--- Retorno: Sucesso : "Cliente cadastrado com sucesso" Erro: "Cliente já existe"
--- Ex: inserirCliente(AB12,Luiz Fernando)

- modificarCliente(CID, "novos dados do cliente")
--- modificarCliente(string,string)
--- Retorno:  Sucesso :"Cliente atualizado com sucesso"  Erro : "Cliente não existe"
--- Ex: inserirCliente(AB12,Gustavo)

- recuperarCliente(CID)
--- recuperarCliente(string)
--- Retorno:  Sucesso :"Objeto Cliente"  Erro : "Cliente não existe"
--- Ex: recuperarCliente(AB12,Gustavo)

- apagarCliente(CID)
--- apagarCliente(string)
--- Retorno:  Sucesso :"Cliente apagado com sucesso"  Erro : "Cliente não existe"
--- Ex: apagarCliente(AB12)

### Produto
- inserirProduto(PID,Nome,Preço,Quantidade)
--- inserirProduto(string,string,decimal,int)
--- Retorno:  Sucesso :"Produto cadastrado com sucesso"  Erro : "Produto já existe"
--- Ex: inserirProduto(PROD123,Leite,2.0,2)

- modificarProduto(PID,Nome,Preço,Quantidade)
--- modificarProduto(string,string,decimal,int)
--- Retorno:  Sucesso :"Produto atualizado com sucesso"  Erro : "Produto não existe"
--- Ex: modificarProduto(PROD13,Laranja,2.0,2)

- recuperarProduto(PID)
--- recuperarProduto(string)
--- Retorno:  Sucesso :"Objeto Produto"  Erro : "Produto não existe"
--- Ex: recuperarProduto(PROD13)

- apagarProduto(PID)
--- recuperarProduto(string)
--- Retorno:  Sucesso :"Produto apagado com sucesso"  Erro : "Produto não existe"
--- Ex: apagarProduto(PROD13)


## Testes
#### Utilizando o postman é possível realizar testes
- https://www.postman.com/
- Teste 1
--- ``` inserirCliente(123,Luiz) ```
---- Deve retornar "Cliente cadastrado com sucesso"
- Teste 2
---  ```recuperarCliente(123)```
--- Deve retornar Objeto com Name = Luiz
- Teste 3
--- ```modificarCliente(123,Gustavo)```
- Teste 4
--- ```recuperarCliente(123)```
--- Deve retornar Objeto com Name = Gustavo
- Teste 5
--- ```apagarCliente(123)```
--- Deve retornar Cliente apagado com sucesso
- Teste 6
--  ```recuperarCliente(123)```
--- Deve retornar Cliente não existe
- Teste 7
--- ``` inserirCliente(123,Luiz) ```
---- Deve retornar "Cliente cadastrado com sucesso"
- Teste 8
--  ```inserirProduto(123,Leite,2.0,2))```
--- Deve retornar "Produto cadastrado com sucesso"
- Teste 9
--  ```recuperarProduto(123)```
--- Deve retornar objeto Produto com Name = Leite
- Teste 10
--  ```modificarProduto(123,Laranja,2.0,2)```
--- Deve retornar "Produto atualizado com sucesso"
- Teste 11
--  ```recuperarProduto(123)```
--- Deve retornar  objeto Produto com Name = Laranja
- Teste 12
--  ```apagarProduto(123)```
--- Deve retornar  objeto Produto com Name = Laranja
- Teste 13
--  ```recuperarProduto(123)```
--- Deve retornar  "Produto não existe"

# Arquitetura
Foi usado uma arquitetura em camadas aonde se tem a parte de Domain que fica as entites, a parte de application aonde fica a logica de negocio e a parte de repository para persistencia, além da inversão e injeção de depência para facilitar manipular classes.

Para a parte de cache foi usando uma Lista simples de objeto
