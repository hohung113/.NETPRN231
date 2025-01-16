﻿$(document).ready(function () {
    $.ajax({
        url: 'https://localhost:7013/api/Category',
        type: 'GET',
        success: function (response) {
            $('#category1').empty();
            $('#category1').append('<option value="">Select a category</option>');

            response.forEach(function (category) {
                $('#category1').append('<option value="' + category.categoryId + '">' + category.categoryName + '</option>');
            });
        },
        error: function (xhr, status, error) {
            console.error('Error fetching categories:', error);
        }
    });
});

$(window).on('beforeunload', function () {
    localStorage.removeItem('token');
    console.log('Token removed from localStorage');
});


function decodeToken(token) {
    try {
        var payload = JSON.parse(atob(token.split('.')[1]));
        return payload;
    } catch (e) {
        console.error('Invalid token format', e);
        return null;
    }
}
//$(document).ready(function () {
//    var userRole = 'Guest';
//    if (userRole === 'Member') {
//        $('#btnCreate').show();
//        $('#addToCartBtn').prop('disabled', false); 
//    }
//    if (userRole === 'Guest') {
//        $('#addToCartBtn').prop('disabled', true);
//        $('#btnCreate').hide();
//    }
//    });


$(document).ready(function () {
    $('#loginForm').on('submit', function (e) {
        e.preventDefault();

        var email = $('#email').val();
        var password = $('#password').val();

        $.ajax({
            url: '/Home/Login',
            type: 'POST',
            data: {
                email: email,
                password: password
            },
            success: function (response) {
                if (response.token) {
                    var token = response.token.token;
                    localStorage.setItem('token', token);
                    setAuthHeader(token);

                    alert('Login successful');
                    $('#loginModal').modal('hide');
                } else {
                    alert('Login failed');
                }
            },
            error: function () {
                alert('An error occurred');
            }
        });
    });

    function setAuthHeader(token) {
        $.ajaxSetup({
            headers: {
                'Authorization': 'Bearer ' + token
            }
        });

        console.log('Authorization Header Set:', 'Bearer ' + token);
    }
});
$(document).ready(function () {
    $('#listCategoryLink').on('click', function (e) {
        e.preventDefault(); 
        var token = localStorage.getItem('token');
        if (!token) {
            alert('You need to log in first.');
            return;
        }

        $.ajax({
            url: 'https://localhost:7013/api/Category',
            type: 'GET',
            headers: {
                'Authorization': 'Bearer ' + token 
            },
            success: function (response) {
                console.log('Categories:', response);
                alert('Categories fetched successfully!');
  
            },
            error: function (xhr, status, error) {
                console.error('Error fetching categories:', error);
                alert('Failed to fetch categories. Please try again.');
            }
        });
    });
});

$(document).ready(function () {
    checkUserRole();

    $('#addToCartBtn').on('click', function (e) {
        e.preventDefault();
        var token = localStorage.getItem('token');
        console.log(token)
        if (!token) {
            console.log(1)
            alert('You need to log in first.');
            return;
        }
     
        var decodedToken = decodeToken(token);
        var userRole = decodedToken
            ? decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]
            : null;
        if (userRole !== 'Member') {
            alert('You do not have permission to perform this action.');
            return;
        }
        
        $.ajax({
            url: '/api/Cart/Add222',
            type: 'POST',
            headers: {
                'Authorization': 'Bearer ' + token 
            },
            data: JSON.stringify({ productId: 123, quantity: 1 }), 
            contentType: 'application/json',
            success: function (response) {
                if (response.success) {
                    alert('Item added to cart!');
                } else {
                    alert('Failed to add item to cart.');
                }
            },
            error: function () {
                alert('An error occurred.');
            }
        });
    });

    function checkUserRole() {
        var token = localStorage.getItem('token');

        if (!token) {
            console.log('No token found.');
            return;
        }
        var decodedToken = decodeToken(token);
        var userRole = decodedToken
            ? decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]
            : null;
     
        if (userRole === 'User') {
            $('.cartt').prop('disabled', false);
        } else {
            console.log('Unauthorized user role:', userRole);
        }
    }
});


$(document).ready(function () {
    $('#createProductModal').on('show.bs.modal', function () {
        $.ajax({
            url: 'https://localhost:7013/api/Category',
            type: 'GET',
            success: function (response) {

                $('#category').empty();
                $('#category').append('<option value="">Select a category</option>');

                response.forEach(function (category) {
                    $('#category').append('<option value="' + category.categoryId + '">' + category.categoryName + '</option>');
                });
            },
            error: function () {
                alert('Failed to load categories');
            }
        });
    });

    $('#createProductForm').on('submit', function (e) {
        e.preventDefault();

        var token = localStorage.getItem('token');
        if (!token) {
            console.log(1)
            alert('You need to log in first.');
            return;
        }

        var decodedToken = decodeToken(token);
        var userRole = decodedToken
            ? decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]
            : null;
        console.log(userRole)
        if (userRole !== 'User') {
            alert('You do not have permission to perform this action.');
            return;
        }
        var productName = $('#productName').val();
        var category = $('#category').val();
        var weight = $('#weight').val();
        var unitInStock = $('#unitInStock').val();
        var unitPrice = $('#unitPrice').val();


        var productData = {
            ProductName: productName,
            CategoryId: category,
            Weight: weight,
            UnitInStock: unitInStock,
            UnitPrice: unitPrice
        };
        $.ajax({
            url: 'https://localhost:7013/api/Product',
            type: 'POST',
            headers: {
                'Authorization': 'Bearer ' + token,
                'Content-Type': 'application/json'
            },
            data: JSON.stringify(productData),
            success: function (response) {
                alert('Product created successfully');
                $('#createProductModal').modal('hide');

                window.location.reload();
            },
            error: function () {
                alert('An error occurred while creating the product');
            }
        });
    });
});

$(document).ready(function () {

    $('body').on('click', '#editBtn', function () {
        var productId = $(this).data('product-id');


        $.ajax({
            url: 'https://localhost:7013/api/Product/' + productId,
            type: 'GET',
            success: function (response) {
                $('#productId2').val(response.productId);
                $('#productName2').val(response.productName);
                $('#weight2').val(response.weight);
                $('#unitInStock2').val(response.unitInStock);
                $('#unitPrice2').val(response.unitPrice);

                $('#category2').empty();
                $('#category2').append('<option value="">Select a category</option>');
                $('#category2').append('<option value="' + response.category.categoryId + '" selected>' + response.category.categoryName + '</option>');

                $('#editProductModal').modal('show'); 
            },
            error: function () {
                alert('Failed to load product details');
            }
        });
    });

    $(document).ready(function () {
        $('#editProductForm').on('submit', function (e) {
            e.preventDefault();
            var token = localStorage.getItem('token');
            if (!token) {
                alert('You need to log in first.');
                return;
            }

            var decodedToken = decodeToken(token);
            var userRole = decodedToken
                ? decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]
                : null;
            console.log(userRole)
            if (userRole !== 'User') {
                alert('You do not have permission to perform this action.');
                return;
            }

            var productData = {
                ProductId: $('#productId2').val(),
                ProductName: $('#productName2').val(),
                CategoryId: $('#category2').val(),
                Weight: $('#weight2').val(),
                UnitInStock: $('#unitInStock2').val(),
                UnitPrice: $('#unitPrice2').val()
            };

            $.ajax({
                url: 'https://localhost:7013/api/Product/' + productData.ProductId,
                type: 'PUT',
                headers: {
                    'Authorization': 'Bearer ' + token,
                    'Content-Type': 'application/json'
                },
                data: JSON.stringify(productData),
                success: function () {
                    alert('Product updated successfully');
                    $('#editProductModal').modal('hide');  
                    window.location.reload(); 
                },
                error: function () {
                    alert('An error occurred while updating the product');
                }
            });
        });
    });

});

//Delele Product
$(document).ready(function () {
    $('.deleteBtn').on('click', function () {
        var productId = $(this).data('product-id');
        console.log(productId)
        var confirmDelete = confirm("Are you sure you want to delete this product?");
        if (confirmDelete) {
            $.ajax({
                url: 'https://localhost:7013/api/Product/' + productId, 
                type: 'DELETE', 
                success: function (response) {
                    alert("Product deleted successfully!");
                    window.location.reload();
                },
                error: function (xhr, status, error) {
                    alert("An error occurred while deleting the product.");
                }
            });
        } else {
            alert("Product not deleted.");
        }
    });
});
