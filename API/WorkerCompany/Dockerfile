#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["WorkerCompany.API/WorkerCompany.API.csproj", "WorkerCompany.API/"]
COPY ["WorkerCompany.DAL/WorkerCompany.DAL.csproj", "WorkerCompany.DAL/"]
COPY ["WorkerCompany.BLL/WorkerCompany.BLL.csproj", "WorkerCompany.BLL/"]
RUN dotnet restore "WorkerCompany.API/WorkerCompany.API.csproj"
COPY . ./
WORKDIR "/src/WorkerCompany.API"
RUN dotnet build "WorkerCompany.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkerCompany.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkerCompany.API.dll"]