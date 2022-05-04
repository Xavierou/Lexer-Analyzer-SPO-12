using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tokens
{
    internal class Lexer
    {
        private string input = string.Empty;
        private int curIndex = 0;
        public Lexer(string input)
        {
            this.input = input;
        }

        /// <summary>
        /// Функция, определяющая переходы по таблице переходов
        /// </summary>
        /// <param name="ch">Текущий символ</param>
        /// <param name="state">Текущее состояние анализатора</param>
        /// <returns>Состояние, в которое должен перейти анализатор</returns>
        private int transTable(char ch, int state)
        {
            switch (state)
            {
                case 0:
                    if (ch >= '0' && ch <= '9')
                    {
                        return 1;
                    }
                    else if (ch == '+' || ch == '*' || ch == '/' || ch == '-' || ch == '^')
                    {
                        return 6;
                    }
                    else if ((ch >= 'a' && ch <= 'z') || ch == '_')
                    {
                        return 7;
                    }
                    else if (ch == '(')
                    {
                        return 8;
                    }
                    else if (ch == ')')
                    {
                        return 9;
                    }
                    else if (ch == ',')
                    {
                        return 10;
                    }
                    else if (ch == ' ' || ch == '\t' || ch == '\n' || ch == '\r')
                    {
                        return 0;
                    }
                    break;
                case 1:
                    if (ch >= '0' && ch <= '9')
                    {
                        return 1;
                    }
                    else if (ch == '.')
                    {
                        return 2;
                    }
                    else if (ch == 'e' || ch == 'E')
                    {
                        return 3;
                    }
                    break;
                case 2:
                    if (ch >= '0' && ch <= '9')
                    {
                        return 2;
                    }
                    else if (ch == 'e' || ch == 'E')
                    {
                        return 3;
                    }
                    break;
                case 3:
                    if (ch >= '0' && ch <= '9')
                    {
                        return 5;
                    }
                    else if (ch == '+' || ch == '-')
                    {
                        return 4;
                    }
                    break;
                case 4:
                    if (ch >= '0' && ch <= '9')
                    {
                        return 5;
                    }
                    break;
                case 5:
                    if (ch >= '0' && ch <= '9')
                    {
                        return 5;
                    }
                    break;
                case 6:
                    break;
                case 7:
                    if ((ch >= 'a' && ch <= 'z') || (ch >= '0' && ch <= '9') || ch == '_')
                    {
                        return 7;
                    }
                    break;
                default:
                    return -1;
            }
            return -1;
        }

        /// <summary>
        /// Функция, обрабатывающая пользовательский ввод и представляющая его в виде токенов
        /// </summary>
        /// <returns>Токен из пользовательского ввода</returns>
        public Token getNextToken()
        {
            input = input.Trim();
            int curState = 0;
            int lastIndex = curIndex;

            if (lastIndex == input.Length)
            {
               return new Token("EoF", "Position " + lastIndex);
            }

            string content = "";
            string buf = string.Empty;
            string type = "";

            while (curState != -1)
            {
                if (curState == 1 || curState == 2 || curState == 5)
                {
                    type = "Number";
                    content = buf;
                    lastIndex = curIndex;
                }
                else if (curState == 6)
                {
                    type = "Operator";
                    content = buf;
                    lastIndex = curIndex;
                }
                else if (curState == 7)
                {
                    type = "ID";
                    content = buf;
                    lastIndex = curIndex;
                }
                else if (curState == 8)
                {
                    type = "LParen";
                    content = buf;
                    lastIndex = curIndex;
                }
                else if (curState == 9)
                {
                    type = "RParen";
                    content = buf;
                    lastIndex = curIndex;
                }
                else if (curState == 10)
                {
                    type = "Comma";
                    content = buf;
                    lastIndex = curIndex;
                }
                if (curIndex >= input.Length)
                {
                    break;
                }

                char ch = input[curIndex];

                curIndex++;
                curState = transTable(ch, curState);

                if (curState > 0)
                {
                    buf += ch;
                }
            }

            curIndex = lastIndex;
            return new Token(type, content);
        }
    }
}
