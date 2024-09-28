#Install

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