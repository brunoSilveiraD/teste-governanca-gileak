# Dependabot no repositorio

Este laboratorio foi simplificado para demonstrar `Dependabot version updates` com o menor numero possivel de variaveis.

## 1. Arquivo de configuracao

Em `.github/dependabot.yml`, o repositorio monitora apenas `npm` na raiz:

```yaml
version: 2
updates:
  - package-ecosystem: npm
    directory: /
    schedule:
      interval: daily
```

Leitura pratica:

- `version: 2` e o formato atual do arquivo
- `package-ecosystem: npm` restringe o teste ao ecossistema Node
- `directory: /` manda olhar o `package.json` da raiz
- `interval: daily` pede verificacao diaria

## 2. Baseline de CI

Em `.github/workflows/ci.yml`, o workflow atual so valida o fluxo `npm`:

- faz `checkout`
- instala Node.js 20
- executa `npm ci`
- executa `npm test`

Isso ajuda a responder a pergunta central do laboratorio: quando o bot abrir um PR, ele consegue passar em um CI minimo e previsivel?

## 3. Manifest e lockfile

O `package.json` atual fixa:

- `axios` em `1.6.0`
- `eslint` em `8.57.0`

Ja o `package-lock.json` continua versionado para:

- reproduzir o estado de dependencias
- permitir que o PR do bot atualize manifest e lockfile juntos

## 4. Por que `axios` foi escolhido

No laboratorio, `axios` foi mantido em versao valida, mas deliberadamente antiga. Isso evita o caso ruim de dependencia inexistente e deixa o repositorio em um estado observavel para `version updates`.

Observacao do laboratorio:

- a intencao aqui nao e provar vulnerabilidade
- a intencao e provar manutencao automatica de versao

## 5. O que observar no GitHub

Quando o baseline esta correto, os sinais esperados sao:

1. O `Dependency graph` consegue ler o ecossistema.
2. O GitHub passa a mostrar o status do `Dependabot`.
3. Um PR automatico de atualizacao pode ser aberto.
4. O PR dispara o workflow de CI.

## 6. O que este exemplo nao cobre

Este desenho propositalmente deixa de fora:

- multiplos ecossistemas ao mesmo tempo
- `target-branch`
- `security updates`
- `github-actions` como ecossistema monitorado
- `nuget` como segundo caso de laboratorio

Esses cenarios sao uteis, mas misturam mais variaveis e dificultam a leitura inicial.

## Referencias oficiais

- https://docs.github.com/en/code-security/concepts/supply-chain-security/about-dependabot-version-updates
- https://docs.github.com/en/code-security/dependabot/dependabot-version-updates/configuring-dependabot-version-updates
- https://docs.github.com/en/code-security/dependency-graph/about-the-dependency-graph
