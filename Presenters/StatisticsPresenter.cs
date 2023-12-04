using BachHoaXanh.Views.InterfaceView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Presenters
{
    public class StatisticsPresenter
    {
        private IStatisticsView view;
        public StatisticsPresenter(IStatisticsView view)
        {
            this.view = view;
            this.view.TabPageGenneral.Click += Genneral;
            this.view.TabPageByYear.Click += ByYear;
            this.view.TabPageByQuater.Click += ByQuater;
            this.view.TabPageByMonth.Click += ByMonth;

        }

        private void ByMonth(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ByQuater(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ByYear(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Genneral(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
