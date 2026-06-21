FROM mcr.microsoft.com/dotnet/sdk:10.0 AS builder
WORKDIR /app
COPY SyncedHealth.Center.Platform/*.csproj SyncedHealth.Center.Platform/
RUN dotnet restore ./SyncedHealth.Center.Platform
COPY . .
RUN dotnet publish ./SyncedHealth.Center.Platform -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=builder /app/out .
EXPOSE 8080
ENTRYPOINT ["dotnet", "SyncedHealth.Center.Platform.dll"]