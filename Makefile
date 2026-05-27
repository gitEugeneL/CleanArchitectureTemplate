# Docker compose

db-up:
	docker compose up -d --build
	
db-down:
	docker compose down
	
db-down-and-clean:
	docker compose down -v
	
# Database migrations

db-migrate:
	# make migrate name=migration_name
	dotnet ef migrations add $(name) --project Infrastructure/Persistence --startup-project Presentation/Api --output-dir Migrations
	
db-update:
	dotnet ef database update --project Infrastructure/Persistence --startup-project Presentation/Api