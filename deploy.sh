dotnet publish -c Release -r debian-x64
cd bin/Release/netcoreapp2.0/debian-x64/publish
chmod +x portfolio-backend
gcloud compute ssh instance-1 --command 'mkdir /var/www/dylanvb.me/public/api'
gcloud compute scp * dylan@instance-1:/var/www/dylanvb.me/public/api/
gcloud compute ssh instance-1 --command 'sudo systemctl restart dylanvb-api'
cd ../../../..
