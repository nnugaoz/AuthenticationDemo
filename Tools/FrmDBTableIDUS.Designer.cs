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
            this.lstField = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnGenerateEntityClass = new System.Windows.Forms.Button();
            this.btnGenerateDao = new System.Windows.Forms.Button();
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
            this.label2.Location = new System.Drawing.Point(6, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "数据库列表";
            // 
            // lstDB
            // 
            this.lstDB.FormattingEnabled = true;
            this.lstDB.ItemHeight = 12;
            this.lstDB.Location = new System.Drawing.Point(8, 73);
            this.lstDB.Name = "lstDB";
            this.lstDB.Size = new System.Drawing.Size(153, 316);
            this.lstDB.TabIndex = 3;
            this.lstDB.SelectedIndexChanged += new System.EventHandler(this.lstDB_SelectedIndexChanged);
            // 
            // lstTable
            // 
            this.lstTable.FormattingEnabled = true;
            this.lstTable.ItemHeight = 12;
            this.lstTable.Location = new System.Drawing.Point(167, 73);
            this.lstTable.Name = "lstTable";
            this.lstTable.Size = new System.Drawing.Size(202, 316);
            this.lstTable.TabIndex = 5;
            this.lstTable.SelectedIndexChanged += new System.EventHandler(this.lstTable_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(165, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "数据库表列表";
            // 
            // lstField
            // 
            this.lstField.FormattingEnabled = true;
            this.lstField.ItemHeight = 12;
            this.lstField.Location = new System.Drawing.Point(375, 73);
            this.lstField.Name = "lstField";
            this.lstField.Size = new System.Drawing.Size(202, 316);
            this.lstField.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(373, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "字段列表";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(583, 23);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(53, 23);
            this.btnConnect.TabIndex = 8;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnGenerateEntityClass
            // 
            this.btnGenerateEntityClass.Location = new System.Drawing.Point(583, 73);
            this.btnGenerateEntityClass.Name = "btnGenerateEntityClass";
            this.btnGenerateEntityClass.Size = new System.Drawing.Size(75, 23);
            this.btnGenerateEntityClass.TabIndex = 9;
            this.btnGenerateEntityClass.Text = "创建实体类";
            this.btnGenerateEntityClass.UseVisualStyleBackColor = true;
            this.btnGenerateEntityClass.Click += new System.EventHandler(this.btnGenerateEntityClass_Click);
            // 
            // btnGenerateDao
            // 
            this.btnGenerateDao.Location = new System.Drawing.Point(583, 114);
            this.btnGenerateDao.Name = "btnGenerateDao";
            this.btnGenerateDao.Size = new System.Drawing.Size(123, 23);
            this.btnGenerateDao.TabIndex = 10;
            this.btnGenerateDao.Text = "创建数据表操作类";
            this.btnGenerateDao.UseVisualStyleBackColor = true;
            // 
            // DBTableIDUS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 393);
            this.Controls.Add(this.btnGenerateDao);
            this.Controls.Add(this.btnGenerateEntityClass);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.lstField);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lstTable);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lstDB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtConnectionString);
            this.Controls.Add(this.label1);
            this.Name = "DBTableIDUS";
            this.Text = "数据库工具";
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
        private System.Windows.Forms.ListBox lstField;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnGenerateEntityClass;
        private System.Windows.Forms.Button btnGenerateDao;
    }
}

