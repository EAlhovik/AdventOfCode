const _ = require('../lib/lodash.min.js');

module.exports = function task({ data }) {
    let food = data.map(item => ({
        ingredients: item.split(' (contains ')[0],
        allergens: item.split(' (contains ')[1]
    })).map(item => ({
        ingredients: item.ingredients.split(' '),
        allergens: item.allergens.slice(0,-1).split(', ')
    }));
    let allergens = _.uniq(_.flatten(food.map(p => p.allergens)));
    let ingredientsWithoutAllergens = [];
    let ingredientsToIgnore = [];

    allergens.forEach(allergen => {
        let i = food.filter(item => item.allergens.indexOf(allergen) >= 0)
        .map(p => p.ingredients);
        let groups = i.length;
        let ingredients = _.flatten(i)
            .reduce((a, c) => (a[c] = (a[c] || 0) + 1, a),{});

            Object.keys(ingredients)
                .forEach(p => {
                    if(ingredients[p] < groups){
                        if(ingredientsWithoutAllergens.indexOf(p) < 0
                            && ingredientsToIgnore.indexOf(p) < 0){
                            ingredientsWithoutAllergens.push(p);
                        }
                    } else {
                        ingredientsToIgnore.push(p);
                        let index = ingredientsWithoutAllergens.indexOf(p);
                        if(index >= 0){
                            ingredientsWithoutAllergens.splice(index, 1);
                        }
                    }
                });
    });

    let allIngredients = _.flatten(food.map(p => p.ingredients))
        .reduce((a, c) => (a[c] = (a[c] || 0) + 1, a),{});

    let ingredientsWithAllergens = _.flatten(allergens.map(allergen => {
        let ingredients = _.uniq(_.flatten(food.filter(item => item.allergens.indexOf(allergen) >= 0)
            .map(p => p.ingredients)))
            .filter(p => ingredientsWithoutAllergens.indexOf(p) < 0);
        return {
            allergen,
            ingredients
        }
    })
    .map(p => p.ingredients.map(i => ({allergen: p.allergen, ingredient: i}))));

    return food.map(p => {
        return {
            allergens: p.allergens,
            ingredients: p.ingredients.filter(i => ingredientsWithoutAllergens.indexOf(i) < 0 )
        }
    });
}
