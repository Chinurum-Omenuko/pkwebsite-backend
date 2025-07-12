FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Set the working directory inside the container to /src
WORKDIR /src

# Copy your project file from the build context (pkbackend folder on your machine) into /src in the container
COPY ["pkbackend.csproj", "./"]

# Restore dependencies based on the project file inside the container's /src folder
RUN dotnet restore

# Copy everything from your build context (your pkbackend folder) into /src inside the container
COPY . .

# Build your project
RUN dotnet build -c Release -o /app/build

# (Then you can do publish and runtime stages...)
