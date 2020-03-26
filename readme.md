﻿# Mate

mate2d

使用antlr

## 使用方法
mate.exe -mate test1.mate -cpp test1.cpp 

依据test1.mate文件生成test1.cpp

## 以下是计划/设计

### 语法说明

使用`@`开头

#### 替换

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

#### 使用C++代码

    @艹 @$ 这里是正常的c++ $@

### CPP中使用

在c++中使用时，请使用``` `( ```与``` )` ```将内容括起来
