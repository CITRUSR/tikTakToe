FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG configuration=Debug
WORKDIR /src
COPY ["server/server.csproj", "server/"]
RUN dotnet restore "server/server.csproj"
COPY . .
WORKDIR "/src/server"
RUN dotnet build "server.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Debug
RUN dotnet publish "server.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "server.dll"]
