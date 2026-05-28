# GitHub Secret Scanning

`GitHub Secret Scanning` e o recurso nativo da plataforma para detectar credenciais expostas no historico e no conteudo hospedado no GitHub.

## O que ele faz

Segundo a documentacao oficial, o recurso:

- varre o historico Git em todos os branches
- gera alertas quando encontra credenciais suportadas
- tambem cobre comentarios, issues, PRs, discussions, wikis e secret gists
- faz rescans periodicos quando novos tipos de segredo passam a ser suportados

## O que ele nao e

`Secret Scanning` nao e a mesma coisa que:

- `Push Protection`, que tenta bloquear antes do push terminar
- `Gitleaks`, que e uma ferramenta externa e configuravel fora do GitHub
- `Dependabot`, que trata manutencao de dependencias

## Availability por tipo de repositorio

Pela documentacao oficial:

- repositorios publicos: `Secret Scanning` roda automaticamente e sem custo
- repositorios privados e internos de organizacao: depende de `GitHub Secret Protection` em `GitHub Team` ou `GitHub Enterprise Cloud`
- repositorios de usuario: o caso empresarial depende do modelo de produto descrito pela doc

## Customizacao relevante

A documentacao atual destaca quatro extensoes importantes:

- `non-provider patterns`
- `custom patterns`
- `validity checks`
- `Copilot secret scanning`

Essas opcoes sao especialmente importantes em ambiente empresarial, onde a deteccao padrao costuma ser insuficiente sozinha.

## Leitura recomendada nesta pasta

- `exemplos-no-repo.md`: o que o laboratorio observou e por que alguns testes nao bloquearam
- `pessoal-vs-empresarial.md`: quando a cobertura gratuita basta e quando a governanca corporativa muda o jogo

## Referencias oficiais

- https://docs.github.com/en/code-security/concepts/secret-security/about-secret-scanning
- https://docs.github.com/en/code-security/reference/secret-security/secret-scanning-detection-scope
- https://docs.github.com/en/code-security/reference/secret-security/supported-secret-scanning-patterns
