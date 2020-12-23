
module.exports = function task({data}){
    let numbers = data.map(p => Number(p));
    
    let result= 0
    numbers.forEach(p => { 
        let w = 2020 - p; 
        if(numbers.indexOf(w) >= 0) {
            result = w * p;
        }
    });
    return result;
}