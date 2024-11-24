# Базовый образ для запуска приложения
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Образ для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Копирование csproj и восстановление зависимостей
COPY ["Crautnot/Crautnot/Crautnot.csproj", "Crautnot/"]
RUN dotnet restore "Crautnot/Crautnot.csproj"

# Копирование всего проекта и очистка старых артефактов сборки
COPY . .
WORKDIR "/src/Crautnot"
RUN dotnet clean "Crautnot.csproj" || true

# Создание приложения с правами доступа root
USER root
RUN mkdir -p /app/build && chmod -R 777 /app/build
RUN dotnet build "Crautnot.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Публикация приложения
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Crautnot.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Финальный образ с опубликованным приложением
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Crautnot.dll"]