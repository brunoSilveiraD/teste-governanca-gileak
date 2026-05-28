# GitHub Secret Scanning, Push Protection e Dependabot

## Resumo do que descobrimos

Este repositorio foi usado para testar recursos de seguranca e governanca do GitHub em um repositorio **publico** e **pessoal**.

Os testes com valores falsos em formatos genericos e de provedores **nao foram bloqueados** no `commit`/`push`. A conclusao mais importante e que, nesse cenario, o GitHub nao bloqueia qualquer texto que "pareca segredo": ele so bloqueia os padroes suportados que consegue identificar com confianca suficiente.

Tambem confirmamos que o item do `task.md` sobre `.github/dependabot.yml` trata de `Dependabot version updates`, que e diferente de `Dependabot alerts` e de `Dependabot security updates`.

## Pontos confirmados na documentacao

### 1. Secret scanning roda automaticamente em repositorios publicos

O GitHub documenta que `secret scanning` roda automaticamente em repositorios publicos, sem custo adicional, para os padroes de segredo suportados pela plataforma. Esse comportamento vale como cobertura base para codigo exposto publicamente no GitHub.com.

Para outros cenarios, a disponibilidade muda. Em repositorios privados e internos de organizacoes, a deteccao de segredos passa a depender de `GitHub Secret Protection`. Em outras palavras, repositorios publicos recebem a cobertura automatica gratuita, enquanto ambientes organizacionais privados usam um modelo de habilitacao e licenciamento proprio.

Na pratica, isso significa que a regra geral nao e "todo repositorio recebe o mesmo nivel de protecao", e sim "repositorios publicos recebem secret scanning automatico, enquanto repositorios privados e corporativos dependem do escopo do produto contratado e habilitado".

Fonte:
- https://docs.github.com/en/code-security/reference/secret-security/supported-secret-scanning-patterns

### 2. Push protection para usuarios vale para repositorios publicos

O GitHub diferencia dois modelos de `push protection`:
- `Push protection for repositories`
- `Push protection for users`

O `push protection for users` e um recurso ligado a conta no GitHub.com. Segundo a documentacao, ele e habilitado por padrao e impede que a propria conta envie segredos para repositorios publicos. Esse e o comportamento mais relevante para contas pessoais e para contribuicoes em repositorios publicos.

Ja o `push protection for repositories` e um recurso configurado no nivel de repositorio, organizacao ou empresa. Ele exige `GitHub Secret Protection`, pode ser habilitado por administradores, e e o modelo usado para proteger repositorios privados, internos ou ambientes corporativos de forma centralizada.

Na pratica, isso ajuda a separar bem os cenarios: contas pessoais em repositorios publicos contam com `push protection for users`; organizacoes e empresas que precisam proteger repositorios privados dependem do modelo de repositorio e do licenciamento correspondente.

Fontes:
- https://docs.github.com/en/code-security/concepts/secret-security/about-push-protection
- https://docs.github.com/en/code-security/how-tos/secure-your-secrets/prevent-future-leaks/manage-user-push-protection

### 3. Push protection nao bloqueia qualquer segredo suportado

O GitHub explica que `secret scanning` e `push protection` nao sao exatamente a mesma cobertura. Um segredo pode estar na lista de padroes suportados por `secret scanning` sem necessariamente ser bloqueado por `push protection`.

A documentacao diz que o `push protection` bloqueia apenas um subconjunto dos padroes com maior identificabilidade entre os segredos que geram alertas para usuarios. A razao declarada pelo GitHub e reduzir falso positivo e evitar bloqueios desnecessarios em pushes legitimos.

Isso vale tanto para repositorios publicos quanto para ambientes corporativos: suporte ao tipo de segredo nao significa bloqueio garantido em todo caso. Na pratica, strings inventadas, exemplos sinteticos ou valores apenas "parecidos com segredo" podem passar sem bloqueio, mesmo quando o tipo geral de token aparece na documentacao.

Fonte:
- https://docs.github.com/en/code-security/reference/secret-security/secret-scanning-detection-scope

Trecho confirmado pela doc:
- `Push protection only blocks leaked secrets on a subset of the most identifiable user-alerted patterns.`

### 4. Padroes genericos e non-provider patterns tem tratamento separado

A documentacao oficial separa os padroes suportados em categorias que incluem segredos de provedor (`provider patterns`), padroes genericos ou `non-provider patterns`, e outros grupos documentados separadamente. Dentro dos padroes genericos, o GitHub lista exemplos como:
- `generic_private_key`
- `openssh_private_key`
- `rsa_private_key`
- `mongodb_connection_string`
- `mysql_connection_url`
- `postgres_connection_string`
- `http_basic_authentication_header`
- `http_bearer_authentication_header`

Esses exemplos sao uteis porque mostram que o GitHub nao trata apenas tokens de provedores como AWS, Anthropic ou Airtable. A plataforma tambem reconhece classes mais genericas de segredo, como chaves privadas, connection strings e headers com credenciais embutidas.

Ao mesmo tempo, a documentacao tambem informa que a deteccao de `non-provider patterns` e um controle separado em repositorios de organizacao, disponivel com `GitHub Secret Protection`. Em cenarios corporativos, isso amplia a cobertura para classes adicionais de segredos; em cenarios pessoais e publicos, nao se deve assumir automaticamente o mesmo comportamento configuravel de uma organizacao.

Na pratica, isso ajuda a entender por que testes com chaves privadas falsas, connection strings sinteticas ou headers fake podem nao acionar bloqueio: a categoria pode existir na documentacao, mas a cobertura real depende do tipo de pattern, do tipo de repositorio e do modelo de habilitacao do recurso.

Fontes:
- https://docs.github.com/en/code-security/reference/secret-security/supported-secret-scanning-patterns
- https://docs.github.com/en/code-security/how-tos/secure-your-secrets/detect-secret-leaks/enabling-secret-scanning-for-non-provider-patterns

### 5. Segredos em par precisam aparecer juntos

Para alguns provedores, a deteccao nao depende de um valor isolado, mas de um par de credenciais. A documentacao usa AWS como exemplo e informa que o identificador e o segredo precisam estar no mesmo arquivo e no mesmo push para que a deteccao aconteca.

Segundo o GitHub, essa exigencia existe para reduzir falso positivo, porque os dois elementos precisam aparecer juntos para representar uma credencial efetivamente utilizavel. Se os valores forem enviados em arquivos diferentes, em pushes diferentes ou fora do mesmo contexto de repositorio, a deteccao nao deve ocorrer como par.

Essa regra vale como explicacao geral de comportamento e e particularmente importante em testes de laboratorio, porque um segredo em par pode parecer "presente" para o leitor sem atender as condicoes que a deteccao realmente exige.

Fonte:
- https://docs.github.com/en/code-security/reference/secret-security/secret-scanning-detection-scope

Exemplo citado pela doc:
- `aws_access_key_id` + `aws_secret_access_key`

### 6. Push protection suporta melhor versoes recentes de tokens

O GitHub informa que provedores atualizam periodicamente os formatos usados para gerar tokens e que um mesmo servico pode manter mais de uma versao em circulacao. Nesses casos, o `push protection` tende a suportar apenas as versoes mais recentes que `secret scanning` consegue identificar com confianca.

O objetivo declarado pela documentacao e evitar bloqueios desnecessarios, especialmente em formatos legados ou ambiguos, que tem maior chance de gerar falso positivo. Isso vale para ambientes pessoais, organizacionais e empresariais: a capacidade de detectar um tipo de token nao implica cobertura identica para todas as versoes historicas daquele token.

Na pratica, isso significa que tokens legados, valores sinteticos ou strings apenas parecidas com o formato oficial podem nao acionar bloqueio, mesmo quando o provedor aparece na lista de padroes suportados. Em ambientes corporativos, o custo adicional de `GitHub Secret Protection` amplia a disponibilidade de recursos em repositorios privados, mas nao elimina essa limitacao de confianca por versao de token.

Fonte:
- https://docs.github.com/en/code-security/reference/secret-security/supported-secret-scanning-patterns

## O que e Dependabot dentro do GitHub

`Dependabot` e a familia de recursos do GitHub voltada para identificar dependencias, sinalizar riscos conhecidos e automatizar parte da manutencao dessas dependencias. Na documentacao, esse conjunto aparece ligado ao ecossistema de seguranca e supply chain do GitHub, em especial quando o repositorio tem dependencias que podem ser mapeadas pelo `Dependency graph`.

As tres capacidades mais importantes para este contexto sao:

- `Dependabot alerts`: notificam quando o GitHub identifica dependencias com vulnerabilidades conhecidas.
- `Dependabot security updates`: abrem pull requests para corrigir dependencias vulneraveis detectadas.
- `Dependabot version updates`: abrem pull requests para atualizar versoes mesmo quando nao existe vulnerabilidade, com base na configuracao do arquivo `.github/dependabot.yml`.

Esses recursos sao relacionados, mas nao equivalentes. `Dependabot alerts` dependem da visibilidade que o GitHub tem sobre as dependencias do repositorio. `Dependabot security updates` dependem da existencia de alertas acionaveis e da capacidade do GitHub de propor uma versao corretiva. Ja `Dependabot version updates` dependem da configuracao declarada em `dependabot.yml` e do ecossistema realmente existir no repositorio com arquivos suportados.

Na pratica, isso significa que o arquivo `.github/dependabot.yml` configura `Dependabot version updates`, nao ativa por si so `Dependabot alerts`, nem substitui a deteccao de vulnerabilidades necessaria para `Dependabot security updates`. Neste repositorio, portanto, o criterio ligado ao `dependabot.yml` se refere especificamente a `version updates`.

Fontes:
- https://docs.github.com/en/code-security/dependabot/dependabot-version-updates/about-dependabot-version-updates
- https://docs.github.com/en/code-security/dependabot/dependabot-version-updates/configuring-dependabot-version-updates
- https://docs.github.com/en/code-security/dependabot/dependabot-security-updates/configuring-dependabot-security-updates

## Como Dependabot se relaciona com outras opcoes de seguranca

Na area de seguranca do GitHub, `Dependabot` funciona em conjunto com outros recursos, mas cada um tem uma responsabilidade diferente:

- `Dependency graph`: descobre e modela as dependencias detectadas no repositorio.
- `Dependency review`: ajuda a avaliar o impacto de mudancas de dependencias em pull requests.
- `Dependabot alerts`: usam essa visibilidade para apontar vulnerabilidades conhecidas em dependencias.
- `Dependabot security updates`: tentam corrigir vulnerabilidades com pull requests automaticos.
- `Dependabot version updates`: mantem dependencias atualizadas de forma continua, mesmo sem alerta de seguranca.
- `Code scanning`: procura vulnerabilidades e outros problemas no codigo-fonte.
- `Secret scanning` e `Push protection`: tratam vazamento de segredos, e nao manutencao de dependencias.

Esses recursos se complementam, mas nao devem ser confundidos. O `Dependency graph` fornece a base de visibilidade sobre dependencias. A partir dessa base, o GitHub pode exibir `Dependabot alerts` quando ha vulnerabilidades conhecidas. Quando houver correcao disponivel e o cenario for suportado, o GitHub pode abrir `Dependabot security updates`. Em paralelo, `Dependabot version updates` seguem uma trilha diferente: elas sao orientadas por configuracao e agenda, e servem para manutencao continua das versoes.

Na pratica, isso ajuda a separar tres tipos de problema:
- visibilidade sobre o que o repositorio usa;
- correcao de vulnerabilidades conhecidas em dependencias;
- atualizacao rotineira de versoes, mesmo sem incidente de seguranca.

Tambem ajuda a deixar claro que recursos como `Code scanning`, `Secret scanning` e `Push protection` pertencem a outras camadas da seguranca do GitHub e nao substituem o papel do `Dependabot`.

Fontes:
- https://docs.github.com/en/code-security/dependency-graph/about-the-dependency-graph
- https://docs.github.com/en/code-security/dependabot/dependabot-version-updates/about-dependabot-version-updates

## Observacoes praticas para este repositorio

De forma geral, `Dependabot version updates` so geram pull requests quando o GitHub encontra no repositorio um ecossistema suportado e arquivos compativeis com esse ecossistema. A configuracao em `.github/dependabot.yml` define quais ecossistemas devem ser monitorados, em quais diretorios, em que frequencia e, quando aplicavel, para qual branch.

Na pratica, isso significa que uma configuracao valida nao basta sozinha. O comportamento esperado tambem depende de o repositorio conter dependencias detectaveis. Alguns exemplos tipicos sao:

- `npm`: manifests como `package.json`
- `nuget`: arquivos como `.csproj` ou `packages.config`
- `github-actions`: workflows em `.github/workflows/*.yml`

Tambem vale lembrar que opcoes como `target-branch` so fazem sentido quando a branch de destino existe e quando o fluxo de atualizacao realmente encontra dependencias suportadas naquele contexto.

Aplicando essa regra geral a este repositorio, o estado atual ajuda a explicar por que a configuracao nao garante PRs imediatos:

1. O repositorio esta quase vazio.
2. A branch `development` nao existe no momento.
3. Nao existem manifests reais de `npm` ou `nuget`.
4. Nao existem workflows em `.github/workflows/` para `github-actions`.

Por isso, a simples existencia de `.github/dependabot.yml` nao garante que `Dependabot version updates` vao aparecer para todos os ecossistemas configurados. Repositorios publicos ou privados, pessoais ou corporativos, podem ter uma configuracao correta e ainda assim nao receber PRs se nao houver manifests validos, workflows presentes, branch alvo existente ou dependencias que o GitHub consiga identificar e atualizar.

Neste repositorio, o arquivo de configuracao continua sendo util como demonstracao de como o recurso e declarado, mas o comportamento observado precisa ser lido a luz dessas pre-condicoes documentadas pelo GitHub.

Fontes:
- https://docs.github.com/en/code-security/dependabot/dependabot-version-updates/configuring-dependabot-version-updates
- https://docs.github.com/en/code-security/dependency-graph/about-the-dependency-graph
- https://docs.github.com/en/code-security/supply-chain-security/understanding-your-software-supply-chain/dependency-graph-supported-package-ecosystems

## O que testamos

### Rodada 1

Arquivo com padroes genericos falsos:
- private keys
- connection strings
- `Authorization: Bearer`
- `Authorization: Basic`

Resultado:
- o GitHub nao bloqueou

### Rodada 2

Arquivo reduzido para padroes de provedor falsos:
- `anthropic_api_key`
- um PAT sintetico de Airtable
- `aws_access_key_id` + `aws_secret_access_key`

Resultado:
- o GitHub tambem nao bloqueou

## Conclusao pratica

Para este cenario, aprendemos que:

1. Repositorio publico pessoal tem protecao, mas nao garante bloqueio para strings fake.
2. Valores inventados podem nao ser reconhecidos como segredo real ou suficientemente confiavel.
3. `Dependabot` e um conjunto de recursos diferentes, e o arquivo `.github/dependabot.yml` controla apenas `version updates`.
4. Configurar `dependabot.yml` sem manifests reais e sem branch alvo existente nao garante PRs imediatos.
5. Testes com `push protection` ficam muito mais confiaveis quando usam:
   - um segredo real de laboratorio, revogado logo apos o teste, ou
   - um repositorio de organizacao com `GitHub Secret Protection`, `non-provider patterns` e/ou `custom patterns`.

## Links oficiais usados nesta documentacao

- https://docs.github.com/en/code-security/reference/secret-security/supported-secret-scanning-patterns
- https://docs.github.com/en/code-security/reference/secret-security/secret-scanning-detection-scope
- https://docs.github.com/en/code-security/concepts/secret-security/about-push-protection
- https://docs.github.com/en/code-security/concepts/secret-security/push-protection-from-the-command-line
- https://docs.github.com/en/code-security/how-tos/secure-your-secrets/prevent-future-leaks/manage-user-push-protection
- https://docs.github.com/en/code-security/how-tos/secure-your-secrets/detect-secret-leaks/enabling-secret-scanning-for-non-provider-patterns
- https://docs.github.com/en/code-security/dependabot/dependabot-version-updates/about-dependabot-version-updates
- https://docs.github.com/en/code-security/dependabot/dependabot-version-updates/configuring-dependabot-version-updates
- https://docs.github.com/en/code-security/dependabot/dependabot-security-updates/configuring-dependabot-security-updates
- https://docs.github.com/en/code-security/dependency-graph/about-the-dependency-graph
- https://docs.github.com/en/code-security/supply-chain-security/understanding-your-software-supply-chain/dependency-graph-supported-package-ecosystems
