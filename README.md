# Task Manger - Davi JR

Componentes:
- WebApi
- MongoDb

## Como Executar?
Certifique-se de possuir o **docker** instalado além da versão 6.0+ da SDK do DotNet Core

#### Command Prompt

````
//Acesse a Pasta Raiz do Projeto e Execute os Seguintes Comandos

$ docker compose up
// Este comando irá iniciar um container com o mongodb e api devidamente parametrizada
````

####
Acessar  a url http://localhost:8080/swagger/index.html


# Fase 2: Refinamento

- Existe a possibilidade de criação de novas modalidades de usuários, se sim quais?
- Quais outras metricas poderiam ser utilizadas para mensurar a produtividade das tasks?
- Faria sentido segregar as tarefas por tipos ou categorias?
- Além do Manager algum perfil poderia ter acesso aos relatorios?
- Quais outros relatorios estão previstos?
- Algum perfil poderá alterar a severidade de uma task já criada?
- O limite de tasks poderá variar por projeto?


# Fase 3: Melhorias

- Caching
- Authenticação
- Permissionamento
- Logs e Monitoria