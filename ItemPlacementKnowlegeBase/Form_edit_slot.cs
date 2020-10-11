using ItemPlacementKnowlegeBase.Models;
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
    public partial class Form_edit_slot : Form
    {
        public KnowlegeBase Knowleges_edit = new KnowlegeBase();
        public Frame cur_frame;
        public Slot cur_slot;
        public FormType form_type;
        public enum FormType
        {
            Insert,
            Update
        }
        public Form_edit_slot(FormType type)
        {
            InitializeComponent();
            InitComb();
            form_type = type;
            switch (type)
            {
                case FormType.Insert:
                    bt_applay.Text = @"Добавить";
                    bt_applay.Enabled = false;
                    break;
                case FormType.Update:
                    bt_applay.Text = @"Изменить";
                    break;
            }
        }
        public void InitComb()
        {            
            cb_type.Items.Clear();
            cb_type.Items.AddRange(new string[] { "Текст", "Фрейм" });
            foreach (Domain domain in Knowleges_edit.Domains)
            {
                cb_type.Items.Add(domain.ToString());
            }

            cb_value.Items.Clear();
            foreach (var frame in Knowleges_edit.Frames)
            {
                if(frame.IsBase)
                    cb_value.Items.Add(frame.Name);
            }
        }

        public string Slot_name
        { 
            get { return tb_name.Text; }
            set { tb_name.Text = value; }
        }

        public string Slot_type
        {
            get
            {
                return cb_type.SelectedItem.ToString();
            }           
        }

        public string Slot_value
        {
            get
            {
                if(cb_type.SelectedIndex!=3)                
                    return tb_value.Text;
                else
                    return cb_value.SelectedItem.ToString();
            }
        }       

        private void Cb_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_type.SelectedIndex==3)
            {
                tb_value.Enabled = false;
                tb_value.Visible = false;
                cb_value.Visible = true;
                cb_value.Enabled = true;

                cb_value.Items.Clear();
                var frames = Knowleges_edit.Frames.Where(x => x.Name != cur_frame.Name);
                foreach (var frame in frames)
                {
                    cb_value.Items.Add(frame.Name);
                }
                bt_applay.Enabled = false;
            }
            else
            {
                cb_value.Visible = false;
                cb_value.Enabled = false;
            }
        }

        private void Bt_applay_Click(object sender, EventArgs e)
        {
            //try
            //{
                
            //    var name_slot = tb_name.Text;
            //    Type type;
                
            //    switch (cb_type.SelectedIndex)
            //    {
            //        case 0:
            //            type = typeof(int);
            //            int value_i=0;
            //            if (!cur_frame.IsBase)
            //                value_i = Int32.Parse(tb_value.Text);
            //            if(form_type == FormType.Insert)
            //                if(cur_frame.IsBase)
            //                    Knowleges_edit[cur_frame.Name].AddSlot(name_slot, type);
            //                else
            //                    Knowleges_edit[cur_frame.Name].AddSlot(name_slot, value_i, type);
            //            else
            //            {
            //                Slot sl = new Slot(cur_slot.Name, value_i, type);
            //                Knowleges_edit[cur_frame.Name].SetSlot(sl);
            //            }
            //            break;
            //        case 1:
            //            type = typeof(float);
            //            float value_f = 0;
            //            if (!cur_frame.IsBase)
            //                value_f = float.Parse(tb_value.Text);                        
            //            if (form_type == FormType.Insert)
            //                if(cur_frame.IsBase)
            //                    Knowleges_edit[cur_frame.Name].AddSlot(name_slot, type);
            //                else
            //                    Knowleges_edit[cur_frame.Name].AddSlot(name_slot, value_f, type);
            //            else
            //            {
            //                Slot sl = new Slot(cur_slot.Name, value_f, type);
            //                Knowleges_edit[cur_frame.Name].SetSlot(sl);
            //            }
            //            break;
            //        case 2:
            //            type = typeof(string);
            //            string value_s = "";
            //            if (!cur_frame.IsBase)
            //                value_s = tb_value.Text;
            //            if (form_type == FormType.Insert)
            //                if(cur_frame.IsBase)
            //                    Knowleges_edit[cur_frame.Name].AddSlot(name_slot, type);
            //                else
            //                    Knowleges_edit[cur_frame.Name].AddSlot(name_slot, value_s, type);
            //            else
            //            {
            //                Slot sl = new Slot(cur_slot.Name, value_s, type);
            //                Knowleges_edit[cur_frame.Name].SetSlot(sl);
            //            }
            //            break;
            //        case 3:
            //            type = typeof(Frame);
            //            Frame value_fr = Knowleges_edit.GetFrame(cb_value.Text);
            //            if (form_type == FormType.Insert)
            //                Knowleges_edit[cur_frame.Name].Slots.Add(name_slot, value_fr, type);
            //            else
            //            {
            //                Slot sl = new Slot(cur_slot.Name, value_fr, type);
            //                Knowleges_edit[cur_frame.Name].SetSlot(sl);
            //            }
            //            tb_value.Text = "";
            //            break;                    
            //    }
                               
            //    this.DialogResult = DialogResult.OK;
            //    this.Close();
            //}
            //catch (Exception ex)
            //{
            //    this.DialogResult = DialogResult.None;
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void Form_edit_slot_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                if (cur_frame.IsBase)
                {
                    lb_value.Visible = false;
                    tb_value.Visible = false;
                    tb_value.Enabled = false;
                }
                else
                {
                    lb_value.Visible = true;
                    tb_value.Visible = true;
                    tb_value.Enabled = true;
                }
                cb_value.Items.Clear();
                var frames = Knowleges_edit.Frames.Where(x => x.Name != cur_frame.Name);
                foreach (var frame in frames)
                {
                    cb_value.Items.Add(frame.Name);
                }

                if (form_type == FormType.Update)
                {
                    tb_name.Text = cur_slot.Name;
                    cb_type.SelectedIndex = cb_type.FindString(cur_slot.TypeAsString);
                    if (cb_type.SelectedIndex == 3)
                    {
                        cb_value.SelectedIndex = cb_value.FindString(cur_slot.ValueAsString);
                    }
                    else
                    {
                        if(cur_slot.ValueAsString != null)
                            tb_value.Text = cur_slot.ValueAsString;
                    }
                    //cb_type.SelectedIndex = (cur)
                }
            }
        }

        private void Bt_abort_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cb_value_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_value.SelectedItem != null & tb_name.Text != "")
                bt_applay.Enabled = true;
            else
                bt_applay.Enabled = false;
                    
        }

        private void tb_name_TextChanged(object sender, EventArgs e)
        {
            if (tb_value.Text != "" & tb_name.Text != "" & cb_type.SelectedIndex != 3 | cb_value.SelectedItem != null & tb_name.Text != "" & cb_type.SelectedIndex == 3)
                bt_applay.Enabled = true;
            else
                bt_applay.Enabled = false;
        }

        private void tb_value_TextChanged(object sender, EventArgs e)
        {
            if (tb_value.Text != "" & tb_name.Text != "" & cb_type.SelectedIndex != 3 | cb_value.SelectedItem != null & tb_name.Text != "" & cb_type.SelectedIndex == 3)
                bt_applay.Enabled = true;
            else
                bt_applay.Enabled = false;
        }
    }
}
