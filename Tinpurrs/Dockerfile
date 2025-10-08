echo FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build>Dockerfile
echo WORKDIR /app>>Dockerfile
echo COPY . .>>Dockerfile
echo RUN dotnet publish -c Release -o out>>Dockerfile
echo.>>Dockerfile
echo FROM mcr.microsoft.com/dotnet/aspnet:8.0>>Dockerfile
echo WORKDIR /app>>Dockerfile
echo COPY --from=build /app/out .>>Dockerfile
echo ENTRYPOINT ["dotnet", "Tinpurrs.dll"]>>Dockerfile