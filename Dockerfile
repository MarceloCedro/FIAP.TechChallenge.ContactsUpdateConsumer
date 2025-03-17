#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 7070
ENV ASPNETCORE_URLS=http://*:7070
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ContactsUpdateConsumer.Api/FIAP.TechChallenge.ContactsUpdateConsumer.Api.csproj", "ContactsUpdateConsumer.Api/"]
COPY ["ContactsUpdateConsumer.Application/FIAP.TechChallenge.ContactsUpdateConsumer.Application.csproj", "ContactsUpdateConsumer.Application/"]
COPY ["ContactsUpdateConsumer.Domain/FIAP.TechChallenge.ContactsUpdateConsumer.Domain.csproj", "ContactsUpdateConsumer.Domain/"]
COPY ["ContactsUpdateConsumer.Infrastructure/FIAP.TechChallenge.ContactsUpdateConsumer.Infrastructure.csproj", "ContactsUpdateConsumer.Infrastructure/"]
RUN dotnet restore "./ContactsUpdateConsumer.Api/FIAP.TechChallenge.ContactsUpdateConsumer.Api.csproj"
COPY . .
WORKDIR "/src/ContactsUpdateConsumer.Api"
RUN dotnet build "./FIAP.TechChallenge.ContactsUpdateConsumer.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FIAP.TechChallenge.ContactsUpdateConsumer.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FIAP.TechChallenge.ContactsUpdateConsumer.Api.dll"]