grammar mate2d;

/*
 * Parser Ruless
 */
 
program 
	: block+
;  

block
	: Block blockname Body
	| Block CppTag Body
;


blockname
	: BlockID
;

CppTag
	: '艹'
;

BlockID
	: [a-zA-Z0-9|'_']+
;

Block
	: '`'
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

