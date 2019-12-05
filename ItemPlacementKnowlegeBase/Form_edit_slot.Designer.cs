namespace ItemPlacementKnowlegeBase
{
    partial class Form_edit_slot
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tb_name = new System.Windows.Forms.TextBox();
            this.tb_value = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.bt_applay = new System.Windows.Forms.Button();
            this.bt_abort = new System.Windows.Forms.Button();
            this.cb_type = new System.Windows.Forms.ComboBox();
            this.cb_value = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // tb_name
            // 
            this.tb_name.Location = new System.Drawing.Point(73, 19);
            this.tb_name.Name = "tb_name";
            this.tb_name.Size = new System.Drawing.Size(178, 20);
            this.tb_name.TabIndex = 0;
            // 
            // tb_value
            // 
            this.tb_value.Location = new System.Drawing.Point(73, 71);
            this.tb_value.Name = "tb_value";
            this.tb_value.Size = new System.Drawing.Size(178, 20);
            this.tb_value.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Имя ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Тип";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Значение";
            // 
            // bt_applay
            // 
            this.bt_applay.Location = new System.Drawing.Point(15, 108);
            this.bt_applay.Name = "bt_applay";
            this.bt_applay.Size = new System.Drawing.Size(105, 23);
            this.bt_applay.TabIndex = 6;
            this.bt_applay.Text = "Применить";
            this.bt_applay.UseVisualStyleBackColor = true;
            this.bt_applay.Click += new System.EventHandler(this.Bt_applay_Click);
            // 
            // bt_abort
            // 
            this.bt_abort.Location = new System.Drawing.Point(146, 108);
            this.bt_abort.Name = "bt_abort";
            this.bt_abort.Size = new System.Drawing.Size(105, 23);
            this.bt_abort.TabIndex = 7;
            this.bt_abort.Text = "Отмена";
            this.bt_abort.UseVisualStyleBackColor = true;
            // 
            // cb_type
            // 
            this.cb_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_type.Location = new System.Drawing.Point(73, 44);
            this.cb_type.Name = "cb_type";
            this.cb_type.Size = new System.Drawing.Size(178, 21);
            this.cb_type.TabIndex = 8;
            this.cb_type.SelectedIndexChanged += new System.EventHandler(this.Cb_type_SelectedIndexChanged);
            // 
            // cb_value
            // 
            this.cb_value.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_value.Enabled = false;
            this.cb_value.Location = new System.Drawing.Point(73, 71);
            this.cb_value.Name = "cb_value";
            this.cb_value.Size = new System.Drawing.Size(178, 21);
            this.cb_value.TabIndex = 9;
            this.cb_value.Visible = false;
            // 
            // Form_edit_slot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 145);
            this.Controls.Add(this.cb_value);
            this.Controls.Add(this.cb_type);
            this.Controls.Add(this.bt_abort);
            this.Controls.Add(this.bt_applay);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_value);
            this.Controls.Add(this.tb_name);
            this.Name = "Form_edit_slot";
            this.Text = "Form_edit_slot";
            this.VisibleChanged += new System.EventHandler(this.Form_edit_slot_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_name;
        private System.Windows.Forms.TextBox tb_value;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bt_applay;
        private System.Windows.Forms.Button bt_abort;
        private System.Windows.Forms.ComboBox cb_type;
        private System.Windows.Forms.ComboBox cb_value;
    }
}