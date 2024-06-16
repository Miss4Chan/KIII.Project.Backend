using Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Domain_models
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }
        [ForeignKey("CommenterId")]
        public ShopApplicationUser Commenter { get; set; }
        [ForeignKey("ReceiverId")]
        public ShopApplicationUser Receiver { get; set; }
        public string FormattedDate { get; set; }
        public string FormattedTime { get; set; }
    }
}
