using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Models
{
    public class Decentralization
    {
        int role_id;
        int module_id;
        int function_id;

        public Decentralization()
        {
        }

        public Decentralization(int role_id, int module_id, int function_id)
        {
            this.role_id = role_id;
            this.module_id = module_id;
            this.function_id = function_id;
        }

        public int Role_id
        {
            get { return role_id; }
            set { role_id = value; }
        }

        public int Module_id
        {
            get { return module_id; }
            set { module_id = value; }
        }

        public int Function_id
        {
            get { return function_id; }
            set { function_id = value; }
        }
        
        public override string ToString()
        {
            return role_id + " | " +
                module_id + " | " +
                function_id;
        }
    }
}
