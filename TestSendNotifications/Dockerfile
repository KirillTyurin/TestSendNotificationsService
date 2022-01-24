FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TestSendNotifications/TestSendNotifications.csproj", "TestSendNotifications/"]
RUN dotnet restore "TestSendNotifications/TestSendNotifications.csproj"
COPY . .
WORKDIR "/src/TestSendNotifications"
RUN dotnet build "TestSendNotifications.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestSendNotifications.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestSendNotifications.dll"]
