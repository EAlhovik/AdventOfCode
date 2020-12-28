const _ = require('../lib/lodash.min.js');

function createListItem(array) {
    let head = null;
    let tail = null;
    for (let index = 0; index < array.length; index++) {
        let item = {
            val: array[index]
        }
        if (tail != null) {
            tail.next = item;
            item.prev = tail
        }
        tail = item;
        if (index == 0) head = item;
    }
    head.prev = tail;
    tail.next = head;

    return head;
}

function pickUp(labeling, current, n = 1) {
    let currentIndex = labeling.findIndex(p => p == current);
    let pickUpIndex = currentIndex + 1 == labeling.length
        ? 0
        : currentIndex + 1;
    let result = labeling.splice(pickUpIndex, n);
    return result.concat(labeling.splice(0, n - result.length))
}

function getMap(head) {
    let map = {};
    let current = head;
    do {
        map[current.val] = current;
        current = current.next;
    } while (current !== head);
    return map;
}
function getArrayFromList(head) {
    let current = head;
    let labelingResult = [];
    do {
        labelingResult.push(current.val);
        current = current.next;
    } while (current !== head);
    return labelingResult;
}

module.exports = function task({ data }) {
    let labeling = data[0].split('').map(p => +p);
    let maxValue = Math.max.apply(Math, labeling);
    Array.from({ length: 1000000 }, (v, i) => i + 1)
        .filter(p => p > maxValue)
        .forEach(p => labeling.push(p));
    maxValue = 1000000;

    let head = createListItem(labeling);
    let map = getMap(head);
    let current = head;
    for (let i = 0; i < 10000000; i++) {
        let pickUp1 = current.next;
        let pickUp2 = pickUp1.next;
        let pickUp3 = pickUp2.next;

        let forwardDestination = pickUp3.next;

        current.next = forwardDestination;
        pickUp1.prev = null;

        forwardDestination.prev = current;
        pickUp3.next = null;

        let destinationVal = [
            current.val - 1,
            current.val - 2,
            current.val - 3,
            current.val - 4]
            .map(val => val < 1 ? maxValue + val : val)
            .find(p => p != pickUp1.val && p != pickUp2.val && p != pickUp3.val);

        current = current.next;
        let destination = map[destinationVal];
        pickUp3.next = destination.next;
        destination.next.prev = pickUp3;

        destination.next = pickUp1;
        pickUp1.prev = destination;
    }

    return pickUp(getArrayFromList(head), 1, 2).reduce((a, b) => a * b);
}