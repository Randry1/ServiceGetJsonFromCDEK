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
    weight*	string  // В граммах
    length*	string  // в мм
    width*	string  // в мм
    height*	string  // в мм
  }
]
```
## Таблица городов

| Город             |     CityId      |            fiasSenderCity            |
|:------------------|:---------------:|:------------------------------------:|
| Новосибирск             |    270    | 8dea00e3-9aab-4d8e-887c-ef2aaa546456 |
| Кемерово            |    272    | 94bb19a3-c1fa-410b-8651-ac1bf7c050cd |
| Барнаул            |   274   | d13945a8-7017-46ab-b1e6-ede1e89317ad |
| Бийск            | 275 | 52f876f6-cb1d-4f23-a22f-b692609fc1e0 |


## Curl запрос для провери сервиса
```curl -X 'POST' 'https://localhost:7202/api/Packages/GetPrice?Id=23&fiasSenderCity=a9958004-d348-442e-ae0d-b09aca9fdf25&fiasReceiverCity=f2976e80-32e1-4284-8eda-06cf19239cd1&weight=3423&length=342&width=342&height=342' -H 'accept: */*' -d ''```


```shell
curl --request GET -sL  --url 'https://localhost:7202/api/Packages/GetAllCities'
```