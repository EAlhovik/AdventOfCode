function addNumberToMem(mem, n){
    mem.index++;
    mem.last = n;
    let memN = mem[n];
    if(memN == null){
        mem[n] = {
            index: mem.index
        }
    } else {
        memN.lastIndex = memN.index;
        memN.index = mem.index
    }
}
module.exports = function task({ data }) {
    return data.map(d => {
        let mem = {
            last: 0,
            index: -1
        };
        d.split(',').map(Number)
            .forEach(n => {
                addNumberToMem(mem, n);
            });

        let turn = mem.index;;
        while(turn != (30000000 - 1)){
            if(mem[mem.last].lastIndex != null) {
                let memN = mem[mem.last];
                let n =  memN.index - memN.lastIndex;
                // console.log(n, mem);
                addNumberToMem(mem, n);
            } else {
                // console.log(0, mem);
                addNumberToMem(mem, 0);
            }
            
            if(turn % 1000000 == 0)  console.log(turn, d, mem.last);
            turn++;
        }
        return {
            d,
            result: mem.last
        };
    })
}
