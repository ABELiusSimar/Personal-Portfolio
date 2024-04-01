namespace Hello_World
{
    partial class HelloWorld
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
            HelloWorldBtn = new Button();
            SuspendLayout();
            // 
            // HelloWorldBtn
            // 
            HelloWorldBtn.Location = new Point(306, 290);
            HelloWorldBtn.Name = "HelloWorldBtn";
            HelloWorldBtn.Size = new Size(161, 29);
            HelloWorldBtn.TabIndex = 0;
            HelloWorldBtn.Text = "Hello World";
            HelloWorldBtn.UseVisualStyleBackColor = true;
            HelloWorldBtn.Click += HelloWorldBtn_Click;
            // 
            // HelloWorld
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(HelloWorldBtn);
            Name = "HelloWorld";
            Text = "Hello World";
            ResumeLayout(false);
        }

        #endregion

        private Button HelloWorldBtn;
    }
}