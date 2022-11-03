namespace QRCodeDesktop
{
    partial class QRCodeDesktop
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QRCodeDesktop));
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pbQR = new System.Windows.Forms.PictureBox();
            this.textQR = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textAgr = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textSN = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbQR)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(352, 231);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(108, 24);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "Генерировать QR";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(475, 231);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(110, 24);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Сохранить файл";
            this.btnSave.UseCompatibleTextRendering = true;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pbQR
            // 
            this.pbQR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbQR.InitialImage = null;
            this.pbQR.Location = new System.Drawing.Point(235, 12);
            this.pbQR.Name = "pbQR";
            this.pbQR.Size = new System.Drawing.Size(350, 200);
            this.pbQR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbQR.TabIndex = 2;
            this.pbQR.TabStop = false;
            // 
            // textQR
            // 
            this.textQR.Location = new System.Drawing.Point(12, 151);
            this.textQR.Name = "textQR";
            this.textQR.Size = new System.Drawing.Size(181, 20);
            this.textQR.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Ссылка";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Тип агрегата";
            // 
            // textAgr
            // 
            this.textAgr.Location = new System.Drawing.Point(12, 32);
            this.textAgr.Name = "textAgr";
            this.textAgr.Size = new System.Drawing.Size(181, 20);
            this.textAgr.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Серийный номер";
            // 
            // textSN
            // 
            this.textSN.Location = new System.Drawing.Point(12, 92);
            this.textSN.Name = "textSN";
            this.textSN.Size = new System.Drawing.Size(181, 20);
            this.textSN.TabIndex = 7;
            // 
            // QRCodeDesktop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 267);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textSN);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textAgr);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textQR);
            this.Controls.Add(this.pbQR);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnGenerate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "QRCodeDesktop";
            this.Text = "Генератор QR кодов";
            ((System.ComponentModel.ISupportInitialize)(this.pbQR)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.PictureBox pbQR;
        private System.Windows.Forms.TextBox textQR;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textAgr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textSN;
    }
}

