#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TodoListApi/TodoListApi.csproj", "TodoListApi/"]
RUN dotnet restore "TodoListApi/TodoListApi.csproj"
COPY ["TodoListUnitTests/TodoListUnitTests.csproj", "TodoListUnitTests/"]
RUN dotnet restore "TodoListUnitTests/TodoListUnitTests.csproj"
COPY . .
WORKDIR "/src/TodoListApi"
RUN dotnet build "TodoListApi.csproj" -c Release -o /app
WORKDIR "/src/TodoListUnitTests"
RUN dotnet build "TodoListUnitTests.csproj" -c Release -o /app
RUN dotnet test "TodoListUnitTests.csproj" --logger "trx;LogFileName=TodoListUnitTests.trx" --collect:"XPlat Code Coverage" --results-directory /app/testresults

FROM build AS publish
WORKDIR "/src/TodoListApi"
RUN dotnet publish "TodoListApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TodoListApi.dll"]
