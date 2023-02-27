FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY PatientRecordApi.csproj .
RUN dotnet restore "PatientRecordApi.csproj"
COPY . .
RUN dotnet publish "PatientRecordApi.csproj" -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as final
WORKDIR /app
COPY --from=build /publish .

ENTRYPOINT [ "dotnet", "PatientRecordApi.dll" ]