using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinguagensFormais
{
    class Syntactic
    {
        int index;
        int numlabel = 0;
        int numtemp = 0;
        String presentToken;
        List<ReadToken> tokenList;
        Boolean elif;
        String codigo;

        public Syntactic(List<ReadToken> tokenList)
        {
            this.tokenList = tokenList;
            this.presentToken = tokenList[0].Token;
            this.index = 0;
            this.elif = false;
            this.codigo = "";
        }

        public ReadToken ErroToken()
        {
            index--;
            return tokenList[index];
        }

        public Boolean Analysis()
        {
            if (Start())
                return true;
            else
                return false;
        }

        private void nextToken()
        {
            this.index++;
            if (index < tokenList.Count)
                this.presentToken = tokenList[index].Token;
            else
                this.presentToken = null;
        }

        private void backToken()
        {
            this.index--;
            this.presentToken = tokenList[index].Token;
        }

        private void geralabel(ref String label)
        {
            label = String.Format("LB{0:000}", numlabel++);
        }

        private void geratemp(ref string temp)
        {
           temp = String.Format("T{0:000}", numtemp++);
        }

        // Start -> DEF ID (PARAM_LIST) : INDENT PROGRAM EOF 

        // Start -> DEF ID PARAM_LIST : INDENT PROGRAM EOF
        private Boolean Start()
        {
            string Pc = "", Ac = "", Ap = "";
            if (presentToken == "TK.DEF")
            {
                nextToken();
                if (presentToken == "TK.ID")
                {
                    nextToken();
                    if (Param_List())
                    {
                        nextToken();
                        if (presentToken == "TK.COLON")
                        {
                            nextToken();
                            if (presentToken == "TK.INDENT")
                            {
                                numtemp = 0;
                                //nextToken();
                                //while (presentToken != "TK.EOF")
                                //{
                                    if(!Program(ref Pc))
                                    {
                                        MessageBox.Show("Erro no comando:\n Token: " + tokenList[index].Token + "\nLexema: " + tokenList[index].Lexema);
                                    }
                                    MessageBox.Show(Pc);
                                    /*
                                     if (presentToken == "TK.EOF")
                                         return true;
                                     else if (presentToken == "TK.DEDENT")
                                     {
                                         nextToken();
                                         if (Start())
                                             return true;
                                         else
                                         {
                                             return false;
                                         }                                   
                                     }else return false;*/
                                //}
                                if(presentToken == "TK.EOF")
                                {
                                    //MessageBox.Show("Fim do arquivo!");
                                    return true;
                                }
                                else return false;
                            }
                            else return false;
                        }
                        else return false;
                    }
                    else return false;
                }
                else return false;
            }
            else return false;
        }
        private Boolean A(ref string Ac,ref string Ap)
        {
            string Relc = "", Relp = "", Ac1 = "", Ap1 = "", RelTrue = "", RelFalse = "";
            if(Rel(ref Relc, ref Relp, ref RelTrue, ref RelFalse))
            {
                if(presentToken == "TK.EQUAL")
                {
                    nextToken();
                    if(A(ref Ac1, ref Ap1))
                    {
                        Ac = Ac1 + "\t" + Relp + " = " + Ap1 + "\n";// Environment.NewLine;
                        //MessageBox.Show(Ac);
                        Ap = Relp;
                        return true;
                    }
                    return false;
                }
                else
                {
                    Ap = Relp;
                    Ac = Relc;
                }
                return true;
            }
            return false;
        }
        private Boolean Rel(ref string Relc, ref string Relp, ref string RelTrue, ref string RelFalse)
        {
            string Ec1 = "", Ep1 = "", Ec2 = "", Ep2 = "";
            if(E(ref Ec1,ref Ep1))
            {
                if (presentToken == "TK.EQUALEQUAL" || presentToken == "TK.NOTEQUAL" || presentToken == "TK.LESS" ||
                        presentToken == "TK.GREATER" || presentToken == "TK.LESSEQUAL" || presentToken == "TK.GREATEREQUAL")
                {
                    string op = " " + tokenList[index].Lexema + " ";
                    nextToken();
                    if(E(ref Ec2,ref Ep2))
                    {
                        geratemp(ref Relp);
                        //Relc = Ec1 + Ec2 + "\t" + Relp + " = " + Ep1 + op + Ep2 + "\n";// Environment.NewLine;
                        Relc = Ec1 + Ec2 + "\tif " + Ep1 + op + Ep2 + " goto " + RelTrue + "\n" + "\tgoto " + RelFalse + "\n";
                        //MessageBox.Show(Relc);
                        return true;
                    }
                    return false;
                }
                else
                {
                    Relc = Ec1;
                    Relp = Ep1;
                    return true;
                }
            }
            return false;
        }
        private Boolean E(ref string Ec, ref string Ep)
        {
            string Tc = "", Tp = "", Rsc = "", Rsp = "", Rhc = "", Rhp = "";
            if(T(ref Tc, ref Tp))
            {
                Rhc = Tc;
                Rhp = Tp;
                if(R(ref Rsc, ref Rsp, ref Rhc,ref Rhp))
                {
                    Ec = Rsc;
                    Ep = Rsp;
                    return true;
                }
            }
            return false;
        }
        private Boolean R(ref string Rsc, ref string Rsp, ref string Rhc, ref string Rhp)
        {
            string Tc = "", Tp = "", Rsc1 = "", Rsp1 = "",Rhc1 = "", Rhp1 = "";
            if(presentToken == "TK.PLUS")
            {
                nextToken();
                if(T(ref Tc, ref Tp))
                {
                    geratemp(ref Rhp1);
                    Rhc1 = Rhc + Tc + "\t" + Rhp1 + " = " + Rhp + " + " + Tp + "\n";// Environment.NewLine; 
                    if(R(ref Rsc1, ref Rsp1, ref Rhc1, ref Rhp1))
                    {
                        Rsp = Rsp1;
                        Rsc = Rsc1;
                        return true;
                    }
                    return false;
                }
                return false;
            }
            Rsp = Rhp;
            Rsc = Rhc;
            return true;
        }
        private Boolean T(ref string Tc, ref string Tp)
        {
            if(presentToken == "TK.INTEGER" || presentToken == "TK.FLOAT")
            {
                geratemp(ref Tp);
                Tc = "\t" + Tp + " = " + tokenList[index].Lexema + Environment.NewLine;
                nextToken();
                return true;
            }
            else if(presentToken == "TK.ID")
            {
                Tc = "";
                Tp = tokenList[index].Lexema;
                nextToken();
                return true;
            }
            return false;
        }
        // Param_List -> (Parameters)
        private Boolean Param_List()
        {
            if (presentToken == "TK.LEFTPAR")
            {
                nextToken();
                if (Parameters())
                {
                    nextToken();
                    if (presentToken == "TK.RIGHTPAR")
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;
        }

        // Parameters -> TYPE ID ADD_PARAMETERS | EMPTY
        private Boolean Parameters()
        {
            if (Type())
            {
                nextToken();
                if (presentToken == "TK.ID")
                {
                    nextToken();
                    if (Add_Parameters())
                    {
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            else
            {
                backToken();
                return true;
            }
        }

        // Add_Parameters -> ,ADD_PARAMETERS | EMPTY
        private Boolean Add_Parameters()
        {
            if(presentToken == "TK.COMMA")
            {
                nextToken();
                if (Parameters())
                    return true;
                else
                    return false;
            }
            else
            {
                backToken();
                return true;
            }
        }


        // Program P -> ATTRIBUTION P | AUTO_OPERATION P | IF P | WHILE P | FOR P | PRINT P | FUNCTION P | EMPTY
        private Boolean Program(ref string Pc)
        {
            string Ac = "", Ap = "", IFc = "", Wc ="", Fc = "";
            if(presentToken == "TK.INDENT")
            {
                nextToken();
                while(presentToken != "TK.DEDENT" && presentToken != "TK.EOF")
                {
                    if (!Program(ref Pc))
                    {
                        MessageBox.Show("Erro no comando");
                        break;
                    }
                }
                if (presentToken == "TK.DEDENT" || presentToken == "TK.EOF")
                {
                    return true;
                }
                else
                    return false;
            }
            if(Com_if(ref IFc))
            {
                Pc += IFc;
               //MessageBox.Show(Pc);
                return true;
            }
             
            if(Com_while(ref Wc))
            {
                Pc += Wc;
                //MessageBox.Show(Pc);
                return true;
            }
            
            if(Com_for(ref Fc))
            {
                Pc += Fc;
                return true;
            }
           /* if (Auto_Operation())
            {
                nextToken();
                if (Program(ref P1c))
                    return true;
                else
                    return false;
            }
            else if (Function())
            {
                nextToken();
                if (Program(ref P1c))
                    return true;
                else
                    return false;
            }*/
            else if (A(ref Ac,ref Ap))
            {
                Pc += Ac;
                //MessageBox.Show(Ac);
                return true;
            }
            /*else if (If_Statment())
            {
                nextToken();
                if (Program(ref P1c))
                    return true;
                else
                    return false;
            }else if (While())
            {
                nextToken();
                if (Program(ref P1c))
                    return true;
                else
                    return false;
            }
            else if (For())
            {
                nextToken();
                if (Program(ref P1c))
                    return true;
                else
                    return false;
            }
            else if (Print())
            {
                nextToken();
                if (Program(ref P1c))
                    return true;
                else
                    return false;
            }
            else
            {
                //backToken();
                return true;
            }*/
            return false;
        }
        // FUNCTION -> ID (ID_LIST)
        // ID_LIST -> ID ADD_ID | EMPTY
        // ADD_ID -> ,ID ADD_ID | EMPTY
        private Boolean Function()
        {
            if (presentToken == "TK.ID")
            {
                nextToken();
                if (presentToken == "TK.LEFTPAR")
                {
                    nextToken();
                    if (ID_List())
                    {
                        nextToken();
                        if (presentToken == "TK.RIGHTPAR")
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else return false;
                }
                else
                {
                    backToken();
                    return false;
                }
            }
            else return false;
        }

        private Boolean ID_List()
        {
            if (presentToken == "TK.ID")
            {
                nextToken();
                if (Add_ID())
                    return true;
                else
                    return false;
            }
            else
            {
                backToken();
                return true;
            }
        }

        private Boolean Add_ID()
        {
            if (presentToken == "TK.COMMA")
            {
                nextToken();
                if (presentToken == "TK.ID")
                {
                    nextToken();
                    if (Add_ID())
                    {
                        return true;
                    }
                    else return false;
                }
                else return false;
            }
            else
            {
                backToken();
                return true;
            }
        }

        // AUTO_OPERATION -> ++ID | --ID | ID AUTO_OPERATION'
        // AUTO_OPERATION' -> ++ | --
        private Boolean Auto_Operation()
        {
            if (presentToken == "TK.PLUS")
            {
                nextToken();
                if (presentToken == "TK.PLUS")
                {
                    nextToken();
                    if (presentToken == "TK.ID")
                        return true;
                    else
                        return false;
                }
                else return false;
            }else if (presentToken == "TK.MINUS")
            {
                nextToken();
                if (presentToken == "TK.MINUS")
                {
                    nextToken();
                    if (presentToken == "TK.ID")
                        return true;
                    else
                        return false;
                }
                else return false;
            }
            else if (presentToken == "TK.ID")
            {
                nextToken();
                if (presentToken == "TK.PLUS")
                {
                    nextToken();
                    if (presentToken == "TK.PLUS")
                        return true;
                    else
                    {
                        backToken();
                        backToken();
                        return false;
                    }
                        
                }
                else if (presentToken == "TK.MINUS")
                {
                    nextToken();
                    if (presentToken == "TK.MINUS")
                        return true;
                    else
                    {
                        backToken();
                        backToken();
                        return false;
                    }
                }
                else
                {
                    backToken();
                    return false;
                }
            }
            else return false;
        }

        // PRINT -> PRINT (PRINT') | PRINT PRINT' | PRINT PRINT'
        // PRINT' -> PRINT_LIST | STRING | ID
        // PRINT_LIST -> STRING ADD_PRINT | ID ADD_PRINT
        // ADD_PRINT -> + STRING ADD_PRINT | + ID ADD_PRINT | EMPTY
        private Boolean Print()
        {
            if (presentToken == "TK.PRINT")
            {
                nextToken();
                if (presentToken == "TK.LEFTPAR")
                {
                    nextToken();
                    if (Print_List())
                    {
                        nextToken();
                        if (presentToken == "TK.RIGHTPAR")
                            return true;
                        else
                            return false;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (presentToken == "TK.STRING")
                {
                    return true;
                }
                else if (presentToken == "TK.ID")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else return false;
        }

        private Boolean Print_List()
        {
            if (presentToken == "TK.STRING" || presentToken == "TK.ID")
            {
                nextToken();
                if (Add_Print())
                    return true;
                else
                    return false;
            }
            else return false;
        }

        private Boolean Add_Print()
        {
            if (presentToken == "TK.PLUS")
            {
                nextToken();
                if (presentToken == "TK.ID" || presentToken == "TK.STRING")
                {
                    nextToken();
                    if (Add_Print())
                        return true;
                    else
                        return false;
                }
                else return false;
            }
            else
            {
                backToken();
                return true;
            }
        }

        // FOR -> ID IN ID (INTEGER) INDENT PROGRAM DEDENT
        private Boolean For()
        {
            string Pc = "";
            if (presentToken == "TK.FOR")
            {
                nextToken();
                if (presentToken == "TK.ID")
                {
                    nextToken();
                    if (presentToken == "TK.IN")
                    {
                        nextToken();
                        if (presentToken == "TK.ID")
                        {
                            nextToken();
                            if (presentToken == "TK.LEFTPAR")
                            {
                                nextToken();
                                if (presentToken == "TK.INTEGER")
                                {
                                    nextToken();
                                    if (presentToken == "TK.RIGHTPAR")
                                    {
                                        nextToken();
                                        if (presentToken == "TK.INDENT")
                                        {
                                            nextToken();
                                            if (Program(ref Pc))
                                            {
                                                nextToken();
                                                if (presentToken == "TK.DEDENT")
                                                {
                                                    return true;
                                                }
                                                else
                                                {
                                                    return false;
                                                }
                                            }
                                            else return false;
                                        }
                                        else return false;
                                    }
                                    else return false;
                                }
                                else return false;
                            }
                            else return false;
                        }
                        else return false;
                    }
                    else return false;
                }
                else return false;
            }
            else return false;
        }

        // WHILE -> WHILE (CONDITION_LIST) INDENT PROGRAM DEDENT 
        private Boolean While()
        {
            string Pc = "";
            if (presentToken == "TK.WHILE")
            {
                nextToken();
                if (presentToken == "TK.LEFTPAR")
                {
                    nextToken();
                    if (Condition_List())
                    {
                        nextToken();
                        if (presentToken == "TK.RIGHTPAR")
                        {
                            nextToken();
                            if (presentToken == "TK.INDENT")
                            {
                                nextToken();
                                if (Program(ref Pc))
                                {
                                    nextToken();
                                    if (presentToken == "TK.DEDENT")
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                                else return false;
                            }
                            else return false;
                        }
                        else return false;
                    }
                    else return false;
                }
                else return false;
            }
            else return false;
        }

        // IF_STATEMENT -> IF (CONDITION_LIT) INDENT PROGRAM DEDENT ELSE_ELIF
        // ELSE_ELIF -> ELSE INDENT PROGRAM DEDENT | ELIF IF_STATMENT | EMPTY
        private Boolean If_Statment()
        {
            string Pc = "";
            if (presentToken == "TK.IF" || elif)
            {
                elif = false;
                nextToken();
                if (presentToken == "TK.LEFTPAR")
                {
                    nextToken();
                    if (Condition_List())
                    {
                        nextToken();
                        if (presentToken == "TK.RIGHTPAR")
                        {
                            nextToken();
                            if (presentToken == "TK.INDENT")
                            {
                                nextToken();
                                if (Program(ref Pc))
                                {
                                    nextToken();
                                    if (presentToken == "TK.DEDENT")
                                    {
                                        nextToken();
                                        if (else_elif())
                                            return true;
                                        else
                                            return false;
                                    }
                                    else if (presentToken == "TK.EOF")
                                    {
                                        backToken();
                                        return true;
                                    }
                                    else if (presentToken == "TK.ELSE")
                                        if (else_elif())
                                            return true;
                                        else
                                            return false;
                                    else
                                        return false;
                                }
                                else return false;
                            }
                            else return false;
                        }
                        else return false;
                    }
                    else return false;
                }
                else return false;
            }
            else return false;
        }

        private Boolean else_elif()
        {
            string Pc = "";
            if (presentToken == "TK.ELSE")
            {
                nextToken();
                if (presentToken == "TK.INDENT")
                {
                    nextToken();
                    if (Program(ref Pc ))
                    {
                        nextToken();
                        if (presentToken == "TK.DEDENT")
                            return true;
                        else if (presentToken == "TK.EOF")
                        {
                            backToken();
                            return true;
                        }
                        else
                            return false;
                    }
                    else return false;
                }
                else return false;
            }
            else if (presentToken == "TK.ELIF")
            {
                elif = true;
                if (If_Statment())
                    return true;
                else
                    return false;
            }
            else
            {
                backToken();
                return true;
            }
        }

        // Condition_List -> EXP ADD_CONDITION 
        // Add_Condition -> && EXP | || EXP | EMPTY 
        // Exp_Conditional E -> EXP == EXP | EXP != EXP | EXP > EXP | EXP < EXP | EXP >= EXP | EXP <= EXP 

        private Boolean Condition_List()
        {
            if (Exp_Conditional())
            {
                nextToken();
                if (Add_Condition())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private Boolean Add_Condition()
        {
            if (presentToken == "TK.VERTBAR" || presentToken == "TK.AMPER")
            {
                nextToken();
                if (Exp_Conditional())
                    return true;
                else
                    return false;
            }
            else
            {
                backToken();
                return true;
            }
        }

        private Boolean Exp_Conditional()
        {
            if (Expression())
            {
                nextToken();
                if (presentToken == "TK.EQUALEQUAL" || presentToken == "TK.NOTEQUAL" || presentToken == "TK.LESS" ||
                        presentToken == "TK.GREATER" || presentToken == "TK.LESSEQUAL" || presentToken == "TK.GREATEREQUAL")
                {
                    nextToken();
                    if (Expression())
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        private Boolean Com_for(ref string for_c)
        {
            if (presentToken == "TK.FOR")
            {
                nextToken();
                if (presentToken == "TK.ID")
                {
                    string Tc1 = "", Tp1 = "";
                    if (T(ref Tc1, ref Tp1))
                    {
                        if(presentToken == "TK.IN")
                        {
                            nextToken();
                            if(presentToken == "TK.RANGE")
                            {
                                nextToken();
                                if (presentToken == "TK.LEFTPAR")
                                {
                                    nextToken();
                                    string Tc2 = "", Tp2 = "";
                                    if(T(ref Tc2, ref Tp2))
                                    {
                                        if(presentToken == "TK.COMMA")
                                        {
                                            nextToken();
                                            string Tc3 = "", Tp3 = "";
                                            if(T(ref Tc3, ref Tp3))
                                            {
                                                if(presentToken == "TK.RIGHTPAR")
                                                {
                                                    nextToken();
                                                    if(presentToken == "TK.COLON")
                                                    {
                                                        nextToken();
                                                        string Pc = "";
                                                        if(Program(ref Pc))
                                                        {
                                                            string LBTrue = "", LBFalse = "";
                                                            geralabel(ref LBTrue);
                                                            geralabel(ref LBFalse);
                                                            if (presentToken == "TK.DEDENT")
                                                            {
                                                                nextToken();
                                                                if(presentToken == "TK.ELSE")
                                                                {
                                                                    nextToken();
                                                                    if(presentToken == "TK.COLON")
                                                                    {
                                                                        nextToken();
                                                                        string Pc1 = "";
                                                                        if(Program(ref Pc1))
                                                                        {
                                                                            for_c += LBTrue + ":\n\t" + Tp1 + " = 1 +" + Tc2 + Tc3  + Tp1 + " > " + Tp2 + " goto " + LBFalse + "\n\t " +
                                                                                     Pc + "\n\tgoto " + LBTrue + "\n" + LBFalse + ":\n" + Pc1 + "\n";
                                                                            return true;
                                                                        }
                                                                        else
                                                                        {
                                                                            MessageBox.Show("Erro no comando do ELSE do FOR");
                                                                            return false;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        MessageBox.Show("Erro esperava um COLON");
                                                                        return false;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    for_c += LBTrue + ":\n\tif " + Tp1 + " > " + Tp2 + " goto " + LBFalse + "\n\t " +
                                                                         Pc + "\n\tgoto " + LBTrue + "\n" + LBFalse + ":\n";
                                                                    return true;
                                                                }
                                                            }
                                                            else if(presentToken == "TK.EOF")
                                                            {
                                                                for_c += LBTrue + ":\n\tif " + Tp1 + " > " + Tp2 + " goto " + LBFalse + "\n\t " +
                                                                         Pc + "\n\tgoto " + LBTrue + "\n" + LBFalse + ":\n";
                                                                return true;
                                                            }
                                                            else
                                                            {
                                                                MessageBox.Show("Erro esperava um DEDENT");
                                                                return false;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("Erro no comando do FOR");
                                                            return false;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Erro esperava um COLON");
                                                        return false;
                                                    }
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Erro esperava um fecha parênteces");
                                                    return false;
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Erro no reconhecimento da segunda variavel do comando Range");
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Erro esperava uma virgula");
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Erro no reconhecimento da primeira variavel do comando Range");
                                        return false;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Erro esperava o abre parênteces");
                                    return false;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Erro esperava a palavra RANGE");
                                return false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Erro esperava o IN do comando FOR");
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Erro no reconhecimento da variavel do comando FOR");
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Esperava uma variavel no comando FOR");
                    return false;
                }
            }
            else
                return false;
        }

         //Com_WHILE ->  WHILE (EA) INDENT PROGRAM DEDENT {FIM = geralabel, INICIO = geralabel,
         //              ComWHILE_c = gen(INICIO, ":") + EA_c + gen("if", EA_p, "=", "0", "goto", FIM) +
         //              Program_c + gen("goto", INICIO) + gen(FIM, ":")} |

         //              WHILE (EA) INDENT PROGRAM DEDENT ELSE INDENT PROGRAM DEDENT {ComELSE = geralabel, INICIO = geralabel,
         //              ComWHILE_c = gen(INICIO, ":") + EA_c + gen("if", EA_p, "=", "0", "goto", ComELSE) +
         //              Program_c + gen("goto", INICIO) + gen(ELSE, ":") + Program_c1}

        private Boolean Com_while(ref string W_c)
        {
            if (presentToken == "TK.WHILE")
            {
                nextToken();
                if (presentToken == "TK.LEFTPAR")
                {
                    nextToken();

                    string EA_c = "";
                    string EATrue = "", EAFalse = "";
                    geralabel(ref EATrue);
                    geralabel(ref EAFalse);

                    if (EA(ref EA_c, ref EATrue, ref EAFalse))
                    {
                        if (presentToken == "TK.RIGHTPAR")
                        {
                            nextToken();
                            if (presentToken == "TK.COLON")
                            {
                                nextToken();
                                if (presentToken == "TK.INDENT")
                                {
                                    //nextToken();
                                    string P_c = "";
                                    if (Program(ref P_c))
                                    {
                                        if (presentToken == "TK.DEDENT")
                                        {
                                            nextToken();
                                            if (presentToken == "TK.ELSE")
                                            {
                                                nextToken();
                                                if(presentToken == "TK.COLON")
                                                {
                                                    nextToken();
                                                    string P_c2 = "";
                                                    if(Program(ref P_c2))
                                                    {
                                                        if(presentToken == "TK.DEDENT")
                                                        {
                                                            nextToken();
                                                            string LBlaco = "";
                                                            geralabel(ref LBlaco);
                                                            W_c += LBlaco + ":\n" + EA_c + P_c + "\tgoto " + LBlaco + "\n" + EAFalse + ":\n" + P_c2;
                                                            return true;
                                                        }
                                                        else if(presentToken == "TK.EOF")
                                                        {
                                                            string LBlaco = "";
                                                            geralabel(ref LBlaco);
                                                            W_c += LBlaco + ":\n" + EA_c + P_c + "\tgoto " + LBlaco + "\n" + EAFalse + ":\n" + P_c2;
                                                            return true;
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("Erro esperava um DEDENT");
                                                            return false;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Erro no comando do ELSE do laco WHILE");
                                                        return false;
                                                    }
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Erro esperava um COLON");
                                                    return false;
                                                }
                                            }
                                            else
                                            {
                                                string LBlaco = "";
                                                geralabel(ref LBlaco);
                                                W_c += LBlaco + ":\n" + EA_c + P_c + "\tgoto " + LBlaco + "\n" + EAFalse + ":\n";
                                                return true;
                                            }
                                        }
                                        else if(presentToken == "TK.EOF")
                                        {
                                            string LBlaco = "";
                                            geralabel(ref LBlaco);
                                            W_c += LBlaco + ":\n" + EA_c + P_c + "\tgoto " + LBlaco + "\n" + EAFalse + ":\n";
                                            return true;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Erro esperava um DEDENT");
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Erro no Comando do While");
                                        return false;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Erro esperava um INDENT");
                                    return false;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Erro esperava um COLON");
                                return false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Erro esperava um fecha parênteces");
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Erro na condição do while");
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Erro esperava um abre parênteces");
                    return false;
                }
            }
            else
                return false;
        }

        //  Com_If -> if {EA_true = geralabel, EA_false = geralabel} 
        // (EA) INDENT Program DEDENT ELSE_ELIF { LBfim = geralabel, Comif_c = EA_c + gen(rotulo EAtrue) +
        //           Program_c + gen("goto" LBfalse) + gen(rotulo EAfalse) + EelseElif_c + gen(rotulo LBfim)}
        private Boolean Com_if(ref string if_c)
        {
            if (presentToken == "TK.IF")
            {
                nextToken();
                if(Com_ifR(ref if_c))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Erro no reconhecimento do IF");
                    return false;
                }
            }
            if (presentToken == "TK.ELIF")
            {
                nextToken();
                if (Com_ifR(ref if_c))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Erro no reconhecimento do ELIF");
                    return false;
                }
            }
            return false;
        }
        private Boolean Com_ifR(ref string if_c)
        {
            if (presentToken == "TK.LEFTPAR")
            {
                nextToken();

                string EA_c = "", EA_p = "", P_c = "", P_c2 = "";
                string EATrue = "", EAFalse = "";

                geralabel(ref EATrue);
                geralabel(ref EAFalse);

                if (EA(ref EA_c, ref EATrue, ref EAFalse))
                {
                    if (presentToken == "TK.RIGHTPAR")
                    {
                        nextToken();
                        if (presentToken == "TK.COLON")
                        {
                            nextToken();
                            if (presentToken == "TK.INDENT")
                            {
                                //nextToken();
                                if (Program(ref P_c))
                                {
                                    if (presentToken == "TK.DEDENT")
                                    {
                                        nextToken();
                                        if(presentToken == "TK.ELIF")
                                        {
                                            if_c += EA_c + P_c + EAFalse + ":\n";
                                            if(Com_if(ref if_c))
                                            {
                                                return true;
                                            }
                                            else
                                            {
                                                MessageBox.Show("Erro no reconhecimento do ELIF");
                                                return false;
                                            }
                                        }
                                        else if (presentToken == "TK.ELSE")
                                        {
                                            nextToken();
                                            if (presentToken == "TK.COLON")
                                            {
                                                nextToken();
                                                if (presentToken == "TK.INDENT")
                                                {
                                                    //nextToken();
                                                    if (Program(ref P_c2))
                                                    {
                                                        string LBFim = "";
                                                        geralabel(ref LBFim);
                                                        //if_c += EA_c + "\n" + P_c + "\t" + "goto" + labelfim + "\n" + labelelse + ":" + "\n" + P_c2 + labelfim + ":" + "\n";
                                                        if_c += EA_c + P_c + "\n\tgoto " + LBFim + "\n" + EAFalse + ":\n" + P_c2 + LBFim + ":\n";
                                                        //MessageBox.Show(if_c);
                                                        if (presentToken == "TK.DEDENT")
                                                        {
                                                            nextToken();
                                                            return true;
                                                        }
                                                        else if (presentToken == "TK.EOF")
                                                        {
                                                            return true;
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("Esperava um TK.DEDENT");
                                                            return false;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Erro no Comando 2");
                                                        return false;
                                                    }
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Esperava um INDENT");
                                                    return false;
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Esperava um TK.COLON");
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            //if_c +=  EA_c + P_c + labelelse + ":" + "\n";
                                            if_c += EA_c + EATrue + ":\n" + P_c2 + EAFalse + ":\n";
                                            //MessageBox.Show(if_c);
                                            return true;
                                        }
                                    }
                                    else if (presentToken == "TK.EOF")
                                    {
                                        //if_c +=  EA_c + P_c + labelelse + ":" + "\n";
                                        if_c += EA_c + P_c + EAFalse + ":\n";
                                        //MessageBox.Show(if_c);
                                        return true;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Esperava um DEDENT");
                                        return false;
                                    }

                                }
                                else
                                {
                                    MessageBox.Show("Erro no Comando 1");
                                    return false;
                                }

                            }
                            else
                            {
                                MessageBox.Show("Esperava um INDENT");
                                return false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Esperava um TK.COLON");
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Esperava um fecha parênteses");
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Erro na primeira condição do comando If");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Esperava um Abre Parênteses");
                return false;
            }
        }


        /*        
                 EA -> { ERtrue = geralabel, ERfalse = EAfalse} ER {EALtrue = EAtrue, EALfalse = EAfalse}
                       EAL {EA_c = ER_c + gen(rotulo ERtrue) + EAL_c} |
         ***           { ERtrue = geralabel, ERfalse = EAfalse} ER {EOLtrue = EAtrue, EOLfalse = EAfalse}
                       EOL {EA_c = ER_c + gen(rotulo ERtrue) + EOL_c}



         ***     EOL -> AND { ERtrue = geralabel, ERfalse = EOLfalse} ER {EOLtrue_1 = EOLtrue, EOLfalse_1 = EOLfalse }
                        EOL { EOL_c = ER_c + gen(rotulo ERtrue) + EOL_c1} |
                        EMPTY { EOL_c = gen("goto" EOLtrue)}
        */
        private Boolean EA(ref string EA_c, ref string labelTrue, ref string labelFalse)
        {
            string Rel_c = "", Rel_p = "", RelTrue = "", RelFalse = "";
            geralabel(ref RelTrue);
            RelFalse = labelFalse;
            if (Rel(ref Rel_c, ref Rel_p, ref RelTrue, ref RelFalse))
            {
                string EAL_c = "";
                string EALTrue = RelTrue, EALFalse = RelFalse;
                if(EAL(ref EAL_c, ref EALTrue, ref EALFalse))
                {
                    EA_c += Rel_c + RelTrue + ":\n" + EAL_c + "\n";
                    //MessageBox.Show(EA_c);
                    return true;
                }
                else
                {
                    EA_c += Rel_c + RelTrue + ":\n";
                    //MessageBox.Show(EA_c);
                    return true;
                }
            }
            else
            {
                MessageBox.Show("Erro no reconhecimento condição");
                return false;
            }
        }

        /*EAL -> AND { ERtrue = geralabel, ERfalse = EALfalse} ER {EALtrue_1 = EALtrue, EALfalse_1 = EALfalse }
                EAL { EAL_c = ER_c + gen(rotulo ERtrue) + EAL_c1} |
                EMPTY { EAL_c = gen("goto" EALtrue)}*/
        private Boolean EAL(ref string EAL_c, ref string labelTrue, ref string labelFalse)
        {

            if (presentToken == "TK.AND")
            {
                nextToken();
                string EAL_c1 = "", Rel_c = "", Rel_p = "", RelTrue = "", RelFalse = "";
                geralabel(ref RelTrue);
                RelFalse = labelFalse;
                if (Rel(ref Rel_c,ref Rel_p, ref RelTrue, ref RelFalse))
                {
                    if(EAL(ref EAL_c1, ref labelTrue, ref labelFalse))
                    {
                        EAL_c += Rel_c + "\n" + RelTrue + ":\n" + EAL_c1 + "\n";
                        return true;
                    }
                    else
                    {
                        EAL_c += Rel_c + RelTrue + ":\n";// + "\tgoto " + RelTrue + "\n";
                        //MessageBox.Show(EAL_c);
                        return true;
                    }
                }
                else
                {
                    MessageBox.Show("Erro no comando AND");
                    return false;
                }
            }
            else
            {
                //EAL_c += "\tgoto " + labelTrue + "\n"; 
                return false;
            }
        }



        // Attribution ->    ID = FUNCTION | ID = EXP | ID += EXP | ID -= EXP | ID *= EXP | ID /= EXP 
        private Boolean Attribution(ref string Ap,ref string Ac)
        {
            //if (presentToken == "TK.ID")
            if (Type())
            {
                nextToken();
                if (presentToken == "TK.EQUAL")
                {
                    nextToken();
                    if (Function())
                        return true;
                    else if (Expression())
                    {
                        return true;
                    }
                    else
                        return false;
                }
                else if (presentToken == "TK.PLUSEQUAL")
                {
                    nextToken();
                    if (Function())
                        return true;
                    else if (Expression())
                        return true;
                    else
                        return false;
                }
                else if (presentToken == "TK.MINEQUAL")
                {
                    nextToken();
                    if (Function())
                        return true;
                    else if (Expression())
                        return true;
                    else
                        return false;
                }
                else if (presentToken == "TK.STAREQUAL")
                {
                    nextToken();
                    if (Function())
                        return true;
                    else if (Expression())
                        return true;
                    else
                        return false;
                }
                else if (presentToken == "TK.SLASHEQUAL")
                {
                    nextToken();
                    if (Function())
                        return true;
                    else if (Expression())
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;
        }

        // Exp_Attrib E -> E + E | E-E | E * E | E / E | E ^ E | E and E | E or E | id | ( E ) | TYPE
        // Fatorando
        // Exp_Attrib E -> E E' | id | ( E ) | TYPE em que E' = Exp_Attrib_1
        // Exp_Attrib_1 E' -> + E | - E | * E | / E | ^ E | and E | or E 
        public Boolean Expression()
        {
            if (Type())
            {
                nextToken();
                if (presentToken == "TK.PLUS")
                {
                    nextToken();
                    if (Expression())
                    {
                        return true;
                    }
                    else
                        return false;
                }
                /*else if (presentToken == "TK.AND" || presentToken == "TK.OR" || presentToken == "TK.STAR" ||
                    presentToken == "TK.SLASH" || presentToken == "TK.CIRCUMFLEX" || presentToken == "TK.PLUS"
                    || presentToken == "TK.MINUS")
                {
                    nextToken();
                    if (Expression(ref E1c,ref E1p))
                        return true;
                    else
                        return false;
                }*/
                else
                {
                    backToken();
                    return true;
                }
            }
            else if(Type())
            {
                nextToken();
                /* if (presentToken == "TK.AND" || presentToken == "TK.OR" || presentToken == "TK.STAR" ||
                     presentToken == "TK.SLASH" || presentToken == "TK.CIRCUMFLEX" || presentToken == "TK.PLUS"
                     || presentToken == "TK.MINUS")
                 {
                     nextToken();
                     if (Expression(ref E1c,ref E1p))
                     {
                         Ec = E1c + Environment.NewLine + Ep + " + " + E1p + Environment.NewLine;
                         MessageBox.Show(Ec);
                         return true;
                     }
                     else
                         return false;
                 }
                 else
                 {
                     backToken();
                     return true;
                 }*/
                return true;
                    
            }
            else if (presentToken == "TK.LEFTPAR")
            {
                nextToken();
                if (Expression())
                {
                    nextToken();
                    if (presentToken == "TK.RIGHTPAR")
                    {
                        return true;
                    }
                    else return false;
                }
                else return false;
            }
            else return false;
        }


        private Boolean Type()
        {
            if (presentToken == "TK.FLOAT")
            {
                return true;
            }
            else if (presentToken == "TK.INTEGER")
            {
                return true;
            }
            else if (presentToken == "TK.STRING")
            {
                return true;
            }
            else if(presentToken == "TK.ID")
            {
                return true;
            }
                return false;
        }

        private static string GetTemp(string temp)
        {
            return temp;
        }
    }
}
