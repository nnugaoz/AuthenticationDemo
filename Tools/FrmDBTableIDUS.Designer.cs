namespace Tools
{
    partial class FrmDBTableIDUS
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lstDB = new System.Windows.Forms.ListBox();
            this.lstTable = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnGenerateEntityClass = new System.Windows.Forms.Button();
            this.btnGenerateDao = new System.Windows.Forms.Button();
            this.dgvFields = new System.Windows.Forms.DataGridView();
            this.btnCreateHtmlListPage = new System.Windows.Forms.Button();
            this.btnCreateHtmlEditPage = new System.Windows.Forms.Button();
            this.btnCreateHandler = new System.Windows.Forms.Button();
            this.btnCreatePagePS = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFields)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(250, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sql Server链接字符串";
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Location = new System.Drawing.Point(16, 48);
            this.txtConnectionString.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(1134, 35);
            this.txtConnectionString.TabIndex = 1;
            this.txtConnectionString.Text = "Data Source=127.0.0.1;Persist Security Info=True;User ID=sa;Password=1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 268);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "数据库列表";
            // 
            // lstDB
            // 
            this.lstDB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstDB.FormattingEnabled = true;
            this.lstDB.ItemHeight = 24;
            this.lstDB.Location = new System.Drawing.Point(18, 294);
            this.lstDB.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.lstDB.Name = "lstDB";
            this.lstDB.Size = new System.Drawing.Size(302, 580);
            this.lstDB.TabIndex = 3;
            this.lstDB.SelectedIndexChanged += new System.EventHandler(this.lstDB_SelectedIndexChanged);
            // 
            // lstTable
            // 
            this.lstTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstTable.FormattingEnabled = true;
            this.lstTable.ItemHeight = 24;
            this.lstTable.Location = new System.Drawing.Point(334, 294);
            this.lstTable.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.lstTable.Name = "lstTable";
            this.lstTable.Size = new System.Drawing.Size(400, 580);
            this.lstTable.TabIndex = 5;
            this.lstTable.SelectedIndexChanged += new System.EventHandler(this.lstTable_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(332, 268);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "数据库表列表";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(746, 268);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 24);
            this.label4.TabIndex = 6;
            this.label4.Text = "字段列表";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(1166, 40);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(170, 46);
            this.btnConnect.TabIndex = 8;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnGenerateEntityClass
            // 
            this.btnGenerateEntityClass.Location = new System.Drawing.Point(18, 94);
            this.btnGenerateEntityClass.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnGenerateEntityClass.Name = "btnGenerateEntityClass";
            this.btnGenerateEntityClass.Size = new System.Drawing.Size(150, 46);
            this.btnGenerateEntityClass.TabIndex = 9;
            this.btnGenerateEntityClass.Text = "创建实体类";
            this.btnGenerateEntityClass.UseVisualStyleBackColor = true;
            this.btnGenerateEntityClass.Click += new System.EventHandler(this.btnGenerateEntityClass_Click);
            // 
            // btnGenerateDao
            // 
            this.btnGenerateDao.Location = new System.Drawing.Point(182, 94);
            this.btnGenerateDao.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnGenerateDao.Name = "btnGenerateDao";
            this.btnGenerateDao.Size = new System.Drawing.Size(240, 46);
            this.btnGenerateDao.TabIndex = 10;
            this.btnGenerateDao.Text = "创建数据表操作类";
            this.btnGenerateDao.UseVisualStyleBackColor = true;
            this.btnGenerateDao.Click += new System.EventHandler(this.btnGenerateDao_Click);
            // 
            // dgvFields
            // 
            this.dgvFields.AllowUserToAddRows = false;
            this.dgvFields.AllowUserToDeleteRows = false;
            this.dgvFields.AllowUserToResizeRows = false;
            this.dgvFields.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFields.Location = new System.Drawing.Point(744, 294);
            this.dgvFields.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvFields.Name = "dgvFields";
            this.dgvFields.RowHeadersVisible = false;
            this.dgvFields.RowTemplate.Height = 37;
            this.dgvFields.Size = new System.Drawing.Size(528, 578);
            this.dgvFields.TabIndex = 11;
            // 
            // btnCreateHtmlListPage
            // 
            this.btnCreateHtmlListPage.Location = new System.Drawing.Point(434, 94);
            this.btnCreateHtmlListPage.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnCreateHtmlListPage.Name = "btnCreateHtmlListPage";
            this.btnCreateHtmlListPage.Size = new System.Drawing.Size(286, 46);
            this.btnCreateHtmlListPage.TabIndex = 12;
            this.btnCreateHtmlListPage.Text = "创建HTML列表页面";
            this.btnCreateHtmlListPage.UseVisualStyleBackColor = true;
            this.btnCreateHtmlListPage.Click += new System.EventHandler(this.btnCreateHtmlListPage_Click);
            // 
            // btnCreateHtmlEditPage
            // 
            this.btnCreateHtmlEditPage.Location = new System.Drawing.Point(732, 94);
            this.btnCreateHtmlEditPage.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnCreateHtmlEditPage.Name = "btnCreateHtmlEditPage";
            this.btnCreateHtmlEditPage.Size = new System.Drawing.Size(286, 46);
            this.btnCreateHtmlEditPage.TabIndex = 13;
            this.btnCreateHtmlEditPage.Text = "创建HTML新增/编辑页面";
            this.btnCreateHtmlEditPage.UseVisualStyleBackColor = true;
            this.btnCreateHtmlEditPage.Click += new System.EventHandler(this.btnCreateHtmlEditPage_Click);
            // 
            // btnCreateHandler
            // 
            this.btnCreateHandler.Location = new System.Drawing.Point(1030, 94);
            this.btnCreateHandler.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnCreateHandler.Name = "btnCreateHandler";
            this.btnCreateHandler.Size = new System.Drawing.Size(264, 46);
            this.btnCreateHandler.TabIndex = 14;
            this.btnCreateHandler.Text = "创建一般处理程序";
            this.btnCreateHandler.UseVisualStyleBackColor = true;
            this.btnCreateHandler.Click += new System.EventHandler(this.btnCreateHandler_Click);
            // 
            // btnCreatePagePS
            // 
            this.btnCreatePagePS.Location = new System.Drawing.Point(19, 152);
            this.btnCreatePagePS.Margin = new System.Windows.Forms.Padding(6);
            this.btnCreatePagePS.Name = "btnCreatePagePS";
            this.btnCreatePagePS.Size = new System.Drawing.Size(264, 46);
            this.btnCreatePagePS.TabIndex = 15;
            this.btnCreatePagePS.Text = "创建分页存储过程";
            this.btnCreatePagePS.UseVisualStyleBackColor = true;
            this.btnCreatePagePS.Click += new System.EventHandler(this.btnCreatePagePS_Click);
            // 
            // FrmDBTableIDUS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1372, 908);
            this.Controls.Add(this.btnCreatePagePS);
            this.Controls.Add(this.btnCreateHandler);
            this.Controls.Add(this.btnCreateHtmlEditPage);
            this.Controls.Add(this.btnCreateHtmlListPage);
            this.Controls.Add(this.dgvFields);
            this.Controls.Add(this.btnGenerateDao);
            this.Controls.Add(this.btnGenerateEntityClass);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lstTable);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lstDB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtConnectionString);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "FrmDBTableIDUS";
            this.Text = "数据库工具";
            ((System.ComponentModel.ISupportInitialize)(this.dgvFields)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lstDB;
        private System.Windows.Forms.ListBox lstTable;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnGenerateEntityClass;
        private System.Windows.Forms.Button btnGenerateDao;
        private System.Windows.Forms.DataGridView dgvFields;
        private System.Windows.Forms.Button btnCreateHtmlListPage;
        private System.Windows.Forms.Button btnCreateHtmlEditPage;
        private System.Windows.Forms.Button btnCreateHandler;
        private System.Windows.Forms.Button btnCreatePagePS;
    }
}

