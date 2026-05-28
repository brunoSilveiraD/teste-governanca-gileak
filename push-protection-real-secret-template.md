# Push Protection Test Template

Este arquivo e um template seguro para testar `push protection` em repositorio publico.

Nao inclui segredo real. Para executar o teste, substitua manualmente um dos placeholders abaixo por uma credencial descartavel, de escopo minimo, com expiracao curta, e revogue logo depois do teste.

## Candidatos mais fortes para teste em repo publico

Observacao importante:
- A documentacao do GitHub mostra que `push protection` cobre apenas os tipos suportados e tende a detectar melhor versoes recentes dos tokens.
- Entao estes candidatos sao de alta probabilidade, nao garantia absoluta.

### 1. GitHub Personal Access Token

Provider: `GitHub`
Secret type: `GITHUB_PERSONAL_ACCESS_TOKEN`
Motivo: e um dos candidatos mais fortes para laboratorio porque o proprio GitHub documenta esse tipo nas APIs e no fluxo de `push protection`.

```text
github_pat_for_test = "<PASTE_REAL_GITHUB_PAT_HERE>"
```

### 2. GitHub SSH Private Key

Provider: `GitHub`
Secret type: `github_ssh_private_key`
Motivo: aparece na documentacao oficial de padroes suportados com `push protection`.

```text
-----BEGIN OPENSSH PRIVATE KEY-----
<PASTE_REAL_TEST_KEY_MATERIAL_HERE>
-----END OPENSSH PRIVATE KEY-----
```

### 3. Slack API Token

Provider: `Slack`
Secret type: `slack_api_token`
Motivo: aparece na documentacao oficial de padroes suportados; pode ser um bom teste se voce tiver um workspace de laboratorio.

```text
slack_api_token = "<PASTE_REAL_SLACK_TOKEN_HERE>"
```

### 4. 1Password Service Account Token

Provider: `1Password`
Secret type: `1password_service_account_token`
Motivo: aparece na lista oficial com suporte a `push protection`.

```text
op_service_account_token = "<PASTE_REAL_1PASSWORD_TOKEN_HERE>"
```

## Como usar no teste

1. Escolha apenas um candidato por rodada.
2. Cole o segredo real descartavel no placeholder.
3. Salve este conteudo em um arquivo temporario de teste, por exemplo `lab-real-secret.txt`.
4. Rode `git add`, `git commit` e `git push`.
5. Se o push for bloqueado, capture a mensagem e revogue o segredo.
6. Se o push passar, revogue o segredo imediatamente e limpe o historico antes de seguir.

## Referencias oficiais

- https://docs.github.com/en/code-security/concepts/secret-security/about-push-protection
- https://docs.github.com/en/enterprise-cloud@latest/code-security/secret-scanning/working-with-secret-scanning-and-push-protection/push-protection-for-users
- https://docs.github.com/en/code-security/reference/secret-security/supported-secret-scanning-patterns
- https://docs.github.com/en/code-security/reference/secret-security/secret-scanning-detection-scope
- https://docs.github.com/en/rest/secret-scanning/push-protection
