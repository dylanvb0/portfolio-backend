dotnet public -c Release -r debian-x64
cd bin/Release/netcoreapp2.0/debian-x64/publish
chmod +x portfolio-backend
gcloud compute scp * dylan@instance-1:/var/www/api.dylanvb.me/public/
gcloud compute ssh instance-1 --command 'sudo systemctl restart dylanvb-api'
cd ../../../..
