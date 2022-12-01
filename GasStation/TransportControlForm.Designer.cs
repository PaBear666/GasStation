namespace GasStation
{
    partial class TransportControlForm
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
            this.addFuelButton = new System.Windows.Forms.Button();
            this.EditTransportButton = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FuelColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Role = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // addFuelButton
            // 
            this.addFuelButton.Location = new System.Drawing.Point(1028, 811);
            this.addFuelButton.Name = "addFuelButton";
            this.addFuelButton.Size = new System.Drawing.Size(260, 84);
            this.addFuelButton.TabIndex = 7;
            this.addFuelButton.Text = "Добавить транспорт";
            this.addFuelButton.UseVisualStyleBackColor = true;
            this.addFuelButton.Click += new System.EventHandler(this.addFuelButton_Click);
            // 
            // EditTransportButton
            // 
            this.EditTransportButton.Location = new System.Drawing.Point(12, 817);
            this.EditTransportButton.Name = "EditTransportButton";
            this.EditTransportButton.Size = new System.Drawing.Size(239, 73);
            this.EditTransportButton.TabIndex = 6;
            this.EditTransportButton.Text = "Сохранить изменения";
            this.EditTransportButton.UseVisualStyleBackColor = true;
            this.EditTransportButton.Click += new System.EventHandler(this.EditTransportButton_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Username,
            this.FuelColumn,
            this.Role});
            this.dataGridView2.Location = new System.Drawing.Point(12, 12);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(1276, 773);
            this.dataGridView2.TabIndex = 5;
            this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
            this.dataGridView2.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView2_EditingControlShowing);
            this.dataGridView2.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridView2_RowsRemoved);
            this.dataGridView2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView2_MouseClick);
            // 
            // Username
            // 
            this.Username.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Username.HeaderText = "Модель транспорта";
            this.Username.MinimumWidth = 6;
            this.Username.Name = "Username";
            // 
            // FuelColumn
            // 
            this.FuelColumn.HeaderText = "Топливо";
            this.FuelColumn.MinimumWidth = 6;
            this.FuelColumn.Name = "FuelColumn";
            this.FuelColumn.ReadOnly = true;
            this.FuelColumn.Width = 125;
            // 
            // Role
            // 
            this.Role.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Role.HeaderText = "Объём топливного бака";
            this.Role.MinimumWidth = 6;
            this.Role.Name = "Role";
            // 
            // TransportControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1314, 946);
            this.Controls.Add(this.addFuelButton);
            this.Controls.Add(this.EditTransportButton);
            this.Controls.Add(this.dataGridView2);
            this.Name = "TransportControlForm";
            this.Text = "TransportControlForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button addFuelButton;
        private System.Windows.Forms.Button EditTransportButton;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Username;
        private System.Windows.Forms.DataGridViewTextBoxColumn FuelColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Role;
    }
}