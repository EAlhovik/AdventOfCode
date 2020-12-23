
module.exports = function task({ data }) {
    let earliestTimestamp = Number(data[0]);
    let ids = data[1].split(',').filter(p => p != 'x').map(Number);

    let id = null
    let minutes = 0
    while(id == null){

        ids.forEach(p => {
            if((earliestTimestamp + minutes) % p == 0){
                id = p
            }
        })

        minutes++;
    }
    minutes--;
    // console.table(ids)
    return {
        id,
        minutes,
        result: id * minutes
    };
}
