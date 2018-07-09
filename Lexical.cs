using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace LinguagensFormais
{
    class Lexical
    {
        Tokens tokens = new Tokens();

        private int line = 1;
        private int pos = 0;
        int lastIndentLevel = 0;

        private List<ReadToken> readTokens = new List<ReadToken>();
        String lexema;

        public int Line { get => line; set => line = value; }
        public int Pos { get => pos; set => pos = value; }
        public string Lexema { get => lexema; set => lexema = value; }
        internal List<ReadToken> ReadTokens { get => readTokens; set => readTokens = value; }

        public int Analysis(String filePath)
        {
            try
            {
                StreamReader sr = new StreamReader(filePath);
                {
                    String readLine;
                    ReadToken newToken;
                    while ((readLine = sr.ReadLine()) != null)
                    {
                        int spaces = 0;
                        if (readLine.Length == 0)
                            continue;

                        if (readLine[0] == '#')
                            continue;

                        while (readLine[spaces] == ' ')
                        {
                            spaces++;
                            if (spaces >= readLine.Length)
                                break;
                        }
                        if (spaces < readLine.Length)
                            if (readLine[spaces] == '#')
                                continue;

                        if (spaces % 4 != 0)
                            return 2;
                        int indent = spaces / 4;

                        int var = indent - lastIndentLevel;

                        while (var > 0)
                        {
                            newToken = new ReadToken("TK.INDENT", "Indentenção", 0, Line);
                            ReadTokens.Add(newToken);
                            var--;
                        }
                        while (var < 0)
                        {
                            newToken = new ReadToken("TK.DEDENT", "Desindentenção", 0, Line);
                            ReadTokens.Add(newToken);
                            var++;
                        }
                        lastIndentLevel = indent;

                        for (Pos = 0; Pos < readLine.Length; Pos++)
                        {
                            Char c = readLine[Pos];
                            Lexema = c.ToString();

                            if (c == ' ') continue;

                            if (c == '#')
                                break;

                            if (c == '(' || c == ')' || c == ',' || c == '{' || c == '}' ||
                                c == '~' || c == '@' || c == '[' || c == ']' || c == ':')
                            {
                                newToken = new ReadToken(tokens.TokenMap[Lexema], Lexema, Pos, Line);
                                ReadTokens.Add(newToken);
                                continue;
                            }

                            if (c == ';')
                            {
                                newToken = new ReadToken(tokens.TokenMap[Lexema], Lexema, Pos, Line);
                                ReadTokens.Add(newToken);
                                Line++;
                                continue;
                            }

                            if (c == '!')
                            {
                                Pos++;
                                if (Pos + 1 > readLine.Length)
                                {
                                    Pos--;
                                    return 1;
                                }
                                c = readLine[Pos];
                                if (c == '=')
                                {
                                    Lexema = "!=";
                                    newToken = new ReadToken(tokens.TokenMap[Lexema], Lexema, Pos, Line);
                                    ReadTokens.Add(newToken);
                                    continue;
                                }
                                else
                                {
                                    return 1;
                                }
                            }

                            if (c == '+' || c == '-' || c == '=' || c == '|' || c == '&' || c == '%' || c == '^')
                            {
                                if (Pos + 1 < readLine.Length)
                                {
                                    Char nextChar = readLine[Pos + 1];
                                    if (nextChar == '=')
                                    {
                                        Lexema = c.ToString() + "=";
                                        newToken = new ReadToken(tokens.TokenMap[Lexema], Lexema, Pos, Line);
                                        ReadTokens.Add(newToken);
                                        Pos++;
                                        continue;
                                    }
                                }
                                Lexema = c.ToString();
                                newToken = new ReadToken(tokens.TokenMap[Lexema], Lexema, Pos, Line);
                                ReadTokens.Add(newToken);
                                continue;
                            }

                            if (c == '*' || c == '/' || c == '<' || c == '>')
                            {
                                if (Pos + 1 < readLine.Length)
                                {
                                    Char nextChar = readLine[Pos + 1];
                                    if (nextChar == '=')
                                    {
                                        Lexema = c.ToString() + "=";
                                        newToken = new ReadToken(tokens.TokenMap[Lexema], Lexema, Pos, Line);
                                        ReadTokens.Add(newToken);
                                        Pos++;
                                        continue;
                                    }

                                    if (nextChar == c)
                                    {
                                        Pos++;
                                        if (Pos + 1 < readLine.Length)
                                        {
                                            nextChar = readLine[Pos + 1];
                                            if (nextChar == '=')
                                            {
                                                Lexema = c.ToString() + c.ToString() + "=";
                                                newToken = new ReadToken(tokens.TokenMap[Lexema], Lexema, Pos - 1, Line);
                                                ReadTokens.Add(newToken);
                                                Pos++;
                                                continue;
                                            }
                                            else
                                            {
                                                Lexema = c.ToString() + c.ToString();
                                                newToken = new ReadToken(tokens.TokenMap[Lexema], Lexema, Pos - 1, Line);
                                                ReadTokens.Add(newToken);
                                                continue;
                                            }
                                        }
                                    }
                                }
                                Lexema = c.ToString();
                                newToken = new ReadToken(tokens.TokenMap[Lexema], Lexema, Pos, Line);
                                ReadTokens.Add(newToken);
                                continue;
                            }

                            if (c == '.')
                            {
                                int beginPos = Pos;
                                Char nextChar;
                                while (Pos + 1 < readLine.Length)
                                {
                                    Pos++;
                                    nextChar = readLine[Pos];
                                    if (nextChar >= '0' && nextChar <= '9')
                                        Lexema = String.Concat(Lexema, nextChar.ToString());
                                    else
                                    {
                                        Pos--;
                                        break;
                                    }
                                }
                                if (lexema.Length == 1)
                                    newToken = new ReadToken(tokens.TokenMap[c.ToString()], Lexema, beginPos, Line);
                                else
                                    newToken = new ReadToken("TK.FLOAT", Lexema, beginPos, Line);
                                ReadTokens.Add(newToken);
                                continue;
                            }

                            if (c >= '1' && c <= '9')
                            {
                                int beginPos = Pos;
                                Char nextChar;
                                String thisToken = "TK.INTEGER";
                                Boolean hasADotAlready = false;
                                while (pos + 1 < readLine.Length)
                                {
                                    Pos++;
                                    nextChar = readLine[Pos];
                                    if (nextChar >= '0' && nextChar <= '9')
                                        Lexema = String.Concat(Lexema, nextChar.ToString());
                                    else if (nextChar == '.')
                                    {
                                        if (hasADotAlready)
                                            break;
                                        else
                                        {
                                            Lexema = String.Concat(Lexema, nextChar.ToString());
                                            thisToken = "TK.FLOAT";
                                        }
                                    }
                                    else
                                    {
                                        Pos--;
                                        break;
                                    }
                                }
                                newToken = new ReadToken(thisToken, Lexema, beginPos, Line);
                                ReadTokens.Add(newToken);
                            }


                            if (c == '0') //Binarios, Octal, Hexa e 0 e 0. ...
                            {
                                int beginPos = Pos;
                                Char nextChar;
                                String thisToken = "TK.INTEGER";
                                if (Pos + 1 < readLine.Length)
                                {
                                    nextChar = readLine[pos + 1];
                                    switch (nextChar)
                                    {
                                        case 'b':
                                        case 'B':
                                            Lexema = String.Concat(Lexema, nextChar.ToString());
                                            Pos++;
                                            while (Pos + 1 < readLine.Length)
                                            {
                                                Pos++;
                                                nextChar = readLine[Pos];
                                                if (nextChar == '0' || nextChar == '1')
                                                    Lexema = String.Concat(Lexema, nextChar.ToString());
                                                else
                                                {
                                                    Pos--;
                                                    break;
                                                }
                                            }
                                            break;
                                        case 'o':
                                        case 'O':
                                            Lexema = String.Concat(Lexema, nextChar.ToString());
                                            Pos++;
                                            while (Pos + 1 < readLine.Length)
                                            {
                                                Pos++;
                                                nextChar = readLine[Pos];
                                                if (nextChar >= '0' && nextChar <= '7')
                                                    Lexema = String.Concat(Lexema, nextChar.ToString());
                                                else
                                                {
                                                    Pos--;
                                                    break;
                                                }
                                            }
                                            break;
                                        case 'x':
                                        case 'X':
                                            Lexema = String.Concat(Lexema, nextChar.ToString());
                                            Pos++;
                                            while (Pos + 1 < readLine.Length)
                                            {
                                                Pos++;
                                                nextChar = readLine[Pos];
                                                if (nextChar >= '0' && nextChar <= '9' ||
                                                    nextChar >= 'a' && nextChar <= 'f' ||
                                                    nextChar >= 'A' && nextChar <= 'F')
                                                    Lexema = String.Concat(Lexema, nextChar.ToString());
                                                else
                                                {
                                                    Pos--;
                                                    break;
                                                }
                                            }
                                            break;
                                        case '.':
                                            Lexema = String.Concat(Lexema, nextChar.ToString());
                                            Pos++;
                                            thisToken = "TK.FLOAT";
                                            while (pos + 1 < readLine.Length)
                                            {
                                                Pos++;
                                                nextChar = readLine[Pos];
                                                if (nextChar >= '0' && nextChar <= '9')
                                                    Lexema = String.Concat(Lexema, nextChar.ToString());
                                                else
                                                {
                                                    Pos--;
                                                    break;
                                                }
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                if (lexema.EndsWith("b") || lexema.EndsWith("B") ||
                                    lexema.EndsWith("o") || lexema.EndsWith("O") ||
                                    lexema.EndsWith("x") || lexema.EndsWith("X"))
                                    return 1;
                                newToken = new ReadToken(thisToken, Lexema, beginPos, Line);
                                ReadTokens.Add(newToken);
                            }

                            if (c == '"' || c == '\'') //String
                            {
                                int beginPos = Pos;
                                Char nextChar;
                                while (Pos + 1 < readLine.Length)
                                {
                                    Pos++;
                                    nextChar = readLine[Pos];
                                    if (nextChar == c)
                                    {
                                        Lexema = String.Concat(Lexema, nextChar.ToString());
                                        break;
                                    }
                                    else
                                    {
                                        Lexema = String.Concat(Lexema, nextChar.ToString());
                                    }
                                }
                                if (Lexema.LastIndexOf(c) == 0)
                                    return 3;

                                newToken = new ReadToken("TK.STRING", Lexema, beginPos, Line);
                                ReadTokens.Add(newToken);
                                continue;
                            }

                            if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || c == '_') // ID ou palavra reservada
                            {
                                int beginPos = Pos;
                                if (Pos + 1 < readLine.Length)
                                {
                                    Char nextChar = readLine[Pos + 1];
                                    while ((nextChar >= 'a' && nextChar <= 'z') ||
                                            (nextChar >= 'A' && nextChar <= 'Z') ||
                                            (nextChar >= '0' && nextChar <= '9') || nextChar == '_')
                                    {
                                        Lexema = String.Concat(Lexema, nextChar.ToString());
                                        Pos++;
                                        if (Pos + 1 < readLine.Length)
                                            nextChar = readLine[Pos + 1];
                                        else
                                            nextChar = ' ';
                                    }
                                }
                                if (tokens.TokenMap.ContainsKey(Lexema))
                                    newToken = new ReadToken(tokens.TokenMap[Lexema], Lexema, beginPos, Line);
                                else
                                    newToken = new ReadToken("TK.ID", Lexema, beginPos, Line);
                                ReadTokens.Add(newToken);
                                continue;
                            }
                        }
                        Line++;
                    }
                }
                int listLength = readTokens.Count;
                int lastPos = ReadTokens[listLength-1].Coluna + 1;
                int lastLine = ReadTokens[listLength-1].Linha;
                ReadToken finalToken = new ReadToken("TK.EOF", "$", lastPos, lastLine);
                ReadTokens.Add(finalToken);
                return 0;
            }
            catch
            {
                return -1;
            }
        }
    }
}
