using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GitarhusetCopyMaker.DataLawyer
{
    [Table("tblProduct")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.Url)]
        public string Url { get; set; }

        public string Manufacturer { get; set; }

        public string Code { get; set; }

        public int Price { get; set; }

        public bool Checked { get; set; }

        public int Quantity { get; set; }

        public int Discount { get; set; }

        public string Description { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public ICollection<Picture> Pictures { get; set; }
    }
}
