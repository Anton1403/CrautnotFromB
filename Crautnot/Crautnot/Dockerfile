FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Crautnot/Crautnot/Crautnot.csproj", "Crautnot/"]
RUN dotnet restore "Crautnot/Crautnot.csproj"
COPY . .
WORKDIR "/src/Crautnot"
RUN dotnet clean "Crautnot.csproj"
RUN dotnet build "Crautnot.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Crautnot.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Crautnot.dll"]