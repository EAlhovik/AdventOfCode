const fs = require('fs');
const path = require('path');
const task2 = require('./task2.js');

fs.readFile('input.txt', 'utf8', function (err, data) {
    if (err) {
        return console.log(err);
    }
    let rows = data.split('\r\n'); rows.pop();

    let result2 = task2({
        data: rows,
        ways: [
            {xStep: 1, yStep: 1},
            {xStep: 3, yStep: 1},
            {xStep: 5, yStep: 1},
            {xStep: 7, yStep: 1},
            {xStep: 1, yStep: 2},
        ]
    });
    console.log(result2);
});
