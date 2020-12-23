const _ = require('../lib/lodash.min.js');

function rotate(matrix) {
    const N = matrix.length - 1;
    const result = matrix.map((row, i) => 
         _.map(row, (val, j) => matrix[N - j][i])
    );
    matrix.length = 0;
    matrix.push(...result);
    return matrix;
}
function getAdjacentCoordinates(x,y) {
    x = +x;
    y = +y;
    return [
        { x:x-1, y:y },
        { x:x+1, y:y },
        { x:x, y:y-1 },
        { x:x, y:y+1 },
    ]
}
function getAllRotations(item){
    let array = _.clone(item);
    let result = [];

    result = result.concat([
        JSON.parse(JSON.stringify(array)),
        rotate(JSON.parse(JSON.stringify(array))),
        rotate(rotate(JSON.parse(JSON.stringify(array)))),
        rotate(rotate(rotate(JSON.parse(JSON.stringify(array))))),
    ]);
    
    array.reverse();
    result = result.concat([
        JSON.parse(JSON.stringify(array)),
        rotate(JSON.parse(JSON.stringify(array))),
        rotate(rotate(JSON.parse(JSON.stringify(array)))),
        rotate(rotate(rotate(JSON.parse(JSON.stringify(array))))),
    ]);
    return result;
}
function isEmpty(map, c){
    return map[c.x] == null || map[c.x][c.y] == null;
}
function isFit(map, c, rotation){
    return [
        { x:c.x-1, y:c.y, test: (cell) => cell.map(w => w[w.length -1]).join('') == rotation.map(w => w[0]).join('') },
        { x:c.x+1, y:c.y, test: (cell) => { 
            return cell.map(w => w[0]).join('') == rotation.map(w => w[w.length -1]).join('')
        } },
        { x:c.x, y:c.y-1, test: (cell) => cell[0].join('') == rotation[rotation.length -1].join('') },
        { x:c.x, y:c.y+1, test: (cell) => cell[cell.length - 1].join('') ==  rotation[0].join('') },
    ]
        .every(coord => {
            if(isEmpty(map, coord)){
                return true;
            } else {
                return coord.test(map[coord.x][coord.y].item);
            }
        })
}
function task({ data }) {
    let tiles = _.chunk(data, data.findIndex(p => p == '') + 1)
        .map(item => {
            let tile = item.shift(); // Tile
            if(_.isEmpty(item[item.length -1])){
                item.pop(); // empty
            }
            return {
                tile,
                tileNumber: +tile.split(' ')[1].split(':')[0],
                item: item.map(p => p.split(''))
            }
        });
    let map = {
        [0]: {
            [0]:  tiles.shift()
        }
    };
    while(tiles.length > 0) {
        var i = tiles.length
        while (i--) {

            let tile = tiles[i];
            let rotations = getAllRotations(tile.item);

            let result = Object.keys(map).some(x => {
                return Object.keys(map[x]).some(y => {
                    let coordinates = getAdjacentCoordinates(x,y)
                        .filter(c => isEmpty(map, c));
                    return coordinates.some(c => {
                        return rotations.some(rotation => {
                            if(isFit(map, c, rotation)){
                                // set
                                map[c.x] = map[c.x] || {};
                                map[c.x][c.y] = {
                                    tile: tile.tile,
                                    tileNumber: tile.tileNumber,
                                    item: rotation
                                }
                                return true;
                            }else{
                                return false;
                            }
                        })
                    });
                })
            })
            if(result){
                tiles.splice(i, 1);
            }

        }
    }
    
    let minX = Math.min.apply(Math, Object.keys(map));
    let maxX = Math.max.apply(Math, Object.keys(map));
    
    let minY = Math.min.apply(Math, Object.keys(map[minX]));
    let maxY = Math.max.apply(Math, Object.keys(map[minX]));
    return {
        result: map[minX][minY].tileNumber
        * map[minX][maxY].tileNumber
        * map[maxX][minY].tileNumber
        * map[maxX][maxY].tileNumber,
        map
    }
}
module.exports = {
    task,
    getAllRotations
}
