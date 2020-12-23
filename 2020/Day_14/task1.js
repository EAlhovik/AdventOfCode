
function updateNumberWithMask(number, mask) {
    let result = [];
    let digits = number.toString(2).split('').reverse();
    mask.split('')
        .reverse()
        .forEach((m, i) => {
            if(m == 'X'){
                result.push(digits[i] || '0');
            }else {
                result.push(m);
            }
        });
    return parseInt(result.reverse().join(''), 2);
}
module.exports = function task({ data }) {
    let mem = {};
    let mask = null;
    
    data.forEach(item => {
        if(item.startsWith('mask')) {
            let value = item.split(' = ');
            let m = `${value[0]} = "${value[1]}"`;
            eval(m);
        }else {
            let setValue = item.split(' = ');
            let number = +setValue[1];
            eval(`${setValue[0]} = ${updateNumberWithMask(number, mask)}`)
        }
    })
    return {
        mask,
        mem,
        sum: Object.values(mem).reduce((accumulator, currentValue) => accumulator + currentValue)
    };
}
