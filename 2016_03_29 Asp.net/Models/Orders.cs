namespace _2016_03_29_Asp.net.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sales.Orders")]
    public partial class Orders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Orders()
        {
            var od = new List<Models.OrderDetails>();
            od.Add(new OrderDetails() { ProductID = 1 });
            this.OrderDetails = od;
        }

        public List<OrderDetails> OrderDetails { get; set; }

        [Key]
        public string OrderID { get; set; }

        public string CustomerID { get; set; }

        public string EmployeeID { get; set; }

        public string CompanyName { get; set; }

        public string OrderDate { get; set; }

        public string RequiredDate { get; set; }

        public string ShippedDate { get; set; }

        public string ShipperID { get; set; }

        [Column(TypeName = "money")]
        public decimal Freight { get; set; }

        [Required]
        [StringLength(40)]
        public string ShipName { get; set; }

        [Required]
        [StringLength(60)]
        public string ShipAddress { get; set; }

        [Required]
        [StringLength(15)]
        public string ShipCity { get; set; }

        [StringLength(15)]
        public string ShipRegion { get; set; }

        [StringLength(10)]
        public string ShipPostalCode { get; set; }

        [Required]
        [StringLength(15)]
        public string ShipCountry { get; set; }

        public virtual Employees Employees { get; set; }

        public virtual Customers Customers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        
        public virtual Shippers Shippers { get; set; }

        public string ProductID { get; set; }

        public string UnitPrice { get; set; }

        public string Qty { get; set; }
    }
}
