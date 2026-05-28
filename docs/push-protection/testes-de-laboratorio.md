# Como testar Push Protection com seguranca

## Objetivo

Se a meta e validar bloqueio de verdade, o laboratorio precisa diferenciar teste ilustrativo de teste confiavel.

## Teste ilustrativo

Usar strings fake ajuda a entender:

- que o GitHub nao bloqueia qualquer texto parecido com segredo
- que formato e contexto importam
- que a ausencia de bloqueio nao prova ausencia de protecao

Esse tipo de teste e seguro, mas fraco para validacao de fluxo.

## Teste confiavel

Para validar de forma mais forte, o caminho recomendado e:

1. usar um segredo real descartavel
2. limitar escopo e tempo de vida
3. fazer o teste em provedor suportado
4. revogar o segredo imediatamente depois

Exemplos fortes de laboratorio ja citados no material antigo:

- `GitHub Personal Access Token`
- `GitHub SSH private key`
- `Slack API token`
- `1Password service account token`

## Quando um segredo fake pode nao bloquear

A documentacao oficial explica varios motivos:

- o tipo nao esta no subconjunto coberto por `push protection`
- o formato e antigo ou sintetico
- o segredo depende de par e nao apareceu no mesmo arquivo
- o push e grande ou complexo demais

## Melhor desenho por contexto

Para uso pessoal em repo publico:

- comece com experimento simples
- se quiser validar bloqueio, prefira um token descartavel real de um provedor suportado

Para uso empresarial:

- teste em repositorio com protecao organizacional habilitada
- inclua fluxo de bypass e auditoria
- valide tambem comportamento de `non-provider patterns` e `custom patterns` quando aplicavel

## Regra de seguranca

Nunca use credencial de producao nesse tipo de teste. O laboratorio so faz sentido com segredo descartavel, de escopo minimo e com revogacao imediata.

## Referencias oficiais

- https://docs.github.com/en/code-security/concepts/secret-security/about-push-protection
- https://docs.github.com/en/code-security/how-tos/secure-your-secrets/prevent-future-leaks/manage-user-push-protection
- https://docs.github.com/en/rest/secret-scanning/push-protection
