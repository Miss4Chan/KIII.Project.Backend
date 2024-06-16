using Domain.CustomExceptions;
using Domain.Domain_models;
using Domain.DTO;
using Domain.Identity;
using Domain.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Interface;
using Service.Interface;
using System;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class UserService : IUserService
    {
        public readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ICommentRepository _commentRepository;
        private readonly IProductRepository _productRepository;

        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher, ICommentRepository commentRepository, IProductRepository productRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _productRepository = productRepository;
            _commentRepository = commentRepository;
        }

        public async Task<AuthenticatedUserDTO> SignIn(ShopApplicationUser user)
        {
            var dbUser = _userRepository.GetByEmail(user.Email);

            if (dbUser == null || _passwordHasher.VerifyHashedPassword(dbUser.Password, user.Password) == PasswordVerificationResult.Failed)
            {
                throw new InvalidEmailPasswordException("Invalid email or password");
            }

            return new AuthenticatedUserDTO()
            {
                Email = user.Email,
                Token = JwtGenerator.GenerateUserToken(user.Email)
            };
        }

        public async Task<AuthenticatedUserDTO> SignUp(ShopApplicationUser user)
        {
            var checkEmail = _userRepository.GetByEmail(user.Email);

            var checkUsername = _userRepository.GetByUsername(user.Username);

            if (checkUsername != null && checkUsername != null)
            {
                throw new EmailAlreadyExistsException("User with this Email or Username already exists!");
            }
            _userRepository.Insert(user);
            user.Password = _passwordHasher.HashPassword(user.Password);

            var userShoppingCart = new ShoppingCart
            {
                UserId = user.Id
            };
 

            var userFavouries = new Favourites
            {
                UserId = user.Id
            };

            var userRented = new Rented
            {
                UserId = user.Id
            };

            user.UserShoppingCart = userShoppingCart;
            user.UserRented = userRented;
            user.UserFavourites = userFavouries;
            user.UserRatingCount = 0;
            user.UserRating = 0;
            user.UserRatingTotal = 0;

            _userRepository.Update(user);

            return new AuthenticatedUserDTO
            {
                Email = user.Email,
                Token = JwtGenerator.GenerateUserToken(user.Email)
        };
        }

        public UserDTO GetProfile(string username)
        {
            var user = this._userRepository.GetByUsername(username);

            if (user != null)
            {
                var productList = _productRepository.GetProductsByEmail(user.Email);
                var comments = this._commentRepository.GetByReceiver(user.Id);

                UserDTO userDTO = new UserDTO
                {
                    Name = user.Name,
                    Surname = user.Surname,
                    Phone = user.Phone,
                    Address = user.Address,
                    Email = user.Email,
                    Username = user.Username,
                    Products = productList,
                    Rating = user.UserRating,
                    RatingCount = user.UserRatingCount,
                    Comments = comments
                };

                return userDTO;
            }

            return null;

        }

    }
    }
