
using pro.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace YourNamespace.Models
{
    public class FileUploadResponse
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string FilePath { get; set; }

        [Required]
        public long FileSize { get; set; }

        [Required]
        public string ContentType { get; set; }

        [JsonIgnore]
        public User User { get; set; }


    }
}
