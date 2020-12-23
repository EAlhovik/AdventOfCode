
module.exports = function task({ data }) {
    let rules = data.splice(0, data.findIndex(p => p == ''))
        .map(rule => ({
            ruleName: rule.split(': ')[0],
            rules: rule.split(': ')[1].split(' or ')
                .map(p => ({ min: Number(p.split('-')[0]), max: Number(p.split('-')[1]) }))
        }));
    data.shift(); // empty line
    data.shift(); // your ticket text
    let yourTicket = data.shift();
    data.shift(); // nearby tickets text
    data.shift(); // empty line
    let nearbyTickets = data;
    
    let result = nearbyTickets.map(t => {
        let w1 = t.split(',').map(Number)
            .filter(p => {
                return !rules.some(rule =>  {
                    return  rule.rules.some(w => w.min <= p && p <= w.max ); //.length == 1;
                } )
            });
        // console.log(w1)
        return w1;
    })
    .filter(p => p.length > 0)
    .map(p => p.pop())
    .reduce((accumulator, currentValue) => accumulator + currentValue)
    
    return {
        result
    }
}
