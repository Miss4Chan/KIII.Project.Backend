using Domain.Domain_models;
using Domain.DTO;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Implementation
{
    public class CommentService : ICommentService
    {
        public readonly IUserRepository _userRepository;
        public readonly ICommentRepository _commentRepository;

        public CommentService(IUserRepository userRepository, ICommentRepository commentRepository)
        {
            this._userRepository = userRepository;
            this._commentRepository = commentRepository;
        }

        public CommentDTO AddComment( CommentDTO comment, int rating)
        {
            var commenter = _userRepository.GetByEmail(comment.CommenterUsername);
            var receiver = _userRepository.GetByEmail(comment.ReceiverUsername);

            if (rating !=null)
            {
                receiver.UserRatingCount += 1;
                receiver.UserRatingTotal += rating;
                receiver.UserRating = receiver.UserRatingTotal / receiver.UserRatingCount;
            }

            if (commenter == null || receiver == null)
            {
                throw new ArgumentException("Invalid commenter or receiver.");
            }

            var newComment = new Comment
            {
                Content = comment.Content,
                Commenter = commenter,
                Receiver = receiver,
                FormattedDate = DateTime.Now.ToString("yyyy-MM-dd"),
                FormattedTime = DateTime.Now.ToString("HH:mm:ss")
            };

            _commentRepository.Insert(newComment);

            return (CommentDTO)newComment;
        }


        public bool DeleteComment(int commentId)
        {
            _commentRepository.Delete(commentId);

            return true;
        }
        public List<CommentDTO> GetCommentsByReceiverUsername(string receiverUsername)
        {
            var receiver = _userRepository.GetByUsername(receiverUsername);
            if (receiver == null)
            {
                throw new ArgumentException("Receiver not found.");
            }

            return _commentRepository.GetByReceiver(receiver.Id);
        }
    }
}
