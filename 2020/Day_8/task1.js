
module.exports = function task({data}){
 
  let commands = data.map(p => {let command = p.split(' '); return {command: command[0], value: +command[1]}});
  var accumulator = 0;
  for(let i=0, index=0; i<commands.length;i++){
    if(commands[index].isExecuted){
      console.log(i, index, accumulator);
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

  return accumulator;
}