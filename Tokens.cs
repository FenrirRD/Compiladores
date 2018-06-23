using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguagensFormais
{
    class Tokens
    {
        private Dictionary<String, String> tokenMap;

        public Tokens()
        {
            TokenMap = new Dictionary<String, String>();
            LoadTokens();
        }

        public Dictionary<String, String> TokenMap { get => tokenMap; set => tokenMap = value; }

        private void LoadTokens()
        {
            //Tokens de Controle
            TokenMap.Add("indent", "TK.INDENT");
            TokenMap.Add("dedent", "TK.DEDENT");

            //Tipos
            TokenMap.Add("string", "TK.STRING");
            TokenMap.Add("float", "TK.FLOAT");
            TokenMap.Add("integer", "TK.INTEGER");

            //Palavras reservadas
            tokenMap.Add("identf", "TK.ID");
            TokenMap.Add("and", "TK.AND");
            TokenMap.Add("del", "TK.DEL");
            TokenMap.Add("from", "TK.FROM");
            TokenMap.Add("not", "TK.NOT");
            TokenMap.Add("while", "TK.WHILE");
            TokenMap.Add("as", "TK.AS");
            TokenMap.Add("elif", "TK.ELIF");
            TokenMap.Add("global", "TK.GLOBAL");
            TokenMap.Add("or", "TK.OR");
            TokenMap.Add("with", "TK.WITH");
            TokenMap.Add("assert", "TK.ASSERT");
            TokenMap.Add("else", "TK.ELSE");
            TokenMap.Add("if", "TK.IF");
            TokenMap.Add("pass", "TK.PASS");
            TokenMap.Add("yield", "TK.YIELD");
            TokenMap.Add("break", "TK.BREAK");
            TokenMap.Add("except", "TK.EXCEPT");
            TokenMap.Add("import", "TK.IMPORT");
            TokenMap.Add("print", "TK.PRINT");
            TokenMap.Add("class", "TK.CLASS");
            TokenMap.Add("exec", "TK.EXEC");
            TokenMap.Add("in", "TK.IN");
            TokenMap.Add("raise", "TK.RAISE");
            TokenMap.Add("continue", "TK.CONTINUE");
            TokenMap.Add("finally", "TK.FINALLY");
            TokenMap.Add("is", "TK.IS");
            TokenMap.Add("return", "TK.RETURN");
            TokenMap.Add("def", "TK.DEF");
            TokenMap.Add("for", "TK.FOR");
            TokenMap.Add("lambda", "TK.LAMBDA");
            TokenMap.Add("try", "TK.TRY");
            TokenMap.Add("none", "TK.NONE");
            TokenMap.Add("nonlocal", "TK.NONLOCAL");
            TokenMap.Add("range", "TK.RANGE");

            //Operadores
            TokenMap.Add("(", "TK.LEFTPAR");
            TokenMap.Add(")", "TK.RIGHTPAR");
            TokenMap.Add(",", "TK.COMMA");
            TokenMap.Add(".", "TK.DOT");
            TokenMap.Add("'", "TK.BACKQUOTE");
            TokenMap.Add("{", "TK.LEFTBRAC");
            TokenMap.Add("}", "TK.RIGHTBRAC");
            TokenMap.Add("~", "TK.TILDE");
            TokenMap.Add("@", "TK.AT");
            TokenMap.Add("!=", "TK.NOTEQUAL");
            TokenMap.Add("[", "TK.LEFTSQB");
            TokenMap.Add("]", "TK.RIGHTSQB");
            TokenMap.Add(":", "TK.COLON");
            TokenMap.Add(";", "TK.SEMICOLON");
            TokenMap.Add("+", "TK.PLUS");
            TokenMap.Add("+=", "TK.PLUSEQUAL");
            TokenMap.Add("-", "TK.MINUS");
            TokenMap.Add("-=", "TK.MINEQUAL");
            TokenMap.Add("*", "TK.STAR");
            TokenMap.Add("*=", "TK.STAREQUAL");
            TokenMap.Add("**", "TK.DOUBLESTAR");
            TokenMap.Add("**=", "TK.DOUBLESTAREQUAL");
            TokenMap.Add("/", "TK.SLASH");
            TokenMap.Add("/=", "TK.SLASHEQUAL");
            TokenMap.Add("//", "TK.DOUBLESLASH");
            TokenMap.Add("//=", "TK.DOUBLESLASHEQUAL");
            TokenMap.Add("|", "TK.VERTBAR");
            TokenMap.Add("|=", "TK.VERTBAREQUAL");
            TokenMap.Add("&", "TK.AMPER");
            TokenMap.Add("&=", "TK.AMPEREQUAL");
            TokenMap.Add("<", "TK.LESS");
            TokenMap.Add("<=", "TK.LESSEQUAL");
            TokenMap.Add("<<", "TK.SHIFTLEFT");
            TokenMap.Add("<<=", "TK.SHIFTLEFTEQUAL");
            TokenMap.Add(">", "TK.GREATER");
            TokenMap.Add(">=", "TK.GREATEREQUAL");
            TokenMap.Add(">>", "TK.SHIFTRIGHT");
            TokenMap.Add(">>=", "TK.SHIFTRIGHTEQUAL");
            TokenMap.Add("=", "TK.EQUAL");
            TokenMap.Add("==", "TK.EQUALEQUAL");
            TokenMap.Add("%", "TK.PERCENT");
            TokenMap.Add("%=", "TK.PERCENTEQUAL");
            TokenMap.Add("^", "TK.CIRCUMFLEX");
            TokenMap.Add("^=", "TK.CIRCUMFLEXEQUAL");
        }
    }
}
