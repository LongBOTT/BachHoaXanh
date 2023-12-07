using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BachHoaXanh.Views.Chart
{
    public class LegendItem : FlowLayoutPanel
    {
        private LabelColor lbColor;
        private Label lbName;

        public LegendItem(ModelLegend data)
        {
            FlowDirection = FlowDirection.LeftToRight;
            //Dock = DockStyle.Fill;
            AutoSize = true;
            Padding = new Padding(5);
            InitializeComponent();
            BackColor = Color.Transparent;
            lbColor.BackColor = data.Color;
            lbName.Text = data.Name;
        }

        private void InitializeComponent()
        {
            lbColor = new LabelColor();
            lbName = new Label();
            lbName.ForeColor = Color.FromArgb(100, 100, 100);
            lbName.Text = "Name";
            lbName.Size = new Size(100, 50);
            lbColor.Size = new Size(100, 50);
            SuspendLayout();
            Controls.Add(lbColor);
            Controls.Add(lbName);
            ResumeLayout(false);
        }
    }
}
