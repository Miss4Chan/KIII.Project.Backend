#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src

COPY ["SecondHandEShop.Api/SecondHandEShop.Api.csproj", "SecondHandEShop.Api/"]
COPY ["Repository/Repository.csproj", "Repository/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Service/Service.csproj", "Service/"]


RUN dotnet restore "SecondHandEShop.Api/SecondHandEShop.Api.csproj"

COPY . .
WORKDIR "/src/SecondHandEShop.Api"
RUN dotnet build "SecondHandEShop.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SecondHandEShop.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SecondHandEShop.Api.dll"]