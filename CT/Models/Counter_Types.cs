using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CT.Models
{
    public class Counter_Types
    {
        public int Id { get; set; }
        [Display(Name = "Наименование")]
        public string CT_NAME { get; set; }
        [Display(Name = "Период поверки")]
        public Nullable<short> CT_PERIOD { get; set; }
        [Display(Name = "До запятой")]
        public short CT_LENGTH { get; set; }
        [Display(Name = "После запятой")]
        public short CT_PREC { get; set; }
        [Display(Name = "Кол-во счетных устр.")]
        public Nullable<short> CT_ELEMENTS { get; set; }
        [Display(Name = "Напряжение")]
        public Nullable<short> CT_VOLTAGE { get; set; }
        [Display(Name = "Диапазон Тока")]
        public string CT_CURRENT { get; set; }
        public int ModctId { get; set; }
        public int FaseId { get; set; }
        public int Types_OnId { get; set; }

        public virtual Modct Modct { get; set; }
        public virtual Fase Fase { get; set; }
        public virtual Types_On Types_On { get; set; }
    }



    public class Types_On
    {
        public Types_On()
        {
            this.Counter_Types = new HashSet<Counter_Types>();
        }

        public int Id { get; set; }
        [Display(Name = "Тип включения")]
        public string Name { get; set; }
        [Display(Name = "Тип включения")]
        public string ShortName { get; set; }

        public virtual ICollection<Counter_Types> Counter_Types { get; set; }
    }

    public class Fase
    {
        public Fase()
        {
            this.Counter_Types = new HashSet<Counter_Types>();
        }

        public int Id { get; set; }
        [Display(Name = "Фазность")]
        public string Name { get; set; }

        public virtual ICollection<Counter_Types> Counter_Types { get; set; }
    }

    public class Modct
    {
        public Modct()
        {
            this.Counter_Types = new HashSet<Counter_Types>();
        }

        public int Id { get; set; }
        [Display(Name = "Модель")]
        public string Name { get; set; }

        public virtual ICollection<Counter_Types> Counter_Types { get; set; }
    }

    public class CTDBContext : DbContext
    {
        public DbSet<Counter_Types> Counter_Types { get; set; }
        public DbSet<Types_On> Types_On { get; set; }
        public DbSet<Fase> Fase { get; set; }
        public DbSet<Modct> Modct { get; set; }
    }
}