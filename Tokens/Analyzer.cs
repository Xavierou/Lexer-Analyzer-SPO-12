using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tokens
{
    internal class Analyzer
    {
        private Token token;
        private Lexer lex;

        public TokenNode Analyze(string input)
        {
            lex = new Lexer(input);
            token = lex.getNextToken();
            return Solve();
        }

        private TokenNode Solve()
        {
            TokenNode node = Expr();
            EOF();
            return node;
        }

        private void EOF()
        {
            if(!token.TokenType.Equals("EoF"))
            {
                throw new ArgumentException("Expected EOF, unexpected " + token.GetTokenInfo());
            }
        }

        private TokenNode Expr()
        {
            TokenNode fstNode = Term();
            TokenNode sndNode = Expr_();
            if (sndNode != null && sndNode.Value.TokenType.Equals("Operator"))
            {
                sndNode.Left = fstNode;
                fstNode = sndNode;
            }
            return fstNode;
        }

        private TokenNode Expr_()
        {
            TokenNode node;
            switch (token.TokenContent)
            {
                case "+":
                case "-":
                    node = new TokenNode(token);
                    token = lex.getNextToken();
                    node.Right = Expr();
                    break;
                default:
                    node = null;
                    break;
            }
            return node;
        }

        private TokenNode Term()
        {
            TokenNode fstNode = Fac();
            TokenNode sndNode = Term_();
            if (sndNode != null && sndNode.Value.TokenType.Equals("Operator"))
            {
                sndNode.Left = fstNode;
                fstNode = sndNode;
            }
            return fstNode;
        }

        private TokenNode Term_()
        {
            TokenNode node;
            switch (token.TokenContent)
            {
                case "*":
                case "/":
                    node = new TokenNode(token);
                    token = lex.getNextToken();
                    node.Right = Term();
                    break;
                default:
                    node = null;
                    break;
            }
            return node;
        }

        private TokenNode Fac()
        {
            TokenNode fstNode = Val();
            TokenNode sndNode = Fac_();
            if (sndNode != null && sndNode.Value.TokenType.Equals("Operator"))
            {
                sndNode.Left = fstNode;
                fstNode = sndNode;
            }
            return fstNode;
        }

        private TokenNode Fac_()
        {
            TokenNode node;
            switch (token.TokenContent)
            {
                case "^":
                    node = new TokenNode(token);
                    token = lex.getNextToken();
                    node.Right = Fac();
                    break;
                default:
                    node = null;
                    break;
            }
            return node;
        }

        private TokenNode Val()
        {
            TokenNode node;
            switch (token.TokenType)
            {
                case "LParen":
                    token = lex.getNextToken();
                    node = Expr();
                    token = lex.getNextToken();
                    break;
                case "ID":
                case "Number":
                    node = new TokenNode(token);
                    token = lex.getNextToken();
                    break;
                case "Operator":
                    node = new TokenNode(token);
                    token = lex.getNextToken();
                    node.Left = Val();
                    break;
                default:
                    throw new ArgumentException("Unexpected terminal: " + token.GetTokenInfo());
            }
            return node;
        }

        public static double ComputeTree(TokenNode tree)
        {
            double output = 0;
            double left = 0;
            double right = 0;
            if (tree.Left != null)
            {
                left = ComputeTree(tree.Left);
            }
            else
            {
                output = double.Parse(tree.Value.TokenContent.Replace('.', ','));
            }

            if (tree.Right != null)
            {
                right = ComputeTree(tree.Right);
            }

            if (tree.Value.TokenType.Equals("Operator"))
            {
                switch (tree.Value.TokenContent)
                {
                    case "+":
                        output = left + right;
                        break;
                    case "-":
                        if (tree.Right != null)
                        {
                            output = left - right;
                        }
                        else
                        {
                            output = -left;
                        }
                        break;
                    case "*":
                        output = left * right;
                        break;
                    case "/":
                        output = left / right;
                        break;
                    case "^":
                        output = Math.Pow(left, right);
                        break;
                    default:
                        throw new ArgumentException("Bad tree: " + tree + " at token " + tree.Value.GetTokenInfo());
                }
            }

            return output;
        }
    }
}
