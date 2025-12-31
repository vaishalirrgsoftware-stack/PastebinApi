# ---------- BUILD STAGE ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy csproj from subfolder
COPY PastebinApi/PastebinApi.csproj PastebinApi/
RUN dotnet restore PastebinApi/PastebinApi.csproj

# copy everything else
COPY . .
WORKDIR /src/PastebinApi

RUN dotnet publish -c Release -o /app/publish

# ---------- RUNTIME STAGE ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PastebinApi.dll"]
