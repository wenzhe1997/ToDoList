angular.module('phoneBookApp', []).controller('CrudCtrl', ['$scope', 'crudService', function ($scope, crudService) {

    $scope.createVM = {}
    $scope.updateVM = {}
    $scope.showAlert = false;

    $scope.loadTableData = function () {
        crudService.serviceCall("GET", "", "").then(function (response) {
            if (response.data.length < 1) {
                $scope.showAlert = true;
                $scope.alertName = "Note!"
                $scope.alertMessage = "These Is No Record To Show."
            }
            $scope.phoneBookData = response.data;
        });
    }

    $scope.addPhoneNumber = function (isValid) {
        if (isValid) {
            $scope.addPhoneNumberForm.$setPristine();
            crudService.serviceCall("POST", "", $scope.createVM).then(function (response) {
                $scope.showAlert = true;
                $scope.alertName = "Success!"
                $scope.alertMessage = "Task Is Added Successfully."
                $scope.createVM = {};
                $scope.loadTableData();
            });
        }
    }

    $scope.openUpdatePhoneNumberModal = function (Id) {
        crudService.serviceCall("GET", Id, "").then(function (response) {
            $scope.updateVM = response.data;
        });
    }

    $scope.updatePhoneNumber = function (Id) {

        crudService.serviceCall("PUT", Id, $scope.updateVM).then(function (response) {
            $scope.showAlert = true;
            $scope.alertName = "Success!"
            $scope.alertMessage = "Task Is Updated Successfully."
            $scope.loadTableData();
        });

    }

    $scope.openDeletePhoneNumberModal = function (Id) {
        $scope.recordIdToDelete = Id;
    }

    $scope.deletePhoneNumber = function (Id) {
        crudService.serviceCall("Delete", Id, "").then(function (response) {
            $scope.showAlert = true;
            $scope.alertName = "Success!"
            $scope.alertMessage = "Task Is Deleted Successfully."
            $scope.loadTableData();
        });
    }

    $scope.loadTableData();

}])
    .service('crudService', ['$http', function ($http) {
        return {
            serviceCall: function (method, parameter, data) {
                return $http({
                    method: method,
                    url: 'https://localhost:44320/api/Directories/' + parameter,
                    data: data
                });
            }
        };
    }]);