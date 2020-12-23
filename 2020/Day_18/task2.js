function trim(str, ch) {
    var start = 0, 
        end = str.length;

    while(start < end && str[start] === ch)
        ++start;

    while(end > start && str[end - 1] === ch)
        --end;

    return (start > 0 || end < str.length) ? str.substring(start, end) : str;
}
function evaluateParentheses(item){
    let lIndex = null;
    let rIndex = item.indexOf(')');
    let i = 1;
    while(lIndex == null){
        if(item[rIndex - i] == '('){
            lIndex = rIndex - i;
        }
        i++;
    }
    let result = evaluateItem(item.substring(lIndex, rIndex+1));
    return item.substring(0, lIndex) + result + item.substring(rIndex+1);
}

function evaluateItem(item){
    item = trim(item, '(');
    item = trim(item, ')');
    let result = 0;
    let equation = '';
    let items = item.split(' ');
    for (let index = 0; index < items.length; index++) {
        if(Number.isInteger(+items[index])){
            let multiplierIndex = items.slice(index, items.length).indexOf('*');
            if(multiplierIndex == 1){
                equation += items[index];
            } else if(multiplierIndex == -1) {
                equation += `(${items.slice(index, items.length).join('')})`
                index = items.length
            }else{
                equation += `(${items.slice(index, index + multiplierIndex).join('')})`
                index +=multiplierIndex-1;
            }
        }else {
            equation += items[index];
        }

    }
    eval(`result= ${equation}`)
    return result;
}

module.exports = function task({ data }) {
    return data.map(item => {
        let i = item;
        while(item.indexOf('(') >= 0){
            item = evaluateParentheses(item);
        }
        let result = evaluateItem(item);
        return {
            original: i,
            result
        };
    })
    .map(p => p.result)
    .reduce((accumulator, currentValue) => accumulator + currentValue)
}
