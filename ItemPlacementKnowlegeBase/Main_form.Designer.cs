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
            this.bt_runApp = new System.Windows.Forms.Button();
            this.bt_load = new System.Windows.Forms.Button();
            this.lv_kb = new System.Windows.Forms.ListView();
            this.bt_delete = new System.Windows.Forms.Button();
            this.bt_save = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bt_runApp
            // 
            this.bt_runApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_runApp.Location = new System.Drawing.Point(265, 212);
            this.bt_runApp.Name = "bt_runApp";
            this.bt_runApp.Size = new System.Drawing.Size(75, 23);
            this.bt_runApp.TabIndex = 0;
            this.bt_runApp.Text = "Запустить";
            this.bt_runApp.UseVisualStyleBackColor = true;
            this.bt_runApp.Click += new System.EventHandler(this.bt_runApp_Click);
            // 
            // bt_load
            // 
            this.bt_load.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bt_load.Location = new System.Drawing.Point(12, 212);
            this.bt_load.Name = "bt_load";
            this.bt_load.Size = new System.Drawing.Size(75, 23);
            this.bt_load.TabIndex = 2;
            this.bt_load.Text = "Загрузить";
            this.bt_load.UseVisualStyleBackColor = true;
            this.bt_load.Click += new System.EventHandler(this.bt_load_Click);
            // 
            // lv_kb
            // 
            this.lv_kb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lv_kb.FullRowSelect = true;
            this.lv_kb.HideSelection = false;
            this.lv_kb.Location = new System.Drawing.Point(12, 14);
            this.lv_kb.MultiSelect = false;
            this.lv_kb.Name = "lv_kb";
            this.lv_kb.Size = new System.Drawing.Size(328, 192);
            this.lv_kb.TabIndex = 3;
            this.lv_kb.UseCompatibleStateImageBehavior = false;
            this.lv_kb.View = System.Windows.Forms.View.List;
            this.lv_kb.SelectedIndexChanged += new System.EventHandler(this.lv_kb_SelectedIndexChanged);
            // 
            // bt_delete
            // 
            this.bt_delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_delete.Location = new System.Drawing.Point(184, 212);
            this.bt_delete.Name = "bt_delete";
            this.bt_delete.Size = new System.Drawing.Size(75, 23);
            this.bt_delete.TabIndex = 4;
            this.bt_delete.Text = "Удалить";
            this.bt_delete.UseVisualStyleBackColor = true;
            this.bt_delete.Click += new System.EventHandler(this.bt_delete_Click);
            // 
            // bt_save
            // 
            this.bt_save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bt_save.Location = new System.Drawing.Point(93, 212);
            this.bt_save.Name = "bt_save";
            this.bt_save.Size = new System.Drawing.Size(75, 23);
            this.bt_save.TabIndex = 5;
            this.bt_save.Text = "Сохранить";
            this.bt_save.UseVisualStyleBackColor = true;
            this.bt_save.Click += new System.EventHandler(this.bt_save_Click);
            // 
            // Main_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 247);
            this.Controls.Add(this.bt_save);
            this.Controls.Add(this.bt_delete);
            this.Controls.Add(this.lv_kb);
            this.Controls.Add(this.bt_load);
            this.Controls.Add(this.bt_runApp);
            this.Name = "Main_form";
            this.Text = "Main_form";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bt_runApp;
        private System.Windows.Forms.Button bt_load;
        private System.Windows.Forms.ListView lv_kb;
        private System.Windows.Forms.Button bt_delete;
        private System.Windows.Forms.Button bt_save;
    }
}