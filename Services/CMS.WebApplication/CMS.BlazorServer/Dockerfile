#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/CMS.WebApplication/CMS.BlazorServer/CMS.BlazorServer.csproj", "Services/CMS.WebApplication/CMS.BlazorServer/"]
COPY ["Services/GrpcService/Infrastructure/Infrastructure.csproj", "Services/GrpcService/Infrastructure/"]
COPY ["Services/GrpcService/Domain/Domain.csproj", "Services/GrpcService/Domain/"]
RUN dotnet restore "Services/CMS.WebApplication/CMS.BlazorServer/CMS.BlazorServer.csproj"
COPY . .
WORKDIR "/src/Services/CMS.WebApplication/CMS.BlazorServer"
RUN dotnet build "CMS.BlazorServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CMS.BlazorServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CMS.BlazorServer.dll"]
