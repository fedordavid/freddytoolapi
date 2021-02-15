# EXPOSE 80
# EXPOSE 443
# Stage 1
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app
#COPY X.Data/. ./X.Data/
COPY freddytoolapi/*.sln .
COPY freddytoolapi/Freddy.API/*.csproj ./Freddy.API/
COPY freddytoolapi/Freddy.Application/*.csproj ./Freddy.Application/
COPY freddytoolapi/Freddy.Persistence/*.csproj ./Freddy.Persistence/
COPY freddytoolapi/Freddy.Host/*.csproj ./Freddy.Host/
COPY freddytoolapi/Freddy.IntegrationTests/*.csproj ./Freddy.IntegrationTests/
COPY freddytoolapi/Freddy.UnitTests/*.csproj ./Freddy.UnitTests/

RUN dotnet restore

COPY freddytoolapi/Freddy.API/. ./Freddy.API/
COPY freddytoolapi/Freddy.Application/. ./Freddy.Application/
COPY freddytoolapi/Freddy.Persistence/. ./Freddy.Persistence/
COPY freddytoolapi/Freddy.Host/. ./Freddy.Host/
COPY freddytoolapi/Freddy.IntegrationTests/. ./Freddy.IntegrationTests/
COPY freddytoolapi/Freddy.UnitTests/. ./Freddy.UnitTests/

RUN dotnet publish -c Release -o /app/out
# Stage 2
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS final
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "Freddy.Host.dll"]