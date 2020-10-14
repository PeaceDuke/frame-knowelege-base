namespace ItemPlacementKnowlegeBase
{
    partial class Main_form
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
            this.gb_items = new System.Windows.Forms.GroupBox();
            this.lv_items = new System.Windows.Forms.ListView();
            this.drawControl1 = new ItemPlacementKnowlegeBase.DrawControl();
            this.gb_items.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_items
            // 
            this.gb_items.Controls.Add(this.lv_items);
            this.gb_items.Location = new System.Drawing.Point(2, 1);
            this.gb_items.Name = "gb_items";
            this.gb_items.Size = new System.Drawing.Size(302, 286);
            this.gb_items.TabIndex = 0;
            this.gb_items.TabStop = false;
            this.gb_items.Text = "Список предметов";
            // 
            // lv_items
            // 
            this.lv_items.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_items.FullRowSelect = true;
            this.lv_items.HideSelection = false;
            this.lv_items.Location = new System.Drawing.Point(3, 16);
            this.lv_items.MultiSelect = false;
            this.lv_items.Name = "lv_items";
            this.lv_items.Size = new System.Drawing.Size(296, 267);
            this.lv_items.TabIndex = 0;
            this.lv_items.UseCompatibleStateImageBehavior = false;
            this.lv_items.View = System.Windows.Forms.View.List;
            this.lv_items.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lv_frames_ItemDrag);
            // 
            // drawControl1
            // 
            this.drawControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.drawControl1.BackColor = System.Drawing.Color.White;
            this.drawControl1.Location = new System.Drawing.Point(2, 354);
            this.drawControl1.Name = "drawControl1";
            this.drawControl1.Size = new System.Drawing.Size(682, 344);
            this.drawControl1.TabIndex = 7;
            // 
            // Main_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 710);
            this.Controls.Add(this.drawControl1);
            this.Controls.Add(this.gb_items);
            this.Name = "Main_form";
            this.Text = "Слоты";
            this.gb_items.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_items;
        private System.Windows.Forms.ListView lv_items;
        private DrawControl drawControl1;
    }
}