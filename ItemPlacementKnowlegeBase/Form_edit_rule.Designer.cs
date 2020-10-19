namespace ItemPlacementKnowlegeBase
{
    partial class Form_edit_rule
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
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.bt_applay = new System.Windows.Forms.Button();
            this.bt_abort = new System.Windows.Forms.Button();
            this.cb_place = new System.Windows.Forms.ComboBox();
            this.cb_type = new System.Windows.Forms.ComboBox();
            this.cb_subject = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_object = new System.Windows.Forms.ComboBox();
            this.tb_reasoning = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Субъект";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Расположение";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(58, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Тип";
            // 
            // bt_applay
            // 
            this.bt_applay.Location = new System.Drawing.Point(26, 162);
            this.bt_applay.Name = "bt_applay";
            this.bt_applay.Size = new System.Drawing.Size(105, 23);
            this.bt_applay.TabIndex = 6;
            this.bt_applay.Text = "Применить";
            this.bt_applay.UseVisualStyleBackColor = true;
            this.bt_applay.Click += new System.EventHandler(this.Bt_applay_Click);
            // 
            // bt_abort
            // 
            this.bt_abort.Location = new System.Drawing.Point(157, 162);
            this.bt_abort.Name = "bt_abort";
            this.bt_abort.Size = new System.Drawing.Size(105, 23);
            this.bt_abort.TabIndex = 7;
            this.bt_abort.Text = "Отмена";
            this.bt_abort.UseVisualStyleBackColor = true;
            this.bt_abort.Click += new System.EventHandler(this.Bt_abort_Click);
            // 
            // cb_place
            // 
            this.cb_place.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_place.Items.AddRange(new object[] {
            "",
            "Выше",
            "Ниже",
            "Слева",
            "Справа",
            "Вместо"});
            this.cb_place.Location = new System.Drawing.Point(90, 73);
            this.cb_place.Name = "cb_place";
            this.cb_place.Size = new System.Drawing.Size(178, 21);
            this.cb_place.TabIndex = 8;
            // 
            // cb_type
            // 
            this.cb_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_type.Items.AddRange(new object[] {
            "Разрешающее",
            "Запрещающее"});
            this.cb_type.Location = new System.Drawing.Point(90, 100);
            this.cb_type.Name = "cb_type";
            this.cb_type.Size = new System.Drawing.Size(178, 21);
            this.cb_type.TabIndex = 9;
            // 
            // cb_subject
            // 
            this.cb_subject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_subject.Location = new System.Drawing.Point(90, 47);
            this.cb_subject.Name = "cb_subject";
            this.cb_subject.Size = new System.Drawing.Size(178, 21);
            this.cb_subject.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Объект";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // cb_object
            // 
            this.cb_object.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_object.Location = new System.Drawing.Point(90, 20);
            this.cb_object.Name = "cb_object";
            this.cb_object.Size = new System.Drawing.Size(178, 21);
            this.cb_object.TabIndex = 12;
            // 
            // tb_reasoning
            // 
            this.tb_reasoning.Location = new System.Drawing.Point(90, 127);
            this.tb_reasoning.Name = "tb_reasoning";
            this.tb_reasoning.Size = new System.Drawing.Size(178, 20);
            this.tb_reasoning.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Объяснение";
            // 
            // Form_edit_rule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 196);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_reasoning);
            this.Controls.Add(this.cb_object);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cb_subject);
            this.Controls.Add(this.cb_type);
            this.Controls.Add(this.cb_place);
            this.Controls.Add(this.bt_abort);
            this.Controls.Add(this.bt_applay);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Name = "Form_edit_rule";
            this.Text = "Form_edit_slot";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button bt_applay;
        private System.Windows.Forms.Button bt_abort;
        private System.Windows.Forms.ComboBox cb_place;
        private System.Windows.Forms.ComboBox cb_type;
        private System.Windows.Forms.ComboBox cb_subject;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cb_object;
        private System.Windows.Forms.TextBox tb_reasoning;
        private System.Windows.Forms.Label label2;
    }
}