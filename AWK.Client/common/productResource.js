(function () {
    "use strict";

    // Custom service that communicates with the backend web api service.
    // Looks up the common.services module, registers the custom factory
    // service with that module.
    angular
        .module("common.services")
        .factory("productResource",
                    ["$resource", "appSettings", productResource]);

    // Here's the actual factory function named productResource.
    function productResource($resource, appSettings) {
        return $resource(appSettings.serverPath + "/api/products/:id");
    }


})();