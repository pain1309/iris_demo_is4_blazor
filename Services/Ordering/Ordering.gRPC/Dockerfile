#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 5012
EXPOSE 5013

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Ordering/Ordering.gRPC/Ordering.gRPC.stub.csproj", "Services/Ordering/Ordering.gRPC/"]
COPY ["Services/GrpcService/Infrastructure/Infrastructure.csproj", "Services/GrpcService/Infrastructure/"]
COPY ["Services/GrpcService/Domain/Domain.csproj", "Services/GrpcService/Domain/"]
RUN dotnet restore "Services/Ordering/Ordering.gRPC/Ordering.gRPC.stub.csproj"
COPY . .
WORKDIR "/src/Services/Ordering/Ordering.gRPC"
RUN dotnet build "Ordering.gRPC.stub.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ordering.gRPC.stub.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordering.gRPC.stub.dll"]
