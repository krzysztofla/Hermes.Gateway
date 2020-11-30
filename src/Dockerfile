FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 8000
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Hermes.Gateway.Ocelot.csproj", "src/"]
RUN dotnet restore "src\Hermes.Gateway.Ocelot.csproj"
COPY . .
WORKDIR "/src/src"
RUN dotnet build "Hermes.Gateway.Ocelot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hermes.Gateway.Ocelot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hermes.Gateway.Ocelot.dll"]
