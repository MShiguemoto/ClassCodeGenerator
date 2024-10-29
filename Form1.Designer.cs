namespace Testes_Estudos
{
    partial class Form1
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
            this.btn_Testar = new System.Windows.Forms.Button();
            this.Selecionar_Xml = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_Testar
            // 
            this.btn_Testar.Location = new System.Drawing.Point(345, 216);
            this.btn_Testar.Name = "btn_Testar";
            this.btn_Testar.Size = new System.Drawing.Size(75, 23);
            this.btn_Testar.TabIndex = 0;
            this.btn_Testar.Text = "testar";
            this.btn_Testar.UseVisualStyleBackColor = true;
            this.btn_Testar.Click += new System.EventHandler(this.btn_Testar_Click);
            // 
            // Selecionar_Xml
            // 
            this.Selecionar_Xml.Location = new System.Drawing.Point(345, 162);
            this.Selecionar_Xml.Name = "Selecionar_Xml";
            this.Selecionar_Xml.Size = new System.Drawing.Size(75, 23);
            this.Selecionar_Xml.TabIndex = 1;
            this.Selecionar_Xml.Text = "Selecionar XML";
            this.Selecionar_Xml.UseVisualStyleBackColor = true;
            this.Selecionar_Xml.Click += new System.EventHandler(this.Selecionar_Xml_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Selecionar_Xml);
            this.Controls.Add(this.btn_Testar);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button btn_Testar;
        private System.Windows.Forms.Button Selecionar_Xml;
    }
}

