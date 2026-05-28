# GitHub Push Protection

`Push Protection` e a camada preventiva do GitHub para segredos: em vez de apenas alertar depois, ela tenta impedir que o segredo chegue ao repositorio.

## Diferenca essencial

- `Secret Scanning`: detecta e alerta depois que o conteudo ja entrou no GitHub
- `Push Protection`: tenta bloquear antes da gravacao no repositorio

Os dois recursos sao relacionados, mas nao identicos.

## Dois modelos

Segundo a documentacao oficial, existem dois tipos:

- `Push protection for users`
- `Push protection for repositories`

Resumo pratico:

- protecao por usuario e ligada a conta no GitHub.com, focada em pushes para repositorios publicos
- protecao por repositorio depende de `GitHub Secret Protection` e faz mais sentido em contexto organizacional

## Bypass e trilha

Em contexto de repositorio protegido, a documentacao oficial destaca:

- possibilidade de bypass com justificativa
- criacao de alertas de bypass
- registro em audit log
- email para papeis administrativos observando o repositorio

## Limitacoes importantes

`Push Protection` nao bloqueia qualquer string parecida com segredo. A propria documentacao destaca limites como:

- suporte apenas a subconjunto dos padroes mais identificaveis
- menor cobertura para formatos antigos
- limites de tamanho e complexidade do push
- saltos de scan em pushes publicos acima de 50 MB

## Leitura recomendada nesta pasta

- `exemplos-no-repo.md`: o que o repositorio tentou testar
- `testes-de-laboratorio.md`: como montar rodadas mais confiaveis e seguras

## Referencias oficiais

- https://docs.github.com/en/code-security/concepts/secret-security/about-push-protection
- https://docs.github.com/en/code-security/reference/secret-security/secret-scanning-detection-scope
