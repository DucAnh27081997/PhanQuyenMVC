  var app = angular.module('myApp', []);
    app.controller('myCtrl', function ($scope, $http) {
        $scope.list = null;
        $scope.LoadData = function () {
            $http.get("/Accounts/getList")
                .then(function (response) {
                    $scope.list = response.data;
                });

        }
        //active user
        $scope.ActiveAccount = function (id) {
           
            $.ajax({
                method: "GET",
                url: "/Accounts/ActiveAccount?id=" + id,
               

                success: function (data) {
                    if (data.active === 1) {
                        toastr.info("On");
                    }
                    else {
                        toastr.error("OfF");
                    }
                }
                ,
                error: function () {
                    toastr.err(data);
                }
            });
        }
        //active user
        $scope.DeleteUser = function (id) {
            
            $.ajax({
                method: "GET",
                url: "/Accounts/DeleteUser?id="+id,

                success: function (data) {
                    if (data.Result === "Success") {
                        toastr.info(data.Message);
                        $scope.LoadData();
                    }
                    else {
                        toastr.error(data.Message);
                    }
                }
                ,
                error: function () {
                    toastr.err("Loi roi");
                }
            });
        }
        $scope.AddUser = function () {


            var Data = {
                UserName: $scope.UserNameForm,
                FullName: $scope.FullNameForm,
                Password: $scope.PasswordForm,
                isAdmin: $scope.isAdminForm,
                Allowed: $scope.isAlowedForm,
                PhoneNumber: $scope.PhoneNumberForm,
                Email: $scope.EmailForm,
                Checked: $scope.isCheckedForm,
            };

            $.ajax({
                method: "POST",
                url: "/Accounts/CreateUser",
                data: Data,

                success: function (data) {
                    if (data.Result === "Success") {
                        toastr.info(data.Message);
                        $scope.LoadData();
                    }
                    else {
                        toastr.error(data.Message);
                    }
                }
                ,
                error: function () {
                    toastr.err("Loi roi");
                }
            });
        }
        $scope.EditUser = function () {


            var account = {
                UserName: $scope.UserNameForm,
                Password: $scope.PasswordForm,
                FullName: $scope.FullNameForm,
                isAdmin: $scope.isAdminForm,
                Allowed: $scope.isAlowedForm,
                PhoneNumber: $scope.PhoneNumberForm,
                Email: $scope.EmailForm,
                Checked: $scope.isCheckedForm,
            };

            $.ajax({
                method: "POST",
                url: "/Accounts/CreateUser",
                data: account,

                success: function (data) {
                    if (data.Result === "Success") {
                        toastr.info(data.Message);
                        $scope.LoadData();
                    }
                    else {
                        toastr.error(data.Message);
                    }
                }
                ,
                error: function () {
                    toastr.err("Loi roi");
                }
            });
        }
        $scope.LoadData();
        $scope.deleteUser = function (id) {
            alert(id);
        }
    });
