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

module.exports = function task({data, preamble}){
    data = data.map(Number);
    for(let i = preamble; i < data.length; i++) {
        let array = data.slice(i-preamble, i);
        let sums = twoSum(array,data[i]);
        if(sums.length == 0){
            return {i, number: data[i], sums, array};
        }
    }
}