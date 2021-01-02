const _ = require('../lib/lodash.min.js');

function replaceChar(origString, replaceChar, index) {
    let firstPart = origString.substr(0, index);
    let lastPart = origString.substr(index + 1);

    let newString = firstPart + replaceChar + lastPart;
    return newString;
}
function updateImage(image) {

    let r1 = new RegExp('..................#.');
    let r2 = new RegExp('#....##....##....###');
    let r3 = new RegExp('.#..#..#..#..#..#...');

    let monsterLen = '..................#.'.length;
    for (let index = 0; index < image.length - 2; index++) {
        for (let imageIndex = 0; imageIndex < image[0].length; imageIndex++) {

            let rr1 = r1.exec(image[index].substring(imageIndex, imageIndex + monsterLen));
            let rr2 = r2.exec(image[index + 1].substring(imageIndex, imageIndex + monsterLen));
            let rr3 = r3.exec(image[index + 2].substring(imageIndex, imageIndex + monsterLen));

            if (rr1 != null
                && rr2 != null
                && rr3 != null
                && rr1.index == rr2.index
                && rr1.index == rr3.index
                && rr3.index == 0
                && rr1.input.indexOf('O') < 0
                && rr2.input.indexOf('O') < 0
                && rr3.input.indexOf('O') < 0
            ) {
                image[index] = replaceChar(image[index], 'O', imageIndex + rr2.index + 18);

                image[index + 1] = replaceChar(image[index + 1], 'O', imageIndex + rr2.index + 0);
                image[index + 1] = replaceChar(image[index + 1], 'O', imageIndex + rr2.index + 5);
                image[index + 1] = replaceChar(image[index + 1], 'O', imageIndex + rr2.index + 6);
                image[index + 1] = replaceChar(image[index + 1], 'O', imageIndex + rr2.index + 11);
                image[index + 1] = replaceChar(image[index + 1], 'O', imageIndex + rr2.index + 12);
                image[index + 1] = replaceChar(image[index + 1], 'O', imageIndex + rr2.index + 17);
                image[index + 1] = replaceChar(image[index + 1], 'O', imageIndex + rr2.index + 18);
                image[index + 1] = replaceChar(image[index + 1], 'O', imageIndex + rr2.index + 19);

                image[index + 2] = replaceChar(image[index + 2], 'O', imageIndex + rr2.index + 1);
                image[index + 2] = replaceChar(image[index + 2], 'O', imageIndex + rr2.index + 4);
                image[index + 2] = replaceChar(image[index + 2], 'O', imageIndex + rr2.index + 7);
                image[index + 2] = replaceChar(image[index + 2], 'O', imageIndex + rr2.index + 10);
                image[index + 2] = replaceChar(image[index + 2], 'O', imageIndex + rr2.index + 13);
                image[index + 2] = replaceChar(image[index + 2], 'O', imageIndex + rr2.index + 16);
            }
        }
    }
    return image;
}

function isFit(tile, [right, top, left, bottom]) {
    let last = tile.length - 1;
    return (right == null || tile.every((_, i) => right[i][0] == tile[i][last]))
        && (left == null || tile.every((_, i) => tile[i][0] == left[i][last]))
        && (top == null || tile.every((_, i) => tile[0][i] == top[last][i]))
        && (bottom == null || tile.every((_, i) => bottom[0][i] == tile[last][i]));
}

function rotate(lines) {
    let map = [];
    let length = lines.length;
    for (let i = 0; i < length; i++) {
        let line = ''
        for (let j = length - 1; j >= 0; j--) {
            line += lines[j][i];
        }
        map.push(line);
    }

    return map;
}
function flip(lines) {
    return lines.map(line => line.split('').reverse().join(''))
}
function getNeighbors(xKey, yKey) {
    return [
        [xKey + 1, yKey], // right
        [xKey, yKey + 1], // top
        [xKey - 1, yKey], // left
        [xKey, yKey - 1] // bottom
    ];
}
function buildSeaMap(map) {
    _.flatten(Object.keys(map).map(xKey => Object.keys(map[xKey]).map(yKey => [xKey, yKey])))
        .forEach(([x, y]) => {
            const lines = map[x][y].lines;
            for (let index = 0; index < lines.length; index++) {
                lines[index] = lines[index].substring(1, lines[index].length - 1);
            }
            lines.pop();
            lines.shift();
            map[x][y].lines = lines;
        });
    let minX = Math.min.apply(Math, Object.keys(map));
    let maxX = Math.max.apply(Math, Object.keys(map));

    let minY = Math.min.apply(Math, Object.keys(map[0]));
    let maxY = Math.max.apply(Math, Object.keys(map[0]));
    const tileLength = map[0][0].lines.length;
    const sea = [];
    for (let y = maxY; y >= minY; y--) {
        const seaTile = []
        map[minX][y].lines.forEach(line => seaTile.push(line));

        for (let x = minX + 1; x <= maxX; x++) {
            map[x][y].lines.forEach((line, i) => seaTile[i] = seaTile[i] + line);
        }
        seaTile.forEach(line => sea.push(line))
    }
    return sea;
}
module.exports = function task({ data }) {
    let tiles = data.join('\n')
        .split('\n\n')
        .map(p => {
            const lines = p.split('\n');
            const tileNumber = +lines.shift().split(' ')[1].split(':')[0];
            return {
                tileNumber,
                lines
            };
        })
        .map(tile => {
            let tile0 = tile.lines;
            let tile1 = rotate(tile0);
            let tile2 = rotate(tile1);
            let tile3 = rotate(tile2);

            let tile4 = flip(tile0);
            let tile5 = rotate(tile4);
            let tile6 = rotate(tile5);
            let tile7 = rotate(tile6);

            return {
                tileNumber: tile.tileNumber,
                positions: [tile0, tile1, tile2, tile3, tile4, tile5, tile6, tile7],
                isMapped: false,
                position: null
            };
        });

    let firstTile = tiles[0];
    firstTile.isMapped = true;
    firstTile.position = 0;
    let map = {
        0: {
            0: {
                tileNumber: firstTile.tileNumber,
                lines: firstTile.positions[0],
                rotate: 0
            }
        }
    };
    while (tiles.some(p => !p.isMapped)) {
        tiles = tiles.filter(p => !p.isMapped);
        for (const tile of tiles) {
            let emptyCells = _.uniqBy(_.flatten(_.flatten(Object.keys(map).map(xKey => Object.keys(map[xKey]).map(yKey => { return [+xKey, +yKey]; })))
                .map(([xKey, yKey]) => {
                    return getNeighbors(xKey, yKey).filter(([x, y]) => map[x] == null || map[x][y] == null);
                })), p => JSON.stringify(p))
            for (const [emptyCellX, emptyCellY] of emptyCells) {
                const [right, top, left, bottom] = getNeighbors(emptyCellX, emptyCellY)
                    .map(([xN, yN]) => {
                        return map[xN] && map[xN][yN] && map[xN][yN].lines;
                    });
                const isAny = tile.positions.some((tileLines, rotateIndex) => {
                    if (isFit(tileLines, [right, top, left, bottom])) {
                        map[emptyCellX] = map[emptyCellX] || {};
                        map[emptyCellX][emptyCellY] = {
                            tileNumber: tile.tileNumber,
                            lines: tileLines,
                            rotate: rotateIndex
                        }

                        tile.isMapped = true;
                        return true;
                    }
                    else {
                        return false;
                    }
                });
                if (isAny) {
                    break;
                }
            }
        }
    }
    let tile0 = buildSeaMap(map);
    let tile1 = rotate(tile0);
    let tile2 = rotate(tile1);
    let tile3 = rotate(tile2);

    let tile4 = flip(tile0);
    let tile5 = rotate(tile4);
    let tile6 = rotate(tile5);
    let tile7 = rotate(tile6);
    return [tile0, tile1, tile2, tile3, tile4, tile5, tile6, tile7].map(sea => updateImage(sea))
        .find(sea => sea.join('').indexOf('O') >= 0)
        .join('').split('').filter(p => p == '#').length;
}