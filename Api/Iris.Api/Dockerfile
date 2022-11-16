#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Api/Iris.Api/Iris.Api.csproj", "Iris.Api/"]
COPY ["Api/Iris.Domain/Iris.Domain.csproj", "Iris.Domain/"]
COPY ["Api/Iris.Infraestructure/Iris.Infraestructure.csproj", "Iris.Infraestructure/"]
RUN dotnet restore "Iris.Api/Iris.Api.csproj"
COPY . .
WORKDIR "/src/Api/Iris.Api"
RUN dotnet build "Iris.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Iris.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Iris.Api.dll"]