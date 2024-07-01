FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Infrastructure/ToDo.WebAPI/ToDo.WebAPI.csproj", "src/Infrastructure/ToDo.WebAPI/"]
COPY ["src/Application/ToDo.Application/ToDo.Application.csproj", "src/Application/ToDo.Application/"]
COPY ["src/Domain/ToDo.Domain/ToDo.Domain.csproj", "src/Domain/ToDo.Domain/"]
COPY ["src/Infrastructure/ToDo.Inftrastructure/ToDo.Inftrastructure.csproj", "src/Infrastructure/ToDo.Inftrastructure/"]
COPY ["src/Infrastructure/ToDo.Contracts/ToDo.Contracts.csproj", "src/Infrastructure/ToDo.Contracts/"]
COPY ["src/Tests/ToDo.Domain.Tests/ToDo.Domain.Tests.csproj", "src/Tests/ToDo.Domain.Tests/"]
RUN dotnet restore "src/Infrastructure/ToDo.WebAPI/ToDo.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/Infrastructure/ToDo.WebAPI"
RUN dotnet build "ToDo.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ToDo.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToDo.WebAPI.dll"]
