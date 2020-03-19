
using Antlr4.Runtime.Misc;

namespace mate2
{
    public class mate2d_BodyVisitor : mate2d_BodyBaseVisitor<object>
    {
        public override object VisitMateId([NotNull] mate2d_BodyParser.MateIdContext context)
        {
            return base.VisitMateId(context);
        }

        public override object VisitMateSymbol([NotNull] mate2d_BodyParser.MateSymbolContext context)
        {
            return base.VisitMateSymbol(context);
        }
    }
}