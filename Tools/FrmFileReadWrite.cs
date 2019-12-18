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
    public partial class FrmDriver : Form
    {
        public FrmDriver()
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
                File.Create(txtCurrentPath.Text + @"\" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".txt");
            }
        }

        private void lstFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCurrentPath.Text = lstFile.SelectedItems.ToString();
        }
    }
}
