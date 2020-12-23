
module.exports = function task({data}){
    var passwords = data.map(p => {
        let password = p.split(': ')[1];
        let policy = p.split(': ')[0];
        let symbol = policy.split(' ')[1];
        let times = policy.split(' ')[0];
    
        return {
        password: password,symbol, min: Number(times.split('-')[0]), max: Number(times.split('-')[1])
        }
    });
    return passwords.filter( p => p.password != null)
     .filter(p => {let w = (p.password.match(new RegExp(p.symbol, "g")) || []).length; return w >= p.min && w <= p.max; } )
     .length;
}