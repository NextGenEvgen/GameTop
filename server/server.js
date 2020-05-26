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

var array = []
//Выполнение запроса
db.all(sql, [], (err, rows) => {
    if (err) {
      throw err;
    }
    rows.forEach((row) => {
      array.push(row.name)
    });
  });

  const express = require("express");
  const app = express();
  
  app.get("/getimage", function(request, response){
    let name = request.query.name;
    fs.createReadStream(name).pipe(response);
  });
  app.get("/getgameslist", function(request, response)
  {
    response.write(JSON.stringify(array));
    response.end();
  });
  app.get("/auth", function(request, response){
    let login = request.query.login;
    let pass = request.query.password;
    db.get("SELECT nickname FROM Users WHERE login = ? AND password = ?",[login, pass],(err, row) => {
      if (err) {

      }
      if (row == undefined)
      {
        response.write("forbidden");
      }
      else {
        response.write("allowed");
      }
      response.end();
      //return row;
    });    
  });
  app.get("/", function(request, response){
      response.send("<h1>hello world</h1>");
  });
  app.listen(25525);