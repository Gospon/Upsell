FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Upsell.csproj", "Upsell/"]
RUN dotnet restore "Upsell/Upsell.csproj"

WORKDIR "/src/Upsell"
COPY . .

RUN dotnet build "Upsell.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Upsell.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Upsell.dll"]