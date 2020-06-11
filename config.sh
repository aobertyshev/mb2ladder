echo 'export MBIILadder_DBConnectionString="<YOUR_CONNECTION_STRING_HERE>"' >> ~/.bashrc
echo 'export MBIILadder_WebAppTokenSecretKey="<YOUR_SECRET_KEY_HERE>"' >> ~/.bashrc
echo 'export MBIILadder_DiscordAPIKey="<YOUR_DISCORD_API_KEY_HERE>"' >> ~/.bashrc
source ~/.bashrc
echo 'You need to restart shell where you start the processes in order to use these new variables'