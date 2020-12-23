
function updateAddressWithMask(address, mask) {
    let result = [];
    let digits = address.toString(2).split('').reverse();
    mask.split('')
        .reverse()
        .forEach((m, i) => {
            if(m == '0'){
                result.push(digits[i] || '0');
            }else if(m == '1'){
                result.push(m);
            } else {
                result.push('X');
            }
        });
    let cases = Math.pow(2,result.filter(p => p == 'X').length);
    return Array.from({length: cases}, (v, i) => i)
        .map(i => {
            let arr = [];
            let bin = i.toString(2).split('').reverse();
            let index = 0;
            result.forEach(p => {
                if(p == 'X'){
                    arr.push(bin[index] || '0');
                    index++;
                }else {
                    arr.push(p);
                }
            })
            return parseInt(arr.reverse().join(''), 2);
        })
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
            let address = +setValue[0].split('[')[1].slice(0, -1);
            let addresses = updateAddressWithMask(address, mask);
            addresses.forEach(addr => mem[addr] = number);
            // console.log(address, mask, updateAddressWithMask(address, mask))
        }
    })
    return {
        sum: Object.values(mem).reduce((accumulator, currentValue) => accumulator + currentValue)
    };
}
