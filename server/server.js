const http = require('http');
const fs = require('fs');
const sqlite3 = require('sqlite3').verbose();
const spawn = require('child_process').spawn;
const iconv = require('iconv-lite');


//Подключение к БД
let db = new sqlite3.Database('dataBase.db', (err) => {
  if (err) {
    return console.log(err.message);
  }
  console.log('Connected');
});


let res = "";

const express = require("express");
const app = express();

app.get("/getimage", function (request, response) {
  let name = request.query.name;
  fs.createReadStream(name).pipe(response);
});
app.get("/getgameslist", function (request, response) {
  var array = []
  let genre = request.query.genre;
  let rating = request.query.rating;
  if (genre == "All") {
    db.all('SELECT DISTINCT Games.id FROM Games WHERE Games.rating >= ?', [rating], (err, rows) => {
      if (err) {
        throw err;
      }
      console.log("hi");
      rows.forEach((row) => {
        array.push(row.id);
      });
      response.write(JSON.stringify(array));
      response.end();
    });
  }
  else {
    db.all('SELECT Games.id FROM Games,Genres,GenreGame WHERE Games.id = GenreGame.game_id and GenreGame.genre_id = Genres.id and Games.rating >= ? and Genres.name = ?', [rating, genre], (err, rows) => {
      if (err) {
        throw err;
      }
      rows.forEach((row) => {
        array.push(row.id);
      });
      response.write(JSON.stringify(array));
      response.end();
    });
  }

});
app.get("/getnews", function (request, response) {
  const proc = spawn('python', ['parser.py']);
  proc.stdout.once('data', function (data) {
    var msg = iconv.decode(data, "cp1251");
    response.write(JSON.stringify(msg));
    response.end();
  })
});
app.get("/reg", function (request, response) {
  let login = request.query.login;
  let nick = request.query.nick;
  let pass = request.query.pass;
  var insrt = db.prepare("INSERT into Users(login, nickname, password) VALUES (?,?,?)");
  insrt.run(login, nick, pass);

  console.log("user {login} added");
  response.end();
});
app.get("/getgame", function (request, response) {
  var id = request.query.id;
  db.get("SELECT GAMES.name, Games.rating, Games.description, Games.releaseDate, Developers.name as devName FROM Games, Developers WHERE Developers.id = Games.id_developer and Games.id=?", [id], (err, row) => {
    console.log(JSON.stringify(row));
    response.write(JSON.stringify(row));
    response.end();
  });
});
app.get("/getgenre", function (request, response) {
  var id = request.query.id;
  db.all("SELECT Genres.name FROM Games,Genres,GenreGame WHERE Games.id = GenreGame.game_id and GenreGame.genre_id = Genres.id and Games.id=?", [id], (err, row) => {
    var res = [];
    row.forEach((r)=>{
      res.push(r.name);
    })
    response.write(JSON.stringify(res));
    response.end();
  });
});
app.get("/getgenres", function (request, response) {

  result = [];
  db.all("SELECT name FROM Genres", [], (err, rows) => {
    if (err) {
      throw err;
    }
    rows.forEach((row) => {
      result.push(row.name);
    })
    response.write(JSON.stringify(result));
    response.end();
  });

});
app.get("/getreviews", function(request, response){
  var game_id = request.query.id;
  db.all("SELECT Users.nickname, Reviews.content FROM Games, Users, Reviews WHERE Reviews.id_game = Games.id and Reviews.id_user = Users.login and Games.id=?",[game_id],(err, rows)=>{
    var res = [];
    rows.forEach((row)=>{
      var rev = {
        nickname : row.nickname,
        content : row.content
      };
      res.push(rev);
    });
    response.write(JSON.stringify(res));
    response.end();
  });
});
app.get("/auth", function (request, response) {
  let login = request.query.login;
  let pass = request.query.password;
  db.get("SELECT nickname FROM Users WHERE login = ? AND password = ?", [login, pass], (err, row) => {
    if (err) {

    }
    if (row == undefined) {
      response.write("forbidden");
    }
    else {
      response.write("allowed");
    }
    response.end();
    //return row;
  });
});
app.get("/postreview", function(request, response)
{
  db.run("INSERT INTO Reviews(id_game, id_user, content, score) VALUES($id_game,$id_user,$content,$score)",{
    $id_game : request.query.id,
    $id_user : request.query.user,
    $content : request.query.review,
    $score : request.query.rating
  })
  response.end();
});
app.get("/getusernick", function(request, response)
{
  db.get("SELECT nickname FROM Users WHERE login = ?",[request.query.login], (err, row)=>
  {
    response.write(row.nickname);
    response.end();
  });
});
app.get("/getuserdate", function(request, response)
{
  db.get("SELECT registrationDate FROM Users where login = ?",[request.query.login], (err, row)=>{
    console.log(row);
    response.write(row.registrationDate);
    response.end();
  });
})
app.get("/getuserreviews", function(request, response)
{
  var res = [];
  db.all("SELECT Games.name, Reviews.score FROM Games, Reviews Where Reviews.id_user = ? and Reviews.id_game = Games.id", [request.query.login],(err, rows)=>
  {
    console.log(rows);
    rows.forEach((row)=>{
      var data =
      {
        name : row.name,
        score : row.score
      };
      res.push(data);
    });
    response.write(JSON.stringify(res));
    response.end();
  });
});
app.listen(25525);