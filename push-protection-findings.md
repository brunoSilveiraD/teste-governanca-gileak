# GitHub Secret Scanning, Push Protection e Dependabot

## Resumo do que descobrimos

Este repositorio foi usado para testar recursos de seguranca e governanca do GitHub em um repositorio **publico** e **pessoal**.

Os testes com valores falsos em formatos genericos e de provedores **nao foram bloqueados** no `commit`/`push`. A conclusao mais importante e que, nesse cenario, o GitHub nao bloqueia qualquer texto que "pareca segredo": ele so bloqueia os padroes suportados que consegue identificar com confianca suficiente.

Tambem confirmamos que o item do `task.md` sobre `.github/dependabot.yml` trata de `Dependabot version updates`, que e diferente de `Dependabot alerts` e de `Dependabot security updates`.

## Pontos confirmados na documentacao

### 1. Secret scanning roda automaticamente em repositorios publicos

O GitHub informa que `secret scanning` esta disponivel para repositorios publicos e roda automaticamente.

Fonte:
- https://docs.github.com/en/code-security/reference/secret-security/supported-secret-scanning-patterns

### 2. Push protection para usuarios vale para repositorios publicos

O GitHub diferencia:
- `Push protection for repositories`
- `Push protection for users`

Para contas pessoais no GitHub.com, a documentacao diz que o `push protection for users`:
- e habilitado por padrao
- protege pushes para repositorios publicos

Fontes:
- https://docs.github.com/en/code-security/concepts/secret-security/about-push-protection
- https://docs.github.com/en/code-security/how-tos/secure-your-secrets/prevent-future-leaks/manage-user-push-protection

### 3. Push protection nao bloqueia qualquer segredo suportado

A documentacao explica que, mesmo para segredos suportados, o `push protection` so bloqueia um subconjunto dos padroes mais identificaveis, justamente para reduzir falso positivo.

Isso explica por que valores inventados podem passar normalmente.

Fonte:
- https://docs.github.com/en/code-security/reference/secret-security/secret-scanning-detection-scope

Trecho confirmado pela doc:
- `Push protection only blocks leaked secrets on a subset of the most identifiable user-alerted patterns.`

### 4. Padroes genericos e non-provider patterns tem tratamento separado

A lista oficial inclui padroes genericos como:
- `generic_private_key`
- `openssh_private_key`
- `rsa_private_key`
- `mongodb_connection_string`
- `mysql_connection_url`
- `postgres_connection_string`
- `http_basic_authentication_header`
- `http_bearer_authentication_header`

Mas a documentacao tambem informa que a deteccao de `non-provider patterns` precisa ser habilitada separadamente e esta documentada para repositorios de organizacao com `GitHub Secret Protection`.

Isso ajuda a explicar por que nossos testes com chaves privadas, connection strings e headers fake nao acionaram bloqueio nesse cenario.

Fontes:
- https://docs.github.com/en/code-security/reference/secret-security/supported-secret-scanning-patterns
- https://docs.github.com/en/code-security/how-tos/secure-your-secrets/detect-secret-leaks/enabling-secret-scanning-for-non-provider-patterns

### 5. Segredos em par precisam aparecer juntos

Para alguns provedores, como AWS, a deteccao depende de pares. A documentacao diz que os dois valores precisam estar no mesmo arquivo e no mesmo push para gerar deteccao.

Fonte:
- https://docs.github.com/en/code-security/reference/secret-security/secret-scanning-detection-scope

Exemplo citado pela doc:
- `aws_access_key_id` + `aws_secret_access_key`

### 6. Push protection suporta melhor versoes recentes de tokens

O GitHub informa que provedores atualizam formatos de token ao longo do tempo e que o `push protection` tende a suportar apenas as versoes mais recentes que ele consegue identificar com confianca.

Isso significa que um token inventado "parecido" com o formato esperado ainda pode passar normalmente.

Fonte:
- https://docs.github.com/en/code-security/reference/secret-security/supported-secret-scanning-patterns

## O que e Dependabot dentro do GitHub

`Dependabot` e o conjunto de recursos do GitHub para visibilidade, alerta e atualizacao automatica de dependencias.

As tres opcoes mais importantes sao:

- `Dependabot alerts`: avisa quando o GitHub detecta dependencias com vulnerabilidades conhecidas.
- `Dependabot security updates`: abre pull requests para corrigir dependencias vulneraveis.
- `Dependabot version updates`: abre pull requests para atualizar versoes mesmo quando nao existe vulnerabilidade, com base no arquivo `.github/dependabot.yml`.

No contexto deste repositorio, o criterio de aceite do `task.md` sobre `.github/dependabot.yml` se refere especificamente a `Dependabot version updates`, nao a `Dependabot security updates`.

Fontes:
- https://docs.github.com/en/code-security/dependabot/dependabot-version-updates/about-dependabot-version-updates
- https://docs.github.com/en/code-security/dependabot/dependabot-version-updates/configuring-dependabot-version-updates
- https://docs.github.com/en/code-security/dependabot/dependabot-security-updates/configuring-dependabot-security-updates

## Como Dependabot se relaciona com outras opcoes de seguranca

Na area de seguranca do GitHub, Dependabot convive com outros recursos que tem finalidades diferentes:

- `Dependency graph`: mapeia as dependencias detectadas no repositorio.
- `Dependency review`: ajuda a analisar impacto de mudancas de dependencias em pull requests.
- `Code scanning`: procura vulnerabilidades e problemas de seguranca no codigo.
- `Secret scanning` e `Push protection`: detectam e ajudam a bloquear vazamento de segredos.

Esses recursos se complementam. Em resumo:

- `Dependency graph` mostra o que existe.
- `Dependabot alerts` mostra onde ha vulnerabilidades conhecidas.
- `Dependabot security updates` tenta corrigir vulnerabilidades com PR automatico.
- `Dependabot version updates` mantem dependencias atualizadas de forma continua.
- `Secret scanning` e `Push protection` tratam vazamento de credenciais, nao atualizacao de bibliotecas.

Fontes:
- https://docs.github.com/en/code-security/dependency-graph/about-the-dependency-graph
- https://docs.github.com/en/code-security/dependabot/dependabot-version-updates/about-dependabot-version-updates

## Observacoes praticas para este repositorio

O estado atual deste repositorio importa para entender o comportamento esperado do Dependabot:

1. O repositorio esta quase vazio.
2. A branch `development` nao existe no momento.
3. Nao existem manifests reais de `npm` ou `nuget`.
4. Nao existem workflows em `.github/workflows/` para `github-actions`.

Por isso, configurar apenas o arquivo `.github/dependabot.yml` nao garante que PRs aparecam imediatamente para todos os ecossistemas.

Para `Dependabot version updates` funcionar de forma pratica:

- `npm` precisa de manifests como `package.json`
- `nuget` precisa de arquivos compativeis, como `.csproj` ou `packages.config`
- `github-actions` precisa de workflows em `.github/workflows/*.yml`

Tambem vale registrar que `target-branch: development` so faz sentido operacionalmente depois que a branch `development` existir no repositorio.

Fontes:
- https://docs.github.com/en/code-security/dependabot/dependabot-version-updates/configuring-dependabot-version-updates
- https://docs.github.com/en/code-security/dependency-graph/about-the-dependency-graph
- https://docs.github.com/en/code-security/supply-chain-security/understanding-your-software-supply-chain/dependency-graph-supported-package-ecosystems

## O que testamos

### Rodada 1

Arquivo com padroes genericos falsos:
- private keys
- connection strings
- `Authorization: Bearer`
- `Authorization: Basic`

Resultado:
- o GitHub nao bloqueou

### Rodada 2

Arquivo reduzido para padroes de provedor falsos:
- `anthropic_api_key`
- um PAT sintetico de Airtable
- `aws_access_key_id` + `aws_secret_access_key`

Resultado:
- o GitHub tambem nao bloqueou

## Conclusao pratica

Para este cenario, aprendemos que:

1. Repositorio publico pessoal tem protecao, mas nao garante bloqueio para strings fake.
2. Valores inventados podem nao ser reconhecidos como segredo real ou suficientemente confiavel.
3. `Dependabot` e um conjunto de recursos diferentes, e o arquivo `.github/dependabot.yml` controla apenas `version updates`.
4. Configurar `dependabot.yml` sem manifests reais e sem branch alvo existente nao garante PRs imediatos.
5. Testes com `push protection` ficam muito mais confiaveis quando usam:
   - um segredo real de laboratorio, revogado logo apos o teste, ou
   - um repositorio de organizacao com `GitHub Secret Protection`, `non-provider patterns` e/ou `custom patterns`.

## Links oficiais usados nesta documentacao

- https://docs.github.com/en/code-security/reference/secret-security/supported-secret-scanning-patterns
- https://docs.github.com/en/code-security/reference/secret-security/secret-scanning-detection-scope
- https://docs.github.com/en/code-security/concepts/secret-security/about-push-protection
- https://docs.github.com/en/code-security/concepts/secret-security/push-protection-from-the-command-line
- https://docs.github.com/en/code-security/how-tos/secure-your-secrets/prevent-future-leaks/manage-user-push-protection
- https://docs.github.com/en/code-security/how-tos/secure-your-secrets/detect-secret-leaks/enabling-secret-scanning-for-non-provider-patterns
- https://docs.github.com/en/code-security/dependabot/dependabot-version-updates/about-dependabot-version-updates
- https://docs.github.com/en/code-security/dependabot/dependabot-version-updates/configuring-dependabot-version-updates
- https://docs.github.com/en/code-security/dependabot/dependabot-security-updates/configuring-dependabot-security-updates
- https://docs.github.com/en/code-security/dependency-graph/about-the-dependency-graph
- https://docs.github.com/en/code-security/supply-chain-security/understanding-your-software-supply-chain/dependency-graph-supported-package-ecosystems
