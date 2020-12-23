
module.exports = function task({data}){
    var bags = data.map(p => p.split('contain')).map(([bag, contain]) => ({
        color: bag.replace('bags', "").replace('bag', "").trim(),
        contain: contain.split(',').map(p => p.replace('bags', "").replace('bag', "").replace(/[,.]/g, "").trim()).map(p => isNaN(p[0]) ? {color: p, count: 0} : {color: p.substr(2), count: +p[0]})
        }));
        
        function getBags(items, color) {
          let item = bags.find(p => p.color == color);
          if (item == null) return 0; 
          let result = item.contain.reduce((accumulator, currentValue) => accumulator + currentValue.count + currentValue.count * getBags(items,currentValue.color), 0);
          return  result;
        }
        
        
        return bags.find(p => p.color == 'shiny gold').contain.reduce((accumulator, currentValue) => accumulator + currentValue.count + currentValue.count * getBags(bags,currentValue.color), 0)
        
}