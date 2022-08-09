# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build-env
WORKDIR /workdir
    
# Copy csproj and restore as distinct layers
COPY ./src/*.csproj ./
RUN dotnet restore
    
# Copy everything else and build
COPY src/. ./
RUN dotnet publish testapicore31.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /workdir
COPY --from=build-env /workdir/out .
ENTRYPOINT ["dotnet", "testapicore31.dll"]
