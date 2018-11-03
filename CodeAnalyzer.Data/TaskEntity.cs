using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CodeAnalyzer.Data
{
    [Table("Tasks")]
    public class TaskEntity
    {
        [Required]
        [Column("FileId")]
        public int FileID { get; set; }

        [Required]
        [Column("StringNumber")]
        public int StringNumber { get; set; }

        [Column("Task")]
        public string Task { get; set; }

        [Column("Tooltip")]
        public string Tooltip { get; set; }

        [ForeignKey(nameof(FileID))]
        public FileEntity File { get; set; }
    }
}
