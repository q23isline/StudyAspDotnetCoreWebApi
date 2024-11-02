# デバッグ コンテナーをカスタマイズする方法と、Visual Studio がこの Dockerfile を使用してより高速なデバッグのためにイメージをビルドする方法については、https://aka.ms/customizecontainer をご覧ください。

# このステージは、VS から高速モードで実行するときに使用されます (デバッグ構成の既定値)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# このステージは、サービス プロジェクトのビルドに使用されますFROM mcr.microsoft.com/dotnet/sdk:8.0 AS with-node
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS with-node
RUN apt-get update
RUN apt-get install curl
RUN curl -sL https://deb.nodesource.com/setup_20.x | bash
RUN apt-get -y install nodejs

FROM with-node AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["studyaspdotnetcorewebapi.client/nuget.config", "studyaspdotnetcorewebapi.client/"]
COPY ["StudyAspDotnetCoreWebApi.Server/StudyAspDotnetCoreWebApi.Server.csproj", "StudyAspDotnetCoreWebApi.Server/"]
COPY ["studyaspdotnetcorewebapi.client/studyaspdotnetcorewebapi.client.esproj", "studyaspdotnetcorewebapi.client/"]
RUN dotnet restore "./StudyAspDotnetCoreWebApi.Server/StudyAspDotnetCoreWebApi.Server.csproj"
COPY . .
WORKDIR "/src/StudyAspDotnetCoreWebApi.Server"
RUN dotnet build "./StudyAspDotnetCoreWebApi.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

# このステージは、最終ステージにコピーするサービス プロジェクトを公開するために使用されます
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./StudyAspDotnetCoreWebApi.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# このステージは、運用環境または VS から通常モードで実行している場合に使用されます (デバッグ構成を使用しない場合の既定)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StudyAspDotnetCoreWebApi.Server.dll"]

# コンテナでマイグレーション等を行えるようにするための開発用ステージ
FROM build AS develop
WORKDIR "/src/StudyAspDotnetCoreWebApi.Server"
RUN dotnet tool restore
