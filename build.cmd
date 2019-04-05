sqlcmd -S localhost\SQLEXPRESS -i sql\fructose-ddl-100-pre-process.sql

sqlcmd -S localhost\SQLEXPRESS -i sql\fructose-ddl-200-process.sql

sqlcmd -S localhost\SQLEXPRESS -i sql\fructose-ddl-300-post-process.sql

sqlcmd -S localhost\SQLEXPRESS -i sql\fructose-dml-100-pre-process.sql

sqlcmd -S localhost\SQLEXPRESS -i sql\fructose-dml-200-process.sql

sqlcmd -S localhost\SQLEXPRESS -i sql\fructose-dml-300-post-process.sql

dotnet ef dbcontext scaffold "Data Source=DESKTOP-NKUJKQ4\SQLEXPRESS;Initial Catalog=Fructose;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -o ORM