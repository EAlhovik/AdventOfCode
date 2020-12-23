function getNeighborsCoordinates(x,y,z){
    return [
        [x-1,y+1,z-1],
        [x,y+1,z-1],
        [x+1,y+1,z-1],
        [x-1,y,z-1],
        [x,y,z-1],
        [x+1,y,z-1],
        [x-1,y-1,z-1],
        [x,y-1,z-1],
        [x+1,y-1,z-1],

        [x-1,y+1,z],
        [x,y+1,z],
        [x+1,y+1,z],
        [x-1,y,z],
        //[x,y,z],
        [x+1,y,z],
        [x-1,y-1,z],
        [x,y-1,z],
        [x+1,y-1,z],

        [x-1,y+1,z+1],
        [x,y+1,z+1],
        [x+1,y+1,z+1],
        [x-1,y,z+1],
        [x,y,z+1],
        [x+1,y,z+1],
        [x-1,y-1,z+1],
        [x,y-1,z+1],
        [x+1,y-1,z+1],
    ].map(p => ({
        x: p[0],
        y: p[1],
        z: p[2],
    }));
}
function getCell(grid, coord) {
    return grid[coord.x] && grid[coord.x][coord.y] && grid[coord.x][coord.y][coord.z] || '.'
}
function getAllCellCoordinates(grid){
    let minX = Math.min.apply(Math, Object.keys(grid));
    let maxX = Math.max.apply(Math, Object.keys(grid));
    
    let minY = Math.min.apply(Math, Object.keys(grid[0]));
    let maxY = Math.max.apply(Math, Object.keys(grid[0]));

    let minZ = Math.min.apply(Math, Object.keys(grid[0][0]));
    let maxZ = Math.max.apply(Math, Object.keys(grid[0][0]));

    let cells = []
    for(let indexX= minX; indexX <= maxX; indexX++){
        for(let indexY= minY ; indexY <= maxY; indexY++){
            for(let indexZ= minZ ; indexZ <= maxZ; indexZ++){
                cells.push({
                    x: indexX,
                    y: indexY,
                    z: indexZ,
                    value: grid[indexX][indexY][indexZ]
                });
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

    grid[minX-1] = {};
    grid[maxX+1] = {};
    for(let indexX= minX -1; indexX <= maxX+1; indexX++){
        grid[indexX] = grid[indexX] || {};
        for(let indexY= minY -1; indexY <= maxY+1; indexY++){
            grid[indexX][indexY] = grid[indexX][indexY] || {};
            for(let indexZ= minZ -1; indexZ <= maxZ+1; indexZ++){
                grid[indexX][indexY][indexZ] = grid[indexX][indexY][indexZ] || '.';
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
                // grid[xIndex][yIndex][0] = grid[xIndex][yIndex][0] || {};
                grid[xIndex][yIndex][0] = x;
            });
        });
    // extendGrid(grid);


    // for (const c of getNeighborsCoordinates(1,1,0)) {
    //     console.log(c,  getCell(grid, c))
    // }
    // let gridSize = 3;
    for(let i=0; i<6; i++){
        extendGrid(grid);
        let newGrid = JSON.parse(JSON.stringify(grid));
        getAllCellCoordinates(grid)
            .forEach(p => {
                let active = getNeighborsCoordinates(p.x,p.y,p.z)
                    .map(p =>  getCell(grid, p))
                    .filter(p => p == '#')
                    .length;
                if(p.value == '#' && (active == 2 || active == 3)){

                }else if (p.value == '.' && active == 3) {
                    newGrid[p.x][p.y][p.z] = '#';
                } else {
                    newGrid[p.x][p.y][p.z] = '.';

                }
            });
        grid = newGrid;
    }
    // extendGrid(grid)
    // console.log(grid)
    return {
        c: getAllCellCoordinates(grid).filter(p => p.value == '#').length,
    };
}
