
using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace App.Models
{
    public class Record : IEntity
    {
        public const int STATUSNEW = 0;
        public const int STATUSCLOSED = 1;
        public const int STATUSPRIVATE = 2;
        public const int STATUSCLOSEDPRIVATE = 3;

        public virtual int Id { get; set; }
        public virtual User? User { get; set; }
        public virtual int Month { get; set; }
        public virtual int Year { get; set; }
        public virtual DateTime? StartDate { get; set; }

        public virtual DateTime? EndDate { get; set; }
        public virtual DateTime? ChangeDate { get; set; }
        public virtual string? StartAddress { get; set; }

        public virtual string? EndAddress { get; set; }

        public virtual float Price { get; set; }
        public virtual float Tax { get; set; }
        public virtual int Status {get;set;}
    }
}