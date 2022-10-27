FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Marajoara.Cinema.Management.Api/Marajoara.Cinema.Management.Api.csproj", "Marajoara.Cinema.Management.Api/"]
COPY ["Marajoara.Cinema.Management.Application/Marajoara.Cinema.Management.Application.csproj", "Marajoara.Cinema.Management.Application/"]
COPY ["Marajoara.Cinema.Management.Domain/Marajoara.Cinema.Management.Domain.csproj", "Marajoara.Cinema.Management.Domain/"]
COPY ["Marajoara.Cinema.Management.Infra.Data.EF/Marajoara.Cinema.Management.Infra.Data.EF.csproj", "Marajoara.Cinema.Management.Infra.Data.EF/"]
RUN dotnet restore "Marajoara.Cinema.Management.Api/Marajoara.Cinema.Management.Api.csproj"
COPY . .
WORKDIR "/src/Marajoara.Cinema.Management.Api"
RUN dotnet build "Marajoara.Cinema.Management.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Marajoara.Cinema.Management.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Marajoara.Cinema.Management.Api.dll"]