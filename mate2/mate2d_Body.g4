grammar mate2d_Body;
/*
 * 对c++代码段解析，用于替换
 */


/*
 * Parser Ruless
 */
 
body
    : word+
;

word
    : MateId
    | MateSymbols
    | CppSymbols
    | AnyWord
;

fragment MateIdTagBegin
    : '`('
;
fragment MateIdTagEnd
    : ')`'
;

MateId
    : MateIdTagBegin .*? MateIdTagEnd
;

MateSymbols
    : '`'
    | '$'
    | '@'
;

CppSymbols 
    : '+'
    | '-'
    | '*'
    | '/'
    | '='
    | '!'
    | '#'
    | '%'
    | '^'
    | '&'
    | '('
    | ')'
    | '{'
    | '}'
    | '['
    | ']'
    | '\\'
    | '/'
    | ':'
    | ';'
    | '\"'
    | '\''
    | '<'
    | '>'
    | '?'
    | ','
    | '.'
;

WS
	: ( '\t' | ' ' | '\r' | '\n' )+   -> skip
;

AnyWord
    : ~('\t'|' '|'\r'|'\n'|'+'|'-'|'*'|'/'|'='|'!'|'#'|'%'|'^'|'&'|'('|')'|'{'|'}'|'['|']'|'\\'|'/'|':'|';'|'\"'|'\''|'<'|'>'|'?'|','|'.'|'`'|'$'|'@')+
;