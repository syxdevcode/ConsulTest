FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY ConsulTest/ConsulTest.csproj ConsulTest/
RUN dotnet restore ConsulTest/ConsulTest.csproj
COPY . .
WORKDIR /src/ConsulTest
RUN dotnet build ConsulTest.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish ConsulTest.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ConsulTest.dll"]
