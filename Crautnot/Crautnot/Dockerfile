FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Crautnot/Crautnot/Crautnot.csproj", "Crautnot/"]
RUN dotnet restore "Crautnot/Crautnot.csproj"
COPY . .
WORKDIR "/src/Crautnot"

RUN dotnet build "Crautnot.csproj" -c Release -o /app/build /p:UseAppHost=false

FROM build AS publish
RUN dotnet publish "Crautnot.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Crautnot.dll"]