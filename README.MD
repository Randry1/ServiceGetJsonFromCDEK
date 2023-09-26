# Сервис для получения стоимости доставки СДЕК
## Сборка проекта
```dotnet run --project .\WebSwager\WebSwager.csproj```

## Адрес swager
``` https://localhost:7202/swagger/index.html ```

## Пример передаваемого запроса
```
[
  {
    "id": "string",
    "fiasSenderCity": "string",
    "fiasReceiverCity": "string",
    "weight": "string",
    "length": "string",
    "width": "string",
    "height": "string"
  }
] 
```

## Обязательные поля помечены звездочкой 
```
[
  {
    id	string
    nullable: true
    fiasSenderCity*	string
    fiasReceiverCity*	string
    weight*	string
    length*	string
    width*	string
    height*	string
  }
]
```
## Curl запрос для провери сервиса
```curl -X 'POST' 'https://localhost:7202/api/Packages/GetPrice?Id=23&fiasSenderCity=a9958004-d348-442e-ae0d-b09aca9fdf25&fiasReceiverCity=f2976e80-32e1-4284-8eda-06cf19239cd1&weight=3423&length=342&width=342&height=342' -H 'accept: */*' -d ''```
