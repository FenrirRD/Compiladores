using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LinguagensFormais
{
    public partial class Form1 : Form
    {
        String filePath;
        Lexical lexicalAnalysis = new Lexical();

        public Form1()
        {
            InitializeComponent();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {          
            filePath = OpenDialog();
            if (filePath != null)
            {
                ReadTextFile(filePath);
            }
        }

        private void ReadTextFile(string filePath)
        {
            Tb_Code.Text = null;
            try
            {
                Tb_Code.Text = File.ReadAllText(filePath);
            }
            catch (Exception e)
            {
                MessageBox.Show("Não é possivel ler o arquivo");
                MessageBox.Show(e.Message);
            }
        }
        private String OpenDialog()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Selecione um arquivo de texto";
            dialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            dialog.Filter = "Texte (*.txt)|*.txt|Todos os Arquivos (*.*)|*.*";
            dialog.RestoreDirectory = false;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.FileName;
            }
            return null;
        }


        private void LexicalMenuItem_Click(object sender, EventArgs e)
        {
            MyGrid.DataSource = null;
            lexicalAnalysis = new Lexical();
            ReadToken.ResetReadToken();

            int status = lexicalAnalysis.Analysis(filePath);

            var bindingList = new BindingList<ReadToken>(lexicalAnalysis.ReadTokens);
            var source = new BindingSource(bindingList, null);
            MyGrid.Visible = true;
            MyGrid.DataSource = source;

            switch (status)
            {
                case -1:
                    MyGrid.Visible = false;
                    MessageBox.Show("Erro durante a leitura do arquivo","Erro Fatal");
                    break;
                case 0:
                    MessageBox.Show("A análise léxica foi concluída com sucesso.","Sucesso!");
                    break;
                case 1:
                    MessageBox.Show("Houve erro na análise léxica." +
                        "\nLexema: "+lexicalAnalysis.Lexema+ " não corresponde a um token.\n"+
                        "O erro ocorreu na linha: "+lexicalAnalysis.Line+" na posição: "+lexicalAnalysis.Pos,"Erro léxico - Token inválido");
                    break;
                case 2:
                    MessageBox.Show("Houve erro na análise léxica." +
                        "\nIndentenção com espaços não são multiplos de 4.\n" +
                        "O erro ocorreu na linha: " + lexicalAnalysis.Line,"Erro léxico - Indentação");
                    break;
                case 3:
                    MessageBox.Show("Houve erro na análise léxica." +
                        "\nString não encontrou as aspas finais.\n" +
                        "O erro ocorreu na linha: " + lexicalAnalysis.Line, "Erro léxico - String");
                    break;
            }
        }

        private void SintaticalMenuItem_Click(object sender, EventArgs e)
        {
            if (lexicalAnalysis.ReadTokens.Count == 0)
                MessageBox.Show("A lista de tokens está vazia, execute a análise léxica primeiramnte.");
            else
            {
                Syntactic sintaticlAnalysis = new Syntactic(lexicalAnalysis.ReadTokens);
                if (sintaticlAnalysis.Analysis())
                    MessageBox.Show("A análise sintática foi concluida com sucesso.");
                else
                {
                    ReadToken erro = sintaticlAnalysis.ErroToken();
                    MessageBox.Show("Ocorreu um erro na análise sintática na linha "+erro.Linha+" na coluna "+erro.Coluna
                                        +" ao analisar o token "+erro.Token+" cujo lexema é \""+erro.Lexema+"\"");
                }
            } 
        }

        private void graMenuItemSave_Click(object sender, EventArgs e)
        {
            try
            {
                String printLine, sequence, token, lexema, line, column;
                String outputPath = filePath.Substring(0, filePath.LastIndexOf(Path.DirectorySeparatorChar));
                using (StreamWriter outputFile = new StreamWriter(outputPath + @"\Saida.lex"))
                {
                    sequence = "| " + String.Format("{0,9}", "Sequência");
                    token = " |" + String.Format("{0,20}", "Token");
                    lexema = " |" + String.Format("{0,50}", "Lexema");
                    line = " | " + String.Format("{0,4}", "Lin.");
                    column = " | " + String.Format("{0,4}", "Col.") + " |";
                    printLine = sequence + token + lexema + line + column;
                    outputFile.WriteLine(printLine);
                    foreach (ReadToken rt in lexicalAnalysis.ReadTokens)
                    {
                        sequence = "| " + String.Format("{0,9}", rt.Sequencia);
                        token = " |" + String.Format("{0,20}", rt.Token);
                        lexema = " |" + String.Format("{0,50}", rt.Lexema);
                        line = " | " + String.Format("{0,4}", rt.Linha);
                        column = " | " + String.Format("{0,4}", rt.Coluna) + " |";
                        printLine = sequence + token + lexema + line + column;
                        outputFile.WriteLine(printLine);
                    }
                }
                MessageBox.Show("A gravaçao do arquivo Saida.lex foi concluída com sucesso.\nVocê pode acessá-lo em:\n"+ outputPath, "Sucesso!");
            }
            catch
            {
                MessageBox.Show("Erro na gravação.","Erro");
            }
        }

        private void MenuItemExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void sobreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Analisador léxico, sintáico e semântico." +
                            "\n\nPrograma desenvolvido por:" +
                            "\n    Rodrigo Dallagnol" +
                            "\n\nDisciplina: Compiladores" +
                            "\nProf.: Ricardo Dorneles ",
                            "Sobre");
        }

        private void instruçoesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1 - Selecionar arquivo com o codigo Python" +
                "\n     Clique em Arquivo -> Abrir e selecione um arquivo." +
                "\n2 - Para a analise lexica do arquivo selecionado" +
                "\n     Clique em Executar -> Análise Léxica" +
                "\n3 - Gerar arquivo contendo a sequência de tokens, lexemas e número da linha e coluna onde ocorreram" +
                "\n     Clique em Arquivo -> Gravar Saída" +
                "\n     O arquivo Saida.lex será gravado no mesmo diretório onde esta o arquivo com o código.", "Instruções");
        }

        private void verTokensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("A lista de tokens pode ser visualiza em no arquivo ListaTokens.txt que foi enviado junto com o executável.", "Lista de Tokens");
        }

        private void análiseSemanticaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Tb_Code_TextChanged(object sender, EventArgs e)
        {

        }

        private void MyGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
