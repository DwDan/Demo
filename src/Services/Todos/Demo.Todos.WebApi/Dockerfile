FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Demo.Todos.WebApi/Demo.Todos.WebApi.csproj", "Demo.Todos.WebApi/"]
COPY ["Demo.Common.WebApi/Demo.Common.WebApi.csproj", "Demo.Common.WebApi/"]
COPY ["Demo.Common.Application/Demo.Common.Application.csproj", "Demo.Common.Application/"]
COPY ["Demo.Todos.Application/Demo.Todos.Application.csproj", "Demo.Todos.Application/"]
COPY ["Demo.Todos.Domain/Demo.Todos.Domain.csproj", "Demo.Todos.Domain/"]
COPY ["Demo.Common.Domain/Demo.Common.Domain.csproj", "Demo.Common.Domain/"]
COPY ["Demo.Todos.Infrastructure/Demo.Todos.Infrastructure.csproj", "Demo.Todos.Infrastructure/"]
COPY ["Demo.Common.Infrastructure/Demo.Common.Infrastructure.csproj", "Demo.Common.Infrastructure/"]
COPY ["Demo.Todos.IoC/Demo.Todos.IoC.csproj", "Demo.Todos.IoC/"]

RUN dotnet restore "./Demo.Todos.WebApi/Demo.Todos.WebApi.csproj"
COPY . .

WORKDIR "/src/Demo.Todos.WebApi"
RUN dotnet build "./Demo.Todos.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Demo.Todos.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Demo.Todos.WebApi.dll"]
