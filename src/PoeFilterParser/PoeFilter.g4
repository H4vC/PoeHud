grammar PoeFilter;

COMMENT: '#'.+?'\n' -> skip;
WS:    [ \t\r\n] -> skip ;
DIGITS: [0-9]+;
COMPAREOP: '='| '<'|'>'|'<='|'>=';
RARITY: 'Normal'| 'Magic'| 'Rare' | 'Unique';
SHOW: 'Show';
HIDE: 'Hide';
SOCKET: [RGBW]+ ;
QUOTESOCKET:'"' SOCKET '"';
QUOTERARITY:'"' RARITY '"';
STR: .+? | ~[\r\n "]+;
QUOTESTR:'"' STR '"';

strValue: QUOTESTR | STR;
socketValue: QUOTESOCKET | SOCKET;
rarityValue: QUOTERARITY | RARITY;

compareOpNullable:COMPAREOP?;
visibility: SHOW | HIDE;
params:strValue (strValue)*;
digitsParams: DIGITS (DIGITS)*;
rariryParams: rarityValue (rarityValue)*;
socketParams: socketValue (socketValue)*;
red:DIGITS;
green:DIGITS;
blue:DIGITS;
alpha:DIGITS?;
color: red green blue alpha;
poeClass: 'Class' params;
poeBaseType: 'BaseType' params;
poeFontSize: 'SetFontSize' DIGITS;
poeTextColor: 'SetTextColor' color;
poeBackgroundColor: 'SetBackgroundColor' color;
poeBorderColor: 'SetBorderColor' color;
poeAlertSound: 'PlayAlertSound' DIGITS DIGITS;

poeRarity: 'Rarity' compareOpNullable rariryParams;
poeSocketGroup: 'SocketGroup' socketParams;
poeLinkedSockets: 'LinkedSockets' compareOpNullable digitsParams;
poeSockets: 'Sockets' compareOpNullable digitsParams;
poeItemLevel: 'ItemLevel' compareOpNullable digitsParams;
poeDropLevel: 'DropLevel' compareOpNullable digitsParams;
poeQuality: 'Quality' compareOpNullable digitsParams;
poeWidth: 'Width' compareOpNullable digitsParams;
poeHeight: 'Height' compareOpNullable digitsParams;

statement:  poeClass
         |  poeFontSize 
         |  poeBackgroundColor
         |  poeBorderColor
         |  poeAlertSound
         |  poeSocketGroup
         |  poeRarity
         |  poeSockets
         |  poeItemLevel
         |  poeDropLevel
         |  poeLinkedSockets
         |  poeQuality
         |  poeBaseType
         |  poeWidth
         |  poeHeight
         |  poeTextColor;

block: visibility statement*?;
main: block+;
