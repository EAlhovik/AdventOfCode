const MAX_DEPTH = 15;
function getSequence(rule) {
    let sequence = rule.split(' ')
        .filter(p => p != '')
        .map(Number);
    return sequence;
}
function buildRegExp(rules){
    return new RegExp(`^${getRegExp(rules, 0, 0)}$`)
}
function getRegExp(rules, index, depth){
    let rule = rules[index];
    if(depth>=MAX_DEPTH){
        return 'c';
    }
    if (rule.letter != null) {
        return rule.letter;
    }
    else if (rule.sequence2 != null) {
        let s1 = rule.sequence1.map(p => getRegExp(rules, p, depth+1)).join('');
        let s2 = rule.sequence2.map(p => getRegExp(rules, p, depth+1)).join('');
        return `(${s1}|${s2})`;
    } else {
        return rule.sequence1.map(p => getRegExp(rules, p, depth+1)).join('');
    }
}
module.exports = function task({ data }) {
    let rules = {};
    data.splice(0, data.findIndex(p => p == ''))
        .map(p => {
            let index = p.split(': ')[0];
            let rule = p.split(': ')[1];
            let obj = {
                index
            };
            if (rule.indexOf('|') >= 0) {
                let sequences = rule.split('|')
                    .map(getSequence);
                obj.sequence1 = sequences[0];
                obj.sequence2 = sequences[1];
            } else if (rule[0] == '"') {
                let letter = rule[1];
                obj.letter = letter;
            } else {
                let sequence1 = getSequence(rule);
                obj.sequence1 = sequence1;
            }
            return obj
        })
        .forEach(rule => {
            rules[rule.index] = {}
            rules[rule.index].sequence1 = rule.sequence1;
            rules[rule.index].sequence2 = rule.sequence2;
            rules[rule.index].letter = rule.letter;
            rules[rule.index].index = rule.index;
        })
    data.shift(); // empty line
    let messages = data;
    rules[8] = {
        sequence1: [42],
        sequence2: [42, 8],
        letter: undefined,
        index: '8'
    }
    rules[11] = {
        sequence1: [42,31],
        sequence2: [42,11,31],
        letter: undefined,
        index: '11'
    }
    let regExp =buildRegExp(rules);
    // return rules[0]
    // return rules;
    return messages.filter(message =>regExp.test(message) ).length
}
