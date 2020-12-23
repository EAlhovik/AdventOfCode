const { count } = require("console");

module.exports =  function task({data}){
    let numbers = data.map(Number);
    let max = Math.max.apply(Math, numbers) + 3;
    numbers = numbers.sort(function(a, b){return a-b});
    numbers.unshift(0);
    numbers.push(max);
    // console.log(numbers)
    let diff = [[0]];

    let dim = [];
    for(let i = 0; i < numbers.length; i++){dim.push(Array.from({length: numbers.length}, (v, i) => 0) )}
    
    for(let x = 1; x < numbers.length ; x++){
        let index = 1;
        if(x-index >= 0) {
            let prev = numbers[x-index];
            let curr = numbers[x];
            if(curr - prev >= 0 && curr - prev <=3){
                dim[x-index][x] = 1;
            }
        }
        index = 2;
        if(x-index >= 0) {
            let prev = numbers[x-index];
            let curr = numbers[x];
            if(curr - prev >= 0 && curr - prev <=3){
                dim[x-index][x] = 1;
            }
        }
        index = 3;
        if(x-index >= 0) {
            let prev = numbers[x-index];
            let curr = numbers[x];
            if(curr - prev >= 0 && curr - prev <=3){
                dim[x-index][x] = 1;
            }
        }
    }

    for(let x = 1; x < numbers.length; x++){
        if(x - 3 >= 0 && dim[x- 3][x]>0){
            dim[x - 2][x]++;
            dim[x - 1][x]++;
        }
        if(x - 2 >= 0 && dim[x- 2][x]>0){
            dim[x - 1][x]++;
        }
    }

    // for(let x = 0; x < numbers.length ; x++){
    //     console.log(x, JSON.stringify(dim[x]));
    // }
    // console.log(' ');
    for(let x = 3; x < numbers.length; x++){
        if(dim[x-1][x] == 1) {
            dim[x-1][x] =dim[x-2][x-1];
        } else if(dim[x-1][x] == 2) {
            dim[x-1][x] =dim[x-2][x-1] + dim[x-3][x-2];
        } else if(dim[x-1][x] == 3) {

            let d3 = 1;
            if(x-4 >= 0) d3 = dim[x-4][x-3];
            dim[x-1][x] = dim[x-2][x-1]
                + dim[x-3][x-2]
                + d3;
        }
    }


    // if(dim[2-2][2] > 0) {
    //     dim[2-1][2] = 2;
    // }
    // for(let x = 3; x < numbers.length; x++){
    //     if(dim[x-2][x] > 0){
    //         dim[x-1][x] = dim[x-2][x] * dim[x-3][x-2] + dim[x-2][x-1];
    //     } else {
    //         dim[x-1][x] =dim[x-2][x-1];
    //     }
    // }

    // for(let x = 0; x < numbers.length ; x++){
    //     console.log(x, JSON.stringify(dim[x]));
    // }
    dim.pop();
    return dim.pop().pop();
}