function arrayRotate(arr, reverse) {
    if (reverse) arr.unshift(arr.pop());
    else arr.push(arr.shift());
    return arr;
}

module.exports = function task({ data }) {
    let position = {
        N: 0,
        E: 0,
        S: 0,
        W: 0,
        waypoint: {
            N: 1,
            E: 10,
            S: 0,
            W: 0,
        }
    };
    data.forEach(item => {
        let command = item[0];
        let value = +item.substr(1);
        // console.log(command, value)
        switch (command) {
            case 'N':
                position.waypoint.N += value;
                break;
            case 'S':
                position.waypoint.S += value;
                break;
            case 'E':
                position.waypoint.E += value;
                break;
            case 'W':
                position.waypoint.W += value;
                break;
            case 'L': {
                let arr = [
                    position.waypoint.N,
                    position.waypoint.E,
                    position.waypoint.S,
                    position.waypoint.W,
                ];
                for (let i = 0; i < (value / 90); i++) {
                    arr = arrayRotate(arr, false);
                }
                position.waypoint.N = arr[0];
                position.waypoint.E = arr[1];
                position.waypoint.S = arr[2];
                position.waypoint.W = arr[3];
                break;
            }
            case 'R': {
                let arr = [
                    position.waypoint.N,
                    position.waypoint.E,
                    position.waypoint.S,
                    position.waypoint.W,
                ];
                for (let i = 0; i < (value / 90); i++) {
                    arr = arrayRotate(arr, true);
                }
                position.waypoint.N = arr[0];
                position.waypoint.E = arr[1];
                position.waypoint.S = arr[2];
                position.waypoint.W = arr[3];
                break;
            }
            case 'F':
                for (let i = 0; i < value; i++) {
                    position.N += position.waypoint.N;
                    position.E += position.waypoint.E;
                    position.S += position.waypoint.S;
                    position.W += position.waypoint.W;
                }
                break;
        }
        // console.log(command, value, position)
    });

    return {
        value: Math.abs(position.E - position.W) + Math.abs(position.N - position.S),
        a1: position.E - position.W,
        a2: position.N - position.S,
        position
    };
}
