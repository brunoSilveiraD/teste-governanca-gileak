# O que o Gitleaks pega

## Objetivo

Este guia resume o que o `Gitleaks` detecta na configuracao padrao, quais tipos de ecossistema aparecem nas regras oficiais e como ler um achado como segredo real, token de teste ou possivel falso positivo.

O conteudo abaixo foi montado apenas com base em:

- `gitleaks/gitleaks` README oficial
- `gitleaks/gitleaks` `config/gitleaks.toml` oficial

## Como o Gitleaks detecta

Pela documentacao oficial, o Gitleaks usa regras configuraveis para detectar segredos. Essas regras podem combinar:

- `regex`
- `keywords`
- `entropy`
- `path`
- `allowlists`

O README oficial descreve que uma regra pode usar expressao regular e entropia minima para considerar que um trecho "pode ser um segredo". A configuracao padrao tambem traz `allowlists` para reduzir matches indesejados.

Em outras palavras: o Gitleaks detecta **padroes** que podem indicar credenciais expostas. A documentacao oficial nao promete que todo match seja uma credencial valida ou ativa.

## Quais tipos de coisa ele pega

A configuracao padrao oficial cobre, entre outros, estes tipos:

- `api key`
- `access token`
- `personal access token`
- `refresh token`
- `client secret`
- `client id`
- `secret key`
- `encryption key`
- `generic api key`

Exemplos diretos da configuracao oficial:

- `aws-access-token`
- `azure-ad-client-secret`
- `gcp-api-key`
- `anthropic-api-key`
- `airtable-api-key`
- `airtable-personnal-access-token`
- `digitalocean-access-token`
- `digitalocean-pat`
- `discord-api-token`
- `discord-client-id`
- `discord-client-secret`
- `droneci-access-token`
- `dropbox-api-token`
- `easypost-api-token`
- `easypost-test-api-token`
- `facebook-access-token`
- `facebook-secret`
- `flutterwave-public-key`
- `flutterwave-secret-key`
- `generic-api-key`

## Quais ecossistemas aparecem na configuracao padrao

A configuracao oficial nao organiza as regras por "ecossistema" em uma tabela unica, mas as regras mostram cobertura para varios grupos de servicos. Exemplos visiveis na config padrao:

- Cloud e infraestrutura: `AWS`, `Alibaba Cloud`, `Azure AD`, `Cloudflare`, `DigitalOcean`, `Fly.io`
- AI e dados: `Anthropic`, `Airtable`, `Algolia`, `Databricks`, `ClickHouse Cloud`, `Cohere`
- DevOps e CI/CD: `Artifactory`, `DroneCI`, `Doppler`
- Colaboracao e armazenamento: `Discord`, `Dropbox`, `Frame.io`
- Observabilidade e operacao: `Datadog`, `Dynatrace`, `Fastly`
- Financeiro e pagamentos: `EasyPost`, `Finicity`, `Finnhub`, `Flutterwave`
- Social e plataforma web: `Facebook`, `Flickr`, `Etsy`
- Criptografia: `age-secret-key`
- Generico: `generic-api-key`

Essa lista e representativa, nao exaustiva. A fonte oficial mais fiel para cobertura completa e sempre o arquivo `config/gitleaks.toml`.

## Segredo real, token de teste ou falso positivo

### 1. Segredo real

Pela forma como as regras oficiais sao descritas, um achado tende a ser tratado como segredo real quando o valor bate em um formato de credencial que a regra identifica como arriscado para acesso indevido.

Exemplos oficiais:

- `aws-access-token`
- `azure-ad-client-secret`
- `anthropic-api-key`
- `digitalocean-pat`
- `facebook-secret`

### 2. Token ou chave de teste

A configuracao padrao oficial tambem detecta alguns formatos de teste, nao so credenciais de producao.

Exemplos oficiais:

- `easypost-test-api-token`
- `flutterwave-public-key`
- `flutterwave-secret-key`

Isso significa que um match pode ser um valor de teste e ainda assim ser detectado, se houver regra explicita para esse formato.

### 3. Possivel falso positivo

Pela documentacao oficial, falsos positivos sao tratados por mecanismos de excecao da propria ferramenta:

- `gitleaks:allow` para ignorar um segredo conhecido naquela linha
- `.gitleaksignore` para ignorar findings especificos por `Fingerprint`
- `allowlists` na configuracao para reduzir matches indevidos

O proprio exemplo de configuracao do README fala em `targetRules` com descricao de arquivos de teste que disparam `false-positives`. A configuracao padrao tambem mostra regras com `allowlists` e `stopwords` para reduzir ruido.

Exemplos oficiais de reducao de falso positivo:

- `gcp-api-key` traz `allowlists` com chaves de exemplo conhecidas
- `generic-api-key` traz `allowlists` e uma lista grande de `stopwords`

## O caso especial de `generic-api-key`

Essa e a regra mais importante para entender o comportamento do Gitleaks fora de provedores especificos.

Na configuracao oficial, `generic-api-key` procura combinacoes de termos como:

- `access`
- `api`
- `auth`
- `key`
- `credential`
- `password`
- `secret`
- `token`

Depois, a regra exige um valor com formato compativel e `entropy = 3.5`.

Por isso, ela consegue pegar muitos segredos "sem provedor", mas tambem e a area em que mais faz sentido existir ajuste fino com:

- `allowlists`
- `.gitleaksignore`
- `gitleaks:allow`

## Como interpretar um achado com seguranca

- Se a regra e de provedor especifico e o valor bate exatamente no formato oficial da regra, trate primeiro como potencial segredo real.
- Se a regra e de teste, trate como credencial detectada, mesmo que nao seja de producao.
- Se a regra e `generic-api-key`, confirme o contexto antes de concluir que houve vazamento real.
- Se o valor for intencional, sintetico ou parte de fixture/teste, a propria documentacao oficial aponta os mecanismos de excecao acima.

## Referencias oficiais

- Gitleaks README: https://github.com/gitleaks/gitleaks
- Configuracao padrao oficial: https://github.com/gitleaks/gitleaks/blob/master/config/gitleaks.toml
