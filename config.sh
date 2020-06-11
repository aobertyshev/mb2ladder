echo 'export MBII_LADDER_DB_CONNECTION_STRING="<YOUR_CONNECTION_STRING_HERE>"' >> ~/.bashrc
echo 'export MBII_LADDER_WEB_APP_TOKEN_SECRET_KEY="<YOUR_SECRET_KEY_HERE>"' >> ~/.bashrc
echo 'export MBII_LADDER_DISCORD_BOT_API_KEY="<YOUR_DISCORD_API_KEY_HERE>"' >> ~/.bashrc
source ~/.bashrc
echo 'You need to restart shell where you start the processes in order to use these new variables'