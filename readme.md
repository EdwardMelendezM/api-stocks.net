# Install

- Run
```
dotnet run
```

- Add dotnet
```
  dotnet tool install --global dotnet-ef --version 8.0.0
```

- Execute migrations
```
  dotnet ef migrations add init
```

- Update database
```
  dotnet ef database update
```

## Paquetes NuGet Instalados

Este proyecto utiliza los siguientes paquetes NuGet:

1. **Microsoft.AspNetCore.Authentication.JwtBearer** (v8.0.0)
   - Proporciona soporte para la autenticación basada en JWT (JSON Web Tokens).

2. **Microsoft.AspNetCore.Identity.EntityFrameworkCore** (v8.0.0)
   - Integra ASP.NET Core Identity con Entity Framework Core para la gestión de usuarios y roles.

3. **Microsoft.AspNetCore.Mvc.NewtonsoftJson** (v8.0.0)
   - Añade soporte para la serialización/deserialización JSON utilizando Newtonsoft.Json.

4. **Microsoft.AspNetCore.OpenApi** (v8.0.8)
   - Proporciona soporte para OpenAPI y la generación de documentación Swagger.

5. **Microsoft.EntityFrameworkCore.Design** (v8.0.0)
   - Herramientas de diseño para Entity Framework Core, necesarias para la creación de migraciones.

6. **Microsoft.EntityFrameworkCore.SqlServer** (v8.0.0)
   - Proveedor de Entity Framework Core para SQL Server.

7. **Microsoft.EntityFrameworkCore.Tools** (v8.0.0)
   - Herramientas CLI para Entity Framework Core, como comandos para migraciones.

8. **Microsoft.Extensions.Identity.Core** (v8.0.0)
   - Funcionalidades centrales de ASP.NET Core Identity.

9. **Newtonsoft.Json** (v13.0.3)
   - Biblioteca popular para la manipulación de objetos JSON.

10. **Swashbuckle.AspNetCore** (v6.4.0)
    - Integra Swagger en ASP.NET Core para la generación automática de documentación de APIs.