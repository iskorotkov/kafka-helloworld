FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["kafka-helloworld/kafka-helloworld.csproj", "kafka-helloworld/"]
RUN dotnet restore "kafka-helloworld/kafka-helloworld.csproj"
COPY . .
WORKDIR "/src/kafka-helloworld"
RUN dotnet build "kafka-helloworld.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "kafka-helloworld.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "kafka-helloworld.dll"]
