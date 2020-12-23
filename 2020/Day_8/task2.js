
module.exports = function task({data}){
  let result = 0;
  data.forEach((p, pIndex) => {
    let commands = data.map(p => {let command = p.split(' '); return {command: command[0], value: +command[1]}});
   
    if(commands[pIndex].command == 'jmp') {
     commands[pIndex].command = 'nop';
    } else if(commands[pIndex].command == 'nop'){
     commands[pIndex].command = 'jmp';
    }
    
    let accumulator = 0;
    for(let i=0, index=0; i<commands.length;i++){
     if(index >= commands.length) {
       console.log('no loops for index', pIndex, 'accumulator=',accumulator);
       result = accumulator;
       break;
      }
     if(commands[index].isExecuted){
       break;
     } else {
       commands[index].isExecuted = true;
     }
     switch(commands[index].command)
     {
       case 'jmp':
       index += commands[index].value;
       break;
       case 'acc':
       accumulator += commands[index].value;
       index++;
       break;
       case 'nop':
       index++;
       break;
     }
   }
   });
   return result;
}