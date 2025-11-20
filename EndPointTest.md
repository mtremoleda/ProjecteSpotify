# Cançons

GET http://localhost:5000/cancons

POST http://localhost:5000/cancons

```JSON
{
  "titol": "Viva la vida",
  "artista": "Coldplay",
  "album": "Quart Album Studi",
  "durada": 243
}
```

GET BY ID http://localhost:5000/cancons/1f1fd487-09da-4cb3-963d-04a97af0504a

PUT http://localhost:5000/cancons/1f1fd487-09da-4cb3-963d-04a97af0504a
```JSON
{
  "titol": "Viva la vida",
  "artista": "Coldplay",
  "album": "Quart Album Studi",
  "durada": 245
}
```
