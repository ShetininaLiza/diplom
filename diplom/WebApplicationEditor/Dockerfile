#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["WebApplicationEditor/WebApplicationEditor.csproj", "WebApplicationEditor/"]
RUN dotnet restore "WebApplicationEditor/WebApplicationEditor.csproj"
COPY . .
WORKDIR "/src/WebApplicationEditor"
RUN dotnet build "WebApplicationEditor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApplicationEditor.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApplicationEditor.dll"]