using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tools
{
    public partial class FrmFileReadWrite : Form
    {
        public FrmFileReadWrite()
        {
            InitializeComponent();
        }

        private void FrmDriver_Load(object sender, EventArgs e)
        {
            DriveInfo[] drivers = DriveInfo.GetDrives();
            foreach (DriveInfo driver in drivers)
            {
                if (driver.DriveType == DriveType.Fixed && driver.DriveFormat == "NTFS")
                {
                    treeDir.Nodes.Add(driver.Name);
                }
            }
        }

        private void treeDir_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string lSelectedDir = treeDir.SelectedNode.Text;
            string[] lSubDir = Directory.GetDirectories(lSelectedDir);

            treeDir.SelectedNode.Nodes.Clear();
            foreach (string lDir in lSubDir)
            {
                treeDir.SelectedNode.Nodes.Add(lDir);
            }

            string[] lSubFile = Directory.GetFiles(lSelectedDir);

            lstFile.Items.Clear();
            foreach (string lFile in lSubFile)
            {
                lstFile.Items.Add(lFile);
            }
        }

        private void treeDir_AfterSelect(object sender, TreeViewEventArgs e)
        {
            txtCurrentPath.Text = treeDir.SelectedNode.Text;
        }

        private void btnCreateFile_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(txtCurrentPath.Text))
            {
                FileStream lFileStream = File.Create(txtCurrentPath.Text + @"\" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".txt");

                byte[] lData = System.Text.Encoding.UTF8.GetBytes("你好");
                lFileStream.Write(lData, 0, lData.Length);
                lFileStream.Close();

                FileInfo lFileInfo = new FileInfo(txtCurrentPath.Text + @"\" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff") + ".txt");

                if (!lFileInfo.Exists)
                {
                    lFileStream = lFileInfo.Create();
                }
                else
                {
                    lFileStream = lFileInfo.Open(FileMode.Append);
                }

                lData = System.Text.Encoding.UTF8.GetBytes("您好");
                lFileStream.Write(lData, 0, lData.Length);
                lFileStream.Close();
            }
        }

        private void lstFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstFile.SelectedItems.Count > 0)
            {
                txtCurrentPath.Text = lstFile.SelectedItems[0].Text.ToString();
            }
        }

        private void btnWriteFile_Click(object sender, EventArgs e)
        {
            FileStream lFileStream = File.Open(txtCurrentPath.Text, FileMode.Append);
            String lContent = "星期四";
            byte[] lData = System.Text.Encoding.UTF8.GetBytes(lContent);
            lFileStream.Write(lData, 0, lData.Length);
            lFileStream.Close();
        }

        private void btnReadFile_Click(object sender, EventArgs e)
        {
            FileStream lFileStream = File.OpenRead(txtCurrentPath.Text);
            string lContent = "";
            byte[] lData = new byte[1024];

            while (lFileStream.Read(lData, 0, lData.Length) > 0)
            {
                lContent += System.Text.Encoding.UTF8.GetString((lData));
            }

            lFileStream.Close();
            MessageBox.Show(lContent);
        }
    }
}
