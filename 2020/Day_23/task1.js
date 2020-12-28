const _ = require('../lib/lodash.min.js');

function pickUp(labeling, current) {
    let currentIndex = labeling.findIndex(p => p == current);
    let pickUpIndex = currentIndex + 1 == labeling.length
        ? 0
        : currentIndex + 1;
    let pickUp = labeling[pickUpIndex]
    labeling.splice(pickUpIndex, 1);
    return pickUp;
}
function move(labeling, moves, current, maxValue) {
    if (moves == 0) {
        return labeling;
    }
    moves--;
    let pickUp1 = pickUp(labeling, current);
    let pickUp2 = pickUp(labeling, current);
    let pickUp3 = pickUp(labeling, current);

    let currentIndex = labeling.findIndex(p => p == current);
    let newCurrent = labeling[currentIndex + 1 == labeling.length ? 0 : currentIndex + 1];

    let destination = -1;
    while (destination == -1) {
        current--;
        if (current < 0) { current = maxValue; }
        let destinationIndex = labeling.findIndex(p => p == current);
        if (destinationIndex >= 0) {
            destination = current;
            labeling.splice(destinationIndex + 1, 0, pickUp3);
            labeling.splice(destinationIndex + 1, 0, pickUp2);
            labeling.splice(destinationIndex + 1, 0, pickUp1);
        }
    }
    return move(labeling, moves, newCurrent, maxValue);
}
module.exports = function task({ data }) {
    let labeling = data.find(p => true).split('').map(p => +p);
    let maxValue = Math.max.apply(Math, labeling);

    let result = move(labeling, 100, labeling[0], maxValue)
        .map(p => p.toString())
        .join('')
        .split('1');
    return result[1] + result[0];
}
