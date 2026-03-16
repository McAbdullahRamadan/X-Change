# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

# انسخ كل مجلدات المشروع اللي فيه solution و المشاريع الفرعية
COPY X-ChangeWebBackend/ X-ChangeWebBackend/
COPY Domain/ Domain/
COPY Application/ Application/
COPY Infrastructure/ Infrastructure/

# اذهب لمجلد المشروع اللي فيه .sln
WORKDIR /src/X-ChangeWebBackend

# نفذ build + publish
RUN dotnet publish -c Release -o /app/publish

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "X-ChangeWebBackend.dll"]