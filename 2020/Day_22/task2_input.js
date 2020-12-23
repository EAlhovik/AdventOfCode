const fs = require('fs');
const task2 = require('./task2.js');

fs.readFile('input.txt', 'utf8', function (err, data) {
    if (err) {
        return console.log(err);
    }
    let rows = data.split('\r\n'); rows.pop();
    console.time('task2');
    let result2 = task2({
        data: rows
    });
    console.log(result2);
    console.timeEnd('task2');
});

