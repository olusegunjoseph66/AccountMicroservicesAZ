#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0.22 AS base

RUN useradd app -u 10001 --create-home --user-group
USER 10001

WORKDIR /app
ENV ASPNETCORE_URLS=http://*:8080
EXPOSE 8080


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Account.API/Account.API.csproj", "Account.API/"]
RUN dotnet restore "Account.API/Account.API.csproj"
COPY . .
WORKDIR "/src/Account.API"
RUN dotnet build "Account.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Account.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --chown=app --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Account.API.dll"]
