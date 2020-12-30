const _ = require('../lib/lodash.min.js');

const directionOffset3D = { //x z y
    se: [0, 1, -1],
    sw: [-1, 1, 0],
    w: [-1, 0, 1],
    nw: [0, -1, 1],
    ne: [1, -1, 0],
    e: [1, 0, -1],
}

function getImmediatelyAdjacent3D([x, z, y]) {
    return Object.values(directionOffset3D).map(([x1, z1, y1]) => {
        return [x + x1, z + z1, y + y1];
    })
}
module.exports = function task({ data }) {
    const input = data.map(line => Array.from(line.matchAll(/(?:se|sw|ne|nw|e|w)/g)).map(match => match[0]));

    let blackTiles = {};
    input.forEach(moves => {
        let referenceCoordinates = [0, 0, 0];
        for (const move of moves) {
            referenceCoordinates[0] += directionOffset3D[move][0];
            referenceCoordinates[1] += directionOffset3D[move][1];
            referenceCoordinates[2] += directionOffset3D[move][2];
        }
        const key = referenceCoordinates.join(',');
        if (blackTiles[key] == null) {
            blackTiles[key] = referenceCoordinates
        } else {
            delete blackTiles[key];
        }
    });

    for (let index = 0; index < 100; index++) {
        let tiles = Object.values(blackTiles)
        let newBlackTiles = {};
        tiles.forEach(tile => {
            const adjacentCells = getImmediatelyAdjacent3D(tile);
            adjacentCells.push(tile);

            for (const adjacent of adjacentCells) {
                const blackAdjacentTiles = getImmediatelyAdjacent3D(adjacent).map(p => blackTiles[p.join(',')])
                    .filter(p => p != null).length;
                const isBlack = blackTiles[adjacent.join(',')];
                if (isBlack) {
                    if (!(blackAdjacentTiles == 0 || blackAdjacentTiles > 2)) {
                        newBlackTiles[adjacent.join(',')] = adjacent;
                    }
                } else {
                    if (blackAdjacentTiles == 2) {
                        newBlackTiles[adjacent.join(',')] = adjacent;
                    }
                }
            }
        });
        blackTiles = newBlackTiles
    }
    return Object.keys(blackTiles).length;
}
