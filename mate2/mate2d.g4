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
	: Block CppTag Body # cpp
	| Block (mateName | mateSymbol)+? Body # rule
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

fragment InBodyOptValueBegin
	: '`('
;
fragment InBodyOptValueEnd
	: ')`'
;

/*
InBodyOptValue
	: InBodyOptValueBegin .*? InBodyOptValueEnd
;
*/


fragment Begin
	: '@$'
;
fragment End
	: '$@'
;


Body
	: Begin .*? End
;



WS
	: ( '\t' | ' ' | '\r' | '\n' )+   -> skip
;

