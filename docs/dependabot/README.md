# Dependabot

`Dependabot` e o recurso do GitHub para manter dependencias atualizadas. No contexto deste repositorio, o foco principal e `Dependabot version updates`, isto e, PRs automaticos para atualizar versoes mesmo quando nao existe vulnerabilidade conhecida.

## Tres recursos parecidos, mas diferentes

- `Dependabot alerts`: avisam quando o GitHub encontra vulnerabilidades conhecidas nas dependencias detectadas.
- `Dependabot security updates`: abrem PRs para corrigir dependencias vulneraveis quando ha caminho de atualizacao suportado.
- `Dependabot version updates`: abrem PRs de manutencao rotineira com base no arquivo `.github/dependabot.yml`.

O ponto mais importante deste laboratorio e nao confundir esses tres nomes. Aqui, o arquivo `.github/dependabot.yml` configura especificamente `version updates`.

## Como isso aparece neste repositorio

O baseline atual foi reduzido ao caminho mais previsivel:

- ecossistema `npm`
- manifest valido na raiz
- lockfile versionado
- CI enxuto para validar os PRs do bot

Com isso, o repositorio fica em um estado simples de observar: se o GitHub detectar que a dependencia fixada esta atrasada, a expectativa e abrir um PR automatico de atualizacao.

## O que o GitHub documenta

Pela documentacao oficial, `Dependabot version updates`:

- esta disponivel para todos os repositorios no GitHub
- depende de um `dependabot.yml` commitado no repositorio
- usa a localizacao dos manifests para descobrir o que monitorar
- abre PRs quando encontra versoes mais novas suportadas

## Leitura recomendada nesta pasta

- `exemplos-no-repo.md`: mostra como o laboratorio atual foi montado
- `cenarios-e-limites.md`: resume casos de sucesso, falha e cuidados de interpretacao

## Referencias oficiais

- https://docs.github.com/en/code-security/concepts/supply-chain-security/about-dependabot-version-updates
- https://docs.github.com/en/code-security/dependabot/dependabot-version-updates/configuring-dependabot-version-updates
- https://docs.github.com/en/code-security/dependabot/dependabot-version-updates/about-the-dependabot.yml-file
