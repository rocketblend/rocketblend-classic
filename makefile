.PHONY: all install-ef update-ef migrations

PROJECT = RocketBlend
CONTEXT = ApplicationDbContext
MIGRATION_NAME = Init

all: migrations

install-ef:
	dotnet tool install --global dotnet-ef --version="6.0.4"

update-ef:
	dotnet tool update --global dotnet-ef

migrations:
	dotnet ef migrations add "${MIGRATION_NAME}" --project "src/${PROJECT}.Infrastructure" --context ${CONTEXT} --startup-project "src/${PROJECT}" --output-dir Persistence/Migrations --verbose