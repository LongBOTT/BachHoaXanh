using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public class Discount
    {
        int id;
        double percent;
        DateTime start_Date;
        DateTime end_Date;
        Boolean status;

        public Discount()
        {
        }

        public Discount(int id, double percent, DateTime start_Date, DateTime end_Date, Boolean status)
        {
            this.id = id;
            this.percent = percent;
            this.start_Date = start_Date;
            this.end_Date = end_Date;
            this.status = status;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public double Percent
        {
            get { return percent; }
            set { percent = value; }
        }

        public DateTime Start_DateTime
        {
            get { return start_Date; }
            set { start_Date = value; }
        }

        public DateTime End_DateTime
        {
            get { return end_Date; }
            set { end_Date = value; }
        }

        public Boolean Status
        {
            get { return status; }
            set { status = value; }
        }

        public override string ToString()
        {
            string status1 = status ? "Ngưng áp dụng" : "Đang áp dụng";
            return id + " | " +
                percent + " | " +
                start_Date + " | " +
                end_Date + " | " +
                status1;
        }
    }
}
