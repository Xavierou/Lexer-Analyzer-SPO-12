using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tokens
{
    internal class TokenNode
    {
        public TokenNode(Token token)
        {
            Value = token;
        }
        public Token Value { get; set; }
        public TokenNode Left { get; set; }
        public TokenNode Right { get; set; }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            output.Append(Value.TokenContent);

            if (Left != null || Right != null)
            {
                output.Append("(");
                if (Left != null)
                {
                    output.Append(Left);
                }
                if (Right != null)
                {
                    output.Append(", " + Right);
                }
                output.Append(")");
            }
            return output.ToString();
        }
    }
}
