function twoSum(arr, target) {
    let res = [];
    let indexes = [];
    for (let i = 0; i < arr.length - 1; i++) {
        for (let j = i + 1; j < arr.length; j++) {
            if (target === arr[i] + arr[j] && !indexes.includes(i) && !indexes.includes(j)) {
                res.push([arr[i], arr[j]]);
                indexes.push(i);
                indexes.push(j);
            }
        }
    }
    return res;
}

module.exports = function task({data, index, target}){
    data = data.map(Number);
    let array = data.slice(0, index+1);
    let result = Array.from({length: array.length}, (v, i) => ({i:i, arr: [], sum: 0, complete: false})) ;

    array.forEach((value, i) => {
        for(let j= 0; j< result.length; j++){
            let item = result[j];
            if(i >= j && !item.complete) {
                let item = result[j];
                if(item.sum + value <= target){
                    item.sum += value;
                    item.arr.push(value);
                }else {
                    item.complete = true;
                }
            }else {
                // skip
            }
        }
    });
    let item = result.filter(p => p.sum == target)[0];
    
    return Math.min.apply(Math, item.arr) + Math.max.apply(Math, item.arr);
}