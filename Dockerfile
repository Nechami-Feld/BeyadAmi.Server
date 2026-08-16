# =========================
# Build stage
# =========================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

# Copy solution and project files
COPY ["BeyadAmi.Server.sln", "./"]
COPY ["BeyadAmi.Server.Api/BeyadAmi.Server.Api.csproj", "BeyadAmi.Server.Api/"]
COPY ["BeyadAmi.Server.Application/BeyadAmi.Server.Application.csproj", "BeyadAmi.Server.Application/"]
COPY ["BeyadAmi.Server.Domain/BeyadAmi.Server.Domain.csproj", "BeyadAmi.Server.Domain/"]
COPY ["BeyadAmi.Server.Infrastructure/BeyadAmi.Server.Infrastructure.csproj", "BeyadAmi.Server.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "BeyadAmi.Server.Api/BeyadAmi.Server.Api.csproj"

# Copy source code
COPY . .

# Build
RUN dotnet build "BeyadAmi.Server.Api/BeyadAmi.Server.Api.csproj" \
    -c Release \
    --no-restore \
    -o /app/build

# Publish
RUN dotnet publish "BeyadAmi.Server.Api/BeyadAmi.Server.Api.csproj" \
    -c Release \
    --no-restore \
    -o /app/publish \
    /p:UseAppHost=false


# =========================
# Runtime stage
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

# Render provides the PORT environment variable
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT}

ENTRYPOINT ["dotnet", "BeyadAmi.Server.Api.dll"]