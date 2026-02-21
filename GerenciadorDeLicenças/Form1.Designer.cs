namespace GerenciadorDeLicenças
{
    partial class Form1
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
            this.dgv = new System.Windows.Forms.DataGridView();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnUpLoad = new System.Windows.Forms.Button();
            this.lblMsg = new System.Windows.Forms.Label();
            this.btnGerarBin = new System.Windows.Forms.Button();
            this.CHAVE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ANO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MES = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DIA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EMPRESA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EMAIL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FTP_USER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FTP_PASSWORD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FTP_SERVER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FTP_PORT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FTP_PATH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CHAVE,
            this.ANO,
            this.MES,
            this.DIA,
            this.EMPRESA,
            this.EMAIL,
            this.FTP_USER,
            this.FTP_PASSWORD,
            this.FTP_SERVER,
            this.FTP_PORT,
            this.FTP_PATH});
            this.dgv.Location = new System.Drawing.Point(12, 12);
            this.dgv.Name = "dgv";
            this.dgv.Size = new System.Drawing.Size(864, 308);
            this.dgv.TabIndex = 0;
            this.dgv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellClick);
            this.dgv.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.dgv_CellValueNeeded);
            this.dgv.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgv_RowsAdded);
            // 
            // btnDownload
            // 
            this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDownload.Location = new System.Drawing.Point(12, 326);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(75, 23);
            this.btnDownload.TabIndex = 1;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnUpLoad
            // 
            this.btnUpLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpLoad.Location = new System.Drawing.Point(801, 326);
            this.btnUpLoad.Name = "btnUpLoad";
            this.btnUpLoad.Size = new System.Drawing.Size(75, 23);
            this.btnUpLoad.TabIndex = 2;
            this.btnUpLoad.Text = "Upload";
            this.btnUpLoad.UseVisualStyleBackColor = true;
            this.btnUpLoad.Click += new System.EventHandler(this.btnUpLoad_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMsg.AutoSize = true;
            this.lblMsg.Location = new System.Drawing.Point(105, 331);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(16, 13);
            this.lblMsg.TabIndex = 3;
            this.lblMsg.Text = "...";
            // 
            // btnGerarBin
            // 
            this.btnGerarBin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGerarBin.Location = new System.Drawing.Point(607, 326);
            this.btnGerarBin.Name = "btnGerarBin";
            this.btnGerarBin.Size = new System.Drawing.Size(75, 23);
            this.btnGerarBin.TabIndex = 4;
            this.btnGerarBin.Text = "Gerar BIN";
            this.btnGerarBin.UseVisualStyleBackColor = true;
            this.btnGerarBin.Click += new System.EventHandler(this.btnGerarBin_Click);
            // 
            // CHAVE
            // 
            this.CHAVE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CHAVE.HeaderText = "Chave";
            this.CHAVE.Name = "CHAVE";
            // 
            // ANO
            // 
            this.ANO.HeaderText = "Ano";
            this.ANO.Name = "ANO";
            this.ANO.Width = 50;
            // 
            // MES
            // 
            this.MES.HeaderText = "Mês";
            this.MES.Name = "MES";
            this.MES.Width = 50;
            // 
            // DIA
            // 
            this.DIA.HeaderText = "Dia";
            this.DIA.Name = "DIA";
            this.DIA.Width = 50;
            // 
            // EMPRESA
            // 
            this.EMPRESA.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EMPRESA.HeaderText = "Empresa";
            this.EMPRESA.Name = "EMPRESA";
            // 
            // EMAIL
            // 
            this.EMAIL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EMAIL.HeaderText = "E-Mail";
            this.EMAIL.Name = "EMAIL";
            // 
            // FTP_USER
            // 
            this.FTP_USER.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FTP_USER.HeaderText = "FTP User";
            this.FTP_USER.Name = "FTP_USER";
            // 
            // FTP_PASSWORD
            // 
            this.FTP_PASSWORD.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FTP_PASSWORD.HeaderText = "FTP Password";
            this.FTP_PASSWORD.Name = "FTP_PASSWORD";
            // 
            // FTP_SERVER
            // 
            this.FTP_SERVER.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FTP_SERVER.HeaderText = "FTP Server";
            this.FTP_SERVER.Name = "FTP_SERVER";
            // 
            // FTP_PORT
            // 
            this.FTP_PORT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FTP_PORT.HeaderText = "FTP Port";
            this.FTP_PORT.Name = "FTP_PORT";
            // 
            // FTP_PATH
            // 
            this.FTP_PATH.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FTP_PATH.HeaderText = "FTP Path";
            this.FTP_PATH.Name = "FTP_PATH";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 361);
            this.Controls.Add(this.btnGerarBin);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.btnUpLoad);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.dgv);
            this.Name = "Form1";
            this.Text = "Gerenciador de Licenças";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnUpLoad;
        public System.Windows.Forms.DataGridView dgv;
        public System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Button btnGerarBin;
        private System.Windows.Forms.DataGridViewTextBoxColumn CHAVE;
        private System.Windows.Forms.DataGridViewTextBoxColumn ANO;
        private System.Windows.Forms.DataGridViewTextBoxColumn MES;
        private System.Windows.Forms.DataGridViewTextBoxColumn DIA;
        private System.Windows.Forms.DataGridViewTextBoxColumn EMPRESA;
        private System.Windows.Forms.DataGridViewTextBoxColumn EMAIL;
        private System.Windows.Forms.DataGridViewTextBoxColumn FTP_USER;
        private System.Windows.Forms.DataGridViewTextBoxColumn FTP_PASSWORD;
        private System.Windows.Forms.DataGridViewTextBoxColumn FTP_SERVER;
        private System.Windows.Forms.DataGridViewTextBoxColumn FTP_PORT;
        private System.Windows.Forms.DataGridViewTextBoxColumn FTP_PATH;
    }
}

