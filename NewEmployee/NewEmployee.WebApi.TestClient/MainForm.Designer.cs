namespace NewEmployee.WebApi.TestClient {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.uiTestDocument = new System.Windows.Forms.Button();
            this.uiTestAllDocuments = new System.Windows.Forms.Button();
            this.uiTestEmployee = new System.Windows.Forms.Button();
            this.uiTestTests = new System.Windows.Forms.Button();
            this.uiTestTestDetail = new System.Windows.Forms.Button();
            this.uiTestTestScore = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // uiTestDocument
            // 
            this.uiTestDocument.Location = new System.Drawing.Point(12, 12);
            this.uiTestDocument.Name = "uiTestDocument";
            this.uiTestDocument.Size = new System.Drawing.Size(119, 23);
            this.uiTestDocument.TabIndex = 0;
            this.uiTestDocument.Text = "Test Document";
            this.uiTestDocument.UseVisualStyleBackColor = true;
            this.uiTestDocument.Click += new System.EventHandler(this.uiTestDocument_Click);
            // 
            // uiTestAllDocuments
            // 
            this.uiTestAllDocuments.Location = new System.Drawing.Point(137, 12);
            this.uiTestAllDocuments.Name = "uiTestAllDocuments";
            this.uiTestAllDocuments.Size = new System.Drawing.Size(133, 23);
            this.uiTestAllDocuments.TabIndex = 1;
            this.uiTestAllDocuments.Text = "Test All Documents";
            this.uiTestAllDocuments.UseVisualStyleBackColor = true;
            this.uiTestAllDocuments.Click += new System.EventHandler(this.uiTestAllDocuments_Click);
            // 
            // uiTestEmployee
            // 
            this.uiTestEmployee.Location = new System.Drawing.Point(12, 41);
            this.uiTestEmployee.Name = "uiTestEmployee";
            this.uiTestEmployee.Size = new System.Drawing.Size(119, 23);
            this.uiTestEmployee.TabIndex = 2;
            this.uiTestEmployee.Text = "Test Employee";
            this.uiTestEmployee.UseVisualStyleBackColor = true;
            this.uiTestEmployee.Click += new System.EventHandler(this.uiTestEmployee_Click);
            // 
            // uiTestTests
            // 
            this.uiTestTests.Location = new System.Drawing.Point(12, 70);
            this.uiTestTests.Name = "uiTestTests";
            this.uiTestTests.Size = new System.Drawing.Size(119, 23);
            this.uiTestTests.TabIndex = 3;
            this.uiTestTests.Text = "Test Tests";
            this.uiTestTests.UseVisualStyleBackColor = true;
            this.uiTestTests.Click += new System.EventHandler(this.uiTestTests_Click);
            // 
            // uiTestTestDetail
            // 
            this.uiTestTestDetail.Location = new System.Drawing.Point(137, 70);
            this.uiTestTestDetail.Name = "uiTestTestDetail";
            this.uiTestTestDetail.Size = new System.Drawing.Size(133, 23);
            this.uiTestTestDetail.TabIndex = 4;
            this.uiTestTestDetail.Text = "Test TestDetail";
            this.uiTestTestDetail.UseVisualStyleBackColor = true;
            this.uiTestTestDetail.Click += new System.EventHandler(this.uiTestTestDetail_Click);
            // 
            // uiTestTestScore
            // 
            this.uiTestTestScore.Location = new System.Drawing.Point(12, 99);
            this.uiTestTestScore.Name = "uiTestTestScore";
            this.uiTestTestScore.Size = new System.Drawing.Size(119, 23);
            this.uiTestTestScore.TabIndex = 5;
            this.uiTestTestScore.Text = "Test TestScore";
            this.uiTestTestScore.UseVisualStyleBackColor = true;
            this.uiTestTestScore.Click += new System.EventHandler(this.uiTestTestScore_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 145);
            this.Controls.Add(this.uiTestTestScore);
            this.Controls.Add(this.uiTestTestDetail);
            this.Controls.Add(this.uiTestTests);
            this.Controls.Add(this.uiTestEmployee);
            this.Controls.Add(this.uiTestAllDocuments);
            this.Controls.Add(this.uiTestDocument);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "NewEmployee API Test Client";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button uiTestDocument;
        private System.Windows.Forms.Button uiTestAllDocuments;
        private System.Windows.Forms.Button uiTestEmployee;
        private System.Windows.Forms.Button uiTestTests;
        private System.Windows.Forms.Button uiTestTestDetail;
        private System.Windows.Forms.Button uiTestTestScore;
    }
}

