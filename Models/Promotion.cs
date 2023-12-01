using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public class Promotion
    {
        int id;
        DateTime start_DateTime;
        DateTime end_DateTime;
        Boolean status;

        public Promotion()
        {
        }

        public Promotion(int id, DateTime start_DateTime, DateTime end_DateTime, Boolean status)
        {
            this.id = id;
            this.start_DateTime = start_DateTime;
            this.end_DateTime = end_DateTime;
            this.status = status;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public DateTime Start_DateTime
        {
            get { return start_DateTime; }
            set { start_DateTime = value; }
        }

        public DateTime End_DateTime
        {
            get { return end_DateTime; }
            set { end_DateTime = value; }
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
                start_DateTime + " | " +
                end_DateTime + " | " +
                status1;
        }
    }
}
