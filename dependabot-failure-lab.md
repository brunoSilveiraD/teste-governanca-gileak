# Laboratorio de Falhas do Dependabot

## Objetivo

Este repositorio agora tem um laboratorio simples para estudar como o GitHub e o `Dependabot` se comportam em tres tipos de situacao:

- PR do Dependabot abre, mas o CI barra
- Dependabot nao consegue processar a configuracao
- Dependencia vulneravel gera alerta ou `security update`

O laboratorio agora foi propositalmente quebrado em `npm` e `nuget` para reproduzir falhas reais de resolucao de dependencia.

## Baseline original

Arquivos base:

- `package.json`
- `src/TesteDependabot/TesteDependabot.csproj`
- `.github/workflows/ci.yml`
- `.github/dependabot.yml`

Comportamento esperado do baseline:

- workflow roda normalmente
- manifests de `npm` e `nuget` sao validos
- `Dependabot` pode analisar `npm`, `nuget` e `github-actions`

## Cenario ativo 1: PR abre, mas o CI barra por falha real

Ecossistemas:
- `npm`
- `nuget`

Como foi preparado:
- `package.json` usa uma versao inexistente de `axios`
- `TesteDependabot.csproj` usa uma versao inexistente de `Newtonsoft.Json`
- o workflow continua normal, sem condicao artificial para falhar

Comportamento esperado:
- o PR do Dependabot continua podendo abrir
- o job de CI falha no `npm install` e/ou no `dotnet restore`
- a falha e de infraestrutura de dependencia, como aconteceria em um projeto real

Objetivo do cenario:
- simular o caso em que o bot consegue propor a atualizacao, mas a validacao do projeto reprova o PR

## Cenarios planejados para aplicar um por vez

### 1. Erro de configuracao do Dependabot

Possiveis rodadas:

- `npm`: alterar `directory` para um caminho inexistente
- `nuget`: apontar o `dependabot.yml` para pasta errada
- `github-actions`: remover ou mover workflow para fora de `.github/workflows`

Resultado esperado:
- ausencia de PR normal para o ecossistema afetado
- erro visivel na execucao/configuracao do Dependabot

### 2. Dependencia vulneravel detectavel

Possiveis rodadas:

- `npm`: manter pacote antigo com advisory reconhecido
- `nuget`: escolher pacote e versao com advisory conhecido

Resultado esperado:
- alerta no `dependency graph` / `Dependabot alerts`
- possivel `security update`, dependendo da deteccao do GitHub

### 3. Falha equivalente em outros ecossistemas

Possiveis rodadas:

- `nuget`: usar `PackageReference` para versao inexistente
- `github-actions`: usar tag invalida de action

Resultado esperado:
- PR abre
- CI falha no ecossistema alterado

## Como operar este laboratorio

1. Aplicar somente um cenario por vez.
2. Abrir PR dedicado para observar o comportamento do GitHub.
3. Registrar o resultado observado:
   - PR abriu e falhou no CI
   - Dependabot acusou erro de configuracao
   - alerta ou `security update` apareceu
4. Reverter o cenario antes de iniciar o proximo.

## Estado atual

Neste momento, o laboratorio esta preparado com:

- baseline original documentado
- documentacao dos cenarios
- um cenario ativo de falha real para `npm` e `nuget`
