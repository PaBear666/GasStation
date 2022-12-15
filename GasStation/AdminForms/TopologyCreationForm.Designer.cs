namespace GasStation
{
    partial class TopologyCreationForm
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.left = new System.Windows.Forms.RadioButton();
            this.right = new System.Windows.Forms.RadioButton();
            this.up = new System.Windows.Forms.RadioButton();
            this.down = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.Wcounterlabel = new System.Windows.Forms.Label();
            this.LcounterLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.Location = new System.Drawing.Point(92, 96);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(314, 38);
            this.textBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(150, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 46);
            this.label1.TabIndex = 1;
            this.label1.Text = "Название";
            // 
            // left
            // 
            this.left.AutoSize = true;
            this.left.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.left.Location = new System.Drawing.Point(563, 112);
            this.left.Name = "left";
            this.left.Size = new System.Drawing.Size(125, 40);
            this.left.TabIndex = 2;
            this.left.TabStop = true;
            this.left.Text = "Слева";
            this.left.UseVisualStyleBackColor = true;
            this.left.CheckedChanged += new System.EventHandler(this.left_CheckedChanged);
            // 
            // right
            // 
            this.right.AutoSize = true;
            this.right.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.right.Location = new System.Drawing.Point(563, 158);
            this.right.Name = "right";
            this.right.Size = new System.Drawing.Size(142, 40);
            this.right.TabIndex = 3;
            this.right.TabStop = true;
            this.right.Text = "Справа";
            this.right.UseVisualStyleBackColor = true;
            this.right.CheckedChanged += new System.EventHandler(this.right_CheckedChanged);
            // 
            // up
            // 
            this.up.AutoSize = true;
            this.up.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.up.Location = new System.Drawing.Point(563, 205);
            this.up.Name = "up";
            this.up.Size = new System.Drawing.Size(138, 40);
            this.up.TabIndex = 4;
            this.up.TabStop = true;
            this.up.Text = "Сверху";
            this.up.UseVisualStyleBackColor = true;
            this.up.CheckedChanged += new System.EventHandler(this.up_CheckedChanged);
            // 
            // down
            // 
            this.down.AutoSize = true;
            this.down.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.down.Location = new System.Drawing.Point(563, 252);
            this.down.Name = "down";
            this.down.Size = new System.Drawing.Size(123, 40);
            this.down.TabIndex = 5;
            this.down.TabStop = true;
            this.down.Text = "Снизу";
            this.down.UseVisualStyleBackColor = true;
            this.down.CheckedChanged += new System.EventHandler(this.down_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(425, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(442, 46);
            this.label2.TabIndex = 6;
            this.label2.Text = "Расположение дороги";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(92, 237);
            this.trackBar1.Maximum = 12;
            this.trackBar1.Minimum = 8;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(314, 56);
            this.trackBar1.TabIndex = 7;
            this.trackBar1.Value = 8;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(168, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 46);
            this.label3.TabIndex = 8;
            this.label3.Text = "Длина";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(150, 309);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(162, 46);
            this.label4.TabIndex = 10;
            this.label4.Text = "Ширина";
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(92, 378);
            this.trackBar2.Maximum = 12;
            this.trackBar2.Minimum = 3;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(314, 56);
            this.trackBar2.TabIndex = 9;
            this.trackBar2.Value = 3;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // Wcounterlabel
            // 
            this.Wcounterlabel.AutoSize = true;
            this.Wcounterlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Wcounterlabel.Location = new System.Drawing.Point(12, 237);
            this.Wcounterlabel.Name = "Wcounterlabel";
            this.Wcounterlabel.Size = new System.Drawing.Size(23, 25);
            this.Wcounterlabel.TabIndex = 11;
            this.Wcounterlabel.Text = "8";
            // 
            // LcounterLabel
            // 
            this.LcounterLabel.AutoSize = true;
            this.LcounterLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LcounterLabel.Location = new System.Drawing.Point(12, 378);
            this.LcounterLabel.Name = "LcounterLabel";
            this.LcounterLabel.Size = new System.Drawing.Size(23, 25);
            this.LcounterLabel.TabIndex = 12;
            this.LcounterLabel.Text = "3";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(17, 473);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(943, 110);
            this.button1.TabIndex = 13;
            this.button1.Text = "Принять";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TopologyCreationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 609);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.LcounterLabel);
            this.Controls.Add(this.Wcounterlabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.down);
            this.Controls.Add(this.up);
            this.Controls.Add(this.right);
            this.Controls.Add(this.left);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "TopologyCreationForm";
            this.Text = "Создание топологии";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton left;
        private System.Windows.Forms.RadioButton right;
        private System.Windows.Forms.RadioButton up;
        private System.Windows.Forms.RadioButton down;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.Label Wcounterlabel;
        private System.Windows.Forms.Label LcounterLabel;
        private System.Windows.Forms.Button button1;
    }
}