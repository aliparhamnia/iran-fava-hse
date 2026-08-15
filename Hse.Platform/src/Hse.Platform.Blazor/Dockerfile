FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY NuGet.Config common.props Hse.Platform.slnx ./
COPY src/ src/
RUN dotnet restore Hse.Platform.slnx
RUN dotnet publish src/Hse.Platform.Blazor/Hse.Platform.Blazor.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
RUN useradd --create-home --uid 1000 appuser
COPY --from=build /app/publish .
RUN mkdir -p /app/BlobStoring && chown -R appuser:appuser /app
USER appuser
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "Hse.Platform.Blazor.dll"]
