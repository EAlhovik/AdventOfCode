
const task1 = require('./task1.js');
function replaceChar(origString, replaceChar, index) {
    let firstPart = origString.substr(0, index);
    let lastPart = origString.substr(index + 1);
      
    let newString = firstPart + replaceChar + lastPart;
    return newString;
}
function updateImage(image){
    
let r1 =new RegExp('..................#.');
let r2 =new RegExp('#....##....##....###');
let r3 =new RegExp('.#..#..#..#..#..#...');

let monsterLen = '..................#.'.length;
    for (let index = 0; index < image.length-2; index++) {
        for (let imageIndex = 0; imageIndex < image[0].length; imageIndex++) {

            let rr1 =r1.exec(image[index].substring(imageIndex,imageIndex+monsterLen));
            let rr2 =r2.exec(image[index + 1].substring(imageIndex,imageIndex+monsterLen));
            let rr3 =r3.exec(image[index + 2].substring(imageIndex,imageIndex+monsterLen));

            if(rr1 != null
                && rr2 != null
                && rr3 != null
                && rr1.index == rr2.index
                && rr1.index == rr3.index
                && rr3.index == 0
                && rr1.input.indexOf('O') < 0
                && rr2.input.indexOf('O') < 0
                && rr3.input.indexOf('O') < 0
                ){
                    image[index] = replaceChar(image[index],'O',imageIndex + rr2.index+ 18);

                    image[index+1] = replaceChar(image[index+1],'O',imageIndex + rr2.index + 0);
                    image[index+1] = replaceChar(image[index+1],'O',imageIndex + rr2.index + 5);
                    image[index+1] = replaceChar(image[index+1],'O',imageIndex + rr2.index + 6);
                    image[index+1] = replaceChar(image[index+1],'O',imageIndex + rr2.index + 11);
                    image[index+1] = replaceChar(image[index+1],'O',imageIndex + rr2.index + 12);
                    image[index+1] = replaceChar(image[index+1],'O',imageIndex + rr2.index + 17);
                    image[index+1] = replaceChar(image[index+1],'O',imageIndex + rr2.index + 18);
                    image[index+1] = replaceChar(image[index+1],'O',imageIndex + rr2.index + 19);

                    image[index+2] = replaceChar(image[index+2],'O',imageIndex + rr2.index + 1);
                    image[index+2] = replaceChar(image[index+2],'O',imageIndex + rr2.index + 4);
                    image[index+2] = replaceChar(image[index+2],'O',imageIndex + rr2.index + 7);
                    image[index+2] = replaceChar(image[index+2],'O',imageIndex + rr2.index + 10);
                    image[index+2] = replaceChar(image[index+2],'O',imageIndex + rr2.index + 13);
                    image[index+2] = replaceChar(image[index+2],'O',imageIndex + rr2.index + 16);
                }
        }
    }
}
module.exports = function task({ data }) {
    let map = task1.task({ data }).map;
    let xKyes = Object.keys(map).map(p => +p).sort();
    let yKeys = Object.keys(map[xKyes[0]]).map(p => +p).sort();
    Object.keys(map).forEach(x => {
        Object.keys(map[x]).forEach(y => {
            let item = map[x][y].item;
            item.pop();
            item.shift();
            item = item.map(p => {
                p.pop();
                p.shift();
                return p.join('')
            })
            map[x][y] = item;
        });
    });
    let size = map[xKyes[0]][yKeys[0]].length;
    let picture = [];
    for (let indexY = 0; indexY < yKeys.length; indexY++) {
        for (let index = 0; index < size; index++) {
            picture.push('')
            for (let indexX = 0; indexX < xKyes.length; indexX++) {
                let x = xKyes[indexX];
                let y = yKeys[indexY];
                picture[picture.length-1] +=map[x][y][size -index - 1]
            }
        }
    }

// let r1 =new RegExp('..................#.');
// let r2 =new RegExp('#....##....##....###');
// let r3 =new RegExp('.#..#..#..#..#..#...');
let imagesWithMonsters = task1.getAllRotations(picture)
.filter((p, pIndex)=> {
    let image = p;
    if(Array.isArray(image[0])){
        image = image.map(i => i.join(''))
    }
    let totalHash = image
        .join('')
        .split('')
        .filter(p => p == '#').length;
    // let monsterLen = '..................#.'.length;
    let monsters = 0;
    updateImage(image);
    if(image.join('').indexOf('O')>=0){
        console.log(image.join('').split('').filter(p => p =='#').length);
    }
    if(monsters > 0){
       console.log({
        pIndex,
        'found monsters': monsters,
        'answer': totalHash - monsters * 15,
       // image,
        'answer2':  image.s
    .join('')
    .split('')
    .filter(p => p == '#').length
       }) 

    }
    return monsters > 0
});
    return ''
}