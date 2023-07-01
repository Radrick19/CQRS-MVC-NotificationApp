FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
RUN apt-get update && apt-get install -y curl
RUN curl -sL https://deb.nodesource.com/setup_14.x | bash -
RUN apt-get install -y nodejs
COPY ["FastSchedule.Mvc/FastSchedule.Mvc.csproj", "FastSchedule.Mvc/"]
COPY ["FastSchedule.Application/FastSchedule.Application.csproj", "FastSchedule.Application/"]
COPY ["FastSchedule.Domain/FastSchedule.Domain.csproj", "FastSchedule.Domain/"]
RUN dotnet restore "FastSchedule.Mvc/FastSchedule.Mvc.csproj"
COPY . .
WORKDIR "/src/FastSchedule.Mvc"
RUN npm install
RUN dotnet build "FastSchedule.Mvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FastSchedule.Mvc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FastSchedule.Mvc.dll"]