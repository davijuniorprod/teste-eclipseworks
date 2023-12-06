FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TaskManager.WebApi/TaskManager.WebApi.csproj", "TaskManager.WebApi/"]
COPY ["TaskManager.Infrastructure/TaskManager.Infrastructure.csproj", "TaskManager.Infrastructure/"]
COPY ["TaskManager.Domain/TaskManager.Domain.csproj", "TaskManager.Domain/"]
COPY ["TaskManager.Core/TaskManager.Core.csproj", "TaskManager.Core/"]
COPY ["TaskManager.Application/TaskManager.Application.csproj", "TaskManager.Application/"]
RUN dotnet restore "TaskManager.WebApi/TaskManager.WebApi.csproj"
RUN ls -x

COPY . .
WORKDIR "/src/TaskManager.WebApi"
RUN dotnet build "TaskManager.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaskManager.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN ls -x
ENTRYPOINT ["dotnet", "TaskManager.WebApi.dll"]
