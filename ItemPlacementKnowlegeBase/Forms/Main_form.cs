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
        public Main_form()
        {
            InitializeComponent();
            InicializeParts();
        }
         private void InicializeParts()
        {
            
        }

        private void Bt_frame_add_Click(object sender, EventArgs e)
        {
            var formFrameAdd = new Form_edit_frame();
            formFrameAdd.Knowleges_edit = Knowleges;
            if (formFrameAdd.ShowDialog() == DialogResult.OK)
            {
                var str = formFrameAdd.Name_frame();
                if (str != "")
                {
                    lv_frames.Items.Add(str);
                }
                
            }
            formFrameAdd.Close();
        }

        private void Lv_frames_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            lv_slots.Clear();
            var slots= Knowleges.GetFrame(e.Item.Text).Slots;
            foreach(var slot in slots)
            {

                var listItem = new ListViewItem(slot.Name);
                listItem.SubItems.Add(slot.ValueType.ToString());
                listItem.SubItems.Add(slot.Value.ToString());
                lv_slots.Items.Add(listItem);
            }
            
        }

        private void Lv_slots_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.ItemIndex == -1)
            {
                bt_slot_delete.Enabled = false;
                bt_slot_edit.Enabled = false;
            }
            else
            {
                bt_slot_delete.Enabled = true;
                bt_slot_edit.Enabled = true;
            }

            
        }

        private void Lv_slots_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
        }

        private void Bt_slot_add_Click(object sender, EventArgs e)
        {
            var formSlotAdd = new Form_edit_slot();
            formSlotAdd.Knowleges_edit = Knowleges;
            if (formSlotAdd.ShowDialog() == DialogResult.OK)
            {
                var str = formSlotAdd.();
                if (str != "")
                {
                    lv_frames.Items.Add(str);
                }

            }
            formSlotAdd.Close();
        }
    }
}
