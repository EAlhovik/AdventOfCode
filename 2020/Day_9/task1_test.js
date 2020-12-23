const fs = require('fs');
const path = require('path');
const task1 = require('./task1.js');

fs.readFile(`${path.parse(__filename).name}.txt`, 'utf8', function (err, data) {
    if (err) {
        return console.log(err);
    }
    let rows = data.split('\n'); rows.pop();
    let result = task1({
        data: rows,
        preamble: 5
    });
    console.log(result);
});

