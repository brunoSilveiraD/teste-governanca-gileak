# Push Protection nos exemplos do repositorio

O repositorio documentou uma expectativa comum: "se eu escrever algo que parece segredo, o GitHub vai bloquear". O laboratorio mostrou que essa expectativa e simplista demais.

## 1. O que foi tentado

As rodadas anteriores registraram:

- segredos genericos falsos
- chaves privadas falsas
- headers de autorizacao sinteticos
- formatos de provedores inventados
- pares AWS simulados

## 2. O que aconteceu

Os pushes de teste nao foram bloqueados da forma esperada.

Como observacao de laboratorio, isso e um dado util. Como conclusao geral sobre o produto, isso nao basta.

## 3. O que a documentacao oficial ajuda a explicar

O GitHub documenta que:

- `push protection` so bloqueia um subconjunto dos padroes mais identificaveis
- alguns segredos precisam aparecer em par
- versoes antigas ou formatos sinteticos podem nao ser suportados
- pushes grandes podem escapar do bloqueio preventivo e gerar apenas alertas depois

## 4. Como ler este resultado sem erro

Leitura errada:

- "o GitHub nao protege repositorios publicos"

Leitura melhor:

- "meu teste fake nao atendeu necessariamente as condicoes que o GitHub usa para bloquear"

## 5. O que este repositorio ensina bem

Ele ensina os limites do experimento com strings artificiais. Isso e valioso, porque ajuda a desenhar um laboratorio melhor na rodada seguinte.

## Referencias oficiais

- https://docs.github.com/en/code-security/concepts/secret-security/about-push-protection
- https://docs.github.com/en/code-security/reference/secret-security/secret-scanning-detection-scope
