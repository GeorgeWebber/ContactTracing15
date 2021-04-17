#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["ContactTracing15/ContactTracing15.csproj", "ContactTracing15/"]
COPY ["ContactTracing15.Models/ContactTracing15.Models.csproj", "ContactTracing15.Models/"]
COPY ["ContactTracing15.Services/ContactTracing15.Services.csproj", "ContactTracing15.Services/"]
RUN dotnet restore "ContactTracing15/ContactTracing15.csproj"
COPY . .
WORKDIR "/src/ContactTracing15"
RUN dotnet build "ContactTracing15.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContactTracing15.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContactTracing15.dll"]