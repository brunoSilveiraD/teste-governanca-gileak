# Secret Scanning nos exemplos do repositorio

O aprendizado mais util deste laboratorio foi perceber que "parece segredo" nao significa automaticamente "o GitHub vai alertar ou bloquear".

## 1. O que foi testado aqui

Os materiais antigos do repositorio registraram rodadas com:

- private keys falsas
- connection strings sinteticas
- `Authorization: Bearer`
- `Authorization: Basic`
- exemplos falsos de provedores
- pares simulados de AWS

## 2. O que foi observado

Nessas rodadas, os valores falsos nao geraram o comportamento forte esperado pelo autor do experimento.

Essa observacao local continua valida como registro do laboratorio, mas precisa ser lida junto da documentacao oficial.

## 3. O que a documentacao explica

A documentacao do GitHub deixa tres pontos importantes:

- nem todo tipo suportado por `secret scanning` e necessariamente bloqueado por `push protection`
- alguns segredos funcionam como par e precisam aparecer juntos no mesmo arquivo
- a deteccao favorece formatos com maior confianca e menor taxa de falso positivo

Por isso, um teste com string inventada pode falhar como experimento sem que isso signifique defeito do produto.

## 4. Como interpretar os resultados deste repo

O jeito mais seguro de ler os experimentos e:

- tratá-los como observacoes locais
- nao generalizar que "GitHub nao detecta segredo"
- verificar sempre se o tipo de segredo, o formato e o contexto do push batem com a documentacao atual

## 5. A principal licao pratica

Para laboratorio serio de `Secret Scanning`, existe uma diferenca grande entre:

- testar com conteudo fake e generico
- testar com segredo real descartavel, em provedor suportado, com formato atual

O primeiro caso ajuda a estudar limites. O segundo ajuda a validar fluxo.

## Referencias oficiais

- https://docs.github.com/en/code-security/concepts/secret-security/about-secret-scanning
- https://docs.github.com/en/code-security/reference/secret-security/secret-scanning-detection-scope
- https://docs.github.com/en/code-security/reference/secret-security/supported-secret-scanning-patterns
