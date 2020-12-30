const _ = require('../lib/lodash.min.js');

const directionOffset = {
    even: {
        se: [1, 1],
        sw: [0, 1],
        w: [-1, 0],
        nw: [0, -1],
        ne: [1, -1],
        e: [1, 0],
    },
    odd: {
        se: [0, 1],
        sw: [-1, 1],
        w: [-1, 0],
        nw: [-1, -1],
        ne: [0, -1],
        e: [1, 0],
    }
};
module.exports = function task({ data }) {
    let input = data.map(line => Array.from(line.matchAll(/(?:se|sw|ne|nw|e|w)/g)).map(match => match[0]));
    let obj = input.map(moves => {
        let referenceCoordinates = [0, 0];
        for (const move of moves) {
            const offsetType = referenceCoordinates[1] % 2 == 0
                ? directionOffset.even
                : directionOffset.odd;
            const offset = offsetType[move];

            referenceCoordinates[0] += offset[0];
            referenceCoordinates[1] += offset[1];
        }
        return referenceCoordinates.join('')
    }).reduce((a, c) => (a[c] = (a[c] || 0) + 1, a), Object.create(null));

    return Object.values(obj).filter(p => p % 2 == 1).length
}
