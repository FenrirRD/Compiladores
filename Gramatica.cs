using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguagensFormais
{
    class GRAMATICA
    {


        /*
         * 
         * 
        Start -> DEF ID Param_List : INDENT Program{Start_c = Program_c, Start_p = Program_p} EOF

        Param_List -> ( Parameters )
        Parameters -> TYPE ID ADD_PARAMETERS | EMPTY
        Add_Parameters -> ,Parameters | EMPTY
        Type -> string | integer | float

        Program -> ATTRIBUTION Program | AUTO_OPERATION Program | Com_IF Program | Com_WHILE Program |
                   Com_FOR Program | Com_PRINT Program | FUNCTION Program | EMPTY

        Attribution -> ID = Attribution {Attribution_c = Attribution_c + gen(=, ID, Attribution_p), Attribution_p = ID} |
                        Rel {Attribution_c = Rel_c, Attribution_p = Rel_p}
                
        Com_If -> if {EA_true = geralabel, EA_false = geralabel} 
                  (EA) INDENT Program DEDENT ELSE_ELIF { LBfim = geralabel, Comif_c = EA_c + gen( rotulo EAtrue) +
                  Program_c + gen("goto" LBfalse) + gen(rotulo EAfalse) + EelseElif_c + gen(rotulo LBfim)}
          ***ERtrue = EAtrue
         EA -> { ERtrue = geralabel, ERfalse = EAfalse} ER {EALtrue = EAtrue, EALfalse = EAfalse}
               EAL {EA_c = ER_c + gen(rotulo ERtrue) + EAL_c} |
 ***           { ERtrue = geralabel, ERfalse = EAfalse} ER {EOLtrue = EAtrue, EOLfalse = EAfalse}
               EOL {EA_c = ER_c + gen(rotulo ERtrue) + EOL_c}

         EAL -> AND { ERtrue = geralabel, ERfalse = EALfalse} ER {EALtrue_1 = EALtrue, EALfalse_1 = EALfalse }
                EAL { EAL_c = ER_c + gen(rotulo ERtrue) + EAL_c1} |
                EMPTY { EAL_c = gen("goto" EALtrue)}

 ***     EOL -> AND { ERtrue = geralabel, ERfalse = EOLfalse} ER {EOLtrue_1 = EOLtrue, EOLfalse_1 = EOLfalse }
                EOL { EOL_c = ER_c + gen(rotulo ERtrue) + EOL_c1} |
                EMPTY { EOL_c = gen("goto" EOLtrue)}

         Com_WHILE ->  WHILE (EA) INDENT PROGRAM DEDENT {FIM = geralabel, INICIO = geralabel,
                       ComWHILE_c = gen(INICIO, ":") + EA_c + gen("if", EA_p, "=", "0", "goto", FIM) +
                       Program_c + gen("goto", INICIO) + gen(FIM, ":")} |

                       WHILE (EA) INDENT PROGRAM DEDENT Com_ELSE INDENT PROGRAM DEDENT {ComELSE = geralabel, INICIO = geralabel,
                       ComWHILE_c = gen(INICIO, ":") + EA_c + gen("if", EA_p, "=", "0", "goto", ComELSE) +
                       Program_c + gen("goto", INICIO) + gen(ComELSE, ":") + Program_c1}

         Com_FOR -> FOR ID IN ID INDENT PROGRAM DEDENT | FOR ID IN ID INDENT PROGRAM DEDENT COM_ELSE INDENT PROGRAM DEDENT

         Rel -> Exp > Exp | Exp < Exp | Exp == Exp | Exp <= Exp | Exp >= Exp | Exp != Exp {Rel_p = geratemp, Rel_c = Exp_c1 + Exp_c2 + gen(Op, Rel_p, Exp_p1, Exp_p2)}
                Exp { Rel_p = Exp_p, Rel_c = Exp_c}

         Exp -> T {Rh_p = T_p, Rh_c = T_c } R {Exp_p = Rs_p, Exp_c = Rs_c }

         R -> + T {Rh_p1 = geratemp, Rh_c1 = Rh_c + T_c + gen(-, Rh_p1, Rh_p, T_p)} R {Rs_p = Rs_p1, Rs_c = Rs_c1 } | 
              - T {Rh_p1 = geratemp, Rh_c1 = Rh_c + T_c + gen(-, Rh_p1, Rh_p, T_p)} R {Rs_p = Rs_p1, Rs_c = Rs_c1 } | 
              EMPTY { Rs_p = Rh_p, Rs_c = Rh_c }

         T -> F {Sh_p = F_p, Sh_c = F_c } S {T_p = Ss_p, T_c = Ss_c }

         S -> * F {Sh_p1 = geratemp, Sh_c1 = Sh_c + F_c + gen(-, Sh_p1, Sh_p, F_p)} S {Ss_p = Ss_p1, Ss_c = Ss_c1 } | 
              / F {Sh_p1 = geratemp, Sh_c1 = Sh_c + F_c + gen(-, Sh_p1, Sh_p, F_p)} S {Ss_p = Ss_p1, Ss_c = Ss_c1 } | 
              EMPTY { Ss_p = Sh_p, Ss_c = Sh_c }

         F -> Const_Int {F_p = geratemp, F_c = gen(=,F_p, lexema)} | 
              Const_Float {F_p = geratemp, F_c = gen(=,F_p, lexema)} | 
              String {F_p = geratemp, F_c = gen(=,F_p, lexema)} | 
              ID {F_p = lexema, F_c = " " } |
              ( Exp ) { F_c = Exp_c, F_p = Exp_p }

         */
    }
}
