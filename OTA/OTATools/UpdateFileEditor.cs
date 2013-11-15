using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OTATools
{
    public partial class UpdateFileEditor : Form
    {
        private UpdateFileInfo update;
        public UpdateFileEditor(UpdateFileInfo update)
        {
            this.update = update;
            InitializeComponent();
        }

        private void buttonCommit_Click(object sender, EventArgs e)
        {
            update.Description = this.textBoxDescription.Text.Trim();
            update.ChangeLog = this.richTextBoxChangeLog.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateFileEditor_Load(object sender, EventArgs e)
        {
            this.textBoxDescription.Text = update.Description;
            this.richTextBoxChangeLog.Text = update.ChangeLog;
        }
    }
}
