(function () {
    "use strict";
    angular
        .module("productManagement")
        .controller("ProductListCtrl",
                    ["productResource", ProductListCtrl]);

    // The registered product resource module is injected into the 
    // below function.
    function ProductListCtrl(productResource) {
        var vm = this;

        vm.searchCriteria = "LJ";
        vm.sortProperty = "Price";
        vm.sortDirection = "desc";

        // The query method expects to receive an array from the
        // backend web api.
        //productResource.query({ search: vm.searchCriteria }, function (data) {
        //    vm.products = data;
        //});

        // OData querying.
        productResource.query({
            $filter: "startswith(ProductName, 'HL')",
            $orderby: "Price desc"
        }, function (data) {
            vm.products = data;
        });
    }
}());
