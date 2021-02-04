# build image
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
COPY . .
RUN dotnet restore "PrintService/PrintService.sln"
RUN dotnet publish "PrintService/PrintService.Api.csproj" -c Release -o /app

# build runtime image with published content
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.3-buster-slim
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=build-env /app .
ENTRYPOINT ["dotnet","PrintService.Api.dll"]
