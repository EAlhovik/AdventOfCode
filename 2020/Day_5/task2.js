function getNumber(pass, l, seats){
    if (pass == '') return seats.pop();
      let i = l.indexOf(pass.charAt(0));
      if(i == 0){
        return getNumber(pass.slice(1), l, seats.slice(0,seats.length / 2));
      } else if(i == 1){
        return getNumber(pass.slice(1), l, seats.slice(seats.length / 2, seats.length));    
      } else if (i < 0) {
        return getNumber(pass.slice(1), l, seats);
      } else throw new Error('dsfsdf')
    }
module.exports = function task({data}){
    let seats = data.map(p => ({
        pass: p,
        row: getNumber(p, ['F','B'], [...Array(128).keys()]),
        col: getNumber(p, ['L','R'], [...Array(8).keys()])
    }))
    .map(p => ({id: p.row *8+ p.col, ...p})).sort(function(a, b){return a.id-b.id})
    .filter((val,index, arr) => {
        if(arr[index-1] == null ^ arr[index+1]==null) {return false}
        else {
        return (arr[index-1].id +1 == val.id) ^ (arr[index+1].id -1==val.id)
        }
    });

    return {
        yourSeat: seats[0].id + 1,
        foundSeats: seats
    };
}