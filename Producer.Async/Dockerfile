FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Producer.Async/Producer.Async.csproj", "Producer.Async/"]
RUN dotnet restore "Producer.Async/Producer.Async.csproj"
COPY . .
WORKDIR "/src/Producer.Async"
RUN dotnet build "Producer.Async.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Producer.Async.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Producer.Async.dll"]
