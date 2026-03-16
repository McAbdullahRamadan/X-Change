# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# انسخ فقط مجلد المشروع اللي فيه .csproj
COPY X-ChangeWebBackend/ X-ChangeWebBackend/

# انتقل إلى مجلد المشروع قبل البناء
WORKDIR /src/X-ChangeWebBackend

# اعمل build
RUN dotnet publish -c Release -o /app/publish

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "X-ChangeWebBackend.dll"]