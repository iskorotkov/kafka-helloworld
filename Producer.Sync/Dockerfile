FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Producer.Sync/Producer.Sync.csproj", "Producer.Sync/"]
RUN dotnet restore "Producer.Sync/Producer.Sync.csproj"
COPY . .
WORKDIR "/src/Producer.Sync"
RUN dotnet build "Producer.Sync.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Producer.Sync.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Producer.Sync.dll"]
