using System;

public Button()
{
    button1 = new Button();
    button1.Text = "Kliknij mnie!";
    button1.Location = new System.Drawing.Point(50, 50);
    button1.Click += new System.EventHandler(this.button1_Click);
    this.Controls.Add(button1);
}