FROM mcr.microsoft.com/dotnet/sdk:3.1

RUN apt-get update && \
    apt-get install -y locales locales-all

ENV LC_ALL="en_US.UTF-8"
ENV LANG="en_US.UTF-8"
ENV LANGUAGE="en_US.UTF-8"