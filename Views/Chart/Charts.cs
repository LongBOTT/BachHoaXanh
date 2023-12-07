using BachHoaXanh.Views.Chart.BlankChart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Views.Chart
{
    public class Charts : Panel
    {
        private List<ModelLegend> legends = new List<ModelLegend>();
        private List<ModelChart> model = new List<ModelChart>();
        private readonly int seriesSize = 12;
        private readonly System.Windows.Forms.Timer animator = new System.Windows.Forms.Timer();
        private float animate;
        private readonly int seriesSpace = 6;

        private BlankPlotChart blankPlotChart;
        private FlowLayoutPanel panelLegend;

        public Charts()
        {
            InitializeComponent();
            //animator.Interval = 16; // Adjust the interval as needed for smooth animation
            //animator.Tick += Animator_Tick;
            //blankPlotChart.BlankPlotChatRender = new CustomPlotChatRender(legends, model, seriesSize, seriesSpace, animate);
            //blankPlotChart.Invalidate();
        }

        private void Animator_Tick(object sender, EventArgs e)
        {
            animate += 0.05f; // Adjust the step as needed
            if (animate >= 1.0f)
            {
                animate = 1.0f;
                animator.Stop();
            }
            blankPlotChart.BlankPlotChatRender = new CustomPlotChatRender(legends, model, seriesSize, seriesSpace, animate);
            Invalidate();
        }

        public void AddLegend(string name, Color color)
        {
            ModelLegend data = new ModelLegend(name, color);
            legends.Add(data);
            panelLegend.Controls.Add(new LegendItem(data));
            panelLegend.Invalidate();
            panelLegend.PerformLayout();
        }

        public void AddData(ModelChart data)
        {
            model.Add(data);
            blankPlotChart.LabelCount = model.Count;
            double max = data.GetMaxValues();
            if (max > blankPlotChart.MaxValues)
            {
                blankPlotChart.MaxValues = max;
            }
        }

        public void Clear()
        {
            animate = 0;
            blankPlotChart.LabelCount = 0;
            blankPlotChart.MaxValues = 0;
            model.Clear();
            Invalidate();
        }

        public void Start()
        {
            blankPlotChart.BlankPlotChatRender = new CustomPlotChatRender(legends, model, seriesSize, seriesSpace, 1.0f);
            Invalidate();
        }

        private void InitializeComponent()
        {
            blankPlotChart = new BlankPlotChart();
            panelLegend = new FlowLayoutPanel();
            panelLegend.FlowDirection = FlowDirection.LeftToRight;
            panelLegend.Dock = DockStyle.Right; 
            panelLegend.Padding = new Padding(5);
            SuspendLayout();

            BackColor = Color.White;

            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel.WrapContents = false; 


            blankPlotChart.Dock = DockStyle.Fill;
            panelLegend.Dock = DockStyle.Fill;

            var tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.SuspendLayout();
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 90F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel.Controls.Add(blankPlotChart, 0, 0);
            tableLayoutPanel.Controls.Add(panelLegend, 0, 1);
            Controls.Add(tableLayoutPanel);
            tableLayoutPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

    }
}
