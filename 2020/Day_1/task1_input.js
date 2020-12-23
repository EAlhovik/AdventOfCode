const fs = require('fs');
const path = require('path');
const task1 = require('./task1.js');

fs.readFile('input.txt', 'utf8', function (err, data) {
    if (err) {
        return console.log(err);
    }
    let rows = data.split('\n'); rows.pop();
    let result = task1({
        data: rows
    });
    console.log(result);
});

