FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Markdown/Markdown.csproj", "Markdown/"]
RUN dotnet restore "Markdown/Markdown.csproj"
COPY . .
WORKDIR "/src/Markdown"
RUN dotnet build "Markdown.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Markdown.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Markdown.dll"]
