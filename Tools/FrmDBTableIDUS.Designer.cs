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
            this.btnCreateHtmlNewPage = new System.Windows.Forms.Button();
            this.btnCreateHandler = new System.Windows.Forms.Button();
            this.btnCreatePagePS = new System.Windows.Forms.Button();
            this.btnCreateHtmlEditPage = new System.Windows.Forms.Button();
            this.btnOneKey = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFields)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sql Server链接字符串";
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Location = new System.Drawing.Point(8, 24);
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(569, 21);
            this.txtConnectionString.TabIndex = 1;
            this.txtConnectionString.Text = "Data Source=127.0.0.1;Persist Security Info=True;User ID=sa;Password=1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "数据库列表";
            // 
            // lstDB
            // 
            this.lstDB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstDB.FormattingEnabled = true;
            this.lstDB.ItemHeight = 12;
            this.lstDB.Location = new System.Drawing.Point(9, 147);
            this.lstDB.Name = "lstDB";
            this.lstDB.Size = new System.Drawing.Size(153, 364);
            this.lstDB.TabIndex = 3;
            this.lstDB.SelectedIndexChanged += new System.EventHandler(this.lstDB_SelectedIndexChanged);
            // 
            // lstTable
            // 
            this.lstTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstTable.FormattingEnabled = true;
            this.lstTable.ItemHeight = 12;
            this.lstTable.Location = new System.Drawing.Point(167, 147);
            this.lstTable.Name = "lstTable";
            this.lstTable.Size = new System.Drawing.Size(202, 364);
            this.lstTable.TabIndex = 5;
            this.lstTable.SelectedIndexChanged += new System.EventHandler(this.lstTable_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(166, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "数据库表列表";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(373, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "字段列表";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(583, 24);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(85, 23);
            this.btnConnect.TabIndex = 8;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnGenerateEntityClass
            // 
            this.btnGenerateEntityClass.Location = new System.Drawing.Point(8, 47);
            this.btnGenerateEntityClass.Name = "btnGenerateEntityClass";
            this.btnGenerateEntityClass.Size = new System.Drawing.Size(75, 23);
            this.btnGenerateEntityClass.TabIndex = 9;
            this.btnGenerateEntityClass.Text = "创建实体类";
            this.btnGenerateEntityClass.UseVisualStyleBackColor = true;
            this.btnGenerateEntityClass.Click += new System.EventHandler(this.btnGenerateEntityClass_Click);
            // 
            // btnGenerateDao
            // 
            this.btnGenerateDao.Location = new System.Drawing.Point(91, 47);
            this.btnGenerateDao.Name = "btnGenerateDao";
            this.btnGenerateDao.Size = new System.Drawing.Size(120, 23);
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
            this.dgvFields.Location = new System.Drawing.Point(372, 147);
            this.dgvFields.Margin = new System.Windows.Forms.Padding(1);
            this.dgvFields.Name = "dgvFields";
            this.dgvFields.RowHeadersVisible = false;
            this.dgvFields.RowTemplate.Height = 37;
            this.dgvFields.Size = new System.Drawing.Size(691, 365);
            this.dgvFields.TabIndex = 11;
            // 
            // btnCreateHtmlListPage
            // 
            this.btnCreateHtmlListPage.Location = new System.Drawing.Point(217, 47);
            this.btnCreateHtmlListPage.Name = "btnCreateHtmlListPage";
            this.btnCreateHtmlListPage.Size = new System.Drawing.Size(143, 23);
            this.btnCreateHtmlListPage.TabIndex = 12;
            this.btnCreateHtmlListPage.Text = "创建HTML列表页面";
            this.btnCreateHtmlListPage.UseVisualStyleBackColor = true;
            this.btnCreateHtmlListPage.Click += new System.EventHandler(this.btnCreateHtmlListPage_Click);
            // 
            // btnCreateHtmlNewPage
            // 
            this.btnCreateHtmlNewPage.Location = new System.Drawing.Point(366, 47);
            this.btnCreateHtmlNewPage.Name = "btnCreateHtmlNewPage";
            this.btnCreateHtmlNewPage.Size = new System.Drawing.Size(143, 23);
            this.btnCreateHtmlNewPage.TabIndex = 13;
            this.btnCreateHtmlNewPage.Text = "创建HTML新增页面";
            this.btnCreateHtmlNewPage.UseVisualStyleBackColor = true;
            this.btnCreateHtmlNewPage.Click += new System.EventHandler(this.btnCreateHtmlNewPage_Click);
            // 
            // btnCreateHandler
            // 
            this.btnCreateHandler.Location = new System.Drawing.Point(8, 76);
            this.btnCreateHandler.Name = "btnCreateHandler";
            this.btnCreateHandler.Size = new System.Drawing.Size(132, 23);
            this.btnCreateHandler.TabIndex = 14;
            this.btnCreateHandler.Text = "创建一般处理程序";
            this.btnCreateHandler.UseVisualStyleBackColor = true;
            this.btnCreateHandler.Click += new System.EventHandler(this.btnCreateHandler_Click);
            // 
            // btnCreatePagePS
            // 
            this.btnCreatePagePS.Location = new System.Drawing.Point(148, 76);
            this.btnCreatePagePS.Name = "btnCreatePagePS";
            this.btnCreatePagePS.Size = new System.Drawing.Size(132, 23);
            this.btnCreatePagePS.TabIndex = 15;
            this.btnCreatePagePS.Text = "创建分页存储过程";
            this.btnCreatePagePS.UseVisualStyleBackColor = true;
            this.btnCreatePagePS.Click += new System.EventHandler(this.btnCreatePagePS_Click);
            // 
            // btnCreateHtmlEditPage
            // 
            this.btnCreateHtmlEditPage.Location = new System.Drawing.Point(516, 47);
            this.btnCreateHtmlEditPage.Name = "btnCreateHtmlEditPage";
            this.btnCreateHtmlEditPage.Size = new System.Drawing.Size(120, 23);
            this.btnCreateHtmlEditPage.TabIndex = 16;
            this.btnCreateHtmlEditPage.Text = "创建HTML编辑页面";
            this.btnCreateHtmlEditPage.UseVisualStyleBackColor = true;
            this.btnCreateHtmlEditPage.Click += new System.EventHandler(this.btnCreateHtmlEditPage_Click);
            // 
            // btnOneKey
            // 
            this.btnOneKey.Location = new System.Drawing.Point(286, 76);
            this.btnOneKey.Name = "btnOneKey";
            this.btnOneKey.Size = new System.Drawing.Size(350, 23);
            this.btnOneKey.TabIndex = 17;
            this.btnOneKey.Text = "根据数据库表一键生成增、删、改、查页面及代码";
            this.btnOneKey.UseVisualStyleBackColor = true;
            this.btnOneKey.Click += new System.EventHandler(this.btnOneKey_Click);
            // 
            // FrmDBTableIDUS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1073, 530);
            this.Controls.Add(this.btnOneKey);
            this.Controls.Add(this.btnCreateHtmlEditPage);
            this.Controls.Add(this.btnCreatePagePS);
            this.Controls.Add(this.btnCreateHandler);
            this.Controls.Add(this.btnCreateHtmlNewPage);
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
        private System.Windows.Forms.Button btnCreateHtmlNewPage;
        private System.Windows.Forms.Button btnCreateHandler;
        private System.Windows.Forms.Button btnCreatePagePS;
        private System.Windows.Forms.Button btnCreateHtmlEditPage;
        private System.Windows.Forms.Button btnOneKey;
    }
}

