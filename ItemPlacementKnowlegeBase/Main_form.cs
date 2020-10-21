using ItemPlacementKnowlegeBase.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ItemPlacementKnowlegeBase
{
    public partial class Main_form : Form
    {
        public Main_form()
        {
            InitializeComponent();
            bt_runApp.Enabled = false;
            bt_delete.Enabled = false;
        }

        private void bt_runApp_Click(object sender, EventArgs e)
        {
            if (lv_kb.SelectedItems.Count > 0)
            {
                KnowlegeBaseManager.Initialise(lv_kb.SelectedItems[0].Tag as string);
                Application_form appForm = new Application_form();
                this.Visible = false;
                appForm.ShowDialog();
                this.Visible = true;
            }
        }

        private void bt_load_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                if (!lv_kb.Items.ContainsKey(ofd.FileName))
                {
                    ListViewItem lvi = new ListViewItem(ofd.SafeFileName);
                    lvi.Name = ofd.FileName;
                    lvi.Tag = ofd.FileName;
                    lv_kb.Items.Add(lvi);
                }
            }
        }

        private void lv_kb_SelectedIndexChanged(object sender, EventArgs e)
        {
            bt_runApp.Enabled = lv_kb.SelectedItems.Count > 0;
            bt_delete.Enabled = lv_kb.SelectedItems.Count > 0;
        }

        private void bt_delete_Click(object sender, EventArgs e)
        {
            if(lv_kb.SelectedItems.Count > 0)
            {
                lv_kb.Items.Remove(lv_kb.SelectedItems[0]);
            }
        }
    }
}
