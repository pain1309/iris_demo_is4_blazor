#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/GrpcService/Ordering.gRPC/Ordering.gRPC.csproj", "Services/GrpcService/Ordering.gRPC/"]
COPY ["Services/GrpcService/Infrastructure/Infrastructure.csproj", "Services/GrpcService/Infrastructure/"]
COPY ["Services/GrpcService/Domain/Domain.csproj", "Services/GrpcService/Domain/"]
RUN dotnet restore "Services/GrpcService/Ordering.gRPC/Ordering.gRPC.csproj"
COPY . .
WORKDIR "/src/Services/GrpcService/Ordering.gRPC"
RUN dotnet build "Ordering.gRPC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ordering.gRPC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordering.gRPC.dll"]