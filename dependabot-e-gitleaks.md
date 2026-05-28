# Dependabot e Gitleaks

`Dependabot` e `Gitleaks` se complementam, mas resolvem problemas diferentes.

## O que cada um faz

- `Dependabot`: cuida de dependencias, versoes e vulnerabilidades conhecidas no supply chain
- `Gitleaks`: procura credenciais hardcoded no codigo, no historico ou em arquivos escaneados

## Onde parece haver sobreposicao

Os dois aparecem no mesmo assunto amplo de seguranca de repositorio e podem rodar em PRs automatizados. Fora isso, a sobreposicao e pequena.

`Dependabot` responde perguntas como:

- esta dependencia esta antiga?
- existe atualizacao automatizavel?
- existe advisory conhecido?

`Gitleaks` responde perguntas como:

- alguem commitou senha, token ou chave?
- o historico carrega segredos esquecidos?
- precisamos falhar o pipeline por vazamento?

## Fluxo recomendado para uso pessoal

Se eu tivesse que comecar simples em um repositório pessoal, faria assim:

1. ativaria `Dependabot version updates`
2. manteria CI minimo para validar PRs do bot
3. adicionaria `Gitleaks` no pipeline apenas se o repositorio tiver risco real de vazamento ou multiplos colaboradores

Melhor para uso pessoal:

- `Dependabot` quase sempre vale o custo, porque e nativo e simples
- `Gitleaks` entra melhor quando voce quer gate explicito no CI ou trabalho multiplataforma

## Fluxo recomendado para uso empresarial

Se eu tivesse que montar o baseline empresarial, faria assim:

1. `Dependabot` para manutencao continua de dependencias
2. `Gitleaks` como gate operacional de segredos no CI
3. regras de excecao pequenas e rastreaveis
4. revisao recorrente de findings e allowlists

Melhor para uso empresarial:

- use os dois
- `Dependabot` cobre supply chain
- `Gitleaks` cobre vazamento de credenciais com mais controle operacional

## Recomendacao final por cenario

Uso pessoal:

- comece por `Dependabot`
- adicione `Gitleaks` quando quiser mais rigor em segredo ou mais automacao no CI

Uso empresarial:

- nao escolha entre um e outro
- combine ambos, porque eles atacam riscos diferentes e igualmente relevantes

## Referencias oficiais

- https://docs.github.com/en/code-security/concepts/supply-chain-security/about-dependabot-version-updates
- https://github.com/gitleaks/gitleaks
- https://github.com/gitleaks/gitleaks-action
