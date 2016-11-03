using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.Entities
{
    public class Journal : BaseEntity, IEquatable<Journal>
    {
        public int JournalId { get; set; }
        public string OwnerId { get; set; }
        [Required]
        [StringLength(75, ErrorMessage = "Titles must be less than 75 :(")]
        public string Title { get; set; }
        [StringLength(1024, ErrorMessage = "Descriptions must be within 1,024 characters!")]
        public string Description { get; set; }
        public string CoverUrl { get; set; }
        public int BookmarkCount { get; set; }
        public int PageCount { get; set; }
        public DateTime LastEdited { get; set; }

        public bool Equals(Journal other)
        {
            if (other == null)
            {
                return false;
            }
            else if(this.JournalId == 0)
            {
                return false;
            }
            else if(ReferenceEquals(this, other))
            {
                return true;
            }
            else if(this.JournalId == other.JournalId
                    && this.OwnerId == other.OwnerId
                    && this.Title == other.Title
                    && this.Description == other.Description
                    && this.CoverUrl == other.CoverUrl)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
