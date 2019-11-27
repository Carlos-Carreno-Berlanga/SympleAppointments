FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
RUN curl -sL https://deb.nodesource.com/setup_10.x |  bash -
RUN apt-get install -y nodejs
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
RUN curl -sL https://deb.nodesource.com/setup_10.x |  bash -
RUN apt-get install -y nodejs
COPY ["SympleAppointments.Web/SympleAppointments.Web.csproj", "SympleAppointments.Web/"]
COPY ["LetsEncrypt/McMaster.AspNetCore.LetsEncrypt.csproj", "LetsEncrypt/"]
COPY ["SympleAppointments.Application/SympleAppointments.Application.csproj", "SympleAppointments.Application/"]
COPY ["SympleAppointments.Domain/SympleAppointments.Domain.csproj", "SympleAppointments.Domain/"]
COPY ["SympleAppointments.Persistance/SympleAppointments.Persistence.csproj", "SympleAppointments.Persistance/"]
RUN dotnet restore "SympleAppointments.Web/SympleAppointments.Web.csproj"
COPY . .
WORKDIR "/src/SympleAppointments.Web"
RUN dotnet build "SympleAppointments.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SympleAppointments.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "SympleAppointments.Web.dll"]
CMD dotnet SympleAppointments.Web.dll