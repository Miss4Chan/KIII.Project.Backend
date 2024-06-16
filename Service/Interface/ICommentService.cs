using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface
{
    public interface ICommentService
    {
        CommentDTO AddComment( CommentDTO comment, int rating);
        bool DeleteComment(int commentId);
        public List<CommentDTO> GetCommentsByReceiverUsername(string receiverUsername);
    }
}
