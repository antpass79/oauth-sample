# OAuthMyLabService

OAuth service to provide jwt for resource services

## How to manage a trusted certificate

	dotnet user-secrets -p oauthmylabservice.csproj set "Kestrel:Certificates:Development:Password" "<<mypassword>>"

	dotnet dev-certs https --trust

## How to run docker image

Follow these steps:

- Install Docker Desktop (<https://www.docker.com/products/docker-desktop/>)
- Create Dockerfile
- From cmd under OAuthMyLabService, follow this guide <https://github.com/dotnet/dotnet-docker/blob/main/samples/run-aspnetcore-https-development.md>

*Notes*

Remember to use this command:

	docker run --rm -it -p 7500:7500 -p 7501:7501 -e ASPNETCORE_URLS="https://+:7501;http://+:7500" -e ASPNETCORE_HTTPS_PORT=7501 -e ASPNETCORE_ENVIRONMENT=Development -v $env:APPDATA\microsoft\UserSecrets\:/root/.microsoft/usersecrets -v $env:USERPROFILE\.aspnet\https:/root/.aspnet/https/ oauthmylabservice

## References

- <https://www.codemag.com/Article/2105051/Implementing-JWT-Authentication-in-ASP.NET-Core-5>
- <https://stackoverflow.com/questions/59747076/is-it-possible-to-have-multiple-audiences-with-clientids-and-secrets>

### Certificates

- <https://thecodeblogger.com/2021/05/07/certificates-and-limits-for-asp-net-core-kestrel-web-server/>
- <https://dev.to/___bn___/free-certified-ssl-certificate-in-asp-net-5-kestrel-application-kgn>
- <https://devblogs.microsoft.com/dotnet/configuring-https-in-asp-net-core-across-different-platforms/>

### Docker

- <https://github.com/dotnet/dotnet-docker/blob/main/samples/run-aspnetcore-https-development.md>