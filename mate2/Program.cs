using System;
using Antlr4.Runtime;

namespace mate2
{
    class Program
    {
        static void Main(string[] args)
        {
            //string input = @"#include <stdio.h>
            //#define MDPEKLF\
            //KKKKK 33
            ////shmmm
            //mate AAA
            //<{
            //        string aa=""888jjjj"";
            //}>
            //endmate
            //int main(){
            //auto a=""ddd"";
            //return
            //0;
            //}";
            string input = @"
@``testname @$ auto b=
""bbb""; $@
@``test2 @$ .next() $@
@`a ``♂ `b @$ `a += `b $@
@``ass `we ``can @$ we------$@

@ 艹 @$ # include<opkopklk>

            //KKKKK 33
            ////shmm mate ddd end
            mate A<{ ddd(); }> end
            int main()
            {
                auto a = ""ddd"";
                int b=20;
                auto c=`(foo<std::string>(a+a))`♂`(b)`;
                return
                0;
            } $@";


//# include<opkopklk>

//            //KKKKK 33
//            ////shmm mate ddd end
//            mate A<{ ddd(); }> end
//            int main()
//            {
//                auto a = ""ddd"";
//                return
//                0;
//            }

            var stream = new AntlrInputStream(input);
            var lexer = new mate2dLexer(stream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new mate2dParser(tokens);
            var tree = parser.program();

            var visitor = new Mate2dVisitor();
            var result = visitor.Visit(tree);

            Console.WriteLine(tree.ToStringTree(parser));
            Console.WriteLine(result);


            var cpps = new AntlrInputStream(visitor.cppBlock.mateBody);
            var cppl = new mate2d_BodyLexer(cpps);
            var cppt = new CommonTokenStream(cppl);
            var cppp = new mate2d_BodyParser(cppt);
            var cpptree = cppp.body();

            var cppv = new mate2d_BodyVisitor();
            var cppr = cppv.Visit(cpptree);

            Console.WriteLine(cpptree.ToStringTree(cppp));
            Console.WriteLine(cppr);
            
            
            // 使用获得的替换规则，对cpp进行替换
            // TODO: 待完成
            // TODO: 问题是空格丢了
        }

    }
}
