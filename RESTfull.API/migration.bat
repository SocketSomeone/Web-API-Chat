pause

dotnet ef migrations add Initial -c DataContext --project ../RESTfull.Domain.Migrations
dotnet ef database update -c DataContext --project ../RESTfull.Domain.Migrations