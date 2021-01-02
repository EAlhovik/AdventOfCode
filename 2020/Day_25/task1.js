function findLoopSize(key) {
    let value = 1;
    const subject = 7;
    let i = 1;
    while (true) {
        value = (value * subject) % 20201227;
        if (value == key) {
            return i;
        }
        i++;
    }
}
function transform(subject, loop) {
    let value = 1;
    for (let index = 1; index <= loop; index++) {
        value = (value * subject) % 20201227;
    }
    return value;
}
module.exports = function task({ data }) {
    const publicKeys = data.map(p => +p);

    return {
        a: transform(publicKeys[0], findLoopSize(publicKeys[1])),
        b: transform(publicKeys[1], findLoopSize(publicKeys[0]))
    }
}
