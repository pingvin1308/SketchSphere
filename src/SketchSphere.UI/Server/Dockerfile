FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SketchSphere.UI/Server/SketchSphere.UI.Server.csproj", "SketchSphere.UI/Server/"]
COPY ["SketchSphere.UI/Client/SketchSphere.UI.Client.csproj", "SketchSphere.UI/Client/"]
COPY ["SketchSphere.UI/Shared/SketchSphere.UI.Shared.csproj", "SketchSphere.UI/Shared/"]
RUN dotnet restore "SketchSphere.UI/Server/SketchSphere.UI.Server.csproj"
COPY . .
WORKDIR "/src/SketchSphere.UI/Server"
RUN dotnet build "SketchSphere.UI.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SketchSphere.UI.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SketchSphere.UI.Server.dll"]
