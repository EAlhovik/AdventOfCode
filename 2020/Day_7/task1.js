
module.exports = function task({data}){
 
    var bags = data.map(p => p.split('contain')).map(([bag, contain]) => ({
        color: bag.replace('bags', "").replace('bag', "").trim(),
        contain: contain.split(',').map(p => p.replace('bags', "").replace('bag', "").replace(/[,.]/g, "").trim()).map(p => isNaN(p[0]) ? {color: p, count: 0} : {color: p.substr(2), count: +p[0]})
        }));
        function canContain(items, bag, color){
          if(bag.color == color) { return true; }
         // if(bag.color == color) { throw new Error(JSON.stringify(bag)) }
          var item = items.find(p => p.color == bag.color);
          if(item == null) { return false; }
          
          return item.contain.some(b => canContain(bags, b ,color));
        
        }
    return bags.filter(item => item.color != 'shiny gold' && item.contain.some(b => canContain(bags, b ,'shiny gold')))
        .length;
}