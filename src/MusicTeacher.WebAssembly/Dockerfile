FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["MusicTeacher.WebAssembly/MusicTeacher.WebAssembly.csproj", "MusicTeacher.WebAssembly/"]
COPY ["MusicTeacher.Shared/MusicTeacher.Shared.csproj", "MusicTeacher.Shared/"]

RUN dotnet restore "MusicTeacher.WebAssembly/MusicTeacher.WebAssembly.csproj"

COPY . .

RUN dotnet publish "MusicTeacher.WebAssembly/MusicTeacher.WebAssembly.csproj" -c Release -o /app/publish

FROM nginx:alpine AS final

COPY MusicTeacher.WebAssembly/nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html