FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Musync.Reader/Musync.Reader.csproj", "Musync.Reader/"]
RUN dotnet restore "Musync.Reader/Musync.Reader.csproj"
COPY . .
WORKDIR "/src/Musync.Reader"
RUN dotnet build "Musync.Reader.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Musync.Reader.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Musync.Reader.dll"]
