FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["WordSentimentModel_WebApi1/PikaMLModule.csproj", "WordSentimentModel_WebApi1/"]
RUN dotnet restore "WordSentimentModel_WebApi1/PikaMLModule.csproj"
COPY . .
WORKDIR "/src/WordSentimentModel_WebApi1"
RUN dotnet build "PikaMLModule.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PikaMLModule.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PikaMLModule.dll"]