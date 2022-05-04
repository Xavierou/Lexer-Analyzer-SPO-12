using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tokens
{
    internal class Token
    {
        public Token(string type, string content)
        {
            tokenType = type;
            tokenContent = content;
        }

        private string tokenType = "Invalid Type";
        private string tokenContent = "Invalid";

        public string TokenType
        {
            get { return tokenType; }
        }
        public string TokenContent
        {
            get { return tokenContent; }
        }

        public string GetTokenInfo()
        {
            return "(" + tokenType + "; " + tokenContent + ")";
        }
    }
}
