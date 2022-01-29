FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Musync.Synchronizer/Musync.Synchronizer.csproj", "Musync.Synchronizer/"]
RUN dotnet restore "Musync.Synchronizer/Musync.Synchronizer.csproj"
COPY . .
WORKDIR "/src/Musync.Synchronizer"
RUN dotnet build "Musync.Synchronizer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Musync.Synchronizer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Musync.Synchronizer.dll"]
