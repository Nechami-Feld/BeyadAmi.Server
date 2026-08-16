FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

COPY ["BeyadAmi.Server.sln", "./"]
COPY ["BeyadAmi.Server.Api/BeyadAmi.Server.Api.csproj", "BeyadAmi.Server.Api/"]
COPY ["BeyadAmi.Server.Application/BeyadAmi.Server.Application.csproj", "BeyadAmi.Server.Application/"]
COPY ["BeyadAmi.Server.Domain/BeyadAmi.Server.Domain.csproj", "BeyadAmi.Server.Domain/"]
COPY ["BeyadAmi.Server.Infrastructure/BeyadAmi.Server.Infrastructure.csproj", "BeyadAmi.Server.Infrastructure/"]

RUN dotnet restore "BeyadAmi.Server.Api/BeyadAmi.Server.Api.csproj"

COPY . .

RUN dotnet build "BeyadAmi.Server.Api/BeyadAmi.Server.Api.csproj" \
    -c Release \
    --no-restore \
    -o /app/build

RUN dotnet publish "BeyadAmi.Server.Api/BeyadAmi.Server.Api.csproj" \
    -c Release \
    --no-restore \
    -o /app/publish \
    /p:UseAppHost=false


FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

# Disable inotify/FileSystemWatcher
ENV DOTNET_USE_POLLING_FILE_WATCHER=1

# Render PORT
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT}

ENTRYPOINT ["dotnet", "BeyadAmi.Server.Api.dll"]