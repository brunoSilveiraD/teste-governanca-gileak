# Gitleaks no repositorio

Os materiais anteriores deste repo mostravam duas coisas ao mesmo tempo:

- que a configuracao padrao do `Gitleaks` cobre muitos provedores e formatos
- que nem todo match deve ser tratado imediatamente como vazamento confirmado

## 1. O que a configuracao padrao costuma cobrir

O arquivo oficial `config/gitleaks.toml` traz regras para varios grupos, por exemplo:

- cloud e infraestrutura
- plataformas de desenvolvimento
- tokens de API de provedores especificos
- chaves e credenciais genericas

No material consolidado deste laboratorio, os exemplos que mais ajudam a leitura sao:

- `aws-access-token`
- `azure-ad-client-secret`
- `gcp-api-key`
- `anthropic-api-key`
- `generic-api-key`

## 2. O caso mais importante: `generic-api-key`

Para uso didatico, a regra `generic-api-key` e a melhor para explicar por que o `Gitleaks` pode encontrar muita coisa:

- ela busca termos como `token`, `secret`, `password`, `api` e `key`
- depois combina isso com formato e entropia

Resultado pratico:

- ela aumenta cobertura para segredos sem provedor explicito
- ela tambem e a area em que mais faz sentido esperar falso positivo

## 3. Como interpretar um finding

Ao ler um achado do `Gitleaks`, a ordem mais segura e:

1. Ver se a regra e especifica de provedor.
2. Confirmar se o valor parece um formato real ou apenas um exemplo sintetico.
3. Ver se o contexto do arquivo indica fixture, doc, teste ou credencial valida.
4. Decidir entre remediar, ignorar ou ajustar regra.

## 4. O que este repositorio ensina bem

Este repo ajuda especialmente a explicar:

- cobertura por padrao
- diferenca entre segredo real e match heuristico
- necessidade de contexto humano na triagem

Ele nao prova sozinho que um finding e valido em producao; ele prova como interpretar a ferramenta.

## 5. Observacao do laboratorio

Ao contrario do `GitHub Secret Scanning`, o `Gitleaks` aqui aparece como ferramenta que voce decide executar e configurar. Isso muda a conversa:

- no GitHub nativo, o foco e cobertura da plataforma
- no Gitleaks, o foco e gate operacional controlado por voce

## Referencias oficiais

- https://github.com/gitleaks/gitleaks
- https://github.com/gitleaks/gitleaks/blob/master/config/gitleaks.toml
