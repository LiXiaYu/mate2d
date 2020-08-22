using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using PowerArgs;

namespace mate2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var parsed = Args.Parse<MyArgs>(args);

                var mateFilePath = parsed.MateFilePath;
                var cppFilePath = string.IsNullOrEmpty(parsed.CppFilePath)?(parsed.MateFilePath+".cpp"): parsed.CppFilePath;

                var mateFile = File.ReadAllText(mateFilePath);

                var mateLibFile = string.Concat(parsed.MateLibFilePaths.Select(p => File.ReadAllText(p)).ToList().Select(p=>p+"\n").ToList());

                Mate2dVisitor visitor = MateRuleVisit(mateLibFile);

                //Console.WriteLine(tree.ToStringTree(parser));
                //Console.WriteLine(result);


                mate2d_BodyParser.BodyContext cpptree = MateBodyVisit(mateFile);

                //Console.WriteLine(cpptree.ToStringTree(cppp));
                //Console.WriteLine(cppr);


                // 使用获得的替换规则，对cpp进行替换
                var rules = visitor.ruleBlocks;
                var text_tokens = cpptree.children.Select(p => p.GetText()).ToList();

                text_tokens = Replace(rules, text_tokens);

                //结束替换

                string s = "";
                foreach (var t in text_tokens)
                {
                    s += t;
                }

                File.WriteAllText(cppFilePath, s);

            }
            catch (ArgException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ArgUsage.GenerateUsageFromTemplate<MyArgs>());
            }




            //
        }

        private static mate2d_BodyParser.BodyContext MateBodyVisit(string input)
        {
            var cpps = new AntlrInputStream(input);
            var cppl = new mate2d_BodyLexer(cpps);
            var cppt = new CommonTokenStream(cppl);
            var cppp = new mate2d_BodyParser(cppt);
            var cpptree = cppp.body();

            var cppv = new mate2d_BodyVisitor();
            var cppr = cppv.Visit(cpptree);
            return cpptree;
        }

        private static Mate2dVisitor MateRuleVisit(string input)
        {
            var stream = new AntlrInputStream(input);
            var lexer = new mate2dLexer(stream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new mate2dParser(tokens);
            var tree = parser.program();

            var visitor = new Mate2dVisitor();
            var result = visitor.Visit(tree);
            return visitor;
        }

        private static List<string> Replace(List<Block> rules, List<string> text_tokens)
        {
            foreach (var rule in rules)
            {

                List<string> texts = new List<string>(text_tokens);

                List<MateTag> tags = new List<MateTag>(rule.mateTags);

                for (int i = 0; i < text_tokens.Count; i++)
                {
                    int index_tags = 0;

                    int counter_tag = 0;
                    int count_now_text = texts.Count;

                    int j = i;
                    try
                    {
                        string replaced = rule.mateBody;

                        int index_text_start_tag = -1;
                        int index_text_stop_tag = -1;


                        for (j = i; j < text_tokens.Count; j++)
                        {
                            var tokentext = text_tokens[j];

                            if (string.IsNullOrWhiteSpace(tokentext))//这个应该用IParseTree的类型判断?但是第二遍就没有了
                            {
                                //texts.Add(tokentext);
                                continue;
                            }

                            if (tags[index_tags] is MateNameTag)
                            {
                                if (index_text_start_tag == -1)
                                {
                                    index_text_start_tag = j;
                                }
                                //texts.Add(tokentext);



                                //用于替换
                                string retokentext = tokentext;
                                if (tokentext[0] == '`' && tokentext[1] == '(' && tokentext[tokentext.Length - 2] == ')' && tokentext[tokentext.Length - 1] == '`')
                                {
                                    retokentext = tokentext.Substring(2, tokentext.Length - 4);
                                }
                                replaced = replaced.Replace("`" + tags[index_tags].text, retokentext);



                                counter_tag++;
                                index_tags++;
                            }
                            else if (tags[index_tags] is MateSymbolTag)
                            {
                                if (tokentext == tags[index_tags].text)
                                {
                                    if (index_text_start_tag == -1)
                                    {
                                        index_text_start_tag = j;
                                    }

                                    //texts.Add(tokentext);
                                    counter_tag++;
                                    index_tags++;
                                }
                                else//如果Symbol匹配不上的话，就说明匹配失败，结束匹配
                                {
                                    //texts.Add(tokentext);
                                    throw new FailedMatchingException();
                                }
                            }


                            if (index_tags >= tags.Count)
                            {
                                break;
                            }
                        }

                        if (counter_tag != tags.Count)
                        {
                            throw new FailedMatchingException();
                        }

                        //完全没有问题了，完全匹配上了：

                        index_text_stop_tag = j;


                        index_text_start_tag -= text_tokens.Count - texts.Count;
                        index_text_stop_tag -= text_tokens.Count - texts.Count;
                        texts.RemoveRange(index_text_start_tag, index_text_stop_tag - index_text_start_tag + 1);
                        texts.Insert(index_text_start_tag, replaced);
                        index_tags = 0;

                        if(index_text_start_tag==index_text_stop_tag)
                        {
                            j++;//单独只替换一个时
                        }
                        //j += index_text_stop_tag - index_text_start_tag + 1;
                        //texts.Add(replaced);
                    }
                    catch (FailedMatchingException e)
                    {
                        j = i + 1;
                        index_tags = 0;
                    }

                    i = j - 1;
                }

                text_tokens = texts;

            }

            return text_tokens;
        }
    }
}
