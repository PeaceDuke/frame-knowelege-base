using ItemPlacementKnowlegeBase.Gui;
using ItemPlacementKnowlegeBase.Models;
using ItemPlacementKnowlegeBase.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace ItemPlacementKnowlegeBase
{
    public partial class Form_edit_rule : Form
    {
        private TestKnowlegeBaseProvider provider;
        public Rule rule;
        public List<Item> items;

        public Form_edit_rule(TestKnowlegeBaseProvider _provider)
        {
            InitializeComponent();
            provider = _provider;
            Init();
            bt_applay.Text = @"Добавить";
        }

        private void Init()
        {
            items = provider.LoadItems();
            foreach (var item in items)
            {
                cb_object.Items.Add(item.Name);
                cb_subject.Items.Add(item.Name);
            }
        }

        private void Bt_applay_Click(object sender, EventArgs e)
        {
            try
            {
                if(cb_object.SelectedItem != null && cb_subject.SelectedItem != null)
                {
                    rule = provider.AddRuleToList(getItem((string)cb_object.SelectedItem), getItem((string)cb_subject.SelectedItem), cb_place.Text, cb_type.Text, tb_reasoning.Text);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                this.DialogResult = DialogResult.None;
                MessageBox.Show(ex.Message);
            }
        }

        private void Bt_abort_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private Item getItem(string name)
        {
            if (items.Where(x => x.Name == name).Any())
                return items.Where(x => x.Name == name).First();
            throw new Exception("Предмет не найден");
        }
    }
}
