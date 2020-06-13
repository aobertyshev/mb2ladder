echo 'export MBII_LADDER_WEB_APP_TOKEN_SECRET_KEY="<YOUR_SECRET_KEY_HERE>"' >> ~/.bashrc
echo 'export MBII_LADDER_DISCORD_BOT_API_KEY="<YOUR_DISCORD_API_KEY_HERE>"' >> ~/.bashrc
echo 'export MBII_LADDER_FIREBASE_BASE_PATH="<YOUR_FIREBASE_BASE_PATH>"' >> ~/.bashrc
echo 'export MBII_LADDER_FIREBASE_AUTH_SECRET="<YOUR_FIREBASE_AUTH_SECRET>"' >> ~/.bashrc
source ~/.bashrc
echo 'You need to restart shell where you start the processes in order to use these new variables'
echo 'Restoring dependencies...'
dotnet restore
cd ./webapp/ClientApp/
npm install
cd ../../
echo 'Dependencies restored. cd to any project folder and dotnet run'
