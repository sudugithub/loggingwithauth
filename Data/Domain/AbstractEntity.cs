using System.ComponentModel.DataAnnotations;

namespace Data.Domain
{
    public class AbstractEntity
    {
        [Key]
        public long Id { get; set; }
        public DateTime? CreatedTime { get; set; }
        public long CreatedById { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public long? UpdatedById { get; set; }
    }
}
