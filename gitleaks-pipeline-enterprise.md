# Gitleaks no pipeline com GitHub Actions

## Objetivo

Este guia mostra uma implementacao enxuta de `Gitleaks` em pipeline com `GitHub Actions`, usando apenas referencias oficiais do projeto e do GitHub. O foco e usar o scan de segredos como etapa automatizada em `push` e `pull_request`.

## O que o Gitleaks faz no pipeline

Segundo a documentacao oficial, o Gitleaks pode ser usado como `cli`, `github-action`, `pre-commit-hook` e scanner em qualquer `ci/cd`.

No contexto de pipeline:

- `gitleaks git` faz scan de repositorios Git.
- `gitleaks dir` faz scan de diretorios e arquivos.
- `gitleaks stdin` faz scan de conteudo recebido via entrada padrao.

Para implementacao em `GitHub Actions`, a trilha principal deste guia usa `gitleaks/gitleaks-action`.

## Pre-requisitos

- Repositorio com workflow YAML em `.github/workflows`, conforme a documentacao oficial do GitHub Actions.
- Permissao para editar workflows do repositorio.
- Se a implementacao usar configuracao customizada, arquivo `.gitleaks.toml` no repositorio ou configuracao equivalente por `--config`, `GITLEAKS_CONFIG` ou `GITLEAKS_CONFIG_TOML`.

## Passo a passo no GitHub Actions

### 1. Criar ou ajustar o workflow

O GitHub documenta que workflows devem ficar em `.github/workflows` e podem ser disparados por eventos como `push` e `pull_request`.

Exemplo minimo:

```yaml
name: gitleaks

on:
  pull_request:
  push:

jobs:
  scan:
    name: gitleaks
    runs-on: ubuntu-latest
    steps:
      - name: Checkout
        uses: actions/checkout@v4

      - name: Gitleaks
        uses: gitleaks/gitleaks-action@v2
        env:
          GITHUB_TOKEN: ${{ secrets.GITHUB_TOKEN }}
```

### 2. Executar o scan antes de build e teste

Para uso em pipeline, a sequencia operacional deste guia e:

1. `checkout`
2. `gitleaks`
3. build e testes

Com isso, o scan de segredos roda antes das demais etapas do CI.

### 3. Usar a action oficial como trilha padrao

A documentacao oficial do `gitleaks-action` apresenta a integracao via workflow GitHub Actions. Ela tambem documenta variaveis como:

- `GITLEAKS_CONFIG`: caminho para arquivo de configuracao do Gitleaks.
- `GITLEAKS_ENABLE_UPLOAD_ARTIFACT`: controla upload de artefato `sarif` quando segredos sao detectados.
- `GITLEAKS_ENABLE_SUMMARY`: controla o resumo do job.
- `GITLEAKS_ENABLE_COMMENTS`: controla comentarios em PR.
- `GITLEAKS_LICENSE`: requerido para organizacoes, segundo o README oficial da action.

## Configuracao e excecoes

### Configuracao

O README oficial do Gitleaks documenta a seguinte precedencia de configuracao:

1. `--config`
2. `GITLEAKS_CONFIG`
3. `GITLEAKS_CONFIG_TOML`
4. `.gitleaks.toml` no caminho alvo

Se nenhuma dessas opcoes for usada, o Gitleaks usa a configuracao padrao.

### Ignorar findings especificos

O mecanismo oficial para ignorar findings especificos e `.gitleaksignore`.

O README oficial informa que cada finding pode ter um `Fingerprint`, e esse valor pode ser usado no `.gitleaksignore` para ignorar aquele achado especifico.

## Operacao corporativa

No GitHub, os controles oficiais relacionados ao tema sao complementares:

- `Secret scanning`: detecta segredos expostos e gera alertas.
- `Push protection`: bloqueia pushes com segredos suportados antes que cheguem ao repositorio.
- `Bypass requests`: quando habilitado no contexto organizacional, permite fluxo de aprovacao para bypass de bloqueio.

Esses recursos do GitHub nao substituem a etapa de `Gitleaks` no CI. Neste guia, o papel do Gitleaks e manter um gate automatizado no workflow de `push` e `pull_request`, enquanto os recursos nativos do GitHub tratam deteccao e bloqueio dentro do ecossistema da plataforma.

## Validacoes esperadas

- O workflow executa em `push` e `pull_request`.
- Um segredo detectado faz a execucao falhar.
- Um repositorio sem findings segue para as proximas etapas do CI.
- Um `.gitleaks.toml` valido e carregado quando usado como fonte de configuracao.
- Um `Fingerprint` listado em `.gitleaksignore` deixa de bloquear aquele finding especifico.
- Quando `push protection` estiver habilitado no contexto aplicavel do GitHub, pushes com segredos suportados podem ser bloqueados antes do merge.

## Referencias oficiais

- Gitleaks README: https://github.com/gitleaks/gitleaks
- Gitleaks Action README: https://github.com/gitleaks/gitleaks-action
- GitHub Actions workflow syntax: https://docs.github.com/en/actions/reference/workflows-and-actions/workflow-syntax
- About secret scanning: https://docs.github.com/en/code-security/concepts/secret-security/about-secret-scanning
- About push protection: https://docs.github.com/en/code-security/secret-scanning/introduction/about-push-protection
- About bypass requests for push protection: https://docs.github.com/en/code-security/concepts/secret-security/about-bypass-requests-for-push-protection
