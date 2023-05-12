namespace QRCodeXLS
{
    partial class QRCodeXLSClass
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QRCodeXLSClass));
            this.tbXLSPath = new System.Windows.Forms.TextBox();
            this.btnGetXLS = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.pBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // tbXLSPath
            // 
            this.tbXLSPath.Location = new System.Drawing.Point(12, 21);
            this.tbXLSPath.Name = "tbXLSPath";
            this.tbXLSPath.ReadOnly = true;
            this.tbXLSPath.Size = new System.Drawing.Size(218, 20);
            this.tbXLSPath.TabIndex = 9999;
            this.tbXLSPath.Text = "Выберите XLS-файл";
            // 
            // btnGetXLS
            // 
            this.btnGetXLS.Location = new System.Drawing.Point(236, 19);
            this.btnGetXLS.Name = "btnGetXLS";
            this.btnGetXLS.Size = new System.Drawing.Size(75, 22);
            this.btnGetXLS.TabIndex = 0;
            this.btnGetXLS.Text = "Выбрать файл";
            this.btnGetXLS.UseVisualStyleBackColor = true;
            this.btnGetXLS.Click += new System.EventHandler(this.btnGetXLS_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Список Агрегатов";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(12, 73);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(299, 23);
            this.btnGenerate.TabIndex = 1;
            this.btnGenerate.Text = "Генерировать изображения этикеток";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // pBar
            // 
            this.pBar.Location = new System.Drawing.Point(12, 47);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(299, 20);
            this.pBar.Step = 1;
            this.pBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pBar.TabIndex = 10000;
            // 
            // QRCodeXLSClass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 104);
            this.Controls.Add(this.pBar);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGetXLS);
            this.Controls.Add(this.tbXLSPath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "QRCodeXLSClass";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Генератор изображений этикеток";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tbXLSPath;
        private System.Windows.Forms.Button btnGetXLS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.ProgressBar pBar;
    }
}

