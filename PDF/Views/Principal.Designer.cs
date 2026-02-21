namespace PDF
{
    partial class Principal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Principal));
            this.label1 = new System.Windows.Forms.Label();
            this.rtfTitulo = new System.Windows.Forms.RichTextBox();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.EXPORTAR = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FOTO = new System.Windows.Forms.DataGridViewImageColumn();
            this.DESCRICAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DATA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CAMINHO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LAT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LNG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ROTACAO = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.nudQtd = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnAvancar = new System.Windows.Forms.Button();
            this.errp = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.arquivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salvarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.novoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adicionarLogoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnOrganizar = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.btnOpcoes = new System.Windows.Forms.ToolStripDropDownButton();
            this.chkMostrarCoordenadas = new System.Windows.Forms.ToolStripMenuItem();
            this.chkMostrarDataHora = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRotacionar = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnR90 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnR180 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnR_90 = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.chaveDoProdutoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copiarParaAÁreaDeTransferênciaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chkMarcar = new System.Windows.Forms.CheckBox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.rtfRelatorio = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkVertical = new System.Windows.Forms.CheckBox();
            this.tmrVersao = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblVersao = new System.Windows.Forms.ToolStripStatusLabel();
            this.pgb = new System.Windows.Forms.ToolStripProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQtd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errp)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Titulo:";
            // 
            // rtfTitulo
            // 
            this.rtfTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtfTitulo.Location = new System.Drawing.Point(12, 45);
            this.rtfTitulo.Name = "rtfTitulo";
            this.rtfTitulo.Size = new System.Drawing.Size(878, 45);
            this.rtfTitulo.TabIndex = 1;
            this.rtfTitulo.Text = "Engeselt Engenharia e Serviços Elétricos LTDA";
            this.rtfTitulo.Validating += new System.ComponentModel.CancelEventHandler(this.rtfTitulo_Validating);
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EXPORTAR,
            this.ID,
            this.FOTO,
            this.DESCRICAO,
            this.DATA,
            this.CAMINHO,
            this.LAT,
            this.LNG,
            this.ROTACAO});
            this.dgv.Location = new System.Drawing.Point(12, 176);
            this.dgv.Name = "dgv";
            this.dgv.Size = new System.Drawing.Size(877, 170);
            this.dgv.TabIndex = 2;
            this.dgv.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgv_CellBeginEdit);
            this.dgv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellClick);
            this.dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentClick);
            this.dgv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellDoubleClick);
            this.dgv.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellEndEdit);
            this.dgv.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellValueChanged);
            this.dgv.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_KeyDown);
            this.dgv.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgv_KeyPress);
            // 
            // EXPORTAR
            // 
            this.EXPORTAR.HeaderText = "Exportar";
            this.EXPORTAR.Name = "EXPORTAR";
            // 
            // ID
            // 
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ID.DefaultCellStyle = dataGridViewCellStyle1;
            this.ID.HeaderText = "Seq. Foto";
            this.ID.Name = "ID";
            // 
            // FOTO
            // 
            this.FOTO.HeaderText = "Foto";
            this.FOTO.Name = "FOTO";
            this.FOTO.ReadOnly = true;
            this.FOTO.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.FOTO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // DESCRICAO
            // 
            this.DESCRICAO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DESCRICAO.DefaultCellStyle = dataGridViewCellStyle2;
            this.DESCRICAO.HeaderText = "Descrição";
            this.DESCRICAO.Name = "DESCRICAO";
            // 
            // DATA
            // 
            this.DATA.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DATA.HeaderText = "Data";
            this.DATA.Name = "DATA";
            this.DATA.ReadOnly = true;
            // 
            // CAMINHO
            // 
            this.CAMINHO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CAMINHO.HeaderText = "Caminho";
            this.CAMINHO.Name = "CAMINHO";
            this.CAMINHO.Visible = false;
            // 
            // LAT
            // 
            this.LAT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.LAT.HeaderText = "X";
            this.LAT.Name = "LAT";
            // 
            // LNG
            // 
            this.LNG.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.LNG.HeaderText = "Y";
            this.LNG.Name = "LNG";
            // 
            // ROTACAO
            // 
            this.ROTACAO.HeaderText = "Rotação";
            this.ROTACAO.Items.AddRange(new object[] {
            "0",
            "90",
            "180",
            "-90",
            "-180"});
            this.ROTACAO.Name = "ROTACAO";
            this.ROTACAO.Visible = false;
            // 
            // nudQtd
            // 
            this.nudQtd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nudQtd.Location = new System.Drawing.Point(660, 414);
            this.nudQtd.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudQtd.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudQtd.Name = "nudQtd";
            this.nudQtd.Size = new System.Drawing.Size(67, 20);
            this.nudQtd.TabIndex = 3;
            this.nudQtd.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(543, 414);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Qtd por página";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(756, 364);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(108, 23);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Adicionar foto";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDel
            // 
            this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDel.Location = new System.Drawing.Point(642, 364);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(108, 23);
            this.btnDel.TabIndex = 6;
            this.btnDel.Text = "Remover foto";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnAvancar
            // 
            this.btnAvancar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAvancar.Location = new System.Drawing.Point(756, 411);
            this.btnAvancar.Name = "btnAvancar";
            this.btnAvancar.Size = new System.Drawing.Size(108, 23);
            this.btnAvancar.TabIndex = 7;
            this.btnAvancar.Text = "Exportar PDF";
            this.btnAvancar.UseVisualStyleBackColor = true;
            this.btnAvancar.Click += new System.EventHandler(this.btnAvancar_Click);
            // 
            // errp
            // 
            this.errp.ContainerControl = this;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.btnOrganizar,
            this.toolStripButton1,
            this.btnOpcoes,
            this.btnRotacionar,
            this.toolStripDropDownButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(928, 25);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.arquivoToolStripMenuItem,
            this.salvarToolStripMenuItem,
            this.novoToolStripMenuItem,
            this.testeToolStripMenuItem,
            this.adicionarLogoToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(67, 22);
            this.toolStripDropDownButton1.Text = "Arquivos";
            this.toolStripDropDownButton1.Click += new System.EventHandler(this.toolStripDropDownButton1_Click);
            // 
            // arquivoToolStripMenuItem
            // 
            this.arquivoToolStripMenuItem.Name = "arquivoToolStripMenuItem";
            this.arquivoToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.arquivoToolStripMenuItem.Text = "Abrir";
            this.arquivoToolStripMenuItem.Click += new System.EventHandler(this.arquivoToolStripMenuItem_Click);
            // 
            // salvarToolStripMenuItem
            // 
            this.salvarToolStripMenuItem.Name = "salvarToolStripMenuItem";
            this.salvarToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.salvarToolStripMenuItem.Text = "Salvar";
            this.salvarToolStripMenuItem.Click += new System.EventHandler(this.salvarToolStripMenuItem_Click);
            // 
            // novoToolStripMenuItem
            // 
            this.novoToolStripMenuItem.Name = "novoToolStripMenuItem";
            this.novoToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.novoToolStripMenuItem.Text = "Novo";
            this.novoToolStripMenuItem.Click += new System.EventHandler(this.novoToolStripMenuItem_Click);
            // 
            // testeToolStripMenuItem
            // 
            this.testeToolStripMenuItem.Name = "testeToolStripMenuItem";
            this.testeToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.testeToolStripMenuItem.Text = "Teste";
            this.testeToolStripMenuItem.Visible = false;
            this.testeToolStripMenuItem.Click += new System.EventHandler(this.testeToolStripMenuItem_Click);
            // 
            // adicionarLogoToolStripMenuItem
            // 
            this.adicionarLogoToolStripMenuItem.Name = "adicionarLogoToolStripMenuItem";
            this.adicionarLogoToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.adicionarLogoToolStripMenuItem.Text = "Adicionar Logo";
            this.adicionarLogoToolStripMenuItem.Visible = false;
            this.adicionarLogoToolStripMenuItem.Click += new System.EventHandler(this.adicionarLogoToolStripMenuItem_Click);
            // 
            // btnOrganizar
            // 
            this.btnOrganizar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnOrganizar.Image = ((System.Drawing.Image)(resources.GetObject("btnOrganizar.Image")));
            this.btnOrganizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOrganizar.Name = "btnOrganizar";
            this.btnOrganizar.Size = new System.Drawing.Size(92, 22);
            this.btnOrganizar.Text = "Organizar fotos";
            this.btnOrganizar.Click += new System.EventHandler(this.btnOrganizar_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(92, 22);
            this.toolStripButton1.Text = "Adicionar Logo";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // btnOpcoes
            // 
            this.btnOpcoes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnOpcoes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chkMostrarCoordenadas,
            this.chkMostrarDataHora,
            this.toolStripMenuItem1});
            this.btnOpcoes.Image = ((System.Drawing.Image)(resources.GetObject("btnOpcoes.Image")));
            this.btnOpcoes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpcoes.Name = "btnOpcoes";
            this.btnOpcoes.Size = new System.Drawing.Size(60, 22);
            this.btnOpcoes.Text = "Opções";
            // 
            // chkMostrarCoordenadas
            // 
            this.chkMostrarCoordenadas.Name = "chkMostrarCoordenadas";
            this.chkMostrarCoordenadas.Size = new System.Drawing.Size(191, 22);
            this.chkMostrarCoordenadas.Text = "Exportar Coordenadas";
            this.chkMostrarCoordenadas.Click += new System.EventHandler(this.chkMostrarCoordenadas_Click);
            // 
            // chkMostrarDataHora
            // 
            this.chkMostrarDataHora.Name = "chkMostrarDataHora";
            this.chkMostrarDataHora.Size = new System.Drawing.Size(191, 22);
            this.chkMostrarDataHora.Text = "Exportar Data e Hora";
            this.chkMostrarDataHora.Click += new System.EventHandler(this.chkMostrarDataHora_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(188, 6);
            // 
            // btnRotacionar
            // 
            this.btnRotacionar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnRotacionar.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnR90,
            this.btnR180,
            this.btnR_90,
            this.defaultToolStripMenuItem});
            this.btnRotacionar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRotacionar.Name = "btnRotacionar";
            this.btnRotacionar.Size = new System.Drawing.Size(77, 22);
            this.btnRotacionar.Text = "Rotacionar";
            // 
            // btnR90
            // 
            this.btnR90.Name = "btnR90";
            this.btnR90.Size = new System.Drawing.Size(112, 22);
            this.btnR90.Text = "90°";
            this.btnR90.Click += new System.EventHandler(this.btnR90_Click);
            // 
            // btnR180
            // 
            this.btnR180.Name = "btnR180";
            this.btnR180.Size = new System.Drawing.Size(112, 22);
            this.btnR180.Text = "180°";
            this.btnR180.Click += new System.EventHandler(this.btnR180_Click);
            // 
            // btnR_90
            // 
            this.btnR_90.Name = "btnR_90";
            this.btnR_90.Size = new System.Drawing.Size(112, 22);
            this.btnR_90.Text = "270°";
            this.btnR_90.Click += new System.EventHandler(this.btnR_90_Click);
            // 
            // defaultToolStripMenuItem
            // 
            this.defaultToolStripMenuItem.Name = "defaultToolStripMenuItem";
            this.defaultToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.defaultToolStripMenuItem.Text = "Default";
            this.defaultToolStripMenuItem.Click += new System.EventHandler(this.defaultToolStripMenuItem_Click);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chaveDoProdutoToolStripMenuItem});
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(50, 22);
            this.toolStripDropDownButton2.Text = "Sobre";
            this.toolStripDropDownButton2.Visible = false;
            // 
            // chaveDoProdutoToolStripMenuItem
            // 
            this.chaveDoProdutoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copiarParaAÁreaDeTransferênciaToolStripMenuItem});
            this.chaveDoProdutoToolStripMenuItem.Name = "chaveDoProdutoToolStripMenuItem";
            this.chaveDoProdutoToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.chaveDoProdutoToolStripMenuItem.Text = "Chave do produto";
            this.chaveDoProdutoToolStripMenuItem.Click += new System.EventHandler(this.chaveDoProdutoToolStripMenuItem_Click);
            // 
            // copiarParaAÁreaDeTransferênciaToolStripMenuItem
            // 
            this.copiarParaAÁreaDeTransferênciaToolStripMenuItem.Name = "copiarParaAÁreaDeTransferênciaToolStripMenuItem";
            this.copiarParaAÁreaDeTransferênciaToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.copiarParaAÁreaDeTransferênciaToolStripMenuItem.Text = "Copiar para a área de transferência";
            this.copiarParaAÁreaDeTransferênciaToolStripMenuItem.Click += new System.EventHandler(this.copiarParaAÁreaDeTransferênciaToolStripMenuItem_Click);
            // 
            // chkMarcar
            // 
            this.chkMarcar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkMarcar.AutoSize = true;
            this.chkMarcar.Location = new System.Drawing.Point(195, 368);
            this.chkMarcar.Name = "chkMarcar";
            this.chkMarcar.Size = new System.Drawing.Size(224, 17);
            this.chkMarcar.TabIndex = 9;
            this.chkMarcar.Text = "Marcar/Desmarcar todos para exportação";
            this.chkMarcar.UseVisualStyleBackColor = true;
            this.chkMarcar.CheckedChanged += new System.EventHandler(this.chkMarcar_CheckedChanged);
            // 
            // lblMsg
            // 
            this.lblMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMsg.AutoSize = true;
            this.lblMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(192, 414);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(20, 16);
            this.lblMsg.TabIndex = 11;
            this.lblMsg.Text = "...";
            // 
            // rtfRelatorio
            // 
            this.rtfRelatorio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtfRelatorio.Location = new System.Drawing.Point(12, 125);
            this.rtfRelatorio.Name = "rtfRelatorio";
            this.rtfRelatorio.Size = new System.Drawing.Size(877, 45);
            this.rtfRelatorio.TabIndex = 13;
            this.rtfRelatorio.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 16);
            this.label3.TabIndex = 12;
            this.label3.Text = "Relatório:";
            // 
            // chkVertical
            // 
            this.chkVertical.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkVertical.AutoSize = true;
            this.chkVertical.Location = new System.Drawing.Point(195, 394);
            this.chkVertical.Name = "chkVertical";
            this.chkVertical.Size = new System.Drawing.Size(118, 17);
            this.chkVertical.TabIndex = 15;
            this.chkVertical.Text = "Exportar na Vertical";
            this.chkVertical.UseVisualStyleBackColor = true;
            this.chkVertical.CheckedChanged += new System.EventHandler(this.chkVertical_CheckedChanged);
            // 
            // tmrVersao
            // 
            this.tmrVersao.Tick += new System.EventHandler(this.tmrVersao_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(11, 363);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(157, 86);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblVersao,
            this.pgb});
            this.statusStrip1.Location = new System.Drawing.Point(0, 450);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(928, 22);
            this.statusStrip1.TabIndex = 16;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblVersao
            // 
            this.lblVersao.Name = "lblVersao";
            this.lblVersao.Size = new System.Drawing.Size(16, 17);
            this.lblVersao.Text = "...";
            // 
            // pgb
            // 
            this.pgb.Name = "pgb";
            this.pgb.Size = new System.Drawing.Size(300, 16);
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(928, 472);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.chkVertical);
            this.Controls.Add(this.rtfRelatorio);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.chkMarcar);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.btnAvancar);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nudQtd);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.rtfTitulo);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Principal";
            this.Text = "Engeselt Software";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Principal_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Principal_FormClosed);
            this.Load += new System.EventHandler(this.Principal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQtd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errp)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnAvancar;
        private System.Windows.Forms.ErrorProvider errp;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem arquivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salvarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem novoToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnOrganizar;
        private System.Windows.Forms.ToolStripMenuItem testeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adicionarLogoToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.RichTextBox rtfTitulo;
        public System.Windows.Forms.DataGridView dgv;
        public System.Windows.Forms.NumericUpDown nudQtd;
        public System.Windows.Forms.CheckBox chkMarcar;
        public System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.RichTextBox rtfRelatorio;
        public System.Windows.Forms.Label lblMsg;
        public System.Windows.Forms.CheckBox chkVertical;
        private System.Windows.Forms.ToolStripDropDownButton btnOpcoes;
        private System.Windows.Forms.ToolStripMenuItem chkMostrarCoordenadas;
        private System.Windows.Forms.ToolStripMenuItem chkMostrarDataHora;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem chaveDoProdutoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copiarParaAÁreaDeTransferênciaToolStripMenuItem;
        private System.Windows.Forms.Timer tmrVersao;
        private System.Windows.Forms.DataGridViewCheckBoxColumn EXPORTAR;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewImageColumn FOTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DESCRICAO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DATA;
        private System.Windows.Forms.DataGridViewTextBoxColumn CAMINHO;
        private System.Windows.Forms.DataGridViewTextBoxColumn LAT;
        private System.Windows.Forms.DataGridViewTextBoxColumn LNG;
        private System.Windows.Forms.DataGridViewComboBoxColumn ROTACAO;
        private System.Windows.Forms.ToolStripDropDownButton btnRotacionar;
        private System.Windows.Forms.ToolStripMenuItem btnR90;
        private System.Windows.Forms.ToolStripMenuItem btnR180;
        private System.Windows.Forms.ToolStripMenuItem btnR_90;
        private System.Windows.Forms.ToolStripMenuItem defaultToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblVersao;
        public System.Windows.Forms.ToolStripProgressBar pgb;
    }
}

