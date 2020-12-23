
module.exports = function task({data}){
    var passports = [{}];
    data.forEach(p => {
        if(p == '') { 
            passports.push({})
        } else {
            let passport = passports[passports.length -1];
            passport.fields = (passport.fields || []).concat(p.split(' '));
        }});
    passports = passports.filter(p => p.fields != null);
    let requiredFileds = ['byr:', 'iyr:', 'eyr:', 'hgt:', 'hcl:', 'ecl:', 'pid:'].sort();
   
    return passports
    .filter(p => p.fields != null)
    .map(p => p.fields.sort())
    .filter(fields =>  requiredFileds.every((value, index) => fields.some(f => f.startsWith(value)) ))
    .length;
}