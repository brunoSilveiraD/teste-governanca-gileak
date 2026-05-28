# Documentacao do Laboratorio

Este repositorio funciona como laboratorio curto para estudar quatro frentes complementares de governanca no GitHub:

- `Dependabot`
- `Gitleaks`
- `GitHub Secret Scanning`
- `GitHub Push Protection`

Cada tema foi separado em sua propria pasta para facilitar leitura, comparacao e reaproveitamento.

## Mapa rapido

- `docs/dependabot/`: atualizacao de dependencias e leitura do experimento com `npm`
- `docs/gitleaks/`: deteccao de segredos via ferramenta externa e uso em pipeline
- `docs/github-secret-scanning/`: deteccao nativa do GitHub para credenciais expostas
- `docs/push-protection/`: bloqueio preventivo de pushes com segredos suportados

## Guias de sintese na raiz

- `dependabot-e-gitleaks.md`
- `dependabot-e-github-secret-scanning.md`

Esses dois arquivos cruzam os temas e respondem quando cada combinacao faz mais sentido em uso pessoal e empresarial.

## Onde esta a configuracao operacional real

Os exemplos desta documentacao apontam para arquivos reais do laboratorio, que continuam nos locais de execucao do projeto:

- `.github/dependabot.yml`
- `.github/workflows/ci.yml`
- `package.json`
- `package-lock.json`
- `src/`

Em outras palavras: `docs/` explica o laboratorio; a configuracao que o GitHub e o CI realmente usam continua fora dela.
