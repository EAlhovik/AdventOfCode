function getSeats(i,j, input){
    let seats = [];

    for(let i1 = i -1, j1 = j; i1 >= 0; i1 --)
    {
        seats.push(input[i1] && input[i1][j1]);
        if(seats[seats.length -1] != '.') break;
    }

    for(let i1 = i -1, j1 = j-1; i1 >= 0 && j1 >= 0; i1--, j1--)
    {
        seats.push(input[i1] && input[i1][j1]);
        if(seats[seats.length -1] != '.') break;
    }
    
    for(let i1 = i, j1 = j-1; j1 >= 0;  j1--)
    {
        seats.push(input[i1] && input[i1][j1]);
        if(seats[seats.length -1] != '.') break;
    }

    for(let i1 = i+1, j1 = j-1; i1 < input.length && j1 >= 0; i1++, j1--)
    {
        seats.push(input[i1] && input[i1][j1]);
        if(seats[seats.length -1] != '.') break;
    }

    for(let i1 = i+1, j1 = j;  i1 < input.length; i1++)
    {
        seats.push(input[i1] && input[i1][j1]);
        if(seats[seats.length -1] != '.') break;
    }

    for(let i1 = i+1, j1 = j+1; i1 < input.length && j1 < input.length; i1++, j1++)
    {
        seats.push(input[i1] && input[i1][j1]);
        if(seats[seats.length -1] != '.') break;
    }

    for(let i1 = i, j1 = j+1; j1 < input.length; j1++)
    {
        seats.push(input[i1] && input[i1][j1]);
        if(seats[seats.length -1] != '.') break;
    }

    for(let i1 = i-1, j1 = j+1; i1 >= 0 && j1 < input.length; i1--, j1++)
    {
        seats.push(input[i1] && input[i1][j1]);
        if(seats[seats.length -1] != '.') break;
    }

    return seats.filter(p => p != null).filter(p => p != '.');
}

function toNextRound(input, w){
    const result = JSON.parse(JSON.stringify(input));

    input.forEach((inputX, i) => {
        inputX.forEach((value, j) => {
            let seats = getSeats(i,j,input);
            // console.log(i,j, seats.length)
            if(value == 'L'){
                if(seats.filter(p => p == '#').length == 0){
                    result[i][j] = '#';
                }
            } else if (value == '#') {
                if(seats.filter(p => p == '#').length >= 5){
                    result[i][j] = 'L';
                }
            }
        });
    });
    return result;
}

// function sleep(ms) {
//     return new Promise(resolve => setTimeout(resolve, ms));
//   }
module.exports = async function task({data}){

    let input = data.map(p => p.split(''));
    let next = toNextRound(input);
    
    let i = 0;
    while(JSON.stringify(next) != JSON.stringify(input)){
        // await sleep(2000);
        let r = toNextRound(next);
        // r.forEach(p => console.log(JSON.stringify(p.join("")))); console.log();
        input = next;
        next = r;
        i++
    }
    console.log('check i for infinite loop',i)
    console.log(JSON.stringify(next).split('').filter(p => p == '#').length)
    return JSON.stringify(next).split('').filter(p => p == '#').length;
}
