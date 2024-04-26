### CORS
Api и клиент должен находится на одном IIS (после аутентификации на клиенте запросы к ApI не требуют повторной аутентификации), если необходимо клиент на другом IIS (IIS Express - отладка), тогда в Program.cs нужно добавить хост:
app.UseCors(builder => builder.WithOrigins("http://localhost:xxxxx").AllowCredentials());
### EFIDS
Выгрузить базу в папку Model
1. в папке EF_IDS -> Открыть в терминале
2. В терминале набрать :
dotnet-ef dbcontext scaffold "data source=krr-sql-paclx03;initial catalog=KRR-PA-CNT-Railway;integrated security=True;TrustServerCertificate=true" Microsoft.EntityFrameworkCore.SqlServer --schema IDS --data-annotations -o Models
3. Если только несколько таблиц, набрать: 
dotnet-ef dbcontext scaffold "data source=krr-sql-paclx03;initial catalog=KRR-PA-CNT-Railway;integrated security=True;TrustServerCertificate=true" Microsoft.EntityFrameworkCore.SqlServer --table operators_of_out_way --table operators_of_station --data-annotations -o Models


