FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY PokerTracker.slnx .
COPY src/PokerTracker.Domain/PokerTracker.Domain.csproj src/PokerTracker.Domain/
COPY src/PokerTracker.Infrastructure/PokerTracker.Infrastructure.csproj src/PokerTracker.Infrastructure/
COPY src/PokerTracker.Api/PokerTracker.Api.csproj src/PokerTracker.Api/
RUN dotnet restore

COPY src/ src/
RUN dotnet publish src/PokerTracker.Api/PokerTracker.Api.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PokerTracker.Api.dll"]
