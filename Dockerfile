
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app


COPY *.csproj .
RUN dotnet restore


COPY . .
RUN dotnet publish -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /publish .


ENTRYPOINT ["dotnet", "E-Banking-API.dll"]

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app


COPY *.csproj .
RUN dotnet restore


COPY . .
RUN dotnet publish -c Release -o /publish


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /publish .


ENTRYPOINT ["dotnet", "E-Banking-API.dll"]"