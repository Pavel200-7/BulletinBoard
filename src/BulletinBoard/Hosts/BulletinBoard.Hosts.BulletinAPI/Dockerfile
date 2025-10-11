FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish "src/BulletinBoard/Hosts/BulletinBoard.Hosts.BulletinAPI/BulletinBoard.Hosts.BulletinAPI.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BulletinBoard.Hosts.BulletinAPI.dll"]

