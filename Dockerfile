# ============================ build ============================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia arquivos de projeto primeiro (melhor cache de restore)
COPY *.sln Directory.Build.props global.json ./
COPY src/InclusaoDiversidade.Domain/*.csproj          src/InclusaoDiversidade.Domain/
COPY src/InclusaoDiversidade.Application/*.csproj     src/InclusaoDiversidade.Application/
COPY src/InclusaoDiversidade.Infrastructure/*.csproj  src/InclusaoDiversidade.Infrastructure/
COPY src/InclusaoDiversidade.Api/*.csproj             src/InclusaoDiversidade.Api/
COPY tests/InclusaoDiversidade.Tests/*.csproj         tests/InclusaoDiversidade.Tests/

RUN dotnet restore src/InclusaoDiversidade.Api/InclusaoDiversidade.Api.csproj

# Copia o restante do código e publica
COPY . .
RUN dotnet publish src/InclusaoDiversidade.Api/InclusaoDiversidade.Api.csproj \
    -c Release -o /app/publish /p:UseAppHost=false

# =========================== runtime ===========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "InclusaoDiversidade.Api.dll"]
