﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ItemPlacementKnowlegeBase.Models;

namespace ItemPlacementKnowlegeBase
{    
    public partial class Form_edit_frame : Form
    {
        public KnowlegeBase Knowleges_edit = new KnowlegeBase();
        public FormType form_type;
        public enum FormType
        {
            Insert,
            Delete
        }

        public bool Child()
        {
            if (cb_parrent.SelectedIndex == -1)
                return false;
            else
                return true;
        }

        public Form_edit_frame(FormType type)
        {
            InitializeComponent();
            Initialize();
            switch (type)
            {
                case FormType.Insert:
                    bt_add.Text = @"Добавить";                   
                    break;
                case FormType.Delete:
                    bt_add.Text = @"Удалить";
                    break;
            }
        }

        public void Initialize()
        {
            bt_add.Enabled = false;
            
        }

        public string Name_frame()
        {
            return tb_name.Text;
        }

        private void Bt_add_Click(object sender, EventArgs e)
        {
            try
            {
                if(cb_parrent.SelectedIndex == -1)
                    Knowleges_edit.AddFrame(tb_name.Text, cb_base.Checked);
                else
                    Knowleges_edit.AddFrame(tb_name.Text, cb_base.Checked, Knowleges_edit.GetFrame(cb_parrent.SelectedItem.ToString()).Name);
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
            if (tb_name.Text != "")
                bt_add.Enabled = true;
            else
                bt_add.Enabled = false;
        }

        private void Form_edit_frame_Load(object sender, EventArgs e)
        {

        }

        private void Bt_abort_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Cb_base_VisibleChanged(object sender, EventArgs e)
        {
            if(this.Visible==true)
            {
                cb_parrent.Items.Clear();
                foreach (var frame in Knowleges_edit.Frames)
                {
                    if (frame.IsBase)
                        cb_parrent.Items.Add(frame.Name);
                }
            }
        }
    }
}
