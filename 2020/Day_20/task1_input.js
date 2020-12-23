const fs = require('fs');
const task1 = require('./task1.js');

fs.readFile('input.txt', 'utf8', function (err, data) {
    if (err) {
        return console.log(err);
    }
    let rows = data.split('\r\n'); rows.pop();
    console.time('task1');
    let result = task1.task({
        data: rows,
        preamble: 25
    });
    console.log(result);
    console.timeEnd('task1');
});

