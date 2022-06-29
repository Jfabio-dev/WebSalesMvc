using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WebSalesMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        [Display(Name = "Nome")]
        [Required(ErrorMessage ="O campo nome é obrigatório")]
        [StringLength(60, MinimumLength =3)]
        public string Name  { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "O campo email é obrigatório")]
        public string Email { get; set; }

        [Display(Name ="Data de nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "O campo data de nascimento é obrigatório")]
        public DateTime BirthDate  { get; set; }
        [Display(Name = "Salário base")]
        [DisplayFormat(DataFormatString ="{0:F2}")]
        [Required(ErrorMessage = "O campo Salário base é obrigatório")]
        public double BaseSalary { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }

        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller()
        {
        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }
        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }
        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}
