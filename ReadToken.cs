using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LinguagensFormais
{
    class ReadToken
    {
        static int nextId;

        private int sequence;
        private String token;
        private String lexema;
        private int line;
        private int column;

        public static void ResetReadToken()
        {
            nextId = 0;
        }
        
        public ReadToken(String token, String lexema, int column, int line)
        {
            this.Sequencia = Interlocked.Increment(ref nextId);
            this.Token = token;
            this.Lexema = lexema;
            this.Coluna = column;
            this.Linha = line;
        }

        public int Sequencia { get => sequence; set => sequence = value; }
        public string Token { get => token; set => token = value; }
        public string Lexema { get => lexema; set => lexema = value; }
        public int Linha { get => line; set => line = value; }
        public int Coluna { get => column; set => column = value; }
    }
}
