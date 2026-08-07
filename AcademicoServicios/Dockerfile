FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["AcademicoServicios/AcademicoServicios.csproj", "AcademicoServicios/"]
COPY ["AcademicoInfraestructura/AcademicoInfraestructura.csproj", "AcademicoInfraestructura/"]
COPY ["AcademicoNegocio/AcademicoNegocio.csproj", "AcademicoNegocio/"]
COPY ["AcademicoDominio/AcademicoDominio.csproj", "AcademicoDominio/"]
RUN dotnet restore "AcademicoServicios/AcademicoServicios.csproj"

COPY . .
WORKDIR /src/AcademicoServicios
RUN dotnet publish "AcademicoServicios.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "AcademicoServicios.dll"]
