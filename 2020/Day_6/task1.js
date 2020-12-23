
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

    return arr.reduce((acc, obj) => acc + obj.count, 0);

}