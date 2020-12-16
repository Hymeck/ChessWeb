# docker pull mcr.microsoft.com/dotnet/sdk:5.0 if does not have
# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /source

COPY *.sln .

# Copy csproj and restore as distinct layers
COPY ChessWeb.Domain/*.csproj ./ChessWeb.Domain/
COPY ChessWeb.Persistence/*.csproj ./ChessWeb.Persistence/
COPY ChessWeb.Service/*.csproj ./ChessWeb.Service/
COPY ChessWeb.Application/*.csproj ./ChessWeb.Application/
COPY ChessWeb.SignalR.Shared/*.csproj ./ChessWeb.SignalR.Shared/
COPY ChessWeb.SignalR.Client/*.csproj ./ChessWeb.SignalR.Client/
COPY ChessWeb.SignalR.Server/*.csproj ./ChessWeb.SignalR.Server/


RUN dotnet restore ./ChessWeb.Domain/
RUN dotnet restore ./ChessWeb.Persistence/
RUN dotnet restore ./ChessWeb.Service/
RUN dotnet restore ./ChessWeb.Application/
RUN dotnet restore ./ChessWeb.SignalR.Shared/
RUN dotnet restore ./ChessWeb.SignalR.Client/
RUN dotnet restore ./ChessWeb.SignalR.Server/


# Copy everything else and build
COPY ChessWeb.Domain/. ./ChessWeb.Domain/
COPY ChessWeb.Persistence/. ./ChessWeb.Persistence/
COPY ChessWeb.Service/. ./ChessWeb.Service/
COPY ChessWeb.Application/. ./ChessWeb.Application/
COPY ChessWeb.SignalR.Shared/. ./ChessWeb.SignalR.Shared/
COPY ChessWeb.SignalR.Client/. ./ChessWeb.SignalR.Client/
COPY ChessWeb.SignalR.Server/. ./ChessWeb.SignalR.Server/


WORKDIR /source/ChessWeb.Application
RUN dotnet publish -c release -o /app --no-restore


# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "ChessWeb.Application.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet ChessWeb.Application.dll