const _ = require('../lib/lodash.min.js');

function intersection(list){
    var result = list[0].split('');
    for(var i =1; i<list.length; i++){
     result = _.intersection(result,list[i].split(''))
     }
    return result;
   }
module.exports = function task({data}){
    var arr = [{}];
    data.forEach(p => {
        if(p != ''){
        var item = arr[arr.length - 1];
        item.list = item.list || [];
        item.list.push(p);
        item.str = item.str || '';
        item.str = item.str.concat(p);
        item.count = ''.concat(...new Set(item.str)).length
        } else {arr.push({});}
    });

    return  arr.map(p => intersection(p.list).length).reduce((a,b)=> a+b);
}