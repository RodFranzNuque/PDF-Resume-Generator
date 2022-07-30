namespace PDF_Resume_Maker
{
    partial class ResumeMakerForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ChooseFilebutton = new System.Windows.Forms.Button();
            this.SaveFilebutton = new System.Windows.Forms.Button();
            this.skControl1 = new SkiaSharp.Views.Desktop.SKControl();
            this.PreviewLabel = new System.Windows.Forms.Label();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.InstructionsLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ChooseFilebutton
            // 
            this.ChooseFilebutton.ForeColor = System.Drawing.Color.RoyalBlue;
            this.ChooseFilebutton.Location = new System.Drawing.Point(112, 423);
            this.ChooseFilebutton.Name = "ChooseFilebutton";
            this.ChooseFilebutton.Size = new System.Drawing.Size(94, 34);
            this.ChooseFilebutton.TabIndex = 0;
            this.ChooseFilebutton.Text = "Choose File";
            this.ChooseFilebutton.UseVisualStyleBackColor = true;
            this.ChooseFilebutton.Click += new System.EventHandler(this.ChooseFilebutton_Click);
            // 
            // SaveFilebutton
            // 
            this.SaveFilebutton.ForeColor = System.Drawing.Color.RoyalBlue;
            this.SaveFilebutton.Location = new System.Drawing.Point(573, 547);
            this.SaveFilebutton.Name = "SaveFilebutton";
            this.SaveFilebutton.Size = new System.Drawing.Size(116, 33);
            this.SaveFilebutton.TabIndex = 1;
            this.SaveFilebutton.Text = "Save as PDF";
            this.SaveFilebutton.UseVisualStyleBackColor = true;
            this.SaveFilebutton.Click += new System.EventHandler(this.SaveFilebutton_Click);
            // 
            // skControl1
            // 
            this.skControl1.BackColor = System.Drawing.Color.LightCyan;
            this.skControl1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.skControl1.Location = new System.Drawing.Point(331, 79);
            this.skControl1.Name = "skControl1";
            this.skControl1.Size = new System.Drawing.Size(358, 453);
            this.skControl1.TabIndex = 2;
            this.skControl1.Text = "skControl1";
            this.skControl1.PaintSurface += new System.EventHandler<SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs>(this.skControl1_PaintSurface);
            // 
            // PreviewLabel
            // 
            this.PreviewLabel.AutoSize = true;
            this.PreviewLabel.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.PreviewLabel.ForeColor = System.Drawing.Color.SteelBlue;
            this.PreviewLabel.Location = new System.Drawing.Point(331, 40);
            this.PreviewLabel.Name = "PreviewLabel";
            this.PreviewLabel.Size = new System.Drawing.Size(111, 25);
            this.PreviewLabel.TabIndex = 3;
            this.PreviewLabel.Text = "Preview:";
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("Impact", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.TitleLabel.ForeColor = System.Drawing.Color.SteelBlue;
            this.TitleLabel.Location = new System.Drawing.Point(17, 149);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(293, 106);
            this.TitleLabel.TabIndex = 4;
            this.TitleLabel.Text = "Generate your \r\nResume";
            this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // InstructionsLabel
            // 
            this.InstructionsLabel.AutoSize = true;
            this.InstructionsLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.InstructionsLabel.ForeColor = System.Drawing.Color.SteelBlue;
            this.InstructionsLabel.Location = new System.Drawing.Point(38, 288);
            this.InstructionsLabel.Name = "InstructionsLabel";
            this.InstructionsLabel.Size = new System.Drawing.Size(244, 112);
            this.InstructionsLabel.TabIndex = 5;
            this.InstructionsLabel.Text = "Choose your JSON file\r\n that you would like to\r\n convert into PDF for your\r\nResum" +
    "e";
            this.InstructionsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ResumeMakerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(739, 624);
            this.Controls.Add(this.InstructionsLabel);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.PreviewLabel);
            this.Controls.Add(this.skControl1);
            this.Controls.Add(this.SaveFilebutton);
            this.Controls.Add(this.ChooseFilebutton);
            this.ForeColor = System.Drawing.Color.MidnightBlue;
            this.Name = "ResumeMakerForm";
            this.Text = "Create PDF resume";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button ChooseFilebutton;
        private Button SaveFilebutton;
        private SkiaSharp.Views.Desktop.SKControl skControl1;
        private Label PreviewLabel;
        private Label TitleLabel;
        private Label InstructionsLabel;
    }
}