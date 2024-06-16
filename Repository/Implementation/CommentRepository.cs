using Domain.Domain_models;
using Domain.DTO;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Implementation
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext context;
        private DbSet<Comment> entities;
        string errorMessage = string.Empty;

        public CommentRepository(AppDbContext context)
        {
            this.context = context;
            entities = context.Set<Comment>();
        }

        public void Delete(int commentId)
        {
            Comment entity = entities.Where(e => e.Id == commentId).FirstOrDefault();

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entities.Remove(entity);
            context.SaveChanges();
        }

        public void Insert(Comment entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(Comment entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entities.Update(entity);
            context.SaveChanges();
        }

        public List<CommentDTO> GetByReceiver(int receiverId)
        {
            return entities.Where(c => c.Receiver.Id == receiverId)
                      .Include(c => c.Commenter)
                      .OrderByDescending(c => c.FormattedDate).ThenByDescending(c => c.FormattedTime)
                      .Select(c => (CommentDTO)c)
                      .ToList();
        }

    }
}
