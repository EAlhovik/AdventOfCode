const fs = require('fs');
const path = require('path');
const task1 = require('./task1.js');

fs.readFile('input.txt', 'utf8', function (err, data) {
    if (err) {
        return console.log(err);
    }
    let rows = data.split('\r\n'); rows.pop();
    let result = task1({
        data: rows,
        xStep: 3,
        yStep: 1
    });
    console.log(result);
});

