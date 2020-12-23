const _ = require('../lib/lodash.min.js');

module.exports = function task({ data }) {
    let [player1, player2] = _.chunk(data, data.findIndex(p => p == '') + 1)
        .map(items => {
            let playerName = items.shift();
            if(items[items.length-1] == ''){
                items.pop();
            }
            return {
                playerName,
                cards: items.map(p => +p)
            }
        });
    while(player1.cards.length > 0 && player2.cards.length > 0 ){
        let p1 = player1.cards.shift();
        let p2 = player2.cards.shift();
        if(p1 > p2){
            player1.cards.push(p1);
            player1.cards.push(p2);
        } else if (p1 > p2) {
            console.log('Error p1 should not equal p2', p1)
        } else {
            player2.cards.push(p2);
            player2.cards.push(p1);
        }
    }
    let cards = player1.cards.length > 0 ? player1.cards : player2.cards;
    return cards.reverse()
        .map((p,index) => p * (index + 1))
        .reduce((a,b) => a+ b);
}
