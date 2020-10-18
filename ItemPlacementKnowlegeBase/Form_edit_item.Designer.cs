namespace ItemPlacementKnowlegeBase
{
    partial class Form_edit_item
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
            this.label1 = new System.Windows.Forms.Label();
            this.bt_add = new System.Windows.Forms.Button();
            this.bt_abort = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_images = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // tb_name
            // 
            this.tb_name.Location = new System.Drawing.Point(103, 18);
            this.tb_name.Name = "tb_name";
            this.tb_name.Size = new System.Drawing.Size(156, 20);
            this.tb_name.TabIndex = 0;
            this.tb_name.TextChanged += new System.EventHandler(this.Tb_name_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Название";
            // 
            // bt_add
            // 
            this.bt_add.Location = new System.Drawing.Point(12, 79);
            this.bt_add.Name = "bt_add";
            this.bt_add.Size = new System.Drawing.Size(105, 23);
            this.bt_add.TabIndex = 2;
            this.bt_add.Text = "Добавить";
            this.bt_add.UseVisualStyleBackColor = true;
            this.bt_add.Click += new System.EventHandler(this.Bt_add_Click);
            // 
            // bt_abort
            // 
            this.bt_abort.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bt_abort.Location = new System.Drawing.Point(154, 79);
            this.bt_abort.Name = "bt_abort";
            this.bt_abort.Size = new System.Drawing.Size(105, 23);
            this.bt_abort.TabIndex = 3;
            this.bt_abort.Text = "Отмена";
            this.bt_abort.UseVisualStyleBackColor = true;
            this.bt_abort.Click += new System.EventHandler(this.Bt_abort_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Изображение";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // cb_images
            // 
            this.cb_images.FormattingEnabled = true;
            this.cb_images.Location = new System.Drawing.Point(103, 47);
            this.cb_images.Name = "cb_images";
            this.cb_images.Size = new System.Drawing.Size(156, 21);
            this.cb_images.TabIndex = 6;
            // 
            // Form_edit_item
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(281, 114);
            this.Controls.Add(this.cb_images);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bt_abort);
            this.Controls.Add(this.bt_add);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_name);
            this.Name = "Form_edit_item";
            this.Text = "Добавить фрейм";
            this.Load += new System.EventHandler(this.Form_edit_frame_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bt_add;
        private System.Windows.Forms.Button bt_abort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_images;
    }
}