const task1 = require('./task1.js');

module.exports = function task({data, ways}){
    let result = 1;
    for(let i =0; i< ways.length; i++){
        let n = task1({
            data: [...data],
            xStep: ways[i].xStep,
            yStep: ways[i].yStep
        });
        console.log(ways[i].xStep, ways[i].yStep, n)
        result = result * n;
    }
    return result;
}