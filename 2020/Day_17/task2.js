function getNeighborsCoordinates(x,y,z,w){
    let neighbors = []
    for (let xOffset = -1; xOffset <= 1; xOffset++) {
        for (let yOffset = -1; yOffset <= 1; yOffset++) {
            for (let zOffset = -1; zOffset <= 1; zOffset++) {
                for (let wOffset = -1; wOffset <= 1; wOffset++) {
                    neighbors.push([x+xOffset,y+yOffset,z+zOffset,w+wOffset]);
                }
            }
        }
    }
    return neighbors
    .map(p => ({
        x: p[0],
        y: p[1],
        z: p[2],
        w: p[3],
    }))
    .filter(p => {
        return !((p.x == x) && (p.y == y) && (p.z == z) && (p.w == w));
    });
}
function getCell(grid, coord) {
    return grid[coord.x] && grid[coord.x][coord.y] && grid[coord.x][coord.y][coord.z] && grid[coord.x][coord.y][coord.z][coord.w] || '.'
}
function getAllCellCoordinates(grid){
    let minX = Math.min.apply(Math, Object.keys(grid));
    let maxX = Math.max.apply(Math, Object.keys(grid));
    
    let minY = Math.min.apply(Math, Object.keys(grid[0]));
    let maxY = Math.max.apply(Math, Object.keys(grid[0]));

    let minZ = Math.min.apply(Math, Object.keys(grid[0][0]));
    let maxZ = Math.max.apply(Math, Object.keys(grid[0][0]));

    let minW = Math.min.apply(Math, Object.keys(grid[0][0][0]));
    let maxW = Math.max.apply(Math, Object.keys(grid[0][0][0]));

    let cells = []
    for(let indexX= minX; indexX <= maxX; indexX++){
        for(let indexY= minY ; indexY <= maxY; indexY++){
            for(let indexZ= minZ ; indexZ <= maxZ; indexZ++){
                for(let indexW= minW ; indexW <= maxW; indexW++){
                    cells.push({
                        x: indexX,
                        y: indexY,
                        z: indexZ,
                        w: indexW,
                        value: grid[indexX][indexY][indexZ][indexW]
                    });
                }
            }
        }
    }
    return cells;
}
function extendGrid(grid){
    let minX = Math.min.apply(Math, Object.keys(grid));
    let maxX = Math.max.apply(Math, Object.keys(grid));
    
    let minY = Math.min.apply(Math, Object.keys(grid[0]));
    let maxY = Math.max.apply(Math, Object.keys(grid[0]));

    let minZ = Math.min.apply(Math, Object.keys(grid[0][0]));
    let maxZ = Math.max.apply(Math, Object.keys(grid[0][0]));

    let minW = Math.min.apply(Math, Object.keys(grid[0][0][0]));
    let maxW = Math.max.apply(Math, Object.keys(grid[0][0][0]));

    grid[minX-1] = {};
    grid[maxX+1] = {};
    for(let indexX= minX -1; indexX <= maxX+1; indexX++){
        grid[indexX] = grid[indexX] || {};
        for(let indexY= minY -1; indexY <= maxY+1; indexY++){
            grid[indexX][indexY] = grid[indexX][indexY] || {};
            for(let indexZ= minZ -1; indexZ <= maxZ+1; indexZ++){
                grid[indexX][indexY][indexZ] = grid[indexX][indexY][indexZ] || {};
                for(let indexW= minW -1; indexW <= maxW+1; indexW++){
                    grid[indexX][indexY][indexZ][indexW] = grid[indexX][indexY][indexZ][indexW] || '.';
                }
            }
        }
    }
}
function printGrid(grid){
    let minX = Math.min.apply(Math, Object.keys(grid));
    let maxX = Math.max.apply(Math, Object.keys(grid));
    
    let minY = Math.min.apply(Math, Object.keys(grid[0]));
    let maxY = Math.max.apply(Math, Object.keys(grid[0]));

    let minZ = Math.min.apply(Math, Object.keys(grid[0][0]));
    let maxZ = Math.max.apply(Math, Object.keys(grid[0][0]));

    let minW = Math.min.apply(Math, Object.keys(grid[0][0][0]));
    let maxW = Math.max.apply(Math, Object.keys(grid[0][0][0]));

    for (let indexW = maxW; indexW >= minW; indexW--) {
        for (let indexZ = maxZ; indexZ >= minZ; indexZ--) {
            console.log(`z=${indexZ}, w=${indexW}`);
            for (let indexY = maxY; indexY >= minY; indexY--) {
                let row = '';
                for (let indexX = maxX; indexX >= minX; indexX--) {
                    row += grid[indexX][indexY][indexZ][indexW];
                }
                console.log(row)
            }
        }
    }

}
module.exports = function task({ data }) {
    let grid = {};
    data.reverse()
        .forEach((yRow, yIndex) => {
            yRow.split('').forEach((x, xIndex) => {
                grid[xIndex] = grid[xIndex] || {};
                grid[xIndex][yIndex] = grid[xIndex][yIndex] || {};
                grid[xIndex][yIndex][0] = grid[xIndex][yIndex][0] || {};
                grid[xIndex][yIndex][0][0] = x;
            });
        });
    for(let i=0; i<6; i++){
        console.time('cycle'+i)
        extendGrid(grid);
        let newGrid = JSON.parse(JSON.stringify(grid));
        getAllCellCoordinates(grid)
            .forEach(p => {
                let active = getNeighborsCoordinates(p.x,p.y,p.z, p.w)
                    .map(p =>  getCell(grid, p))
                    .filter(p => p == '#')
                    .length;
                if(p.value == '#' && (active == 2 || active == 3)){

                }else if (p.value == '.' && active == 3) {
                    newGrid[p.x][p.y][p.z][p.w] = '#';
                } else {
                    newGrid[p.x][p.y][p.z][p.w] = '.';
                }
            });
        grid = newGrid;
        console.timeEnd('cycle'+i)
        // printGrid(grid);
    }
    return {
        c: getAllCellCoordinates(grid).filter(p => p.value == '#').length,
    };
}
