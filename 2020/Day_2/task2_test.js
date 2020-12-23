const fs = require('fs');
const path = require('path');
const task2 = require('./task2.js');

fs.readFile(`${path.parse(__filename).name}.txt`, 'utf8', function (err, data) {
    if (err) {
        return console.log(err);
    }
    let rows = data.split('\n'); rows.pop();

    let result2 = task2({
        data: rows
    });
    console.log(result2);
});

