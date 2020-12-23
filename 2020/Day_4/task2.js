
module.exports = function task({data, index, target}){
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
    .filter(fields => requiredFileds.every((value, index) => fields.some(f => f.startsWith(value)) ))
    .filter(fields => { var val = Number(fields.find(p => p.startsWith('byr')).split(':')[1]); return val >= 1920 && val <= 2002; })
    .filter(fields => { var val = Number(fields.find(p => p.startsWith('iyr')).split(':')[1]); return val >= 2010 && val <= 2020; })
    .filter(fields => { var val = Number(fields.find(p => p.startsWith('eyr')).split(':')[1]); return val >= 2020 && val <= 2030; })
    .filter(fields => { var val = fields.find(p => p.startsWith('hgt')).split(':')[1]; let num = Number(val.slice(0,-2)); return val.endsWith('cm') ? num >= 150 && num <=193 :  num >= 59 && num <=76; })
    .filter(fields => { var val = fields.find(p => p.startsWith('hcl')).split(':')[1]; return val.match(/^#[0-9a-f]{3,6}$/i) != null; })
    .filter(fields => { var val = fields.find(p => p.startsWith('ecl')).split(':')[1]; return ['amb','blu','brn','gry','grn','hzl','oth'].some(w => w===val); })
    .filter(fields => { var val = fields.find(p => p.startsWith('pid')).split(':')[1]; return val.match(/^[0-9]{9,9}$/) != null; })
    .length;
}