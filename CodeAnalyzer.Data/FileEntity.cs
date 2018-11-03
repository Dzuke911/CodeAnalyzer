using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CodeAnalyzer.Data
{
    [Table("Files")]
    public class FileEntity
    {
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("SaveTime")]
        public DateTime SaveTime { get; set; }
    }
}
