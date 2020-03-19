using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Antlr4.Runtime.Misc;

namespace mate2
{
    public class Mate2dVisitor : mate2dBaseVisitor<object>
    {
        /// <summary>
        /// 要被替换的cpp原文
        /// </summary>
        public Block cppBlock;
        /// <summary>
        /// 替换规则集合
        /// </summary>
        public List<Block> ruleBlocks = new List<Block>();

        Dictionary<string, object> memory = new Dictionary<string, object>();

        public override object VisitCpp([Antlr4.Runtime.Misc.NotNull] mate2dParser.CppContext context)
        {
            var body = context.Body();
            var bodyText = body.GetText();

            this.cppBlock = new Block();
            this.cppBlock.mateTags = new List<MateTag>();
            this.cppBlock.mateTags.Add(new MateTag() { text = "艹" });
            this.cppBlock.mateBody = bodyText.Substring(2, bodyText.Length - 4);

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

            Block block1 = new Block() { mateTags=new List<MateTag>(), mateBody = blockcppText };
            //把替换规则里面的符号和占位符都拿出来
            for(int i=1;i<context.children.Count-1;i++)
            {
                var id = context.children[i].GetChild(1).GetText();
                if (context.children[i].GetChild(0).GetText() == "`")
                {
                    block1.mateTags.Add(new MateNameTag() { text = id });
                }
                else if(context.children[i].GetChild(0).GetText() == "``")
                {
                    block1.mateTags.Add(new MateSymbolTag() { text = id });
                }
            }

            this.ruleBlocks.Add(block1);

            return base.VisitRule(context);
        }
    }
}
