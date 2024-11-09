# StudyAspDotnetCoreWebApi
ASP.NET Core Web API と Vue.js の勉強用リポジトリ

`dotnet run`はエラーなく動くけどブラウザからアクセスできない！
ポートのマッピングが正しくないと思われる
このリポジトリは破棄する！
最終コミットのファイルたちは不要かもしれない


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
    APPDATA=./docker/local/dotnet docker compose build --no-cache
    APPDATA=./docker/local/dotnet docker compose down -v
    APPDATA=./docker/local/dotnet docker compose up -d
    APPDATA=./docker/local/dotnet docker compose exec studyaspdotnetcorewebapi.server dotnet restore "StudyAspDotnetCoreWebApi.Server.csproj"
    APPDATA=./docker/local/dotnet docker compose exec studyaspdotnetcorewebapi.server dotnet tool restore
    APPDATA=./docker/local/dotnet docker compose exec studyaspdotnetcorewebapi.server dotnet build "./StudyAspDotnetCoreWebApi.Server.csproj" -c Debug
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


```bash
openssl genpkey -algorithm RSA -out studyaspdotnetcorewebapi.client.key -pkeyopt rsa_keygen_bits:4096
openssl req -new -key studyaspdotnetcorewebapi.client.key -out studyaspdotnetcorewebapi.client.csr
# Country Name (2 letter code) [AU]:
JP
# State or Province Name (full name) [Some-State]:
Tokyo
# Locality Name (eg, city) []:
Minato-ku
# Organization Name (eg, company) [Internet Widgits Pty Ltd]:
Example Company
# Common Name (e.g. server FQDN or YOUR name) []:
www.example.com
openssl x509 -req -days 3650 -in studyaspdotnetcorewebapi.client.csr -signkey studyaspdotnetcorewebapi.client.key -out studyaspdotnetcorewebapi.client.pem


openssl pkcs12 -export -out studyaspdotnetcorewebapi.client.pfx -inkey studyaspdotnetcorewebapi.client.key -in studyaspdotnetcorewebapi.client.pem
# Enter Export Password:
41559b8b-e831-4972-8afa-21ee8b952d85
```





```bash
dotnet aspnet-codegenerator controller -name ProfilesController -async -api -m Profile -dc MyContext -outDir Controllers
```

```bash
APPDATA=./docker/local/dotnet docker compose exec studyaspdotnetcorewebapi.server dotnet run
```
