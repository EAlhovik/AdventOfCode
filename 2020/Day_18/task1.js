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

    let result = 0;
    let equation = '';
    item.split(' ').forEach((p, index) => {
        if(index <= 1){
            equation += p;
        } else if( index % 2 == 0){
            equation = `(${equation}${p})`
        }else {
            equation += p;
        }
    })
    // console.log(item, equation)
    eval(`result= ${equation}`)
    return result;
}

module.exports = function task({ data }) {
    return data.map(item => {
        // console.log(item)
        while(item.indexOf('(') >= 0){
            item = evaluateParentheses(item);
            // console.log('->',item)
        }
        let result = evaluateItem(item);
        return {
            item,
            result
        };
    })
    .map(p => p.result)
    .reduce((accumulator, currentValue) => accumulator + currentValue)
}
