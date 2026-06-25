FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ClientePersona.Api/ClientePersona.Api.csproj", "ClientePersona.Api/"]
RUN dotnet restore "ClientePersona.Api/ClientePersona.Api.csproj"
COPY . .
WORKDIR "/src/ClientePersona.Api"
RUN dotnet publish "ClientePersona.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ClientePersona.Api.dll"]