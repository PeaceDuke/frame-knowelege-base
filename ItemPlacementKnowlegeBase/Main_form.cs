using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ItemPlacementKnowlegeBase.Models;
using ItemPlacementKnowlegeBase.Gui;
using ItemPlacementKnowlegeBase.Services;

namespace ItemPlacementKnowlegeBase
{
    public partial class Main_form : Form
    {
        public static Item draggedData = null;
        public TestKnowlegeBaseProvider provider;
        public Main_form()
        {
            InitializeComponent();
            //InicializeParts();
            provider = KnowlegeBaseManager.get();
            foreach(var item in provider.LoadItems())
            {
                ListViewItem lvi = new ListViewItem(item.Name);
                lvi.Tag = item;
                lv_items.Items.Add(lvi);
            }
            foreach(var rule in provider.LoadRules())
            {
                ListViewItem lvi = new ListViewItem(rule.GetDescription());
                lvi.Tag = rule;
                lv_rules.Items.Add(lvi);
            }
        }
        // private void InicializeParts()
        //{
        //    bt_slot_delete.Enabled = false;
        //    bt_slot_edit.Enabled = false;
        //    bt_frame_delete.Enabled = false;
        //    bt_slot_add.Enabled = false;
        //    lv_slots.Columns.Add("Name");
        //    lv_slots.Columns.Add("Type");
        //    lv_slots.Columns.Add("Value");
        //}

        //private void Bt_frame_add_Click(object sender, EventArgs e)
        //{
        //    var formFrameAdd = new Form_edit_frame(Form_edit_frame.FormType.Insert);
        //    formFrameAdd.Knowleges_edit = Knowleges;
            
        //    if (formFrameAdd.ShowDialog() == DialogResult.OK)
        //    {                
        //        var str = formFrameAdd.Name_frame();                
        //        lv_items.Items.Add(str);                                
        //    }
        //    formFrameAdd.Close();
        //}

        //private void Lv_frames_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        //{
        //    if (e.IsSelected)
        //    {
        //        cur_frame = Knowleges[e.Item.Text];
        //        lv_slots.Items.Clear();
        //        gb_frame.Text = "Frame " + e.Item.Text;
        //        if(Knowleges[e.Item.Text].Parent!=null)
        //        {
        //            gb_frame.Text += ":" + Knowleges[e.Item.Text].Parent.Name;
        //        }
        //        var slots = Knowleges[e.Item.Text].Slots;
        //        foreach (var slot in slots)
        //        {
        //            var listItem = new ListViewItem(slot.Name);
        //            listItem.SubItems.Add(slot.TypeAsString);
        //            if(slot.ValueAsString!=null)
        //                listItem.SubItems.Add(slot.ValueAsString);
        //            lv_slots.Items.Add(listItem);
        //        }
        //        bt_frame_delete.Enabled = true;
        //        bt_slot_add.Enabled = true;
        //    }
        //    else
        //    {
        //        bt_slot_delete.Enabled = false;
        //        bt_slot_edit.Enabled = false;
        //        bt_frame_delete.Enabled = false;
        //        bt_slot_add.Enabled = false;
        //    }
            
        //}

        //private void Lv_slots_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        //{
        //    bool slot_of_parent=false;
            
        //    if (!e.IsSelected)
        //    {
        //        bt_slot_delete.Enabled = false;
        //        bt_slot_edit.Enabled = false;
        //    }
        //    else
        //    {
        //        if (cur_frame.Parent != null)
        //        {
        //            //slot_of_parent = cur_frame.ParentFrameName.GetSlot(lv_slots.SelectedItems[0].Text) != null;
        //        }
        //        if (!slot_of_parent)
        //        {
        //            bt_slot_delete.Enabled = true;                    
        //        }
        //        bt_slot_edit.Enabled = true;
        //    }            
        //}
        
        //private void Bt_slot_add_Click(object sender, EventArgs e)
        //{
        //    var formSlotAdd = new Form_edit_slot(Form_edit_slot.FormType.Insert);
        //    formSlotAdd.Knowleges_edit = Knowleges;

        //    formSlotAdd.cur_frame = Knowleges[lv_items.SelectedItems[0].Text];
        //    if (formSlotAdd.ShowDialog() == DialogResult.OK)
        //    {
        //        var slot_name = formSlotAdd.Slot_name;
        //        var slot_type = formSlotAdd.Slot_type;
        //        var slot_value = formSlotAdd.Slot_value;

        //        var listItem = new ListViewItem(slot_name);
        //        listItem.SubItems.Add(slot_type);
        //        listItem.SubItems.Add(slot_value);
        //        lv_slots.Items.Add(listItem);
        //    }
        //    formSlotAdd.Close();
        //}
        
        //private void Bt_frame_delete_Click(object sender, EventArgs e)
        //{            
        //    var lvItem = lv_items.SelectedItems[0];
        //    lv_items.Items.Remove(lvItem);
        //    var frame = Knowleges[lvItem.Text];
        //    Knowleges.Frames.Remove(frame);

        //    bt_frame_delete.Enabled = false;
        //    bt_slot_delete.Enabled = false;
        //    bt_slot_edit.Enabled = false;
        //    bt_slot_add.Enabled = false;
        //    lv_slots.Items.Clear();
        //    gb_frame.Text = "Фрейм";
        //}

        //private void Bt_slot_delete_Click(object sender, EventArgs e)
        //{
        //    var lvSlotItem = lv_slots.SelectedItems[0];
        //    var lvFramItem = lv_items.SelectedItems[0];
        //    var frame = Knowleges[lvFramItem.Text];
        //    frame.Slots.Remove(frame[lvSlotItem.Text]);
        //    lv_slots.Items.Remove(lvSlotItem);
        //    bt_slot_delete.Enabled = false;
        //    bt_slot_edit.Enabled = false;
        //}

        //private void Bt_slot_edit_Click(object sender, EventArgs e)
        //{
        //    var formSlotAdd = new Form_edit_slot(Form_edit_slot.FormType.Update);
        //    formSlotAdd.Knowleges_edit = Knowleges;

        //    var cur_frame = Knowleges[lv_items.SelectedItems[0].Text];
        //    formSlotAdd.cur_frame = cur_frame;
        //    formSlotAdd.cur_slot = cur_frame[lv_slots.SelectedItems[0].Text];
        //    formSlotAdd.form_type = Form_edit_slot.FormType.Update;
        //    int a = 0;
        //    if (formSlotAdd.ShowDialog() == DialogResult.OK)
        //    {
        //        var lvSlotItem = lv_slots.SelectedItems[0];
                
        //        lvSlotItem.SubItems[0].Text = formSlotAdd.Slot_name;
        //        lvSlotItem.SubItems[1].Text = formSlotAdd.Slot_type;
        //        lvSlotItem.SubItems[2].Text = formSlotAdd.Slot_value;
                
        //    }
        //    formSlotAdd.Close();
        //}
        
        private void startDragDrop(Item data)
        {
            draggedData = data;
        }
        private void lv_frames_ItemDrag(object sender, ItemDragEventArgs e)
        {
            startDragDrop((sender as ListView)?.SelectedItems[0]?.Tag as Item);
        }

        private void btn_addItem_Click(object sender, EventArgs e)
        {
            var formFrameAdd = new Form_edit_item(provider);

            if (formFrameAdd.ShowDialog() == DialogResult.OK)
            {
                var item = formFrameAdd.item;
                ListViewItem lvi = new ListViewItem(item.Name);
                lvi.Tag = item;
                lv_items.Items.Add(lvi);
            }
            formFrameAdd.Close();
        }

        private void btn_changeItem_Click(object sender, EventArgs e)
        {
            if (lv_items.SelectedItems.Count == 0)
                MessageBox.Show("Выберете предмет");
            var formFrameChange = new Form_edit_item(provider, (Item)lv_items.SelectedItems[0].Tag);

            if (formFrameChange.ShowDialog() == DialogResult.OK)
            {
                var item = formFrameChange.item;
                ListViewItem lvi = new ListViewItem(item.Name);
                lvi.Tag = item;
                lv_items.Items.Add(lvi);
            }
            formFrameChange.Close();

        }

        private void btn_removeItem_Click(object sender, EventArgs e)
        {
            if (lv_items.SelectedItems.Count <= 0)
                MessageBox.Show("Выберете предмет");
            else
                provider.RemoveItemFromList((Item)lv_items.SelectedItems[0].Tag);

        }

        private void btn_addRule_Click(object sender, EventArgs e)
        {
            var formFrameAdd = new Form_edit_rule(provider);

            if (formFrameAdd.ShowDialog() == DialogResult.OK)
            {
                var rule = formFrameAdd.rule;
                ListViewItem lvi = new ListViewItem(rule.GetDescription());
                lvi.Tag = rule;
                lv_rules.Items.Add(lvi);
            }
            formFrameAdd.Close();
        }

        private void btn_removeRule_Click(object sender, EventArgs e)
        {
            if (lv_rules.SelectedItems.Count <= 0)
                MessageBox.Show("Выберете правило");
            else
            {
                var selectedItem = lv_rules.SelectedItems[0];
                provider.RemoveRuleFromList((Rule)selectedItem.Tag);
                lv_rules.Items.Remove(selectedItem);
            }
        }
    }
}
