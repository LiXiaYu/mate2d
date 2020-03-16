using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Antlr4.Runtime.Misc;

namespace mate2
{
    public class Mate2dVisitor : mate2dBaseVisitor<object>
    {

        public string cppText;

        Dictionary<string, object> memory = new Dictionary<string, object>();

        public override object VisitCpp([Antlr4.Runtime.Misc.NotNull] mate2dParser.CppContext context)
        {
            var body = context.Body();
            var bodyText = body.GetText();
            this.cppText = bodyText.Substring(2, bodyText.Length - 4);

            return base.VisitCpp(context);
        }

        public override object VisitProgram([Antlr4.Runtime.Misc.NotNull] mate2dParser.ProgramContext context)
        {
            return base.VisitProgram(context);
        }

        public override object VisitRule([Antlr4.Runtime.Misc.NotNull] mate2dParser.RuleContext context)
        {
            var block = context.children[context.children.Count - 1];
            var blockText = block.GetText();
            var blockcppText = blockText.Substring(2, blockText.Length - 4);

            var names =new List<string>();
            //通过拼接正则表达式完成替换
            string exp = ""; 
            for(int i=1;i<context.children.Count-1;i++)
            {
                var id = context.children[i].GetChild(1).GetText();
                if (context.children[i].GetChild(0).GetText() == "`")
                {
                    names.Add("`" + id);
                    exp += @"`\(.*?\)`";
                }
                else if(context.children[i].GetChild(0).GetText() == "``")
                {
                    exp += id;
                }


                if(i<context.children.Count-2)
                {
                    exp += @"\s*?";
                }
            }

            return base.VisitRule(context);
        }
    }
}
