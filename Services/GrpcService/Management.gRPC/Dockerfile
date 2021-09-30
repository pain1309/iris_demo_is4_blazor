#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/GrpcService/Management.gRPC/Management.gRPC.csproj", "Services/GrpcService/Management.gRPC/"]
COPY ["Services/GrpcService/Infrastructure/Infrastructure.csproj", "Services/GrpcService/Infrastructure/"]
COPY ["Services/GrpcService/Domain/Domain.csproj", "Services/GrpcService/Domain/"]
RUN dotnet restore "Services/GrpcService/Management.gRPC/Management.gRPC.csproj"
COPY . .
WORKDIR "/src/Services/GrpcService/Management.gRPC"
RUN dotnet build "Management.gRPC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Management.gRPC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Management.gRPC.dll"]
