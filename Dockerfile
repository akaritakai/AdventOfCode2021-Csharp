FROM mcr.microsoft.com/dotnet/sdk:6.0

WORKDIR "/opt/aoc"

COPY . .

ENTRYPOINT ["dotnet", "run", "--project", "src/aoc.csproj"]