
module.exports = function task({ data }) {
    let rules = data.splice(0, data.findIndex(p => p == ''))
        .map(rule => ({
            ruleName: rule.split(': ')[0],
            rules: rule.split(': ')[1].split(' or ')
                .map(p => ({ min: Number(p.split('-')[0]), max: Number(p.split('-')[1]) }))
        }));
    data.shift(); // empty line
    data.shift(); // your ticket text
    let yourTicket = data.shift().split(',').map(Number);
    data.shift(); // nearby tickets text
    data.shift(); // empty line
    let nearbyTickets = data;
    
    let mainRule = {

    }
    yourTicket.forEach((p, index) => {
        mainRule[index] = rules.filter(rule =>  {
            return  rule.rules.some(w => w.min <= p && p <= w.max );
        } )
    });

    let result = nearbyTickets.map(t => {
        let w1 = t.split(',').map(Number)
            .filter(p => {
                return !rules.some(rule =>  {
                    return  rule.rules.some(w => w.min <= p && p <= w.max );
                } )
            });
        return {t, w1};
    })
    .filter(p => p.w1.length == 0)
    .map(p => p.t.split(',').map(Number))
    .forEach(ticket => {
        ticket.forEach((p, index) => {
            mainRule[index] = mainRule[index].filter(rule =>  {
                return  rule.rules.some(w => w.min <= p && p <= w.max );
            } )
        });
    })
    
    let res = Object.values(mainRule).map((w, index)=> ({
        index,
        ruleNames: w.map(e => e.ruleName),
        // ruleNamesW: w.map(e => e.ruleName).join(','),
        count: w.map(e => e.ruleName).length
    })).sort(function(a, b) {
        return a.ruleNames.length - b.ruleNames.length;
      });
      res = res.map((item, index, array) => {
        for (let i = index+1; i < res.length; i++) {
            array[i].ruleNames.splice(array[i].ruleNames.indexOf(item.ruleNames[0]),1)
        }
        return { index: item.index, ruleNamesW:  item.ruleNames.join(',')}
      }).sort(function(a, b) {
        return a.index - b.index;
      });

      
    return {
        result: res.filter(p => p.ruleNamesW.startsWith('departure'))
        .map(p => yourTicket[p.index] )
        .reduce((a, b) => a * b, 1)
    }
}
