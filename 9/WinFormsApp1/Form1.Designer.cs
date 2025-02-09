namespace WinFormsApp1;

partial class Form1
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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        button1 = new System.Windows.Forms.Button();
        textBox1 = new System.Windows.Forms.TextBox();
        TextPanel = new System.Windows.Forms.ListBox();
        textBox2 = new System.Windows.Forms.TextBox();
        button2 = new System.Windows.Forms.Button();
        SuspendLayout();
        // 
        // button1
        // 
        button1.Location = new System.Drawing.Point(1101, 1314);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(195, 39);
        button1.TabIndex = 0;
        button1.Text = "Отправить";
        button1.UseVisualStyleBackColor = true;
        button1.Visible = false;
        button1.Click += button1_Click_1;
        // 
        // textBox1
        // 
        textBox1.Location = new System.Drawing.Point(33, 1314);
        textBox1.Name = "textBox1";
        textBox1.Size = new System.Drawing.Size(1043, 39);
        textBox1.TabIndex = 2;
        textBox1.Visible = false;
        // 
        // TextPanel
        // 
        TextPanel.FormattingEnabled = true;
        TextPanel.Location = new System.Drawing.Point(33, 5);
        TextPanel.Name = "TextPanel";
        TextPanel.Size = new System.Drawing.Size(1711, 1284);
        TextPanel.TabIndex = 3;
        TextPanel.Visible = false;
        // 
        // textBox2
        // 
        textBox2.Location = new System.Drawing.Point(566, 457);
        textBox2.Name = "textBox2";
        textBox2.Size = new System.Drawing.Size(648, 39);
        textBox2.TabIndex = 4;
        // 
        // button2
        // 
        button2.Location = new System.Drawing.Point(563, 516);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(651, 67);
        button2.TabIndex = 5;
        button2.Text = "Отправить";
        button2.UseVisualStyleBackColor = true;
        button2.Click += button2_Click;
        // 
        // Form1
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(1777, 1395);
        Controls.Add(button2);
        Controls.Add(textBox2);
        Controls.Add(TextPanel);
        Controls.Add(textBox1);
        Controls.Add(button1);
        Text = "Form1";
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.Button button2;

    private System.Windows.Forms.TextBox textBox2;

    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.ListBox TextPanel;

    #endregion
}