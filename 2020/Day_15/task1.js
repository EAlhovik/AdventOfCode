
module.exports = function task({ data }) {
    return data.map(d => {
        let numbers = d.split(',').map(Number);
        let turn = numbers.length;
        while(turn != 2020){
            let number = numbers.pop();
            let index = numbers.lastIndexOf( number);
            // console.log(number, index, numbers)
            numbers.push(number);
            numbers.push(index >= 0 ? numbers.length - index -1: 0);
            turn++;
        }
        return {
            d,
            number: numbers.pop(),
            turn
        };
    })
}
