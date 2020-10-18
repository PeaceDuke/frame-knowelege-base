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
            this.btn_removeItem = new System.Windows.Forms.Button();
            this.btn_changeItem = new System.Windows.Forms.Button();
            this.btn_addItem = new System.Windows.Forms.Button();
            this.lv_items = new System.Windows.Forms.ListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_removeRule = new System.Windows.Forms.Button();
            this.btn_addRule = new System.Windows.Forms.Button();
            this.lv_rules = new System.Windows.Forms.ListView();
            this.drawControl1 = new ItemPlacementKnowlegeBase.DrawControl();
            this.gb_items.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_items
            // 
            this.gb_items.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_items.Controls.Add(this.btn_removeItem);
            this.gb_items.Controls.Add(this.btn_changeItem);
            this.gb_items.Controls.Add(this.btn_addItem);
            this.gb_items.Controls.Add(this.lv_items);
            this.gb_items.Location = new System.Drawing.Point(2, 1);
            this.gb_items.Name = "gb_items";
            this.gb_items.Size = new System.Drawing.Size(302, 347);
            this.gb_items.TabIndex = 0;
            this.gb_items.TabStop = false;
            this.gb_items.Text = "Список предметов";
            // 
            // btn_removeItem
            // 
            this.btn_removeItem.Location = new System.Drawing.Point(202, 312);
            this.btn_removeItem.Name = "btn_removeItem";
            this.btn_removeItem.Size = new System.Drawing.Size(90, 29);
            this.btn_removeItem.TabIndex = 4;
            this.btn_removeItem.Text = "Удалить";
            this.btn_removeItem.UseVisualStyleBackColor = true;
            this.btn_removeItem.Click += new System.EventHandler(this.btn_removeItem_Click);
            // 
            // btn_changeItem
            // 
            this.btn_changeItem.Location = new System.Drawing.Point(106, 312);
            this.btn_changeItem.Name = "btn_changeItem";
            this.btn_changeItem.Size = new System.Drawing.Size(90, 29);
            this.btn_changeItem.TabIndex = 3;
            this.btn_changeItem.Text = "Изменить";
            this.btn_changeItem.UseVisualStyleBackColor = true;
            this.btn_changeItem.Click += new System.EventHandler(this.btn_changeItem_Click);
            // 
            // btn_addItem
            // 
            this.btn_addItem.Location = new System.Drawing.Point(10, 312);
            this.btn_addItem.Name = "btn_addItem";
            this.btn_addItem.Size = new System.Drawing.Size(90, 29);
            this.btn_addItem.TabIndex = 2;
            this.btn_addItem.Text = "Добавить";
            this.btn_addItem.UseVisualStyleBackColor = true;
            this.btn_addItem.Click += new System.EventHandler(this.btn_addItem_Click);
            // 
            // lv_items
            // 
            this.lv_items.Dock = System.Windows.Forms.DockStyle.Top;
            this.lv_items.FullRowSelect = true;
            this.lv_items.HideSelection = false;
            this.lv_items.Location = new System.Drawing.Point(3, 16);
            this.lv_items.MultiSelect = false;
            this.lv_items.Name = "lv_items";
            this.lv_items.Size = new System.Drawing.Size(296, 290);
            this.lv_items.TabIndex = 0;
            this.lv_items.UseCompatibleStateImageBehavior = false;
            this.lv_items.View = System.Windows.Forms.View.List;
            this.lv_items.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lv_frames_ItemDrag);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btn_removeRule);
            this.groupBox1.Controls.Add(this.btn_addRule);
            this.groupBox1.Controls.Add(this.lv_rules);
            this.groupBox1.Location = new System.Drawing.Point(310, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(302, 347);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Список правил";
            // 
            // btn_removeRule
            // 
            this.btn_removeRule.Location = new System.Drawing.Point(198, 312);
            this.btn_removeRule.Name = "btn_removeRule";
            this.btn_removeRule.Size = new System.Drawing.Size(90, 29);
            this.btn_removeRule.TabIndex = 5;
            this.btn_removeRule.Text = "Удалить";
            this.btn_removeRule.UseVisualStyleBackColor = true;
            this.btn_removeRule.Click += new System.EventHandler(this.btn_removeRule_Click);
            // 
            // btn_addRule
            // 
            this.btn_addRule.Location = new System.Drawing.Point(6, 312);
            this.btn_addRule.Name = "btn_addRule";
            this.btn_addRule.Size = new System.Drawing.Size(90, 29);
            this.btn_addRule.TabIndex = 1;
            this.btn_addRule.Text = "Добавить";
            this.btn_addRule.UseVisualStyleBackColor = true;
            this.btn_addRule.Click += new System.EventHandler(this.btn_addRule_Click);
            // 
            // lv_rules
            // 
            this.lv_rules.Dock = System.Windows.Forms.DockStyle.Top;
            this.lv_rules.FullRowSelect = true;
            this.lv_rules.HideSelection = false;
            this.lv_rules.Location = new System.Drawing.Point(3, 16);
            this.lv_rules.MultiSelect = false;
            this.lv_rules.Name = "lv_rules";
            this.lv_rules.Size = new System.Drawing.Size(296, 290);
            this.lv_rules.TabIndex = 0;
            this.lv_rules.UseCompatibleStateImageBehavior = false;
            this.lv_rules.View = System.Windows.Forms.View.List;
            // 
            // drawControl1
            // 
            this.drawControl1.BackColor = System.Drawing.Color.White;
            this.drawControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.drawControl1.Location = new System.Drawing.Point(0, 366);
            this.drawControl1.Name = "drawControl1";
            this.drawControl1.Size = new System.Drawing.Size(619, 344);
            this.drawControl1.TabIndex = 7;
            // 
            // Main_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 710);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.drawControl1);
            this.Controls.Add(this.gb_items);
            this.Name = "Main_form";
            this.Text = "Слоты";
            this.gb_items.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_items;
        private System.Windows.Forms.ListView lv_items;
        private DrawControl drawControl1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lv_rules;
        private System.Windows.Forms.Button btn_removeItem;
        private System.Windows.Forms.Button btn_changeItem;
        private System.Windows.Forms.Button btn_addItem;
        private System.Windows.Forms.Button btn_removeRule;
        private System.Windows.Forms.Button btn_addRule;
    }
}