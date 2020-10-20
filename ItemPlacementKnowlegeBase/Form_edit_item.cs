using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ItemPlacementKnowlegeBase.Images;
using ItemPlacementKnowlegeBase.Gui;
using ItemPlacementKnowlegeBase.Models;
using ItemPlacementKnowlegeBase.Services;

namespace ItemPlacementKnowlegeBase
{    
    public partial class Form_edit_item : Form
    {
        public Item item;
        public TestKnowlegeBaseProvider provider;
        private String fullImageName = "";

        public Form_edit_item(TestKnowlegeBaseProvider _provider)
        {
            InitializeComponent();
            Initialize();
            provider = _provider;
            bt_add.Text = @"Добавить";
        }

        public Form_edit_item(TestKnowlegeBaseProvider _provider, Item _item)
        {
            InitializeComponent();
            Initialize();
            provider = _provider;
            item = _item;
            bt_add.Text = @"Обновить";
            tb_name.Text = item.Name;
        }

        public void Initialize()
        {
            bt_add.Enabled = false;
        }

        private void Bt_add_Click(object sender, EventArgs e)
        {
            try
            {
                if(item == null)
                {
                    item = provider.AddItemToList(tb_name.Text, fullImageName);
                }
                else
                {
                    item = provider.ChangeItemFromList(item, tb_name.Text, fullImageName);
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch(Exception ex)
            {
                this.DialogResult = DialogResult.None;
                MessageBox.Show(ex.Message);
            }            
        }

        private void Tb_name_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(tb_name.Text))
                bt_add.Enabled = false;
            else
                bt_add.Enabled = true;
        }

        private void Form_edit_frame_Load(object sender, EventArgs e)
        {

        }

        private void Bt_abort_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void bt_loadImage_Click(object sender, EventArgs e)
        {
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                label_imageName.Text = ofd.SafeFileName;
                fullImageName = ofd.FileName;
            }
        }
    }
}
