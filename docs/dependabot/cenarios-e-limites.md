# Cenarios e limites do Dependabot

Esta pasta consolida tres tipos de aprendizado que estavam separados no repositorio: baseline bem-sucedido, laboratorio de falha e limites de interpretacao.

## Cenario 1. Baseline bem-sucedido

No estado atual da `main`, o laboratorio esta focado em `npm` apenas. Esse desenho reduz o numero de causas possiveis para "o bot nao abriu PR".

O que ja esta alinhado:

- manifest valido
- lockfile versionado
- branch padrao coerente
- CI pequeno
- dependencia real e desatualizada

Esse e o melhor ponto de partida para demonstracao e onboarding.

## Cenario 2. O bot pode estar correto e o CI falhar

Uma das licoes do laboratorio e que `Dependabot` e CI nao sao a mesma coisa.

Pode acontecer de:

- o bot entender o ecossistema
- abrir o PR
- o pipeline reprovar por problema real de restore, build ou teste

Isso continua sendo um comportamento normal. O PR do bot nao substitui a validacao do projeto.

## Cenario 3. A configuracao pode ser valida, mas insuficiente

Ter `.github/dependabot.yml` sozinho nao garante PR automatico. O GitHub tambem precisa encontrar:

- ecossistema suportado
- arquivos compativeis
- dependencia detectavel
- caminho de atualizacao compativel

Em outras palavras: o arquivo configura a intencao, mas o repositorio precisa ter material real para o GitHub analisar.

## Cenario 4. Laboratorio de falha controlada

Os rascunhos anteriores do repo exploravam falhas como:

- versao inexistente de pacote
- caminho errado em `directory`
- ecossistema configurado sem manifest correspondente

Essas rodadas sao uteis para estudo, mas nao devem ser confundidas com o baseline didatico principal. Para demonstracao, o mais importante e manter um caso simples que funcione.

## O que este laboratorio ainda nao prova

Hoje o repositorio nao prova sozinho:

- `Dependabot security updates`
- `Dependabot alerts` com advisory real
- comportamento de `nuget` e `github-actions` neste baseline atual
- estrategia de update para branch secundaria

Se esses temas entrarem em uma proxima rodada, o ideal e testar um por vez.

## Regra de leitura

Quando voce olhar um resultado do laboratorio, separe sempre:

- comportamento documentado do GitHub
- observacao concreta deste repositorio

Isso evita concluir que uma ausencia de PR significa falha do produto, quando as vezes o problema era apenas de manifest, branch ou dependencias.

## Referencias oficiais

- https://docs.github.com/en/code-security/concepts/supply-chain-security/about-dependabot-version-updates
- https://docs.github.com/en/code-security/dependabot/dependabot-version-updates/configuring-dependabot-version-updates
- https://docs.github.com/en/code-security/supply-chain-security/understanding-your-software-supply-chain/dependency-graph-supported-package-ecosystems
