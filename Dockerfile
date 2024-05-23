FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Exam.Services.Identity/Exam.Services.Identity.csproj", "Exam.Services.Identity/"]
RUN dotnet restore "Exam.Services.Identity/Exam.Services.Identity.csproj"
COPY . .
WORKDIR "/src/Exam.Services.Identity"
RUN dotnet build "Exam.Services.Identity.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Exam.Services.Identity.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Exam.Services.Identity.dll"]
