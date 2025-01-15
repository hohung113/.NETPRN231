$(document).ready(function () {
    var userRole = 'Guest';
    if (userRole === 'Member') {
        $('#btnCreate').show(); 
        $('#addToCartBtn').prop('disabled', false);
    } else {
        $('#btnCreate').hide();
        $('#addToCartBtn').prop('disabled', true);
    }
});

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
    checkUserRole();

    $('#addToCartBtn').on('click', function (e) {
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
        if (userRole !== 'User') {
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

    function decodeToken(token) {
        try {
            var payload = JSON.parse(atob(token.split('.')[1]));
            return payload;
        } catch (e) {
            console.error('Invalid token format', e);
            return null;
        }
    }
});
