using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Favourites",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favourites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rented",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rented", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShopApplicationUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    UserShoppingCartId = table.Column<int>(nullable: true),
                    UserFavouritesId = table.Column<int>(nullable: true),
                    UserRentedId = table.Column<int>(nullable: true),
                    UserRatingCount = table.Column<int>(nullable: false),
                    UserRating = table.Column<double>(nullable: false),
                    UserRatingTotal = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopApplicationUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopApplicationUsers_Favourites_UserFavouritesId",
                        column: x => x.UserFavouritesId,
                        principalTable: "Favourites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShopApplicationUsers_Rented_UserRentedId",
                        column: x => x.UserRentedId,
                        principalTable: "Rented",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShopApplicationUsers_ShoppingCarts_UserShoppingCartId",
                        column: x => x.UserShoppingCartId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(nullable: true),
                    CommenterId = table.Column<int>(nullable: true),
                    ReceiverId = table.Column<int>(nullable: true),
                    FormattedDate = table.Column<string>(nullable: true),
                    FormattedTime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_ShopApplicationUsers_CommenterId",
                        column: x => x.CommenterId,
                        principalTable: "ShopApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_ShopApplicationUsers_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "ShopApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    DeliveryType = table.Column<int>(nullable: false),
                    DeliveryAddress = table.Column<string>(nullable: true),
                    DeliveryPhone = table.Column<string>(nullable: true),
                    DeliveryCity = table.Column<string>(nullable: true),
                    DeliveryPostalCode = table.Column<string>(nullable: true),
                    Subtotal = table.Column<float>(nullable: false),
                    Total = table.Column<float>(nullable: false),
                    FormattedDate = table.Column<string>(nullable: true),
                    FormattedTime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_ShopApplicationUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ShopApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductDescription = table.Column<string>(nullable: true),
                    ProductName = table.Column<string>(nullable: false),
                    ProductType = table.Column<int>(nullable: false),
                    ProductMeasurements = table.Column<string>(nullable: true),
                    ProductBrand = table.Column<string>(nullable: true),
                    ProductMaterial = table.Column<string>(nullable: true),
                    Condition = table.Column<int>(nullable: false),
                    ProductColor = table.Column<string>(nullable: true),
                    ProductSubcategory = table.Column<int>(nullable: true),
                    ProductSize = table.Column<int>(nullable: true),
                    ProductSizeNumber = table.Column<int>(nullable: true),
                    ProductPrice = table.Column<float>(nullable: false),
                    ProductAvailablity = table.Column<bool>(nullable: false),
                    ProductRent = table.Column<bool>(nullable: false),
                    ProductDaysRent = table.Column<int>(nullable: false),
                    ProductImage = table.Column<string>(nullable: true),
                    ProductSex = table.Column<int>(nullable: false),
                    ShopApplicationUserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ShopApplicationUsers_ShopApplicationUserId",
                        column: x => x.ShopApplicationUserId,
                        principalTable: "ShopApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductsInFavourites",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false),
                    FavouritesId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsInFavourites", x => new { x.ProductId, x.FavouritesId });
                    table.ForeignKey(
                        name: "FK_ProductsInFavourites_Favourites_FavouritesId",
                        column: x => x.FavouritesId,
                        principalTable: "Favourites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsInFavourites_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductsInOrders",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsInOrders", x => new { x.ProductId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_ProductsInOrders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsInOrders_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductsInRented",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false),
                    RentedId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsInRented", x => new { x.ProductId, x.RentedId });
                    table.ForeignKey(
                        name: "FK_ProductsInRented_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsInRented_Rented_RentedId",
                        column: x => x.RentedId,
                        principalTable: "Rented",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductsInShoppingCarts",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false),
                    ShoppingCartId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsInShoppingCarts", x => new { x.ProductId, x.ShoppingCartId });
                    table.ForeignKey(
                        name: "FK_ProductsInShoppingCarts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsInShoppingCarts_ShoppingCarts_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommenterId",
                table: "Comments",
                column: "CommenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ReceiverId",
                table: "Comments",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ShopApplicationUserId",
                table: "Products",
                column: "ShopApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsInFavourites_FavouritesId",
                table: "ProductsInFavourites",
                column: "FavouritesId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsInOrders_OrderId",
                table: "ProductsInOrders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsInRented_RentedId",
                table: "ProductsInRented",
                column: "RentedId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsInShoppingCarts_ShoppingCartId",
                table: "ProductsInShoppingCarts",
                column: "ShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopApplicationUsers_UserFavouritesId",
                table: "ShopApplicationUsers",
                column: "UserFavouritesId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopApplicationUsers_UserRentedId",
                table: "ShopApplicationUsers",
                column: "UserRentedId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopApplicationUsers_UserShoppingCartId",
                table: "ShopApplicationUsers",
                column: "UserShoppingCartId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "ProductsInFavourites");

            migrationBuilder.DropTable(
                name: "ProductsInOrders");

            migrationBuilder.DropTable(
                name: "ProductsInRented");

            migrationBuilder.DropTable(
                name: "ProductsInShoppingCarts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ShopApplicationUsers");

            migrationBuilder.DropTable(
                name: "Favourites");

            migrationBuilder.DropTable(
                name: "Rented");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");
        }
    }
}
