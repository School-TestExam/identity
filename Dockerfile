FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG version
ARG NUGET_USERNAME
ARG NUGET_TOKEN
WORKDIR /src

COPY ["Exam.Services.Identity/Exam.Services.Identity.csproj", "Exam.Services.Identity/"]
COPY ["NuGet.config", "Exam.Services.Identity/"]

RUN dotnet restore "Exam.Services.Identity/Exam.Services.Identity.csproj" --configfile Exam.Services.Identity/NuGet.config

COPY . .

RUN dotnet publish "Exam.Services.Identity/Exam.Services.Identity.csproj" -c Release -o out /p:Version=$version

FROM mcr.microsoft.com/dotnet/aspnet:8.0 
WORKDIR /app

EXPOSE 80
EXPOSE 443

COPY --from=build /src/out .
ENTRYPOINT ["dotnet", "Exam.Services.Identity.dll"]
