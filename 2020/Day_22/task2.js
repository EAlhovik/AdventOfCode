const _ = require('../lib/lodash.min.js');

function canPlaySubGame(p1, player1cards, p2, player2cards){
    return p1 <= player1cards.length
        && p2 <= player2cards.length;
}
let round = 1;
function game(player1cards, player2cards) {
    let roundsOfPlayer1 = [];
    let roundsOfPlayer2 = [];
    let localRound = round
    // console.log(' '.repeat(localRound), 'start game round', localRound)
    while(player1cards.length > 0 && player2cards.length > 0 ){
        if(roundsOfPlayer1.indexOf(JSON.stringify(player1cards)) >= 0){
            return [player1cards, []]
        }else{
            roundsOfPlayer1.push(JSON.stringify(player1cards));
        }
        if(roundsOfPlayer2.indexOf(JSON.stringify(player2cards)) >= 0){
            return [player1cards, []]
        }else{
            roundsOfPlayer2.push(JSON.stringify(player2cards));
        }

        let p1 = player1cards.shift();
        let p2 = player2cards.shift();
        if(canPlaySubGame(p1, player1cards, p2, player2cards)) {
            round+=1;
            let [r1, r2] = game(_.clone(player1cards).slice(0,p1), _.clone(player2cards.slice(0,p2))) // 
            if(r1.length > r2.length){
                player1cards.push(p1);
                player1cards.push(p2);
            } else {
                player2cards.push(p2);
                player2cards.push(p1);
            }
        }else {
            if(p1 > p2){
                player1cards.push(p1);
                player1cards.push(p2);
            } else if (p1 > p2) {
                console.log('Error p1 should not equal p2', p1)
            } else {
                player2cards.push(p2);
                player2cards.push(p1);
            }
        }
    }
    // console.log(' '.repeat(localRound), 'end game round', localRound)
    return [player1cards, player2cards ];
}
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
    game(player1.cards, player2.cards);

    let cards = player1.cards.length > 0 ? player1.cards : player2.cards;
    return cards.reverse()
        .map((p,index) => p * (index + 1))
        .reduce((a,b) => a+ b);
}
