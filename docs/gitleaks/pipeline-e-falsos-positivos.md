# Gitleaks em pipeline e leitura de falsos positivos

## Pipeline minimo com GitHub Actions

O caminho mais simples para este laboratorio e usar a `gitleaks/gitleaks-action` em um workflow de `push` e `pull_request`.

Exemplo minimo:

```yaml
name: gitleaks

on:
  push:
  pull_request:

jobs:
  scan:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
        with:
          fetch-depth: 0
      - uses: gitleaks/gitleaks-action@v2
        env:
          GITHUB_TOKEN: ${{ secrets.GITHUB_TOKEN }}
```

Leitura pratica:

- `fetch-depth: 0` ajuda quando o objetivo e escanear historico Git
- a action oficial ja cobre o caminho mais direto de integracao

## Pessoal vs organizacao

Segundo o README oficial da action:

- conta pessoal nao exige `GITLEAKS_LICENSE`
- repositorios de organizacao exigem `GITLEAKS_LICENSE`

Isso importa bastante na recomendacao final:

- para uso pessoal, o custo de adocao e baixo
- para empresa, o fit continua bom, mas exige governanca adicional

## Como reduzir ruido

A documentacao oficial do `Gitleaks` aponta tres caminhos principais:

- `gitleaks:allow` em linha conhecida
- `.gitleaksignore` com `Fingerprint`
- `allowlists` na configuracao

Uso recomendado neste laboratorio:

- use `gitleaks:allow` quando o caso e muito localizado
- use `.gitleaksignore` quando voce quer ignorar um finding especifico e rastreavel
- use `allowlists` quando o padrao de excecao e recorrente

## O que e falso positivo aqui

Em pratica, falso positivo e um match que bate na regra, mas nao representa risco real no contexto do repositorio.

Exemplos comuns:

- valores sinteticos em documentacao
- fixtures de teste
- textos que parecem token, mas nao sao credenciais utilizaveis

## O que nao fazer

- nao tratar toda ocorrencia de `generic-api-key` como incidente confirmado
- nao ignorar findings em massa sem manter trilha de justificativa
- nao usar allowlist ampla demais, porque isso pode esconder segredo real depois

## Como este laboratorio recomenda operar

1. Rodar o scan cedo no workflow.
2. Triar primeiro regras especificas de provedor.
3. Revisar com mais contexto os matches genericos.
4. Registrar excecoes de forma minima e rastreavel.

## Referencias oficiais

- https://github.com/gitleaks/gitleaks
- https://github.com/gitleaks/gitleaks-action
- https://docs.github.com/en/actions/reference/workflows-and-actions/workflow-syntax
