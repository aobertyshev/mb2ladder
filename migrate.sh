#/usr/local/bin/bash

cd ./Shared/
dotnet ef migrations add $1
dotnet ef database update
cd ../
