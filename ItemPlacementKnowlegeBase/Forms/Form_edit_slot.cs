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
        public Form_edit_slot()
        {
            InitializeComponent();
        }
        public string Slot_name()
        {
            return tb_name.Text;
        }
        public string Slot_type()
        {
            return tb_type.Text;
        }
        public string Slot_value()
        {
            return tb_value.Text;
        }
       
    }
}
