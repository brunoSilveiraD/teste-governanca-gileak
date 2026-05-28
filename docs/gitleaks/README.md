# Gitleaks

`Gitleaks` e uma ferramenta externa, separada do GitHub, voltada para detectar segredos hardcoded em repositorios Git. Ela pode ser usada localmente, em `pre-commit`, em CI/CD ou como `GitHub Action`.

## Onde ele entra

Neste laboratorio, o `Gitleaks` representa a camada de scan que voce controla diretamente no pipeline, independentemente dos recursos nativos do GitHub.

Ele e util quando voce quer:

- falhar o CI ao encontrar segredos
- adicionar regras proprias
- trabalhar tambem fora do GitHub
- ter um scanner dedicado em PR, push ou rotina agendada

## Como ele detecta

Segundo a documentacao oficial, o `Gitleaks` usa combinacoes de:

- `regex`
- `keywords`
- `entropy`
- `allowlists`
- filtros de caminho

O resultado e um scanner de padrao. Isso significa que um achado pode ser:

- segredo real
- segredo de teste
- valor sintetico
- falso positivo

## Como ler esta pasta

- `exemplos-no-repo.md`: mostra o tipo de cobertura e a leitura de findings
- `pipeline-e-falsos-positivos.md`: mostra como colocar o scan em workflow e como lidar com ruido

## Referencias oficiais

- https://github.com/gitleaks/gitleaks
- https://github.com/gitleaks/gitleaks/blob/master/config/gitleaks.toml
- https://github.com/gitleaks/gitleaks-action
