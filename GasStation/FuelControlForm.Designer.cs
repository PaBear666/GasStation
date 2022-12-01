namespace GasStation
{
    partial class FuelControlForm
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
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Role = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addFuelButton = new System.Windows.Forms.Button();
            this.EditFuelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Username,
            this.Role});
            this.dataGridView2.Location = new System.Drawing.Point(12, 12);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(776, 375);
            this.dataGridView2.TabIndex = 1;
            this.dataGridView2.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView2_EditingControlShowing);
            this.dataGridView2.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridView2_RowsRemoved);
            // 
            // Username
            // 
            this.Username.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Username.HeaderText = "Тип топлива";
            this.Username.MinimumWidth = 6;
            this.Username.Name = "Username";
            // 
            // Role
            // 
            this.Role.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Role.HeaderText = "Стоимость топлива";
            this.Role.MinimumWidth = 6;
            this.Role.Name = "Role";
            // 
            // addFuelButton
            // 
            this.addFuelButton.Location = new System.Drawing.Point(563, 415);
            this.addFuelButton.Name = "addFuelButton";
            this.addFuelButton.Size = new System.Drawing.Size(211, 23);
            this.addFuelButton.TabIndex = 4;
            this.addFuelButton.Text = "Добавить топливо";
            this.addFuelButton.UseVisualStyleBackColor = true;
            this.addFuelButton.Click += new System.EventHandler(this.addFuelButton_Click);
            // 
            // EditFuelButton
            // 
            this.EditFuelButton.Location = new System.Drawing.Point(12, 415);
            this.EditFuelButton.Name = "EditFuelButton";
            this.EditFuelButton.Size = new System.Drawing.Size(211, 23);
            this.EditFuelButton.TabIndex = 3;
            this.EditFuelButton.Text = "Сохранить изменения";
            this.EditFuelButton.UseVisualStyleBackColor = true;
            this.EditFuelButton.Click += new System.EventHandler(this.EditFuelButton_Click);
            // 
            // FuelControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 474);
            this.Controls.Add(this.addFuelButton);
            this.Controls.Add(this.EditFuelButton);
            this.Controls.Add(this.dataGridView2);
            this.Name = "FuelControlForm";
            this.Text = "FuelControlForm";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FuelControlForm_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button addFuelButton;
        private System.Windows.Forms.Button EditFuelButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn Username;
        private System.Windows.Forms.DataGridViewTextBoxColumn Role;
    }
}