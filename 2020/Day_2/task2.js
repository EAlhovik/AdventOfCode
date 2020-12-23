
module.exports = function task({data, index, target}){
    var passwords = data.map(p => {
        let password = p.split(': ')[1];
        let policy = p.split(': ')[0];
        let symbol = policy.split(' ')[1];
        let times = policy.split(' ')[0];
    
        return {
        password: password,symbol, min: Number(times.split('-')[0]), max: Number(times.split('-')[1])
        }
    });
    return passwords
    .filter( p => p.password != null).filter(p => (p.password[p.min -1] == p.symbol) ^ (p.password[p.max -1] == p.symbol))
    .length;
}