using System;
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
    public partial class Main_form : Form
    {
        KnowlegeBase Knowleges = new KnowlegeBase();
        Frame cur_frame;
        public Main_form()
        {
            InitializeComponent();
            InicializeParts();
        }
         private void InicializeParts()
        {
            bt_slot_delete.Enabled = false;
            bt_slot_edit.Enabled = false;
            bt_frame_delete.Enabled = false;
            bt_slot_add.Enabled = false;
            lv_slots.Columns.Add("Name");
            lv_slots.Columns.Add("Type");
            lv_slots.Columns.Add("Value");
        }

        private void Bt_frame_add_Click(object sender, EventArgs e)
        {
            var formFrameAdd = new Form_edit_frame(Form_edit_frame.FormType.Insert);
            formFrameAdd.Knowleges_edit = Knowleges;
            
            if (formFrameAdd.ShowDialog() == DialogResult.OK)
            {                
                var str = formFrameAdd.Name_frame();               
                
                lv_frames.Items.Add(str);                                
            }
            formFrameAdd.Close();
        }

        private void Lv_frames_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                cur_frame = Knowleges.GetFrame(e.Item.Text);
                lv_slots.Items.Clear();
                gb_frame.Text = "Frame " + e.Item.Text;
                if(Knowleges.GetFrame(e.Item.Text).ParentFrame!=null)
                {
                    gb_frame.Text += ":" + Knowleges.GetFrame(e.Item.Text).ParentFrame.Name;
                }
                var slots = Knowleges.GetFrame(e.Item.Text).Slots;
                foreach (var slot in slots)
                {
                    var listItem = new ListViewItem(slot.Name);
                    listItem.SubItems.Add(slot.ValueType.Name);
                    if(slot.Value!=null)
                        listItem.SubItems.Add(slot.Value.ToString());
                    lv_slots.Items.Add(listItem);
                }
                bt_frame_delete.Enabled = true;
                bt_slot_add.Enabled = true;
            }
            else
            {
                bt_slot_delete.Enabled = false;
                bt_slot_edit.Enabled = false;
                bt_frame_delete.Enabled = false;
                bt_slot_add.Enabled = false;
            }
            
        }

        private void Lv_slots_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            bool slot_of_parent=false;
            
            if (!e.IsSelected)
            {
                bt_slot_delete.Enabled = false;
                bt_slot_edit.Enabled = false;
            }
            else
            {
                if (cur_frame.ParentFrame != null)
                {
                    //slot_of_parent = cur_frame.ParentFrameName.GetSlot(lv_slots.SelectedItems[0].Text) != null;
                }
                if (!slot_of_parent)
                {
                    bt_slot_delete.Enabled = true;                    
                }
                bt_slot_edit.Enabled = true;
            }            
        }

        private void Lv_slots_SelectedIndexChanged(object sender, EventArgs e)
        {   
            
        }

        private void Bt_slot_add_Click(object sender, EventArgs e)
        {
            var formSlotAdd = new Form_edit_slot(Form_edit_slot.FormType.Insert);
            formSlotAdd.Knowleges_edit = Knowleges;

            formSlotAdd.cur_frame = Knowleges.GetFrame(lv_frames.SelectedItems[0].Text);
            if (formSlotAdd.ShowDialog() == DialogResult.OK)
            {
                var slot_name = formSlotAdd.Slot_name;
                var slot_type = formSlotAdd.Slot_type;
                var slot_value = formSlotAdd.Slot_value;

                var listItem = new ListViewItem(slot_name);
                listItem.SubItems.Add(slot_type);
                listItem.SubItems.Add(slot_value);
                lv_slots.Items.Add(listItem);
            }
            formSlotAdd.Close();
        }
        
        private void Bt_frame_delete_Click(object sender, EventArgs e)
        {            
            var lvItem = lv_frames.SelectedItems[0];
            lv_frames.Items.Remove(lvItem);
            var frame = Knowleges.GetFrame(lvItem.Text);
            Knowleges.RemoveFrame(frame);

            bt_frame_delete.Enabled = false;
            bt_slot_delete.Enabled = false;
            bt_slot_edit.Enabled = false;
            bt_slot_add.Enabled = false;
            lv_slots.Items.Clear();
            gb_frame.Text = "Фрейм";
        }

        private void Bt_slot_delete_Click(object sender, EventArgs e)
        {
            var lvSlotItem = lv_slots.SelectedItems[0];
            var lvFramItem = lv_frames.SelectedItems[0];
            var frame = Knowleges.GetFrame(lvFramItem.Text);
            frame.RemoveSlot(lvSlotItem.Text);
            lv_slots.Items.Remove(lvSlotItem);
            bt_slot_delete.Enabled = false;
            bt_slot_edit.Enabled = false;
        }

        private void Bt_slot_edit_Click(object sender, EventArgs e)
        {
            var formSlotAdd = new Form_edit_slot(Form_edit_slot.FormType.Update);
            formSlotAdd.Knowleges_edit = Knowleges;

            var cur_frame = Knowleges.GetFrame(lv_frames.SelectedItems[0].Text);
            formSlotAdd.cur_frame = cur_frame;
            formSlotAdd.cur_slot = cur_frame.GetSlot(lv_slots.SelectedItems[0].Text);
            formSlotAdd.form_type = Form_edit_slot.FormType.Update;
            int a = 0;
            if (formSlotAdd.ShowDialog() == DialogResult.OK)
            {
                var lvSlotItem = lv_slots.SelectedItems[0];
                
                lvSlotItem.SubItems[0].Text = formSlotAdd.Slot_name;
                lvSlotItem.SubItems[1].Text = formSlotAdd.Slot_type;
                lvSlotItem.SubItems[2].Text = formSlotAdd.Slot_value;
                
            }
            formSlotAdd.Close();
        }
    }
}
