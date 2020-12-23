function getShiftedString(s, leftShifts, rightShifts) {
    // using `split('')` will result in certain unicode characters being separated incorrectly
    // use Array.from instead:
    const arr = Array.from(s);
    const netLeftShifts = (leftShifts - rightShifts) % arr.length;
    return [...arr.slice(netLeftShifts), ...arr.slice(0, netLeftShifts)]
        .join('');
}

module.exports = function task({ data }) {
    let position = {
        N: 0,
        E: 0,
        S: 0,
        W: 0,
        facing: 'E'
    };
    data.forEach(item => {
        let command = item[0];
        let value = +item.substr(1);
        // console.log(command, value)
        switch (command) {
            case 'N':
                position.N += value;
                break;
            case 'S':
                position.S += value;
                break;
            case 'E':
                position.E += value;
                break;
            case 'W':
                position.W += value;
                break;
            case 'L': {
                let directionIndex = 'NESW'.split('').findIndex(p => p == position.facing);
                let current = getShiftedString('NESW', directionIndex, 0);
                position.facing = getShiftedString(current, 0, (value / 90))[0];
                break;
            }
            case 'R': {
                let directionIndex = 'NESW'.split('').findIndex(p => p == position.facing);
                let current = getShiftedString('NESW', directionIndex, 0);
                position.facing = getShiftedString(current, (value / 90), 0)[0];
                break;
            }
            case 'F':
                position[position.facing] += value;
                break;
        }
        // console.log(position)
    });

    return {
        value: Math.abs(position.E - position.W) + Math.abs(position.N - position.S),
        a1: position.E - position.W,
        a2: position.N - position.S,
        position
    };
}
