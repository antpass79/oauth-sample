# oauth-sample

The repository provides two authentication servers (.NET Frameowork 4.6.2 and .NET 6) and one resource server (.NET Framework 4.6.2)

## Projects

The repository is composed by:

- Authorization.Core: it contains common components for both AuthorizationServer and ResourceServer
- AuthorizationServer: it provides the jwt
- ResourceServer: it uses the jwt for accessing to resource endpoints
- OAuthMyLabService: it provides the jwt

The AuthorizationServer and OAuthMyLabService do the same function, they provide the jwt for ResourceServer.

The difference is in the implementation:

- AuthorizationServer is based on .NET Framework 4.6.2, as ResourceServer
- OAuthMyLabService is based on .NET 6

OAuthMyLabService is able to provide jtw for ResourceServer also.

## References

- <https://stackoverflow.com/questions/32616069/customizing-allowed-grant-types-with-oauthauthorizationserver>
- <https://www.cnblogs.com/eedc/p/6294325.html>
- <https://github.com/txgz999/aspnet/wiki/OAuth2-Resource-Owner-Password-Credentials-Grant-Flow>