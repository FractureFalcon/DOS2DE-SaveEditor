using DOS2DE_SaveEditor.Source.Main;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DOS2DE_SaveEditor
{
    public partial class MainForm : Form
    {
        public FormBindings _formBindings;

        public MainForm(FormBindings formBindings)
        {
            _formBindings = formBindings;

            InitializeComponent();

            TestButton.Click += new EventHandler(TestButton_Click);
            ImportButton.Click += new EventHandler(ImportButton_Click);
            ExportButton.Click += new EventHandler(ExportButton_Click);
        }

        public void TestButton_Click(object sender, EventArgs eventArgs)
        {
            _formBindings.RunNullSaveTest();
        }

        public void ImportButton_Click(object sender, EventArgs eventArgs)
        {
            SaveGame saveGame = _formBindings.ImportSaveGameLsv(saveGameInputTextBox.Text);

            if (saveGame.LoadedSuccessfully())
            {
                saveGameContentsDisplayLabel.Text = saveGame.SaveName;
                treeView1.Nodes.Add(saveGame.XmlContents);
            }
        }

        public void ExportButton_Click(object sender, EventArgs eventArgs)
        {
            _formBindings.ExportCurrentSaveGame(saveGameOutputTextBox.Text);
        }

        private Label saveGameInputLabel;
        private TextBox saveGameInputTextBox;
        private Label saveGameOutputLabel;
        private Button TestButton;
        private FlowLayoutPanel mainFlowLayoutPanel;
        private Button ImportButton;
        private Button ExportButton;
        private Label saveGameContentsLabel;
        private FlowLayoutPanel saveGameContentflowLayout;
        private Label saveGameContentsDisplayLabel;
        private TreeView treeView1;
        private TextBox saveGameOutputTextBox;

        private void InitializeComponent()
        {
            this.saveGameInputLabel = new System.Windows.Forms.Label();
            this.saveGameInputTextBox = new System.Windows.Forms.TextBox();
            this.saveGameOutputLabel = new System.Windows.Forms.Label();
            this.saveGameOutputTextBox = new System.Windows.Forms.TextBox();
            this.TestButton = new System.Windows.Forms.Button();
            this.mainFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.ImportButton = new System.Windows.Forms.Button();
            this.saveGameContentflowLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.saveGameContentsLabel = new System.Windows.Forms.Label();
            this.saveGameContentsDisplayLabel = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.ExportButton = new System.Windows.Forms.Button();
            this.mainFlowLayoutPanel.SuspendLayout();
            this.saveGameContentflowLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveGameInputLabel
            // 
            this.saveGameInputLabel.AutoSize = true;
            this.saveGameInputLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.saveGameInputLabel.Location = new System.Drawing.Point(15, 15);
            this.saveGameInputLabel.Margin = new System.Windows.Forms.Padding(5);
            this.saveGameInputLabel.Name = "saveGameInputLabel";
            this.saveGameInputLabel.Size = new System.Drawing.Size(227, 25);
            this.saveGameInputLabel.TabIndex = 0;
            this.saveGameInputLabel.Text = "Save game input path:";
            // 
            // saveGameInputTextBox
            // 
            this.saveGameInputTextBox.Location = new System.Drawing.Point(15, 50);
            this.saveGameInputTextBox.Margin = new System.Windows.Forms.Padding(5);
            this.saveGameInputTextBox.Name = "saveGameInputTextBox";
            this.saveGameInputTextBox.Size = new System.Drawing.Size(745, 31);
            this.saveGameInputTextBox.TabIndex = 1;
            // 
            // saveGameOutputLabel
            // 
            this.saveGameOutputLabel.AutoSize = true;
            this.saveGameOutputLabel.Location = new System.Drawing.Point(15, 274);
            this.saveGameOutputLabel.Margin = new System.Windows.Forms.Padding(5);
            this.saveGameOutputLabel.Name = "saveGameOutputLabel";
            this.saveGameOutputLabel.Size = new System.Drawing.Size(240, 25);
            this.saveGameOutputLabel.TabIndex = 2;
            this.saveGameOutputLabel.Text = "Save game output path:";
            // 
            // saveGameOutputTextBox
            // 
            this.saveGameOutputTextBox.Location = new System.Drawing.Point(15, 309);
            this.saveGameOutputTextBox.Margin = new System.Windows.Forms.Padding(5);
            this.saveGameOutputTextBox.Name = "saveGameOutputTextBox";
            this.saveGameOutputTextBox.Size = new System.Drawing.Size(745, 31);
            this.saveGameOutputTextBox.TabIndex = 3;
            // 
            // TestButton
            // 
            this.TestButton.Location = new System.Drawing.Point(15, 399);
            this.TestButton.Margin = new System.Windows.Forms.Padding(5);
            this.TestButton.Name = "TestButton";
            this.TestButton.Size = new System.Drawing.Size(119, 39);
            this.TestButton.TabIndex = 4;
            this.TestButton.Text = "Test";
            this.TestButton.UseVisualStyleBackColor = true;
            // 
            // mainFlowLayoutPanel
            // 
            this.mainFlowLayoutPanel.AutoSize = true;
            this.mainFlowLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mainFlowLayoutPanel.Controls.Add(this.saveGameInputLabel);
            this.mainFlowLayoutPanel.Controls.Add(this.saveGameInputTextBox);
            this.mainFlowLayoutPanel.Controls.Add(this.ImportButton);
            this.mainFlowLayoutPanel.Controls.Add(this.saveGameContentflowLayout);
            this.mainFlowLayoutPanel.Controls.Add(this.treeView1);
            this.mainFlowLayoutPanel.Controls.Add(this.saveGameOutputLabel);
            this.mainFlowLayoutPanel.Controls.Add(this.saveGameOutputTextBox);
            this.mainFlowLayoutPanel.Controls.Add(this.ExportButton);
            this.mainFlowLayoutPanel.Controls.Add(this.TestButton);
            this.mainFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.mainFlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.mainFlowLayoutPanel.Margin = new System.Windows.Forms.Padding(5);
            this.mainFlowLayoutPanel.Name = "mainFlowLayoutPanel";
            this.mainFlowLayoutPanel.Padding = new System.Windows.Forms.Padding(10);
            this.mainFlowLayoutPanel.Size = new System.Drawing.Size(774, 885);
            this.mainFlowLayoutPanel.TabIndex = 5;
            // 
            // ImportButton
            // 
            this.ImportButton.Location = new System.Drawing.Point(15, 91);
            this.ImportButton.Margin = new System.Windows.Forms.Padding(5);
            this.ImportButton.Name = "ImportButton";
            this.ImportButton.Size = new System.Drawing.Size(119, 39);
            this.ImportButton.TabIndex = 5;
            this.ImportButton.Text = "Import";
            this.ImportButton.UseVisualStyleBackColor = true;
            // 
            // saveGameContentflowLayout
            // 
            this.saveGameContentflowLayout.AutoSize = true;
            this.saveGameContentflowLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.saveGameContentflowLayout.Controls.Add(this.saveGameContentsLabel);
            this.saveGameContentflowLayout.Controls.Add(this.saveGameContentsDisplayLabel);
            this.saveGameContentflowLayout.Location = new System.Drawing.Point(13, 138);
            this.saveGameContentflowLayout.Name = "saveGameContentflowLayout";
            this.saveGameContentflowLayout.Size = new System.Drawing.Size(388, 25);
            this.saveGameContentflowLayout.TabIndex = 10;
            // 
            // saveGameContentsLabel
            // 
            this.saveGameContentsLabel.AutoSize = true;
            this.saveGameContentsLabel.Location = new System.Drawing.Point(3, 0);
            this.saveGameContentsLabel.Name = "saveGameContentsLabel";
            this.saveGameContentsLabel.Size = new System.Drawing.Size(214, 25);
            this.saveGameContentsLabel.TabIndex = 8;
            this.saveGameContentsLabel.Text = "Save game contents:";
            // 
            // saveGameContentsDisplayLabel
            // 
            this.saveGameContentsDisplayLabel.AutoSize = true;
            this.saveGameContentsDisplayLabel.Location = new System.Drawing.Point(223, 0);
            this.saveGameContentsDisplayLabel.Name = "saveGameContentsDisplayLabel";
            this.saveGameContentsDisplayLabel.Size = new System.Drawing.Size(162, 25);
            this.saveGameContentsDisplayLabel.TabIndex = 9;
            this.saveGameContentsDisplayLabel.Text = "No save loaded";
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(13, 169);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(747, 97);
            this.treeView1.TabIndex = 11;
            // 
            // ExportButton
            // 
            this.ExportButton.Location = new System.Drawing.Point(15, 350);
            this.ExportButton.Margin = new System.Windows.Forms.Padding(5);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(119, 39);
            this.ExportButton.TabIndex = 6;
            this.ExportButton.Text = "Export";
            this.ExportButton.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(774, 885);
            this.Controls.Add(this.mainFlowLayoutPanel);
            this.Name = "MainForm";
            this.mainFlowLayoutPanel.ResumeLayout(false);
            this.mainFlowLayoutPanel.PerformLayout();
            this.saveGameContentflowLayout.ResumeLayout(false);
            this.saveGameContentflowLayout.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void saveGameContentsListbox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
