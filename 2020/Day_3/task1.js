

module.exports = function task({data, xStep, yStep}){
    function extendMap(){
        for(let i = 0; i< map.length; i++) map[i] += initialMap[i]
      }
      String.prototype.replaceAt = function(index, replacement) {
          return this.substr(0, index) + replacement + this.substr(index + replacement.length);
      }
    let initialMap = data;
    let map = data;
    let x = 0;
    let trees = 0;
    for(let y =0;y<map.length - 1; ){
       if(xStep + x >= map[y].length) extendMap();
       x += xStep;
       y += yStep;
       if(map[y][x] == '#') {
         trees++;
         map[y] = map[y].replaceAt(x,'X');
       }
       else {
           map[y] = map[y].replaceAt(x, 'O');
        }
    }
    return trees;
}