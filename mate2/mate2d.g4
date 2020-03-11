grammar mate2d;
//主要是设计语法
//做一个宏替换器

/*
 * Parser Ruless
 */
 
program 
	: block+
;  

block
	: Block CppTag Body
	| Block (mateName | mateSymbol)+? Body
;

mateName
	: MateNameTag BlockId
;

mateSymbol
	: MateSymbolTag BlockId
;

blockname
	: BlockID
;

CppTag
	: '艹'
;

Block
	: '@'
;

MateNameTag
	: '`'
;

MateSymbolTag
	: '``'
;

BlockID
	: ~['@'|'`'|'$'|'\t'|' '|'\r'|'\n']+
;


/*
fragment Begin
	: '@$'
;
fragment End
	: '$@'
;
*/

Body
	: '@$' .*? '$@'
;



WS
	: ( '\t' | ' ' | '\r' | '\n' )+   -> skip
;

