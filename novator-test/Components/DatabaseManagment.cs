using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using novator_test.Models;

namespace novator_test.Components
{
    public class DatabaseManagment : DbContext
    {
        public DbSet<StaffDTO> staffs { get; set; }

        public DatabaseManagment() {
            Database.EnsureCreated();//чтобы в случае отсутствия бд она создалась
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //тут просто данные для подключения к бд
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-Q7GK63V\SQLEXPRESS;Database=novator;Trusted_Connection=True;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True");
            }
        }
    }

    public class StaffManagment
    {

        static ObservableCollection<StaffDTO> DbSetToObservable(DbSet<StaffDTO> dbSet) {
//сделал эту функцию для того, чтобы преобразовать коллекцию для БД в коллекцию для DataGrid, т.к. нет готового решения (по крайней мере я не знаю)
            ObservableCollection<StaffDTO> staffs = new ObservableCollection<StaffDTO>();
            foreach (StaffDTO item in dbSet)
            {
                staffs.Add(new StaffDTO(item.name, item.lastname, item.patronymic));
            }
            return staffs;
        }

        public StaffDTO Add(string name, string lastname, string patronymic) {
            StaffDTO staff = new StaffDTO(name, lastname, patronymic);
            using (DatabaseManagment db = new DatabaseManagment())
            {
                db.staffs.Add(staff);
                db.SaveChanges();

            }
            return staff;
        }

        public ObservableCollection<StaffDTO> Get() {

            using (DatabaseManagment db = new DatabaseManagment())
            {
                return SortingStaffs(DbSetToObservable(db.staffs));
            }

        }

        public bool Delete(string name)
        {
            using (DatabaseManagment db = new DatabaseManagment())
            {
                db.staffs.Remove(db.staffs.FirstOrDefault((x) => x.name == name));
                db.SaveChanges();
                return true;
            }
        }

        static ObservableCollection<StaffDTO> SortingStaffs(ObservableCollection<StaffDTO> array)
        {
            StaffDTO temp;
            Trace.WriteLine($"count - {array.Count}");

            for (int i = 0; i < array.Count; i++)
            {
                for (int j = 0; j < array.Count; j++)
                {
                    if ((int)(array[i].lastname[0]) < (int)(array[j].lastname[0]))
                    {
                        Trace.WriteLine($"Буква {(array[i].lastname[0])} - {(int)(array[i].lastname[0])} < буква {(array[j].lastname[0])} - {(int)(array[j].lastname[0])}");
                        temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
                }

            }

            return array;
        }
    }
}
