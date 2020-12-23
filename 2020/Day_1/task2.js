
module.exports = function task({data, index, target}){

    let numbers = data.map(p => Number(p)).sort();
    let result = 0;
    for (let i = 0; i < numbers.length - 2; i++) {
        // Fix the second element as A[j]  
        for (let j = i + 1; j < numbers.length - 1; j++) {
            // Now look for the third number  
            for (let k = j + 1; k < numbers.length; k++) {
                if (numbers[i] + numbers[j] + numbers[k] == 2020) {
                    result = numbers[i] * numbers[j] * numbers[k];
                }
            }
        }
    }
    return result;
}