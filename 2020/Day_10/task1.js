function pairwise(arr, func){
    for(var i=0; i < arr.length - 1; i++){
        func(arr[i], arr[i + 1])
    }
}

module.exports = function task({data}){
    let numbers = data.map(Number);
    let max = Math.max.apply(Math, numbers) + 3;
    numbers = numbers.sort(function(a, b){return a-b});
    numbers.unshift(0);
    numbers.push(max);
    let diff = [];
    pairwise(numbers, function(current, next){
        diff.push(next - current);
    })
    if(diff.filter(p => p != 1 && p != 3).length > 0){
        console.log('wrong jolts diff', diff.filter(p => p != 1 && p != 3));
    }
    return diff.filter(p => p == 1).length * diff.filter(p => p == 3).length;
}