# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy solution file and project files
COPY TennisStats.sln ./
COPY src/TennisStats.Domain/TennisStats.Domain.csproj src/TennisStats.Domain/
COPY src/TennisStats.Application/TennisStats.Application.csproj src/TennisStats.Application/
COPY src/TennisStats.Infrastructure/TennisStats.Infrastructure.csproj src/TennisStats.Infrastructure/
COPY src/TennisStats.Api/TennisStats.Api.csproj src/TennisStats.Api/
COPY tests/TennisStats.Tests/TennisStats.Tests.csproj tests/TennisStats.Tests/

# Restore dependencies
RUN dotnet restore

# Copy everything else
COPY . .

# Build and Publish
WORKDIR /app/src/TennisStats.Api
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Expose port 8080 (standard for many cloud providers)
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "TennisStats.Api.dll"]
