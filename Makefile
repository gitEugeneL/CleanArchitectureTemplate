# Docker compose

up:
	docker compose up -d --build
	
down:
	docker compose down
	
down-and-clean:
	docker compose down -v
	
# Database migrations

db-migrate:
	# make migrate name=migration_name
	dotnet ef migrations add $(name) --project Infrastructure/Persistence --startup-project Presentation/Api --output-dir Migrations
	
db-update:
	dotnet ef database update --project Infrastructure/Persistence --startup-project Presentation/Api