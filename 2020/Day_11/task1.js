function getSeats(i,j, input){
    let seats = [];
    seats.push(input[i-1] && input[i-1][j]);
    seats.push(input[i-1] && input[i-1][j-1]);
    seats.push(input[i] && input[i][j-1]);
    seats.push(input[i+1] && input[i+1][j-1]);
    seats.push(input[i+1] && input[i+1][j]);
    seats.push(input[i+1] && input[i+1][j+1]);
    seats.push(input[i] && input[i][j+1]);
    seats.push(input[i-1] && input[i-1][j+1]);

    return seats.filter(p => p != null);
}

function toNextRound(input, w){
    const result = JSON.parse(JSON.stringify(input));

    input.forEach((inputX, i) => {
        inputX.forEach((value, j) => {
            let seats = getSeats(i,j,input);

            if(value == 'L'){
                if(seats.filter(p => p == '#').length == 0){
                    result[i][j] = '#';
                }
            } else if (value == '#') {
                if(seats.filter(p => p == '#').length >= 4){
                    result[i][j] = 'L';
                }
            }
        });
    });
    return result;
}

module.exports = function task({data}){

    // let w = [
    //     [1,2,3,4,5,6,7,8,9],
    //     ['a1','b1','c1','d','e','f','g','r','y'],
    //     [1,2,3,4,5,6,7,8,9],
    //     ['a','b','c','d','e','f','g','r','y'],
    // ];
    // w.forEach(p => console.log(JSON.stringify(p.join("")))); console.log();
    // console.log(getSeats(2,1,w))

    let input = data.map(p => p.split(''));
    let next = toNextRound(input);
    
    let i = 0;
    while(i > 1000 || JSON.stringify(next) != JSON.stringify(input)){
        let r = toNextRound(next);
        input = next;
        next = r;
        i++
    }
    console.log('check i for infinite loop',i)
    
    return JSON.stringify(next).split('').filter(p => p == '#').length;
}
