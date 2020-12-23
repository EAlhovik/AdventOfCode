const _ = require('../lib/lodash.min.js');

function pickUp(labeling, current){
    let currentIndex = labeling.indexOf(current);
    let pickUpIndex = currentIndex+1 == labeling.length
        ? 0
        : currentIndex+1;
    let pickUp = labeling[pickUpIndex]
    labeling.splice(pickUpIndex, 1);
    return pickUp;
}
function move(labeling, moves, current, maxValue){
    if(moves == 0){
        return labeling;
    }
    moves--;
    let pickUp1 = pickUp(labeling, current);
    let pickUp2 = pickUp(labeling, current);
    let pickUp3 = pickUp(labeling, current);

    let currentIndex = labeling.indexOf(current);
    // let currentIndex = labeling.findIndex(p => p == current);
    let newCurrent = labeling[currentIndex+1 == labeling.length?0:currentIndex+1];

    let destination = -1;
    while(destination == -1){
        current--;
        if(current < 0) { current = maxValue; }
        let destinationIndex = labeling.indexOf(current);
        // let destinationIndex = labeling.findIndex(p => p == current);
        if(destinationIndex >= 0){
            destination = current;
            labeling.splice(destinationIndex+1, 0, pickUp3);
            labeling.splice(destinationIndex+1, 0, pickUp2);
            labeling.splice(destinationIndex+1, 0, pickUp1);
        }
    }
    return {
        moves,newCurrent,maxValue
    }
    return move(labeling,moves,newCurrent,maxValue);
}
module.exports = function task({ data }) {
    let labeling = data.find(p => true).split('').map(p => +p);
    let maxValue = Math.max.apply(Math, labeling);
    Array.from({length: 1000000}, (v, i) => i+1)
        .filter(p => p > maxValue)
        .forEach(p => labeling.push(p))

    let result = [];
    let obj = {
        newCurrent: labeling[0]
    }
    for(let i=0; i< 10000000; i++){
        if(i%10000 == 0){
            console.log(i, new Date)
        }
        obj = move(labeling, 10000000, labeling[0], maxValue);
    }

    return [pickUp(result, 1), pickUp(result, 1)];


    // let result = move(labeling, 10000000, labeling[0], maxValue);

    // return [pickUp(result, 1), pickUp(result, 1)];
}
