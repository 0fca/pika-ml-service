FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
RUN ls -lah /src
COPY ["PikaMLModule.csproj", "."]
RUN dotnet restore "PikaMLModule.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "PikaMLModule.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PikaMLModule.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=base /src/ML/HateSpeechPL/HS_PL.zip ./ML/HateSpeechPL/HS_PL.zip
ENTRYPOINT ["dotnet", "PikaMLModule.dll"]