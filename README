# CDoc

CDoc is a read-only wiki generated from markdown files on a git repository.  

Wrote this due to not finding a good self hosted markdown site generator that didn't require CI/CD pipelines.  
CDoc is simple in that it `just` displays what is stored on the repository and automatically syncs with your repositories.


Features include:

- Supports markdown, pdf and images as preview, other files as binary downloads
- Files can be organized in a folder structure that will be the browsable menu
- Supports both public and private repositories (SSH auth)
- Supports multiple sources/git repositories
- Supports ordering based on a `xx_` prefix
- API that can be used for custom integrations
- Runs using docker(-compose) or kubernetes
- Self hosted
- Light weight

## Resources

| | |
|---|---|
| GitHub | [https://github.com/nvanlaerebeke/cdoc](https://github.com/nvanlaerebeke/cdoc)
| Demo website | [https://cdoc.crazytje.com](https://cdoc.crazytje.com)
| Docker hub (api) | [https://hub.docker.com/r/crazytje/cdoc.api](https://hub.docker.com/r/crazytje/cdoc.api)
| Docker hub (frontend) | [https://hub.docker.com/r/crazytje/cdoc.web](https://hub.docker.com/r/crazytje/cdoc.web)

## Quickstart

In the `./examples` folder samples can be found for running on:

- docker
- docker-compose
- kubernetes
- helm chart

## Configuration

CDoc is split in two parts, a backend API that provides the data and a frontend angular application that uses that API.

### Backend

The CDoc API uses a simple configuration file:

```json
{
  "sources": [
    {
      "repository": "",
      "ssh_key": "",
      "cache": "disk",
      "auto_update_seconds": 3600,
      "pull_before_read": false
    }
  ]
}
```

See `/etc/cdoc.json.example`.  

Each repository can be added as a source, if authentication is needed (example a private github/gitea/bitbucket/... repository) provide the `ssh_key` used to connect.  
This file must be mounted in the container on `/etc/cdoc.json`, see the `docker-compose` and `kubernetes` examples.

### Frontend

The frontend only needs a single configuration parameter and that is the API endpoint.

```json
{
    "apiBaseUrl": "http://localhost:4201"
}
```

This file must be mounted on `/usr/share/nginx/html/cdoc.web/assets/cdoc.web.json`, see docker(-compose) and kubernetes examples.

