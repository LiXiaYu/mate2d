using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Antlr4.Runtime.Misc;

namespace mate2
{
    public class Mate2bVisitor : mate2dBaseVisitor<object>
    {
        Dictionary<string, object> memory = new Dictionary<string, object>();

        public override object VisitBlock([Antlr4.Runtime.Misc.NotNull] mate2dParser.BlockContext context)
        {
            return base.VisitBlock(context);
        }

        public override object VisitBlockname([Antlr4.Runtime.Misc.NotNull] mate2dParser.BlocknameContext context)
        {
            return base.VisitBlockname(context);
        }

        public override object VisitProgram([Antlr4.Runtime.Misc.NotNull] mate2dParser.ProgramContext context)
        {
            return base.VisitProgram(context);
        }
    }
}
