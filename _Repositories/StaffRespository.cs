﻿using BachHoaXanh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh._Repositories
{
    public class StaffRepository : Repository, IStaffRepository
    {
        private List<Staff> _staffs;
        public StaffRepository() : base("staff",
           new List<string> { "id",
                "name",
                "gender",
                "birthdate",
                "phone",
                "address",
                "email",
                "entry_date",
                "deleted"})
        {
            _staffs = SearchStaff(new List<string> { "deleted = 0" });
        }

        public List<Staff> convertToStaff(List<List<string>> data)
        {
            return convert(data, row =>
            {
                row[2] = row[2].Equals("0") ? "false" : "true";
                row[row.Count() - 1] = row[row.Count() - 1].Equals("0") ? "false" : "true";
                try
                {
                    return new Staff(
                    Convert.ToInt16(row[0]), // id
                    row[1], // name
                    Convert.ToBoolean(row[2]), // gender
                    Convert.ToDateTime(row[3]), // birthday
                    row[4], // phone
                    row[5], // address
                    row[6], // email
                    Convert.ToDateTime(row[7]), // entry_date
                    Convert.ToBoolean(row[8])
                    );
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error occurred in StaffRepository.convertToStaff(): " + e.Message);
                }
                return new Staff();
            });
        }
        public void Add()
        {
            throw new NotImplementedException();
        }

        public int Delete(Staff staff)
        {
            throw new NotImplementedException();
        }

        public void Edit(Staff staff)
        {
            throw new NotImplementedException();
        }


        public List<Staff> SearchStaff(List<string> conditions)
        {
            try
            {
                return convertToStaff(read(conditions));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred in StaffRepository.searchStaff(): " + e.Message);
            }
            return new List<Staff>();
        }

        public int GetAutoID()
        {
            return GetAutoID(_staffs);
        }

        public Object GetValueByKey(Staff staff, string key)
        {
            return key switch
            {
                "id" => staff.Id,
                "name" => staff.Name,
                "gender" => staff.Gender,
                "birthday" => staff.Birthday,
                "phone" => staff.Phone,
                "address" => staff.Address,
                "email" => staff.Email,
                "entry_date" => staff.Entry_DateTime,
                _ => new Object()
            };
        }

        public List<Staff> FindObjectsBy(String key, Object value, IEnumerable<Staff> objectList)
        {
            List<Staff> objects = new List<Staff>();
            foreach (Staff staff in objectList)
                if (GetValueByKey(staff, key).Equals(value))
                    objects.Add(staff);
            return objects;
        }

        public List<Staff> FindStaffsBy(Dictionary<string, Object> conditions)
        {
            List<Staff> staffs = new List<Staff>();
            foreach (KeyValuePair<string, Object> entry in conditions)
                staffs = FindObjectsBy(entry.Key, entry.Value, _staffs);
            return staffs;
        }

        public List<Staff> FindStaffs(string key, string value)
        {
            List<Staff> list = new List<Staff>();

            foreach (Staff staff in _staffs)
            {
                if (GetValueByKey(staff, key)?.ToString()?.ToLower().Contains(value.ToLower()) == true)
                {
                    list.Add(staff);
                }
            }

            return list;
        }

        public IEnumerable<Staff> GetAll()
        {
            return _staffs;
        }
    }
}
