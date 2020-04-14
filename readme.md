# Mate

mate2d

使用antlr


### 语法说明

使用`@`开头

#### 替换规则

    @ [macro] @$ [cpp-code] $@

`marco`中一个反引号（``` ` ```） 后面跟的是会被替换掉的名字，两个反引号（``` `` ```）后面跟的是会被用来识别的符号，当成运算符
`@$ [cpp-code] $@`中的`[cpp-code]`用于替换将要被替换的部分`[macro]`

--------

例如:

    @`a ``♂ `b @$ `a += `b $@
    auto c=`(foo<std::string>(a+a))`♂`(b)`;

被替换为

```cpp
auto c=foo<std::string>(a+a) += b;
```

### 使用C++代码

    c++代码被写在*.mate文件中

#### CPP中使用

在c++中使用时，请使用``` `( ```与``` )` ```将内容括起来


### mate2.exe

    命令行帮助使用-help

    -mate后面跟mate文件
    -matelib后面跟替换规则所在的文件，可以跟多个，将会被合并起来一起作用于mate文件的替换
    -cpp后面跟要生成的cpp文件名，不加这个参数的话，会在-mate文件名字后面加.cpp
        
例如：
    -mate test1.mate -matelib test1_0.matelib test1_1.matelib -cpp test1.cpp