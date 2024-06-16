using Domain.Domain_models;
using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface ICommentRepository
    {
        List<CommentDTO> GetByReceiver(int receiverId);
        void Insert(Comment entity);
        void Update(Comment entity);
        void Delete(int commentId);
    }
}
