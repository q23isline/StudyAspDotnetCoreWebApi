# StudyAspDotnetCoreWebApi
ASP.NET Core Web API と Vue.js の勉強用リポジトリ

## はじめにやること

1. ソースダウンロード

    ```bash
    git clone 'https://github.com/q23isline/StudyAspDotnetCoreWebApi.git'
    ```

2. リポジトリのカレントディレクトリへ移動

    ```bash
    cd StudyAspDotnetCoreWebApi
    ```

3. 開発準備

    ```bash
    cp studyaspdotnetcorewebapi.client/.vscode/launch.json.default studyaspdotnetcorewebapi.client/.vscode/launch.json
    cp studyaspdotnetcorewebapi.client/.vscode/settings.json.default studyaspdotnetcorewebapi.client/.vscode/settings.json
    cp studyaspdotnetcorewebapi.client/studyaspdotnetcorewebapi.client.esproj.user.default studyaspdotnetcorewebapi.client/studyaspdotnetcorewebapi.client.esproj.user
    cp StudyAspDotnetCoreWebApi.Server/StudyAspDotnetCoreWebApi.Server.csproj.user.default StudyAspDotnetCoreWebApi.Server/StudyAspDotnetCoreWebApi.Server.csproj.user
    cp docker-compose.dcproj.user.default docker-compose.dcproj.user
    ```

4. DB コンテナ起動時に Permission Denied で起動できない状態にならないように権限付与する

    ```bash
    sudo chmod -R ugo+w logs
    ```

5. アプリ立ち上げ

    ```bash
    APPDATA=${pwd}/docker/local/dotnet docker compose build --no-cache
    APPDATA=${pwd}/docker/local/dotnet docker compose down -v
    APPDATA=${pwd}/docker/local/dotnet docker compose up -d
    APPDATA=${pwd}/docker/local/dotnet docker compose exec studyaspdotnetcorewebapi.server dotnet restore "StudyAspDotnetCoreWebApi.Server.csproj"
    APPDATA=${pwd}/docker/local/dotnet docker compose exec studyaspdotnetcorewebapi.server dotnet tool restore
    APPDATA=${pwd}/docker/local/dotnet docker compose exec studyaspdotnetcorewebapi.server dotnet build "./StudyAspDotnetCoreWebApi.Server.csproj" -c Debug
    sudo chmod 777 -R StudyAspDotnetCoreWebApi.Server/bin StudyAspDotnetCoreWebApi.Server/obj studyaspdotnetcorewebapi.client/node_modules studyaspdotnetcorewebapi.client/obj
    docker exec -it app dotnet ef database update
    ```

## データベースへの接続

- サーバー名
    - 127.0.0.1
- 認証
    - SQL Server 認証
- ユーザー名
    - sa
- パスワード
    - Passw0rd
- サーバー証明書を信頼する
    - ON
