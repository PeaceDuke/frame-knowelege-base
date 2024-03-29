﻿namespace ItemPlacementKnowlegeBase
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
            this.gb_frames = new System.Windows.Forms.GroupBox();
            this.lv_frames = new System.Windows.Forms.ListView();
            this.gb_frames_edit = new System.Windows.Forms.GroupBox();
            this.bt_frame_delete = new System.Windows.Forms.Button();
            this.bt_frame_add = new System.Windows.Forms.Button();
            this.gb_frame = new System.Windows.Forms.GroupBox();
            this.bt_slot_delete = new System.Windows.Forms.Button();
            this.bt_slot_edit = new System.Windows.Forms.Button();
            this.bt_slot_add = new System.Windows.Forms.Button();
            this.gb_slots = new System.Windows.Forms.GroupBox();
            this.lv_slots = new System.Windows.Forms.ListView();
            this.gb_frames.SuspendLayout();
            this.gb_frames_edit.SuspendLayout();
            this.gb_frame.SuspendLayout();
            this.gb_slots.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_frames
            // 
            this.gb_frames.Controls.Add(this.lv_frames);
            this.gb_frames.Location = new System.Drawing.Point(2, 1);
            this.gb_frames.Name = "gb_frames";
            this.gb_frames.Size = new System.Drawing.Size(302, 286);
            this.gb_frames.TabIndex = 0;
            this.gb_frames.TabStop = false;
            this.gb_frames.Text = "Список фреймов";
            // 
            // lv_frames
            // 
            this.lv_frames.FullRowSelect = true;
            this.lv_frames.HideSelection = false;
            this.lv_frames.Location = new System.Drawing.Point(17, 20);
            this.lv_frames.MultiSelect = false;
            this.lv_frames.Name = "lv_frames";
            this.lv_frames.Size = new System.Drawing.Size(268, 249);
            this.lv_frames.TabIndex = 0;
            this.lv_frames.UseCompatibleStateImageBehavior = false;
            this.lv_frames.View = System.Windows.Forms.View.List;
            this.lv_frames.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.Lv_frames_ItemSelectionChanged);
            // 
            // gb_frames_edit
            // 
            this.gb_frames_edit.Controls.Add(this.bt_frame_delete);
            this.gb_frames_edit.Controls.Add(this.bt_frame_add);
            this.gb_frames_edit.Location = new System.Drawing.Point(2, 287);
            this.gb_frames_edit.Name = "gb_frames_edit";
            this.gb_frames_edit.Size = new System.Drawing.Size(302, 61);
            this.gb_frames_edit.TabIndex = 1;
            this.gb_frames_edit.TabStop = false;
            this.gb_frames_edit.Text = "Редактирование фреймов";
            // 
            // bt_frame_delete
            // 
            this.bt_frame_delete.Location = new System.Drawing.Point(154, 23);
            this.bt_frame_delete.Name = "bt_frame_delete";
            this.bt_frame_delete.Size = new System.Drawing.Size(131, 23);
            this.bt_frame_delete.TabIndex = 3;
            this.bt_frame_delete.Text = "Удалить";
            this.bt_frame_delete.UseVisualStyleBackColor = true;
            // 
            // bt_frame_add
            // 
            this.bt_frame_add.Location = new System.Drawing.Point(17, 23);
            this.bt_frame_add.Name = "bt_frame_add";
            this.bt_frame_add.Size = new System.Drawing.Size(131, 23);
            this.bt_frame_add.TabIndex = 2;
            this.bt_frame_add.Text = "Добавить";
            this.bt_frame_add.UseVisualStyleBackColor = true;
            this.bt_frame_add.Click += new System.EventHandler(this.Bt_frame_add_Click);
            // 
            // gb_frame
            // 
            this.gb_frame.Controls.Add(this.bt_slot_delete);
            this.gb_frame.Controls.Add(this.bt_slot_edit);
            this.gb_frame.Controls.Add(this.bt_slot_add);
            this.gb_frame.Controls.Add(this.gb_slots);
            this.gb_frame.Location = new System.Drawing.Point(310, 1);
            this.gb_frame.Name = "gb_frame";
            this.gb_frame.Size = new System.Drawing.Size(374, 347);
            this.gb_frame.TabIndex = 2;
            this.gb_frame.TabStop = false;
            this.gb_frame.Text = "Фрейм";
            // 
            // bt_slot_delete
            // 
            this.bt_slot_delete.Location = new System.Drawing.Point(258, 309);
            this.bt_slot_delete.Name = "bt_slot_delete";
            this.bt_slot_delete.Size = new System.Drawing.Size(110, 23);
            this.bt_slot_delete.TabIndex = 5;
            this.bt_slot_delete.Text = "Удалить слот";
            this.bt_slot_delete.UseVisualStyleBackColor = true;
            // 
            // bt_slot_edit
            // 
            this.bt_slot_edit.Location = new System.Drawing.Point(132, 309);
            this.bt_slot_edit.Name = "bt_slot_edit";
            this.bt_slot_edit.Size = new System.Drawing.Size(110, 23);
            this.bt_slot_edit.TabIndex = 4;
            this.bt_slot_edit.Text = "Изменить слот";
            this.bt_slot_edit.UseVisualStyleBackColor = true;
            // 
            // bt_slot_add
            // 
            this.bt_slot_add.Location = new System.Drawing.Point(6, 309);
            this.bt_slot_add.Name = "bt_slot_add";
            this.bt_slot_add.Size = new System.Drawing.Size(110, 23);
            this.bt_slot_add.TabIndex = 3;
            this.bt_slot_add.Text = "Добавить слот";
            this.bt_slot_add.UseVisualStyleBackColor = true;
            this.bt_slot_add.Click += new System.EventHandler(this.Bt_slot_add_Click);
            // 
            // gb_slots
            // 
            this.gb_slots.Controls.Add(this.lv_slots);
            this.gb_slots.Location = new System.Drawing.Point(6, 19);
            this.gb_slots.Name = "gb_slots";
            this.gb_slots.Size = new System.Drawing.Size(362, 284);
            this.gb_slots.TabIndex = 0;
            this.gb_slots.TabStop = false;
            this.gb_slots.Text = "Слоты";
            // 
            // lv_slots
            // 
            this.lv_slots.FullRowSelect = true;
            this.lv_slots.HideSelection = false;
            this.lv_slots.Location = new System.Drawing.Point(16, 19);
            this.lv_slots.Name = "lv_slots";
            this.lv_slots.Size = new System.Drawing.Size(328, 248);
            this.lv_slots.TabIndex = 0;
            this.lv_slots.UseCompatibleStateImageBehavior = false;
            this.lv_slots.View = System.Windows.Forms.View.Details;
            this.lv_slots.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.Lv_slots_ItemSelectionChanged);
            this.lv_slots.SelectedIndexChanged += new System.EventHandler(this.Lv_slots_SelectedIndexChanged);
            // 
            // Main_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 402);
            this.Controls.Add(this.gb_frame);
            this.Controls.Add(this.gb_frames_edit);
            this.Controls.Add(this.gb_frames);
            this.Name = "Main_form";
            this.Text = "Слоты";
            this.gb_frames.ResumeLayout(false);
            this.gb_frames_edit.ResumeLayout(false);
            this.gb_frame.ResumeLayout(false);
            this.gb_slots.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_frames;
        private System.Windows.Forms.ListView lv_frames;
        private System.Windows.Forms.GroupBox gb_frames_edit;
        private System.Windows.Forms.Button bt_frame_delete;
        private System.Windows.Forms.Button bt_frame_add;
        private System.Windows.Forms.GroupBox gb_frame;
        private System.Windows.Forms.Button bt_slot_delete;
        private System.Windows.Forms.Button bt_slot_edit;
        private System.Windows.Forms.Button bt_slot_add;
        private System.Windows.Forms.GroupBox gb_slots;
        private System.Windows.Forms.ListView lv_slots;
    }
}