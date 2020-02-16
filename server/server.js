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

//Выполнение запроса
db.all(sql, [], (err, rows) => {
    if (err) {
      throw err;
    }
    rows.forEach((row) => {
      res += row.name + ',';
    });
    console.log(res);
  });

http.createServer(function(request, response){
    //response.setHeader("UserId", 12);
    console.log("Url: " + request.url);
    console.log("Тип запроса: " + request.method);
    console.log("User-Agent: " + request.headers["user-agent"]);
    console.log("Все заголовки");
    console.log(request.headers);
    

    // console.log("намалевал");
    // response.setHeader("Content-Type", "image/jpg");
    // fs.readFile(filePath , (err, image) => {
    //     response.end(image);
    // });

    if (request.url == '/')
    {
        response.write(res);
        console.log("ответил");
        response.end();
    }
    else {
        var filePath = request.url.substr(1);
        fs.createReadStream(filePath).pipe(response);
        //response.end();
    }


}).listen(8080);