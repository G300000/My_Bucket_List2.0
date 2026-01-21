using System.ComponentModel.DataAnnotations;

namespace Bucket_list_mvc.Models
{
    public class BucketItem
    {
        [Key]
        public int Id { get; set; }
        public string ActivityName { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
