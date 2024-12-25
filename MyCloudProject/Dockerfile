#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER root
WORKDIR /app
# Install dependencies
RUN apt-get update && apt-get install -y \
    libfontconfig1 \
    libfreetype6 \
    libpng16-16 \
    libjpeg62-turbo \
    libx11-6 \
    libxext6 \
    libxrender1 \
    libxi6 \
    libxtst6 \
    libxrandr2 \
    libxfixes3 \
    libxcursor1 \
    libxinerama1 \
    libglib2.0-0 \
    libsm6 \
    libice6 \
    libxau6 \
    libxdmcp6 \
    libxcb1 \
    libxcomposite1 \
    libxdamage1 \
    libxshmfence1 \
    libx11-xcb1 \
    libxkbcommon0 \
    libxkbcommon-x11-0 \
    libwayland-client0 \
    libwayland-cursor0 \
    libwayland-egl1 \
    libegl1-mesa \
    libgbm1 \
    libdrm2 \
    libasound2 \
    libpulse0 \
    libudev1 \
    libusb-1.0-0 \
    libdbus-1-3 \
    libexpat1 \
    libuuid1 \
    libblkid1 \
    libmount1 \
    libselinux1 \
    libpcre3 \
    liblzma5 \
    libbz2-1.0 \
    libzstd1 \
    liblz4-1 \
    libcap2 \
    libattr1 \
    libacl1 \
    libseccomp2 \
    libapparmor1 \
    libsystemd0 \
    libpam0g \
    libaudit1 \
    libgcrypt20 \
    libgpg-error0 \
    liblz4-1 \
    liblzma5 \
    libzstd1 \
    libbz2-1.0 \
    libpcre3 \
    libselinux1 \
    libmount1 \
    libblkid1 \
    libuuid1 \
    libexpat1 \
    libdbus-1-3 \
    libusb-1.0-0 \
    libudev1 \
    libpulse0 \
    libasound2 \
    libdrm2 \
    libgbm1 \
    libegl1-mesa \
    libwayland-egl1 \
    libwayland-cursor0 \
    libwayland-client0 \
    libxkbcommon-x11-0 \
    libxkbcommon0 \
    libx11-xcb1 \
    libxshmfence1 \
    libxdamage1 \
    libxcomposite1 \
    libxcb1 \
    libxdmcp6 \
    libxau6 \
    libice6 \
    libsm6 \
    libglib2.0-0 \
    libxinerama1 \
    libxcursor1 \
    libxfixes3 \
    libxrandr2 \
    libxtst6 \
    libxi6 \
    libxrender1 \
    libxext6 \
    libx11-6 \
    libjpeg62-turbo \
    libpng16-16 \
    libfreetype6 \
    libfontconfig1

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MyCloudProject/MyCloudProject.csproj", "MyCloudProject/"]
COPY ["MyCloudProject.Common/MyCloudProject.Common.csproj", "MyCloudProject.Common/"]
COPY ["MyExperiment/MyExperiment.csproj", "MyExperiment/"]
RUN dotnet restore "./MyCloudProject/./MyCloudProject.csproj"
COPY . .
WORKDIR "/src/MyCloudProject"
RUN dotnet build "./MyCloudProject.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MyCloudProject.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyCloudProject.dll"]