namespace TryDrawNet
{
	partial class DrawingForm
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
			this.pictureBox_NeuroWorking = new System.Windows.Forms.PictureBox();
			this.pictureBox_NeuroShedule = new System.Windows.Forms.PictureBox();
			this.textBoxInfo = new System.Windows.Forms.TextBox();
			this.button_test = new System.Windows.Forms.Button();
			this.button_Study = new System.Windows.Forms.Button();
			this.button_divideKoef = new System.Windows.Forms.Button();
			this.button_multiplyKoef = new System.Windows.Forms.Button();
			this.button_Koh = new System.Windows.Forms.Button();
			this.button_addVRGB = new System.Windows.Forms.Button();
			this.button_Save = new System.Windows.Forms.Button();
			this.button_Load = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox_NeuroWorking)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox_NeuroShedule)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox_NeuroWorking
			// 
			this.pictureBox_NeuroWorking.Location = new System.Drawing.Point(13, 13);
			this.pictureBox_NeuroWorking.Name = "pictureBox_NeuroWorking";
			this.pictureBox_NeuroWorking.Size = new System.Drawing.Size(933, 495);
			this.pictureBox_NeuroWorking.TabIndex = 0;
			this.pictureBox_NeuroWorking.TabStop = false;
			// 
			// pictureBox_NeuroShedule
			// 
			this.pictureBox_NeuroShedule.Location = new System.Drawing.Point(958, 514);
			this.pictureBox_NeuroShedule.Name = "pictureBox_NeuroShedule";
			this.pictureBox_NeuroShedule.Size = new System.Drawing.Size(306, 217);
			this.pictureBox_NeuroShedule.TabIndex = 1;
			this.pictureBox_NeuroShedule.TabStop = false;
			this.pictureBox_NeuroShedule.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_NeuroShedule_MouseClick);
			// 
			// textBoxInfo
			// 
			this.textBoxInfo.Location = new System.Drawing.Point(958, 13);
			this.textBoxInfo.Multiline = true;
			this.textBoxInfo.Name = "textBoxInfo";
			this.textBoxInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxInfo.Size = new System.Drawing.Size(306, 495);
			this.textBoxInfo.TabIndex = 2;
			// 
			// button_test
			// 
			this.button_test.Location = new System.Drawing.Point(13, 514);
			this.button_test.Name = "button_test";
			this.button_test.Size = new System.Drawing.Size(118, 77);
			this.button_test.TabIndex = 3;
			this.button_test.Text = "Тест";
			this.button_test.UseVisualStyleBackColor = true;
			this.button_test.Click += new System.EventHandler(this.button_test_Click);
			// 
			// button_Study
			// 
			this.button_Study.Location = new System.Drawing.Point(137, 514);
			this.button_Study.Name = "button_Study";
			this.button_Study.Size = new System.Drawing.Size(118, 77);
			this.button_Study.TabIndex = 4;
			this.button_Study.Text = "Обучение (стандартное)";
			this.button_Study.UseVisualStyleBackColor = true;
			this.button_Study.Click += new System.EventHandler(this.button_Study_Click);
			// 
			// button_divideKoef
			// 
			this.button_divideKoef.Location = new System.Drawing.Point(261, 514);
			this.button_divideKoef.Name = "button_divideKoef";
			this.button_divideKoef.Size = new System.Drawing.Size(118, 77);
			this.button_divideKoef.TabIndex = 5;
			this.button_divideKoef.Text = "/";
			this.button_divideKoef.UseVisualStyleBackColor = true;
			this.button_divideKoef.Click += new System.EventHandler(this.button_divideKoef_Click);
			// 
			// button_multiplyKoef
			// 
			this.button_multiplyKoef.Location = new System.Drawing.Point(385, 514);
			this.button_multiplyKoef.Name = "button_multiplyKoef";
			this.button_multiplyKoef.Size = new System.Drawing.Size(118, 77);
			this.button_multiplyKoef.TabIndex = 6;
			this.button_multiplyKoef.Text = "*";
			this.button_multiplyKoef.UseVisualStyleBackColor = true;
			this.button_multiplyKoef.Click += new System.EventHandler(this.button_multiplyKoef_Click);
			// 
			// button_Koh
			// 
			this.button_Koh.Location = new System.Drawing.Point(137, 597);
			this.button_Koh.Name = "button_Koh";
			this.button_Koh.Size = new System.Drawing.Size(118, 77);
			this.button_Koh.TabIndex = 7;
			this.button_Koh.Text = "Обучение (кохонен)";
			this.button_Koh.UseVisualStyleBackColor = true;
			this.button_Koh.Click += new System.EventHandler(this.button_Koh_Click);
			// 
			// button_addVRGB
			// 
			this.button_addVRGB.Location = new System.Drawing.Point(12, 597);
			this.button_addVRGB.Name = "button_addVRGB";
			this.button_addVRGB.Size = new System.Drawing.Size(118, 77);
			this.button_addVRGB.TabIndex = 8;
			this.button_addVRGB.Text = "Тестовые векторы (Кохонен)";
			this.button_addVRGB.UseVisualStyleBackColor = true;
			this.button_addVRGB.Click += new System.EventHandler(this.button_addVRGB_Click);
			// 
			// button_Save
			// 
			this.button_Save.Location = new System.Drawing.Point(828, 524);
			this.button_Save.Name = "button_Save";
			this.button_Save.Size = new System.Drawing.Size(118, 77);
			this.button_Save.TabIndex = 9;
			this.button_Save.Text = "Сохранить";
			this.button_Save.UseVisualStyleBackColor = true;
			this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
			// 
			// button_Load
			// 
			this.button_Load.Location = new System.Drawing.Point(828, 607);
			this.button_Load.Name = "button_Load";
			this.button_Load.Size = new System.Drawing.Size(118, 77);
			this.button_Load.TabIndex = 10;
			this.button_Load.Text = "Загрузить";
			this.button_Load.UseVisualStyleBackColor = true;
			this.button_Load.Click += new System.EventHandler(this.button_Load_Click);
			// 
			// DrawingForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1276, 745);
			this.Controls.Add(this.button_Load);
			this.Controls.Add(this.button_Save);
			this.Controls.Add(this.button_addVRGB);
			this.Controls.Add(this.button_Koh);
			this.Controls.Add(this.button_multiplyKoef);
			this.Controls.Add(this.button_divideKoef);
			this.Controls.Add(this.button_Study);
			this.Controls.Add(this.button_test);
			this.Controls.Add(this.textBoxInfo);
			this.Controls.Add(this.pictureBox_NeuroShedule);
			this.Controls.Add(this.pictureBox_NeuroWorking);
			this.Name = "DrawingForm";
			this.Text = "-_-";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox_NeuroWorking)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox_NeuroShedule)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox_NeuroWorking;
		private System.Windows.Forms.PictureBox pictureBox_NeuroShedule;
		private System.Windows.Forms.TextBox textBoxInfo;
		private System.Windows.Forms.Button button_test;
		private System.Windows.Forms.Button button_Study;
		private System.Windows.Forms.Button button_divideKoef;
		private System.Windows.Forms.Button button_multiplyKoef;
		private System.Windows.Forms.Button button_Koh;
		private System.Windows.Forms.Button button_addVRGB;
		private System.Windows.Forms.Button button_Save;
		private System.Windows.Forms.Button button_Load;
	}
}

