using System;
using System.Collections.Generic;
using System.Text;

namespace mate2
{
    public class Block
    {
        public List<MateTag> mateTags;
        public string mateBody;
    }

    public class MateTag
    {
        public string text;
    }

    public class MateNameTag : MateTag
    {

    }

    public class MateSymbolTag:MateTag
    {

    }
}
