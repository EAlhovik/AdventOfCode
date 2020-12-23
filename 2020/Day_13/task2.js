
module.exports = function task({ data }) {
    let [first, ...buses] = data[1]
    .split(",")
    .map((x, i) => [ Number(x), i ])
    .filter(([x]) => !isNaN(x));
let multiplier = first[0];

let i = 0;

for(let [bus, index] of buses) {
    while(true) {
        if((i + index) % bus === 0) {
            console.log(bus, i,multiplier,index)
            multiplier *= bus;
            break;
        }
        i += multiplier;
    }
}
    return {
        i,buses
    }
}
