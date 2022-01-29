FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Musync.FeedUpdater/Musync.FeedUpdater.csproj", "Musync.FeedUpdater/"]
RUN dotnet restore "Musync.FeedUpdater/Musync.FeedUpdater.csproj"
COPY . .
WORKDIR "/src/Musync.FeedUpdater"
RUN dotnet build "Musync.FeedUpdater.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Musync.FeedUpdater.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Musync.FeedUpdater.dll"]
