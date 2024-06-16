using Domain.CustomExceptions;
using Domain.Domain_models;
using Domain.DTO;
using Domain.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Repository;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Implementation
{
    public class UserProfileService : IUserProfileService
    {
        public readonly IUserRepository _userRepository;
        public readonly ICommentRepository _commentRepository;
        public readonly ShopApplicationUser _user;
        private readonly IPasswordHasher _passwordHasher;

        public UserProfileService (IHttpContextAccessor httpContextAccessor, IUserRepository userRepository, ICommentRepository commentRepository, IPasswordHasher passwordHasher)
        {
            this._userRepository = userRepository;
            this._user = _userRepository.GetByEmail(httpContextAccessor.HttpContext.User.Identity.Name);
            this._commentRepository = commentRepository;
            this._passwordHasher = passwordHasher;
      }
        public UserDTO GetMyProfile()
        {
            var comments = this._commentRepository.GetByReceiver(_user.Id);

            UserDTO userDTO = new UserDTO
            {
                Name = _user.Name,
                Surname = _user.Surname,
                Phone = _user.Phone,
                Address = _user.Address,
                Email = _user.Email,
                Username = _user.Username,
                City = _user.City,
                PostalCode = _user.PostalCode,
                Rating = _user.UserRating,
                RatingCount = _user.UserRatingCount,
                Comments = comments
            };

            return userDTO;
        }

        public UserDTO EditMyUserProfile(UserDTO updatedUserDTO, string password)
        {
            var currentUser = _userRepository.GetByUsername(_user.Username);

            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(currentUser.Password, password);

            if (passwordVerificationResult != PasswordVerificationResult.Success)
            {
                throw new InvalidPasswordException();
            }

            currentUser.Name = updatedUserDTO.Name;
            currentUser.Surname = updatedUserDTO.Surname;
            currentUser.Phone = updatedUserDTO.Phone;
            currentUser.Address = updatedUserDTO.Address;
            currentUser.City = updatedUserDTO.City;
            currentUser.PostalCode = updatedUserDTO.PostalCode;

            try
            {
                _userRepository.Update(currentUser);

                return new UserDTO
                {
                    Name = currentUser.Name,
                    Surname = currentUser.Surname,
                    Email = currentUser.Email,
                    Phone = currentUser.Phone,
                    Address = currentUser.Address,
                    Username = currentUser.Username,
                    City = currentUser.City,
                    PostalCode = currentUser.PostalCode,
                    Rating = currentUser.UserRating,
                    RatingCount = currentUser.UserRatingCount,
                    Comments = _commentRepository.GetByReceiver(currentUser.Id)
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update user profile.", ex);
            }
        }

        public UserDTO ChangePassword(string oldPassword, string newPassword, string repeatNewPassword)
        {
            var currentUser = _userRepository.GetByUsername(_user.Username);

            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(currentUser.Password, oldPassword);

            if (passwordVerificationResult != PasswordVerificationResult.Success)
            {
                throw new InvalidPasswordException("The current password you have entered is incorrect.");
            }

            if (newPassword != repeatNewPassword)
            {
                throw new PasswordMismatchException("New passwords do not match.");
            }

            currentUser.Password = _passwordHasher.HashPassword(newPassword);

            try
            {
                _userRepository.Update(currentUser);

                return new UserDTO
                {
                    Name = currentUser.Name,
                    Surname = currentUser.Surname,
                    Email = currentUser.Email,
                    Phone = currentUser.Phone,
                    Address = currentUser.Address,
                    Username = currentUser.Username,
                    City = currentUser.City,
                    PostalCode = currentUser.PostalCode,
                    Rating = currentUser.UserRating,
                    RatingCount = currentUser.UserRatingCount,
                    Comments = _commentRepository.GetByReceiver(currentUser.Id)
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update password.", ex);
            }
        }


    }
}
