FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /app
COPY . /app


RUN ls
RUN dotnet publish src/dotnet//AmzErp.Web/AmzErp.Web.csproj -c Release -o out 
FROM mcr.microsoft.com/dotnet/aspnet:3.1 as runtime
RUN ln -sf /usr/share/zoneinfo/Asia/Shanghai /etc/localtime
RUN echo 'Asia/Shanghai' >/etc/timezone
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 80
EXPOSE 8527
ENTRYPOINT ["dotnet","AmzErp.Web.dll"]