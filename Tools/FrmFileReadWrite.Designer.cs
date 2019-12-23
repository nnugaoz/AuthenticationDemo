namespace Tools
{
    partial class FrmFileReadWrite
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
            this.treeDir = new System.Windows.Forms.TreeView();
            this.lstFile = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCurrentPath = new System.Windows.Forms.TextBox();
            this.btnCreateFile = new System.Windows.Forms.Button();
            this.btnWriteFile = new System.Windows.Forms.Button();
            this.btnReadFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treeDir
            // 
            this.treeDir.Location = new System.Drawing.Point(12, 60);
            this.treeDir.Margin = new System.Windows.Forms.Padding(6);
            this.treeDir.Name = "treeDir";
            this.treeDir.Size = new System.Drawing.Size(294, 864);
            this.treeDir.TabIndex = 0;
            this.treeDir.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeDir_AfterSelect);
            this.treeDir.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeDir_MouseDoubleClick);
            // 
            // lstFile
            // 
            this.lstFile.Location = new System.Drawing.Point(324, 60);
            this.lstFile.Margin = new System.Windows.Forms.Padding(6);
            this.lstFile.Name = "lstFile";
            this.lstFile.Size = new System.Drawing.Size(498, 864);
            this.lstFile.TabIndex = 1;
            this.lstFile.UseCompatibleStateImageBehavior = false;
            this.lstFile.View = System.Windows.Forms.View.List;
            this.lstFile.SelectedIndexChanged += new System.EventHandler(this.lstFile_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "目录列表";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(320, 18);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "文件列表";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(834, 18);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "当前目录";
            // 
            // txtCurrentPath
            // 
            this.txtCurrentPath.Location = new System.Drawing.Point(838, 60);
            this.txtCurrentPath.Margin = new System.Windows.Forms.Padding(6);
            this.txtCurrentPath.Name = "txtCurrentPath";
            this.txtCurrentPath.Size = new System.Drawing.Size(794, 35);
            this.txtCurrentPath.TabIndex = 5;
            // 
            // btnCreateFile
            // 
            this.btnCreateFile.Location = new System.Drawing.Point(838, 128);
            this.btnCreateFile.Margin = new System.Windows.Forms.Padding(6);
            this.btnCreateFile.Name = "btnCreateFile";
            this.btnCreateFile.Size = new System.Drawing.Size(326, 46);
            this.btnCreateFile.TabIndex = 6;
            this.btnCreateFile.Text = "创建文件";
            this.btnCreateFile.UseVisualStyleBackColor = true;
            this.btnCreateFile.Click += new System.EventHandler(this.btnCreateFile_Click);
            // 
            // btnWriteFile
            // 
            this.btnWriteFile.Location = new System.Drawing.Point(838, 212);
            this.btnWriteFile.Margin = new System.Windows.Forms.Padding(6);
            this.btnWriteFile.Name = "btnWriteFile";
            this.btnWriteFile.Size = new System.Drawing.Size(326, 46);
            this.btnWriteFile.TabIndex = 7;
            this.btnWriteFile.Text = "写入文件";
            this.btnWriteFile.UseVisualStyleBackColor = true;
            this.btnWriteFile.Click += new System.EventHandler(this.btnWriteFile_Click);
            // 
            // btnReadFile
            // 
            this.btnReadFile.Location = new System.Drawing.Point(838, 301);
            this.btnReadFile.Margin = new System.Windows.Forms.Padding(6);
            this.btnReadFile.Name = "btnReadFile";
            this.btnReadFile.Size = new System.Drawing.Size(326, 46);
            this.btnReadFile.TabIndex = 8;
            this.btnReadFile.Text = "读取文件";
            this.btnReadFile.UseVisualStyleBackColor = true;
            this.btnReadFile.Click += new System.EventHandler(this.btnReadFile_Click);
            // 
            // FrmDriver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1664, 948);
            this.Controls.Add(this.btnReadFile);
            this.Controls.Add(this.btnWriteFile);
            this.Controls.Add(this.btnCreateFile);
            this.Controls.Add(this.txtCurrentPath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstFile);
            this.Controls.Add(this.treeDir);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "FrmDriver";
            this.Text = "FrmDriver";
            this.Load += new System.EventHandler(this.FrmDriver_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeDir;
        private System.Windows.Forms.ListView lstFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCurrentPath;
        private System.Windows.Forms.Button btnCreateFile;
        private System.Windows.Forms.Button btnWriteFile;
        private System.Windows.Forms.Button btnReadFile;
    }
}