namespace PythonInterfaceConnection
{
    partial class PythonInterfaceConnection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PythonInterfaceConnection));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textCode = new System.Windows.Forms.TextBox();
            this.buttonCalculate = new System.Windows.Forms.Button();
            this.textOutput = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.textRelativePath = new System.Windows.Forms.TextBox();
            this.buttonRelativePath = new System.Windows.Forms.Button();
            this.textScriptModule = new System.Windows.Forms.TextBox();
            this.buttonLoadModule = new System.Windows.Forms.Button();
            this.textLoadModule = new System.Windows.Forms.TextBox();
            this.buttonRunScript = new System.Windows.Forms.Button();
            this.buttonPrintLocal = new System.Windows.Forms.Button();
            this.textLocalDictionary = new System.Windows.Forms.TextBox();
            this.buttonClearLocal = new System.Windows.Forms.Button();
            this.ButtonProfile = new System.Windows.Forms.Button();
            this.LabelProfile = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "c = [1, 2, 3]";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "d = [ [1, 2, 3], [4, 5, 6] ]";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "a = 5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "b = 4";
            // 
            // textCode
            // 
            this.textCode.Location = new System.Drawing.Point(17, 183);
            this.textCode.Multiline = true;
            this.textCode.Name = "textCode";
            this.textCode.Size = new System.Drawing.Size(239, 90);
            this.textCode.TabIndex = 6;
            this.textCode.Text = "a = 2*a**3 # still integer\r\nb = \"I\'m a string now!\"\r\nc = 2*c + 1.0 # arrays are n" +
    "umpy arrays\r\nd = np.transpose(d)";
            // 
            // buttonCalculate
            // 
            this.buttonCalculate.Location = new System.Drawing.Point(17, 279);
            this.buttonCalculate.Name = "buttonCalculate";
            this.buttonCalculate.Size = new System.Drawing.Size(238, 44);
            this.buttonCalculate.TabIndex = 7;
            this.buttonCalculate.Text = "calculate";
            this.buttonCalculate.UseVisualStyleBackColor = true;
            this.buttonCalculate.Click += new System.EventHandler(this.buttonCalculate_Click);
            // 
            // textOutput
            // 
            this.textOutput.Location = new System.Drawing.Point(17, 344);
            this.textOutput.Multiline = true;
            this.textOutput.Name = "textOutput";
            this.textOutput.Size = new System.Drawing.Size(232, 157);
            this.textOutput.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 325);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Script Output:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 165);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Script:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(244, 78);
            this.label7.TabIndex = 11;
            this.label7.Text = resources.GetString("label7.Text");
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 142);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "numpy as np";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 94);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Input Parameters:";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(276, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(2, 487);
            this.panel1.TabIndex = 14;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(291, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(236, 65);
            this.label10.TabIndex = 15;
            this.label10.Text = resources.GetString("label10.Text");
            // 
            // textRelativePath
            // 
            this.textRelativePath.Location = new System.Drawing.Point(294, 99);
            this.textRelativePath.Name = "textRelativePath";
            this.textRelativePath.Size = new System.Drawing.Size(133, 20);
            this.textRelativePath.TabIndex = 16;
            this.textRelativePath.Text = "scripts";
            // 
            // buttonRelativePath
            // 
            this.buttonRelativePath.Location = new System.Drawing.Point(443, 97);
            this.buttonRelativePath.Name = "buttonRelativePath";
            this.buttonRelativePath.Size = new System.Drawing.Size(107, 23);
            this.buttonRelativePath.TabIndex = 17;
            this.buttonRelativePath.Text = "Add Relative Path";
            this.buttonRelativePath.UseVisualStyleBackColor = true;
            this.buttonRelativePath.Click += new System.EventHandler(this.buttonRelativePath_Click);
            // 
            // textScriptModule
            // 
            this.textScriptModule.Location = new System.Drawing.Point(293, 162);
            this.textScriptModule.Multiline = true;
            this.textScriptModule.Name = "textScriptModule";
            this.textScriptModule.Size = new System.Drawing.Size(253, 48);
            this.textScriptModule.TabIndex = 18;
            this.textScriptModule.Text = "a = script1.add(1.0, 2.0)";
            // 
            // buttonLoadModule
            // 
            this.buttonLoadModule.Location = new System.Drawing.Point(443, 126);
            this.buttonLoadModule.Name = "buttonLoadModule";
            this.buttonLoadModule.Size = new System.Drawing.Size(107, 23);
            this.buttonLoadModule.TabIndex = 20;
            this.buttonLoadModule.Text = "Load Module";
            this.buttonLoadModule.UseVisualStyleBackColor = true;
            this.buttonLoadModule.Click += new System.EventHandler(this.buttonLoadModule_Click);
            // 
            // textLoadModule
            // 
            this.textLoadModule.Location = new System.Drawing.Point(294, 128);
            this.textLoadModule.Name = "textLoadModule";
            this.textLoadModule.Size = new System.Drawing.Size(133, 20);
            this.textLoadModule.TabIndex = 19;
            this.textLoadModule.Text = "script1";
            // 
            // buttonRunScript
            // 
            this.buttonRunScript.Location = new System.Drawing.Point(295, 217);
            this.buttonRunScript.Name = "buttonRunScript";
            this.buttonRunScript.Size = new System.Drawing.Size(250, 28);
            this.buttonRunScript.TabIndex = 21;
            this.buttonRunScript.Text = "Run Script";
            this.buttonRunScript.UseVisualStyleBackColor = true;
            this.buttonRunScript.Click += new System.EventHandler(this.buttonRunScript_Click);
            // 
            // buttonPrintLocal
            // 
            this.buttonPrintLocal.Location = new System.Drawing.Point(295, 256);
            this.buttonPrintLocal.Name = "buttonPrintLocal";
            this.buttonPrintLocal.Size = new System.Drawing.Size(250, 28);
            this.buttonPrintLocal.TabIndex = 22;
            this.buttonPrintLocal.Text = "Print Local Dictionary";
            this.buttonPrintLocal.UseVisualStyleBackColor = true;
            this.buttonPrintLocal.Click += new System.EventHandler(this.buttonPrintLocal_Click);
            // 
            // textLocalDictionary
            // 
            this.textLocalDictionary.Location = new System.Drawing.Point(296, 291);
            this.textLocalDictionary.Multiline = true;
            this.textLocalDictionary.Name = "textLocalDictionary";
            this.textLocalDictionary.Size = new System.Drawing.Size(248, 72);
            this.textLocalDictionary.TabIndex = 23;
            // 
            // buttonClearLocal
            // 
            this.buttonClearLocal.Location = new System.Drawing.Point(296, 369);
            this.buttonClearLocal.Name = "buttonClearLocal";
            this.buttonClearLocal.Size = new System.Drawing.Size(250, 28);
            this.buttonClearLocal.TabIndex = 24;
            this.buttonClearLocal.Text = "Clear Local Dictionary";
            this.buttonClearLocal.UseVisualStyleBackColor = true;
            this.buttonClearLocal.Click += new System.EventHandler(this.buttonClearLocal_Click);
            // 
            // ButtonProfile
            // 
            this.ButtonProfile.Location = new System.Drawing.Point(296, 403);
            this.ButtonProfile.Name = "ButtonProfile";
            this.ButtonProfile.Size = new System.Drawing.Size(89, 98);
            this.ButtonProfile.TabIndex = 25;
            this.ButtonProfile.Text = "Profile";
            this.ButtonProfile.UseVisualStyleBackColor = true;
            this.ButtonProfile.Click += new System.EventHandler(this.ButtonProfile_Click);
            // 
            // LabelProfile
            // 
            this.LabelProfile.AutoSize = true;
            this.LabelProfile.Location = new System.Drawing.Point(409, 410);
            this.LabelProfile.Name = "LabelProfile";
            this.LabelProfile.Size = new System.Drawing.Size(112, 39);
            this.LabelProfile.TabIndex = 26;
            this.LabelProfile.Text = "1000x1000 array copy\r\nin:\r\nout:";
            // 
            // PythonInterfaceConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 513);
            this.Controls.Add(this.LabelProfile);
            this.Controls.Add(this.ButtonProfile);
            this.Controls.Add(this.buttonClearLocal);
            this.Controls.Add(this.textLocalDictionary);
            this.Controls.Add(this.buttonPrintLocal);
            this.Controls.Add(this.buttonRunScript);
            this.Controls.Add(this.buttonLoadModule);
            this.Controls.Add(this.textLoadModule);
            this.Controls.Add(this.textScriptModule);
            this.Controls.Add(this.buttonRelativePath);
            this.Controls.Add(this.textRelativePath);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textOutput);
            this.Controls.Add(this.buttonCalculate);
            this.Controls.Add(this.textCode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "PythonInterfaceConnection";
            this.Text = "PythonInterfaceConnection";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textCode;
        private System.Windows.Forms.Button buttonCalculate;
        private System.Windows.Forms.TextBox textOutput;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textRelativePath;
        private System.Windows.Forms.Button buttonRelativePath;
        private System.Windows.Forms.TextBox textScriptModule;
        private System.Windows.Forms.Button buttonLoadModule;
        private System.Windows.Forms.TextBox textLoadModule;
        private System.Windows.Forms.Button buttonRunScript;
        private System.Windows.Forms.Button buttonPrintLocal;
        private System.Windows.Forms.TextBox textLocalDictionary;
        private System.Windows.Forms.Button buttonClearLocal;
        private System.Windows.Forms.Button ButtonProfile;
        private System.Windows.Forms.Label LabelProfile;
    }
}

