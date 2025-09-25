# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /src

# Copy solution and projects
COPY *.sln ./
COPY Api/*.csproj ./Api/
COPY Domain/*.csproj ./Domain/
COPY Service/*.csproj ./Service/
COPY Repository/*.csproj ./Repository/

# Restore dependencies
RUN dotnet restore --disable-parallel

# Copy the rest of the source
COPY . .

# Publish API project
WORKDIR /src/Api
RUN dotnet publish -c Release -o /app/publish --no-restore

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
COPY --from=build-env /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]
