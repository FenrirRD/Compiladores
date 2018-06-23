namespace LinguagensFormais
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.arquivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graMenuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.ferramentasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LexicalMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SintaticalMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SemanticaMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ajudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verTokensToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.instruçoesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sobreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Tb_Code = new System.Windows.Forms.TextBox();
            this.MyGrid = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MyGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.arquivoToolStripMenuItem,
            this.ferramentasToolStripMenuItem,
            this.ajudaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1350, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // arquivoToolStripMenuItem
            // 
            this.arquivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirToolStripMenuItem,
            this.graMenuItemSave,
            this.MenuItemExit});
            this.arquivoToolStripMenuItem.Name = "arquivoToolStripMenuItem";
            this.arquivoToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.arquivoToolStripMenuItem.Text = "Arquivo";
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.abrirToolStripMenuItem.Text = "Abrir";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.abrirToolStripMenuItem_Click);
            // 
            // graMenuItemSave
            // 
            this.graMenuItemSave.Name = "graMenuItemSave";
            this.graMenuItemSave.Size = new System.Drawing.Size(139, 22);
            this.graMenuItemSave.Text = "Gravar Saída";
            this.graMenuItemSave.Click += new System.EventHandler(this.graMenuItemSave_Click);
            // 
            // MenuItemExit
            // 
            this.MenuItemExit.Name = "MenuItemExit";
            this.MenuItemExit.Size = new System.Drawing.Size(139, 22);
            this.MenuItemExit.Text = "Sair";
            this.MenuItemExit.Click += new System.EventHandler(this.MenuItemExit_Click);
            // 
            // ferramentasToolStripMenuItem
            // 
            this.ferramentasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LexicalMenuItem,
            this.SintaticalMenuItem,
            this.SemanticaMenuItem});
            this.ferramentasToolStripMenuItem.Name = "ferramentasToolStripMenuItem";
            this.ferramentasToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.ferramentasToolStripMenuItem.Text = "Executar";
            // 
            // LexicalMenuItem
            // 
            this.LexicalMenuItem.Name = "LexicalMenuItem";
            this.LexicalMenuItem.Size = new System.Drawing.Size(170, 22);
            this.LexicalMenuItem.Text = "Análise Léxica";
            this.LexicalMenuItem.Click += new System.EventHandler(this.LexicalMenuItem_Click);
            // 
            // SintaticalMenuItem
            // 
            this.SintaticalMenuItem.Name = "SintaticalMenuItem";
            this.SintaticalMenuItem.Size = new System.Drawing.Size(170, 22);
            this.SintaticalMenuItem.Text = "Análise Sintática";
            this.SintaticalMenuItem.Click += new System.EventHandler(this.SintaticalMenuItem_Click);
            // 
            // SemanticaMenuItem
            // 
            this.SemanticaMenuItem.Name = "SemanticaMenuItem";
            this.SemanticaMenuItem.Size = new System.Drawing.Size(170, 22);
            this.SemanticaMenuItem.Text = "Análise Semântica";
            this.SemanticaMenuItem.Click += new System.EventHandler(this.análiseSemanticaToolStripMenuItem_Click);
            // 
            // ajudaToolStripMenuItem
            // 
            this.ajudaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.verTokensToolStripMenuItem,
            this.instruçoesToolStripMenuItem,
            this.sobreToolStripMenuItem});
            this.ajudaToolStripMenuItem.Name = "ajudaToolStripMenuItem";
            this.ajudaToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.ajudaToolStripMenuItem.Text = "Ajuda";
            // 
            // verTokensToolStripMenuItem
            // 
            this.verTokensToolStripMenuItem.Name = "verTokensToolStripMenuItem";
            this.verTokensToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.verTokensToolStripMenuItem.Text = "Ver Tokens";
            this.verTokensToolStripMenuItem.Click += new System.EventHandler(this.verTokensToolStripMenuItem_Click);
            // 
            // instruçoesToolStripMenuItem
            // 
            this.instruçoesToolStripMenuItem.Name = "instruçoesToolStripMenuItem";
            this.instruçoesToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.instruçoesToolStripMenuItem.Text = "Instruções";
            this.instruçoesToolStripMenuItem.Click += new System.EventHandler(this.instruçoesToolStripMenuItem_Click);
            // 
            // sobreToolStripMenuItem
            // 
            this.sobreToolStripMenuItem.Name = "sobreToolStripMenuItem";
            this.sobreToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.sobreToolStripMenuItem.Text = "Sobre";
            this.sobreToolStripMenuItem.Click += new System.EventHandler(this.sobreToolStripMenuItem_Click);
            // 
            // Tb_Code
            // 
            this.Tb_Code.Location = new System.Drawing.Point(12, 27);
            this.Tb_Code.MinimumSize = new System.Drawing.Size(200, 200);
            this.Tb_Code.Multiline = true;
            this.Tb_Code.Name = "Tb_Code";
            this.Tb_Code.Size = new System.Drawing.Size(749, 662);
            this.Tb_Code.TabIndex = 1;
            this.Tb_Code.TextChanged += new System.EventHandler(this.Tb_Code_TextChanged);
            // 
            // MyGrid
            // 
            this.MyGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MyGrid.Location = new System.Drawing.Point(798, 27);
            this.MyGrid.Name = "MyGrid";
            this.MyGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MyGrid.Size = new System.Drawing.Size(552, 662);
            this.MyGrid.TabIndex = 2;
            this.MyGrid.Visible = false;
            this.MyGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MyGrid_CellContentClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 701);
            this.Controls.Add(this.MyGrid);
            this.Controls.Add(this.Tb_Code);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Linguagens Formais";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MyGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem arquivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem graMenuItemSave;
        private System.Windows.Forms.ToolStripMenuItem MenuItemExit;
        private System.Windows.Forms.ToolStripMenuItem ferramentasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LexicalMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SintaticalMenuItem;
        private System.Windows.Forms.TextBox Tb_Code;
        private System.Windows.Forms.DataGridView MyGrid;
        private System.Windows.Forms.ToolStripMenuItem ajudaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verTokensToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem instruçoesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sobreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SemanticaMenuItem;
    }
}

