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
            this.SuspendLayout();
            // 
            // bt_runApp
            // 
            this.bt_runApp.Location = new System.Drawing.Point(12, 415);
            this.bt_runApp.Name = "bt_runApp";
            this.bt_runApp.Size = new System.Drawing.Size(75, 23);
            this.bt_runApp.TabIndex = 0;
            this.bt_runApp.Text = "Запустить";
            this.bt_runApp.UseVisualStyleBackColor = true;
            this.bt_runApp.Click += new System.EventHandler(this.bt_runApp_Click);
            // 
            // Main_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.bt_runApp);
            this.Name = "Main_form";
            this.Text = "Main_form";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bt_runApp;
    }
}