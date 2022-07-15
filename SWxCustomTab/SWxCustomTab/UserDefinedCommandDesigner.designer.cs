namespace SWxCustomTab
{
    partial class UserDefinedCommandDesigner
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pCenter = new System.Windows.Forms.Panel();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDocType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colImage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCmdType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colFilename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colModule = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFunction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWaitForExit = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colArgs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pBottom = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.pBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pCenter
            // 
            this.pCenter.Controls.Add(this.dgv);
            this.pCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pCenter.Location = new System.Drawing.Point(0, 0);
            this.pCenter.Name = "pCenter";
            this.pCenter.Size = new System.Drawing.Size(984, 561);
            this.pCenter.TabIndex = 0;
            // 
            // dgv
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgv.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colTitle,
            this.colDescription,
            this.colDocType,
            this.colImage,
            this.colCmdType,
            this.colFilename,
            this.colModule,
            this.colFunction,
            this.colWaitForExit,
            this.colArgs});
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersWidth = 32;
            this.dgv.Size = new System.Drawing.Size(984, 561);
            this.dgv.TabIndex = 0;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "Name";
            this.colName.FillWeight = 82.91032F;
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            // 
            // colTitle
            // 
            this.colTitle.DataPropertyName = "Title";
            this.colTitle.FillWeight = 82.91032F;
            this.colTitle.HeaderText = "Titel";
            this.colTitle.Name = "colTitle";
            // 
            // colDescription
            // 
            this.colDescription.DataPropertyName = "Description";
            this.colDescription.FillWeight = 82.91032F;
            this.colDescription.HeaderText = "Beschreibung";
            this.colDescription.Name = "colDescription";
            // 
            // colDocType
            // 
            this.colDocType.DataPropertyName = "DocType";
            this.colDocType.FillWeight = 82.91032F;
            this.colDocType.HeaderText = "sichtbar in";
            this.colDocType.Name = "colDocType";
            // 
            // colImage
            // 
            this.colImage.DataPropertyName = "ImageIndex";
            this.colImage.FillWeight = 50F;
            this.colImage.HeaderText = "Bild-Index";
            this.colImage.Name = "colImage";
            // 
            // colCmdType
            // 
            this.colCmdType.DataPropertyName = "CommandType";
            this.colCmdType.FillWeight = 82.91032F;
            this.colCmdType.HeaderText = "Art";
            this.colCmdType.Items.AddRange(new object[] {
            "Macro",
            "Process",
            "DLL"});
            this.colCmdType.Name = "colCmdType";
            // 
            // colFilename
            // 
            this.colFilename.DataPropertyName = "Filename";
            this.colFilename.FillWeight = 82.91032F;
            this.colFilename.HeaderText = "Dateiname";
            this.colFilename.Name = "colFilename";
            // 
            // colModule
            // 
            this.colModule.DataPropertyName = "ModuleName";
            this.colModule.FillWeight = 82.91032F;
            this.colModule.HeaderText = "Modul-Name (Makro)";
            this.colModule.Name = "colModule";
            // 
            // colFunction
            // 
            this.colFunction.DataPropertyName = "FunctionName";
            this.colFunction.FillWeight = 82.91032F;
            this.colFunction.HeaderText = "Funktion-Name (Makro)";
            this.colFunction.Name = "colFunction";
            // 
            // colWaitForExit
            // 
            this.colWaitForExit.DataPropertyName = "WaitForExit";
            this.colWaitForExit.FillWeight = 82.91032F;
            this.colWaitForExit.HeaderText = "auf Beendigung warten";
            this.colWaitForExit.Name = "colWaitForExit";
            // 
            // colArgs
            // 
            this.colArgs.DataPropertyName = "Arguments";
            this.colArgs.HeaderText = "Argumente";
            this.colArgs.Name = "colArgs";
            // 
            // pBottom
            // 
            this.pBottom.Controls.Add(this.pictureBox1);
            this.pBottom.Controls.Add(this.btnSave);
            this.pBottom.Controls.Add(this.btnClose);
            this.pBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pBottom.Location = new System.Drawing.Point(0, 497);
            this.pBottom.Name = "pBottom";
            this.pBottom.Size = new System.Drawing.Size(984, 64);
            this.pBottom.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(716, 27);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(125, 25);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Speichern";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(847, 27);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(125, 25);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Schließen";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SWxCustomTab.Properties.Resources.Logo_200;
            this.pictureBox1.Location = new System.Drawing.Point(7, 7);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // UserDefinedCommandDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.pBottom);
            this.Controls.Add(this.pCenter);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(800, 400);
            this.Name = "UserDefinedCommandDesigner";
            this.Text = "Einstellungen";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UserDefinedCommandDesigner_FormClosing);
            this.pCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.pBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pCenter;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Panel pBottom;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private System.Windows.Forms.DataGridViewComboBoxColumn colDocType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colImage;
        private System.Windows.Forms.DataGridViewComboBoxColumn colCmdType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFilename;
        private System.Windows.Forms.DataGridViewTextBoxColumn colModule;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFunction;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colWaitForExit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colArgs;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}