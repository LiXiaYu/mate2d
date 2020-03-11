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
@`testname @$ auto b=
""bbb""; $@
@`test2 @$ .next() $@
@`a ``♂ `b @$ a+=b $@
@`ass ``we `can @$ we------$@

@ 艹 @$ # include<opkopklk>

            //KKKKK 33
            ////shmm mate ddd end
            mate A<{ ddd(); }> end
            int main()
            {
                auto a = ""ddd"";
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

            var visitor = new Mate2bVisitor();
            var result = visitor.Visit(tree);

            Console.WriteLine(tree.ToStringTree(parser));
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
