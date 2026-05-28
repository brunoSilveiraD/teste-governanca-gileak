# Secret Scanning para uso pessoal e empresarial

## Uso pessoal

Para repositório publico pessoal, o `GitHub Secret Scanning` ja entrega uma base muito forte porque:

- roda automaticamente
- nao exige pipeline extra
- ajuda a descobrir segredos expostos no historico e em outros artefatos do repositorio

Limite importante:

- a cobertura gratuita nao transforma o ambiente pessoal em uma politica corporativa completa
- voce depende bastante dos padroes suportados pelo GitHub

## Uso empresarial

Em empresa, a conversa muda porque entram mais exigencias:

- repositorios privados e internos
- trilha de auditoria
- padroes internos da organizacao
- priorizacao de risco

Nessa camada, faz mais sentido usar `GitHub Secret Protection` para ganhar:

- cobertura em repositorios privados e internos
- `push protection` por repositorio
- `non-provider patterns`
- `custom patterns`
- `validity checks`

## Melhor para cada contexto

Melhor para uso pessoal:

- comece com os recursos nativos do GitHub para repositório publico
- trate os alertas como linha base de higiene

Melhor para uso empresarial:

- use `Secret Scanning` como capacidade central da plataforma
- complemente com configuracao organizacional e padroes customizados
- nao dependa apenas da cobertura padrao de provider secrets

## Regra simples

Se o objetivo e rapidez em repo publico pessoal, o nativo do GitHub costuma ser o melhor ponto de partida.

Se o objetivo e governanca em larga escala, o valor real aparece quando a organizacao habilita os recursos pagos e customizaveis.

## Referencias oficiais

- https://docs.github.com/en/code-security/concepts/secret-security/about-secret-scanning
- https://docs.github.com/en/code-security/how-tos/secure-your-secrets/detect-secret-leaks/enabling-secret-scanning-for-non-provider-patterns
