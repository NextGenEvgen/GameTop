const http = require('http');
const fs = require('fs');
const sqlite3 = require('sqlite3').verbose();


//Подключение к БД
let db = new sqlite3.Database('dataBase.db', (err)=>{
    if (err){
        return console.log(err.message);
    }
    console.log('Connected');
});


let res = "";

let sql = 'select Name name from Games';

var dataToClient = {
    'resolution': null,
    'other': null
  }
var jsonObj = {
    "WSCI_TYPE" : "WSCI_DATA",
    "WSCI_DATA" : dataToClient
  }
  
//Выполнение запроса
db.all(sql, [], (err, rows) => {
    if (err) {
      throw err;
    }
    rows.forEach((row) => {
      res += row.name + ',';
    });
  });

  const express = require("express");
  const app = express();
  
  app.get("/getimage", function(request, response){
    let name = request.query.name;
    fs.createReadStream(name).pipe(response);
  });
  app.get("/auth", function(request, response){
    let login = request.query.login;
    let pass = request.query.password;
    db.get("SELECT nickname FROM Users WHERE login = ? AND password = ?",[login, pass],(err, row) => {
      if (err) {

      }
      if (row == undefined)
      {
		jsonObj.WSCI_DATA['resolution']='forbidden';
        response.send(JSON.stringify(jsonObj));
      }
      else {
		jsonObj.WSCI_DATA['resolution']='allowed';
        response.send(JSON.stringify(jsonObj));
      }
      response.end();
      //return row;
    });    
  });
  app.get("/", function(request, response){
     jsonObj.WSCI_DATA['resolution']='hello world';
     response.send(JSON.stringify(jsonObj));
  });
  app.listen(25525);