#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Management.Api/Management.Api.csproj", "src/Management.Api/"]
COPY ["src/Business.Logic/Business.Logic.csproj", "src/Business.Logic/"]
COPY ["src/DataAccess.Layer/DataAccess.Layer.csproj", "src/DataAccess.Layer/"]
RUN dotnet restore "src/Management.Api/Management.Api.csproj"
COPY . .
WORKDIR "/src/src/Management.Api"
RUN dotnet build "Management.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Management.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Management.Api.dll"]