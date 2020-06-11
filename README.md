# MBII competitive platform


## WIP


Web application: .NET Core 3.1 + Ionic Framework 5 + Angular 9

Infrastructure: Kubernetes + Terraform

Discord bot: .NET Core 3.1

Server manager: .NET Core 3.1


## Set up environment

1. Install .NET Core SDK 3.1
2. Install NodeJs LTS
3. Install PostgreSQL
4. Pull this repo
5. Change the values in `config.sh` to your own
6. `chmod +x config.sh && ./config.sh && dotnet restore && cd ./webapp/ClientApp/ && npm install && cd ../../`
7. Start any of the services with `dotnet run` in a respective folder
