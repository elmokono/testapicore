1. enable experimental features in docker

docker pull dpage/pgadmin4

docker run -p 8080:80 -e 'PGADMIN_DEFAULT_EMAIL=elmokono@hotmail.com' -e 'PGADMIN_DEFAULT_PASSWORD=yayayaya' -d dpage/pgadmin4

docker pull postgres:12.9

docker run -p 5432:5432 -e POSTGRES_PASSWORD=yayayaya -d postgres:12.9