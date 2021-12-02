﻿
namespace LocationDesigner
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tilesetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.backgroundPanel = new System.Windows.Forms.Panel();
            this.foregroundPanel = new System.Windows.Forms.Panel();
            this.renderPanel1 = new LevelDesigner.Controls.RenderPanel();
            this.upPanel = new System.Windows.Forms.Panel();
            this.leftPanel = new System.Windows.Forms.Panel();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.downPanel = new System.Windows.Forms.Panel();
            this.doodadPanel = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.tilesetsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1904, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveFileToolStripMenuItem,
            this.loadFileToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveFileToolStripMenuItem
            // 
            this.saveFileToolStripMenuItem.Name = "saveFileToolStripMenuItem";
            this.saveFileToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.saveFileToolStripMenuItem.Text = "Save File";
            this.saveFileToolStripMenuItem.Click += new System.EventHandler(this.SaveFileToolStripMenuItem_Click);
            // 
            // loadFileToolStripMenuItem
            // 
            this.loadFileToolStripMenuItem.Name = "loadFileToolStripMenuItem";
            this.loadFileToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.loadFileToolStripMenuItem.Text = "Load File";
            this.loadFileToolStripMenuItem.Click += new System.EventHandler(this.LoadFileToolStripMenuItem_Click);
            // 
            // tilesetsToolStripMenuItem
            // 
            this.tilesetsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backgroundToolStripMenuItem});
            this.tilesetsToolStripMenuItem.Name = "tilesetsToolStripMenuItem";
            this.tilesetsToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.tilesetsToolStripMenuItem.Text = "Tilesets";
            // 
            // backgroundToolStripMenuItem
            // 
            this.backgroundToolStripMenuItem.Name = "backgroundToolStripMenuItem";
            this.backgroundToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.backgroundToolStripMenuItem.Text = "Set Tileset Directory";
            this.backgroundToolStripMenuItem.Click += new System.EventHandler(this.SetTileSetDirectory_Click);
            // 
            // backgroundPanel
            // 
            this.backgroundPanel.AutoScroll = true;
            this.backgroundPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.backgroundPanel.Location = new System.Drawing.Point(12, 157);
            this.backgroundPanel.Name = "backgroundPanel";
            this.backgroundPanel.Size = new System.Drawing.Size(563, 421);
            this.backgroundPanel.TabIndex = 2;
            // 
            // foregroundPanel
            // 
            this.foregroundPanel.AutoScroll = true;
            this.foregroundPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.foregroundPanel.Location = new System.Drawing.Point(12, 584);
            this.foregroundPanel.Name = "foregroundPanel";
            this.foregroundPanel.Size = new System.Drawing.Size(563, 421);
            this.foregroundPanel.TabIndex = 3;
            // 
            // renderPanel1
            // 
            this.renderPanel1.DrawInterval = 100;
            this.renderPanel1.Location = new System.Drawing.Point(596, 45);
            this.renderPanel1.MouseHoverUpdatesOnly = false;
            this.renderPanel1.Name = "renderPanel1";
            this.renderPanel1.Size = new System.Drawing.Size(1280, 960);
            this.renderPanel1.TabIndex = 1;
            this.renderPanel1.Text = "renderPanel1";
            // 
            // upPanel
            // 
            this.upPanel.Location = new System.Drawing.Point(596, 27);
            this.upPanel.Name = "upPanel";
            this.upPanel.Size = new System.Drawing.Size(1280, 23);
            this.upPanel.TabIndex = 4;
            this.upPanel.Tag = "2";
            // 
            // leftPanel
            // 
            this.leftPanel.Location = new System.Drawing.Point(578, 45);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(17, 960);
            this.leftPanel.TabIndex = 5;
            this.leftPanel.Tag = "0";
            this.leftPanel.MouseHover += new System.EventHandler(this.MouseHover);
            // 
            // rightPanel
            // 
            this.rightPanel.Location = new System.Drawing.Point(1875, 45);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(29, 960);
            this.rightPanel.TabIndex = 6;
            this.rightPanel.Tag = "1";
            // 
            // downPanel
            // 
            this.downPanel.Location = new System.Drawing.Point(596, 1006);
            this.downPanel.Name = "downPanel";
            this.downPanel.Size = new System.Drawing.Size(1280, 33);
            this.downPanel.TabIndex = 5;
            this.downPanel.Tag = "3";
            // 
            // doodadPanel
            // 
            this.doodadPanel.AutoScroll = true;
            this.doodadPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.doodadPanel.Location = new System.Drawing.Point(12, 27);
            this.doodadPanel.Name = "doodadPanel";
            this.doodadPanel.Size = new System.Drawing.Size(563, 124);
            this.doodadPanel.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.doodadPanel);
            this.Controls.Add(this.downPanel);
            this.Controls.Add(this.rightPanel);
            this.Controls.Add(this.leftPanel);
            this.Controls.Add(this.upPanel);
            this.Controls.Add(this.foregroundPanel);
            this.Controls.Add(this.backgroundPanel);
            this.Controls.Add(this.renderPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tilesetsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backgroundToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private LevelDesigner.Controls.RenderPanel renderPanel1;
        private System.Windows.Forms.Panel backgroundPanel;
        private System.Windows.Forms.Panel foregroundPanel;
        private System.Windows.Forms.ToolStripMenuItem saveFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadFileToolStripMenuItem;
        private System.Windows.Forms.Panel upPanel;
        private System.Windows.Forms.Panel leftPanel;
        private System.Windows.Forms.Panel rightPanel;
        private System.Windows.Forms.Panel downPanel;
        private System.Windows.Forms.Panel doodadPanel;
    }
}

