FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 80

RUN apk add git openssh --no-cache

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY ["CDoc/CDoc.csproj", "CDoc/"]
RUN dotnet restore "CDoc/CDoc.csproj"
COPY . .
WORKDIR "/src/CDoc"
RUN dotnet build "CDoc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CDoc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CDoc.dll"]
