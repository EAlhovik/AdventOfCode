const fs = require('fs');
const path = require('path');
const task1 = require('./task1.js');
const task2 = require('./task2.js');

fs.readFile(`${path.parse(__filename).name}.txt`, 'utf8', function (err, data) {
    if (err) {
        return console.log(err);
    }
    let rows = data.split('\n'); rows.pop();
    let result1 = task1({
        data: rows,
        preamble: 5
    });
    let result2 = task2({
        data: rows,
        index: result1.i,
        target: result1.number
    });
    console.log(result2);
});

