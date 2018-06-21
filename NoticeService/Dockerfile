FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY NoticeService.csproj NoticeService/
RUN dotnet restore NoticeService/NoticeService.csproj
COPY . .
WORKDIR /src/NoticeService
RUN dotnet build NoticeService.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish NoticeService.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "NoticeService.dll"]
