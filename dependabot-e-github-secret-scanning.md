# Dependabot e GitHub Secret Scanning

`Dependabot` e `GitHub Secret Scanning` vivem na mesma area de seguranca do GitHub, mas tratam riscos diferentes.

## Relacao entre os recursos

- `Dependency Graph`: da visibilidade para as dependencias do repositorio
- `Dependabot`: usa esse contexto para alertas e PRs de atualizacao
- `Secret Scanning`: procura credenciais vazadas no conteudo hospedado
- `Push Protection`: tenta bloquear parte desses vazamentos antes do push completar

## Manutencao de dependencia vs vazamento de credencial

`Dependabot` ajuda quando o problema e:

- dependencia antiga
- dependencia vulneravel
- manutencao rotineira de versao

`Secret Scanning` e `Push Protection` ajudam quando o problema e:

- senha, token ou chave expostos
- segredo no historico Git
- vazamento em comentario, issue, PR ou wiki

Um nao substitui o outro.

## Melhor para uso pessoal em repositorio publico

Se eu tivesse que comecar simples, faria assim:

1. habilitaria `Dependabot version updates`
2. confiaria na cobertura nativa de `Secret Scanning` em repo publico
3. trataria `Push Protection for users` como linha preventiva adicional

Melhor para uso pessoal:

- a combinacao nativa do GitHub costuma ser a melhor
- pouco atrito, boa cobertura inicial e sem pipeline extra obrigatorio

## Melhor para uso empresarial

Em empresa, o melhor desenho costuma ser:

1. `Dependabot` para supply chain
2. `GitHub Secret Protection` para repositorios privados e internos
3. `Push Protection` por repositorio com governanca de bypass
4. expansao com `non-provider patterns`, `custom patterns` e `validity checks` quando fizer sentido

Melhor para uso empresarial:

- a combinacao `Dependabot` + `Secret Scanning` nativo do GitHub faz mais sentido quando a organizacao ja trabalha fortemente dentro do ecossistema GitHub
- ela ganha ainda mais valor quando ha necessidade de auditoria, alertas centralizados e politica de bypass

## Recomendacao final por cenario

Uso pessoal:

- priorize os recursos nativos do GitHub
- eles ja cobrem dependencia e segredo com baixo custo operacional

Uso empresarial:

- use `Dependabot` para manutencao
- use `Secret Scanning` e `Push Protection` como camada central de protecao de credenciais
- complemente com scanner externo apenas se houver lacuna operacional especifica

## Referencias oficiais

- https://docs.github.com/en/code-security/concepts/supply-chain-security/about-dependabot-version-updates
- https://docs.github.com/en/code-security/concepts/secret-security/about-secret-scanning
- https://docs.github.com/en/code-security/concepts/secret-security/about-push-protection
- https://docs.github.com/en/code-security/dependency-graph/about-the-dependency-graph
