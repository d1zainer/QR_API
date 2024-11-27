
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/QR_API.csproj", "src/"]
RUN dotnet restore "./src/QR_API.csproj"
COPY . .
WORKDIR "/src/src"
RUN dotnet build "./QR_API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./QR_API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false



FROM mcr.microsoft.com/dotnet/runtime-deps:8.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .
ENTRYPOINT ["./QR_API"]